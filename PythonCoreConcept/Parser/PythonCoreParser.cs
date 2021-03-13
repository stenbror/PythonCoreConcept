
// PythonCore - This is the Recursive descend parser for Python 3.9 grammar.
// Written by Richard Magnor Stenbro. (C) 2021 By Richard Magnor Stenbro
// Free to use for none commercial uses.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using PythonCoreConcept.Parser.AST;

namespace PythonCoreConcept.Parser
{
    public class PythonCoreParser
    {
        private PythonCoreTokenizer _lexer;
        
        public PythonCoreParser(PythonCoreTokenizer tokenizer)
        {
            _lexer = tokenizer;
        }
        
#region Expression rules

        private ExpressionNode ParseAtom()
        {
            var startPos = _lexer.Position;
            var curSymbol = _lexer.CurSymbol;
            switch (_lexer.CurSymbol.Kind)
            {
                case TokenKind.PyFalse:
                    _lexer.Advance();
                    return new AtomFalse(startPos, _lexer.Position, curSymbol);
                case TokenKind.PyNone:
                    _lexer.Advance();
                    return new AtomNone(startPos, _lexer.Position, curSymbol);
                case TokenKind.PyTrue:
                    _lexer.Advance();
                    return new AtomTrue(startPos, _lexer.Position, curSymbol);
                case TokenKind.PyElipsis:
                    _lexer.Advance();
                    return new AtomElipsis(startPos, _lexer.Position, curSymbol);
                case TokenKind.Name:
                    _lexer.Advance();
                    return new AtomName(startPos, _lexer.Position, curSymbol as NameToken);
                case TokenKind.Number:
                    _lexer.Advance();
                    return new AtomNumber(startPos, _lexer.Position, curSymbol as NumberToken);
                case TokenKind.String:
                {
                    var nodes = new List<StringToken>();
                    nodes.Add(curSymbol as StringToken);
                    _lexer.Advance();
                    while (_lexer.CurSymbol.Kind == TokenKind.String)
                    {
                        nodes.Add(_lexer.CurSymbol as StringToken);
                        _lexer.Advance();
                    }
                    return new AtomString(startPos, _lexer.Position, nodes.ToArray());
                }
                case TokenKind.PyLeftParen:
                {
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind == TokenKind.PyRightParen)
                    {
                        var symbol2 = _lexer.CurSymbol;
                        _lexer.Advance();

                        return new AtomTuple(startPos, _lexer.Position, curSymbol, null, symbol2);
                    }

                    if (_lexer.CurSymbol.Kind == TokenKind.PyYield)
                    {
                        var right = ParseYieldExpr();
                        if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen)
                            throw new SyntaxError(_lexer.Position, "Missing ')' in literal!", _lexer.CurSymbol);
                        var symbol2 = _lexer.CurSymbol;
                        _lexer.Advance();
                        
                        return new AtomTuple(startPos, _lexer.Position, curSymbol, right, symbol2);
                    }
                    
                    var rightNode = ParseTestListComp();
                    if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen)
                        throw new SyntaxError(_lexer.Position, "Missing ')' in literal!", _lexer.CurSymbol);
                    var symbol3 = _lexer.CurSymbol;
                    _lexer.Advance();
                        
                    return new AtomTuple(startPos, _lexer.Position, curSymbol, rightNode, symbol3);
                    
                }
                case TokenKind.PyLeftBracket:
                {
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind == TokenKind.PyRightBracket)
                    {
                        var symbol2 = _lexer.CurSymbol;
                        _lexer.Advance();

                        return new AtomList(startPos, _lexer.Position, curSymbol, null, symbol2);
                    }
                    else
                    {
                        var rightNode = ParseTestListComp();
                        if (_lexer.CurSymbol.Kind != TokenKind.PyRightBracket)
                            throw new SyntaxError(_lexer.Position, "Missing ']' in literal!", _lexer.CurSymbol);
                        var symbol3 = _lexer.CurSymbol;
                        _lexer.Advance();
                        
                        return new AtomList(startPos, _lexer.Position, curSymbol, rightNode, symbol3);
                    }
                }
                case TokenKind.PyLeftCurly:
                {
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind == TokenKind.PyRightCurly)
                    {
                        var symbol2 = _lexer.CurSymbol;
                        _lexer.Advance();
                        
                        return new AtomDictionary(startPos, _lexer.Position, curSymbol, null, symbol2);
                    }
                    else
                    {
                        var right = ParseDictorSetMaker();
                        if (_lexer.CurSymbol.Kind != TokenKind.PyRightCurly)
                            throw new SyntaxError(_lexer.Position, "Missing '}' in literal!", _lexer.CurSymbol);
                        var symbol2 = _lexer.CurSymbol;
                        _lexer.Advance();
                        if (right is DictionaryContainer)
                        {
                            return new AtomDictionary(startPos, _lexer.Position, curSymbol, right, symbol2);
                        }
                        
                        return new AtomSet(startPos, _lexer.Position, curSymbol, right, symbol2);
                    }
                }
                default:
                    throw new SyntaxError(_lexer.Position, "Illegal literal!", curSymbol);
            }
        }

        private ExpressionNode ParseAtomExpr()
        {
            var startPos = _lexer.Position;
            if (_lexer.CurSymbol.Kind == TokenKind.PyAwait)
            {
                var awaitNode = _lexer.CurSymbol;
                _lexer.Advance();
                var node = ParseAtom();
                var trailing = new List<ExpressionNode>();
                while (_lexer.CurSymbol.Kind == TokenKind.PyLeftParen
                       || _lexer.CurSymbol.Kind == TokenKind.PyLeftBracket
                       || _lexer.CurSymbol.Kind == TokenKind.PyDot)
                {
                    trailing.Add(ParseTrailer());
                }

                return new AtomExpr(startPos, _lexer.Position, awaitNode, node, trailing.ToArray());
            }
            var nodePlain = ParseAtom();
            var trailingPlain = new List<ExpressionNode>();
            while (_lexer.CurSymbol.Kind == TokenKind.PyLeftParen
                   || _lexer.CurSymbol.Kind == TokenKind.PyLeftBracket
                   || _lexer.CurSymbol.Kind == TokenKind.PyDot)
            {
                trailingPlain.Add(ParseTrailer());
            }

            if (trailingPlain.Count == 0) return nodePlain; // Just the atom node returned.

            return new AtomExpr(startPos, _lexer.Position, null, nodePlain, trailingPlain.ToArray());
        }

        private ExpressionNode ParsePower()
        {
            var startPos = _lexer.Position;
            var left = ParseAtomExpr();
            if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseFactor();

                return new Power(startPos, _lexer.Position, left, symbol, right);
            }

            return left;
        }

        [SuppressMessage("Resharper", "RecursiveCall.Global")]
        private ExpressionNode ParseFactor()
        {
            var startPos = _lexer.Position;
            var curSymbol = _lexer.CurSymbol;
            switch (_lexer.CurSymbol.Kind)
            {
                case TokenKind.PyPlus:
                    _lexer.Advance();
                    var rightPlus = ParseFactor();
                    return new UnaryPlus(startPos, _lexer.Position, curSymbol, rightPlus);
                case TokenKind.PyMinus:
                    _lexer.Advance();
                    var rightMinus = ParseFactor();
                    return new UnaryMinus(startPos, _lexer.Position, curSymbol, rightMinus);
                case TokenKind.PyBitInvert:
                    _lexer.Advance();
                    var rightInvert = ParseFactor();
                    return new UnaryBitInvert(startPos, _lexer.Position, curSymbol, rightInvert);
                default:
                    return ParsePower();
            }
        }

        private ExpressionNode ParseTerm()
        {
            var startPos = _lexer.Position;
            var left = ParseFactor();
            while (_lexer.CurSymbol.Kind == TokenKind.PyMul
                || _lexer.CurSymbol.Kind == TokenKind.PyMatrice
                || _lexer.CurSymbol.Kind == TokenKind.PyDiv
                || _lexer.CurSymbol.Kind == TokenKind.PyModulo
                || _lexer.CurSymbol.Kind == TokenKind.PyFloorDiv)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseFactor();
                switch (symbol.Kind)
                {
                    case TokenKind.PyMul:
                        left = new Mul(startPos, _lexer.Position, left, symbol, right);
                        break;
                    case TokenKind.PyMatrice:
                        left = new Matrice(startPos, _lexer.Position, left, symbol, right);
                        break;
                    case TokenKind.PyDiv:
                        left = new Div(startPos, _lexer.Position, left, symbol, right);
                        break;
                    case TokenKind.PyModulo:
                        left = new Modulo(startPos, _lexer.Position, left, symbol, right);
                        break;
                    case TokenKind.PyFloorDiv:
                        left = new FloorDiv(startPos, _lexer.Position, left, symbol, right);
                        break;
                }
            }

            return left;
        }
        
        private ExpressionNode ParseArith()
        {
            var startPos = _lexer.Position;
            var left = ParseTerm();
            while (_lexer.CurSymbol.Kind == TokenKind.PyPlus || _lexer.CurSymbol.Kind == TokenKind.PyMinus)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseTerm();
                switch (symbol.Kind)
                {
                    case TokenKind.PyPlus:
                        left = new Plus(startPos, _lexer.Position, left, symbol, right);
                        break;
                    case TokenKind.PyMinus:
                        left = new Minus(startPos, _lexer.Position, left, symbol, right);
                        break;
                }
            }

            return left;
        }
        
        private ExpressionNode ParseShift()
        {
            var startPos = _lexer.Position;
            var left = ParseArith();
            while (_lexer.CurSymbol.Kind == TokenKind.PyShiftLeft || _lexer.CurSymbol.Kind == TokenKind.PyShiftRight)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseArith();
                switch (symbol.Kind)
                {
                    case TokenKind.PyShiftLeft:
                        left = new ShiftLeft(startPos, _lexer.Position, left, symbol, right);
                        break;
                    case TokenKind.PyShiftRight:
                        left = new ShiftRight(startPos, _lexer.Position, left, symbol, right);
                        break;
                }
            }

            return left;
        }
        
        private ExpressionNode ParseAnd()
        {
            var startPos = _lexer.Position;
            var left = ParseShift();
            while (_lexer.CurSymbol.Kind == TokenKind.PyBitAnd)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseShift();
                left = new BitAnd(startPos, _lexer.Position, left, symbol, right);
            }

            return left;
        }
        
        private ExpressionNode ParseXor()
        {
            var startPos = _lexer.Position;
            var left = ParseAnd();
            while (_lexer.CurSymbol.Kind == TokenKind.PyBitXor)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseAnd();
                left = new BitXor(startPos, _lexer.Position, left, symbol, right);
            }

            return left;
        }
        
        private ExpressionNode ParseOr()
        {
            var startPos = _lexer.Position;
            var left = ParseXor();
            while (_lexer.CurSymbol.Kind == TokenKind.PyBitOr)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseXor();
                left = new BitOr(startPos, _lexer.Position, left, symbol, right);
            }

            return left;
        }

        private ExpressionNode ParseStarExpr()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var right = ParseOr();
            return new StarExpr(startPos, _lexer.Position, symbol, right);
        }

        private ExpressionNode ParseComparison()
        {
            var startPos = _lexer.Position;
            var left = ParseOr();
            while (_lexer.CurSymbol.Kind == TokenKind.PyLess
                   || _lexer.CurSymbol.Kind == TokenKind.PyLessEqual
                   || _lexer.CurSymbol.Kind == TokenKind.PyEqual
                   || _lexer.CurSymbol.Kind == TokenKind.PyGreater
                   || _lexer.CurSymbol.Kind == TokenKind.PyGreaterEqual
                   || _lexer.CurSymbol.Kind == TokenKind.PyNotEqual
                   || _lexer.CurSymbol.Kind == TokenKind.PyIn
                   || _lexer.CurSymbol.Kind == TokenKind.PyNot
                   || _lexer.CurSymbol.Kind == TokenKind.PyIs)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                if (symbol.Kind == TokenKind.PyNot)
                {
                    if (_lexer.CurSymbol.Kind == TokenKind.PyIn)
                    {
                        var symbol2 = _lexer.CurSymbol;
                        _lexer.Advance();
                        var right = ParseOr();
                        left = new CompareNotIn(startPos, _lexer.Position, left, symbol, symbol2, right);
                    }
                    else
                    {
                        throw new SyntaxError(_lexer.Position, "Expecting 'not in', but missing 'in'!", _lexer.CurSymbol);
                    }
                }
                else if (symbol.Kind == TokenKind.PyIs)
                {
                    if (_lexer.CurSymbol.Kind == TokenKind.PyNot)
                    {
                        var symbol2 = _lexer.CurSymbol;
                        _lexer.Advance();
                        var right = ParseOr();
                        left = new CompareIsNot(startPos, _lexer.Position, left, symbol, symbol2, right);
                    }
                    else
                    {
                        var right = ParseOr();
                        left = new CompareIs(startPos, _lexer.Position, left, symbol, right);
                    }
                    
                }
                else
                {
                    var right = ParseOr();
                    switch (symbol.Kind)
                    {
                        case TokenKind.PyLess:
                            left = new CompareLess(startPos, _lexer.Position, left, symbol, right);
                            break;
                        case TokenKind.PyLessEqual:
                            left = new CompareLessEqual(startPos, _lexer.Position, left, symbol, right);
                            break;
                        case TokenKind.PyEqual:
                            left = new CompareEqual(startPos, _lexer.Position, left, symbol, right);
                            break;
                        case TokenKind.PyGreater:
                            left = new CompareGreater(startPos, _lexer.Position, left, symbol, right);
                            break;
                        case TokenKind.PyGreaterEqual:
                            left = new CompareGreaterEqual(startPos, _lexer.Position, left, symbol, right);
                            break;
                        case TokenKind.PyNotEqual:
                            left = new CompareNotEqual(startPos, _lexer.Position, left, symbol, right);
                            break;
                        case TokenKind.PyIn:
                            left = new CompareIn(startPos, _lexer.Position, left, symbol, right);
                            break;
                    }
                }
            }

            return left;
        }

        [SuppressMessage("Resharper", "RecursiveCall.Global")]
        private ExpressionNode ParseNotTest()
        {
            if (_lexer.CurSymbol.Kind == TokenKind.PyNot)
            {
                var startPos = _lexer.Position;
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseNotTest();
                return new NotTest(startPos, _lexer.Position, symbol, right);
            }

            return ParseComparison();
        }

        private ExpressionNode ParseAndTest()
        {
            var startPos = _lexer.Position;
            var left = ParseNotTest();
            while (_lexer.CurSymbol.Kind == TokenKind.PyAnd)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseNotTest();
                left = new AndTest(startPos, _lexer.Position, left, symbol, right);
            }

            return left;
        }
        
        private ExpressionNode ParseOrTest()
        {
            var startPos = _lexer.Position;
            var left = ParseAndTest();
            while (_lexer.CurSymbol.Kind == TokenKind.PyOr)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseAndTest();
                left = new OrTest(startPos, _lexer.Position, left, symbol, right);
            }

            return left;
        }

        private ExpressionNode ParseLambda(bool isConditional)
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            ExpressionNode left = null;
            if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
            {
                left = ParseVarArgsList();
            }

            if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                throw new SyntaxError(_lexer.Position, "Missing ':' in lambda expression!", _lexer.CurSymbol);
            var symbol2 = _lexer.CurSymbol;
            _lexer.Advance();
            var right = isConditional ? ParseTest() : ParseTestNoCond();

            return new Lambda(startPos, _lexer.Position, symbol, left, symbol2, right);
        }

        private ExpressionNode ParseTestNoCond()
        {
            if (_lexer.CurSymbol.Kind == TokenKind.PyLambda) return ParseLambda(false);
            return ParseOrTest();
        }

        private ExpressionNode ParseTest()
        {
            if (_lexer.CurSymbol.Kind == TokenKind.PyLambda) return ParseLambda(true);
            
            var startPos = _lexer.Position;
            var left = ParseOrTest();
            if (_lexer.CurSymbol.Kind == TokenKind.PyIf)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseOrTest();
                if (_lexer.CurSymbol.Kind != TokenKind.PyElse) throw new SyntaxError(_lexer.Position, "Missing 'else' in test expression!", _lexer.CurSymbol);
                var symbol2 = _lexer.CurSymbol;
                _lexer.Advance();
                var next = ParseTest();
                return new Test(startPos, _lexer.Position, left, symbol, right, symbol2, next);
            }

            return left;
        }

        private ExpressionNode ParseNamedExpr()
        {
            var startPos = _lexer.Position;
            var left = ParseTest();
            if (_lexer.CurSymbol.Kind == TokenKind.PyColonAssign)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseTest();

                return new NamedExpr(startPos, _lexer.Position, left, symbol, right);
            }

            return left;
        }

        private ExpressionNode ParseTestListComp()
        {
            var startPos = _lexer.Position;
            var nodes = new List<ExpressionNode>();
            var separators = new List<Token>();
            nodes.Add( _lexer.CurSymbol.Kind == TokenKind.PyMul ? ParseStarExpr() : ParseNamedExpr() );
            if (_lexer.CurSymbol.Kind == TokenKind.PyFor || _lexer.CurSymbol.Kind == TokenKind.PyAsync)
            {
                nodes.Add( ParseCompFor());
            }
            else
            {
                while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                {
                    separators.Add( _lexer.CurSymbol );
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen &&
                        _lexer.CurSymbol.Kind != TokenKind.PyRightBracket)
                    {
                        nodes.Add( _lexer.CurSymbol.Kind == TokenKind.PyMul ? ParseStarExpr() : ParseNamedExpr() );
                    }
                    else if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                    {
                        throw new SyntaxError(_lexer.Position, "Missing element in list!", _lexer.CurSymbol);
                    }
                }
            }

            if (nodes.Count == 1 && separators.Count == 0) return nodes[0]; // Single node without commas return just the node.

            return new TestListComp(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray());
        }

        private ExpressionNode ParseTrailer()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            if (symbol.Kind == TokenKind.PyLeftParen)
            {
                ExpressionNode right = null;
                if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen)
                {
                    right = ParseArgList();
                }

                if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen) throw new SyntaxError(_lexer.Position, "Expecting ')' in call!", _lexer.CurSymbol);
                var symbol2 = _lexer.CurSymbol;
                _lexer.Advance();
                
                return new Call(startPos, _lexer.Position, symbol, right, symbol2);
            }
            else if (symbol.Kind == TokenKind.PyLeftBracket)
            {
                ExpressionNode right = ParseSubscriptList();
                
                if (_lexer.CurSymbol.Kind != TokenKind.PyRightBracket) throw new SyntaxError(_lexer.Position, "Expecting ']' in index!", _lexer.CurSymbol);
                var symbol2 = _lexer.CurSymbol;
                _lexer.Advance();
                
                return new AST.Index(startPos, _lexer.Position, symbol, right, symbol2);
            }
            else
            {
                if (_lexer.CurSymbol.Kind != TokenKind.Name) throw new SyntaxError(_lexer.Position, "Expecting NAME literal after '.'!", _lexer.CurSymbol);
                var symbol2 = _lexer.CurSymbol;
                _lexer.Advance();

                return new DotName(startPos, _lexer.Position, symbol, symbol2 as NameToken);
            }
        }

        private ExpressionNode ParseSubscriptList()
        {
            var startPos = _lexer.Position;
            var nodes = new List<ExpressionNode>();
            var separators = new List<Token>();
            nodes.Add( ParseSubscript() );
            while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
            {
                separators.Add( _lexer.CurSymbol );
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind == TokenKind.PyRightBracket) break;
                nodes.Add( ParseSubscript() );
            }
            
            if (nodes.Count == 1 && separators.Count == 0) return nodes[0];

            return new SubscriptList(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray());
        }

        private ExpressionNode ParseSubscript()
        {
            var startPos = _lexer.Position;
            ExpressionNode first = null, second = null, third = null;
            Token one = null, two = null;
            if (_lexer.CurSymbol.Kind != TokenKind.PyColon) first = ParseTest();
            if (_lexer.CurSymbol.Kind == TokenKind.PyColon)
            {
                one = _lexer.CurSymbol;
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.PyComma &&
                    _lexer.CurSymbol.Kind != TokenKind.PyColon &&
                    _lexer.CurSymbol.Kind != TokenKind.PyRightBracket) second = ParseTest();
                if (_lexer.CurSymbol.Kind == TokenKind.PyColon)
                {
                    two = _lexer.CurSymbol;
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind != TokenKind.PyComma && _lexer.CurSymbol.Kind != TokenKind.PyRightBracket)
                        third = ParseTest();
                }
            }

            return new Subscript(startPos, _lexer.Position, first, one,second, two, third);
        }

        private ExpressionNode ParseExprList()
        {
            var startPos = _lexer.Position;
            var nodes = new List<ExpressionNode>();
            var separators = new List<Token>();
            nodes.Add( _lexer.CurSymbol.Kind == TokenKind.PyMul ? ParseStarExpr() : ParseOr() );
            while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
            {
                separators.Add( _lexer.CurSymbol );
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind == TokenKind.PyIn) break;
                nodes.Add( _lexer.CurSymbol.Kind == TokenKind.PyMul ? ParseStarExpr() : ParseOr() );
            }
            
            if (nodes.Count == 1 && separators.Count == 0) return nodes[0];

            return new ExprList(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray());
        }
        
        private ExpressionNode ParseTestList()
        {
            var startPos = _lexer.Position;
            var nodes = new List<ExpressionNode>();
            var separators = new List<Token>();
            nodes.Add( ParseTest() );
            while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
            {
                separators.Add( _lexer.CurSymbol );
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind == TokenKind.PySemiColon 
                    || _lexer.CurSymbol.Kind == TokenKind.Newline 
                    || _lexer.CurSymbol.Kind == TokenKind.EndOfFile) break;
                nodes.Add( ParseTest() );
            }

            if (nodes.Count == 1 && separators.Count == 0) return nodes[0];

            return new TestList(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray());
        }

        private ExpressionNode ParseDictorSetMaker()
        {
            var startPos = _lexer.Position;
            var nodes = new List<ExpressionNode>();
            var separators = new List<Token>();
            var isDictionary = true;
            
            if (_lexer.CurSymbol.Kind == TokenKind.PyMul)
            {
                isDictionary = false;
                var right = ParseStarExpr();
                nodes.Add( right );
            }
            else if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
            {
                var powerOp = _lexer.CurSymbol;
                _lexer.Advance();
                var powerNode = ParseOr();
                
                nodes.Add( new DictionaryKW(startPos, _lexer.Position, powerOp, powerNode) );
            }
            else
            {
                var key = ParseTest();
                if (_lexer.CurSymbol.Kind == TokenKind.PyColon)
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var value = ParseTest();
                    nodes.Add( new DictionaryEntry(startPos, _lexer.Position, key, symbol, value) );
                }
                else
                {
                    isDictionary = false;
                    nodes.Add( key );
                }
            }

            if (_lexer.CurSymbol.Kind == TokenKind.PyAsync || _lexer.CurSymbol.Kind == TokenKind.PyFor)
                nodes.Add( ParseCompFor() );
            else
            {
                while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                {
                    separators.Add( _lexer.CurSymbol );
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind == TokenKind.PyRightCurly) break;
                    if (isDictionary)
                    {
                        if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
                        {
                            var powerOp = _lexer.CurSymbol;
                            _lexer.Advance();
                            var powerNode = ParseOr();
                
                            nodes.Add( new DictionaryKW(startPos, _lexer.Position, powerOp, powerNode) );
                        }
                        else
                        {
                            var key = ParseTest();
                            if (_lexer.CurSymbol.Kind != TokenKind.PyColon) 
                                throw new SyntaxError(_lexer.Position, "Missing ':' in dictionary entry!", _lexer.CurSymbol);
                            var symbol = _lexer.CurSymbol;
                            _lexer.Advance();
                            var value = ParseTest();
                            nodes.Add( new DictionaryEntry(startPos, _lexer.Position, key, symbol, value) );
                        }
                    }
                    else
                    {
                        if (_lexer.CurSymbol.Kind == TokenKind.PyMul) nodes.Add(ParseStarExpr());
                        else nodes.Add( ParseTest() );
                    }
                }
            }

            if (isDictionary)
            {
                return new DictionaryContainer(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray());
            }
            else
            {
                return new SetContainer(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray());
            }
        }
        
        private ExpressionNode ParseArgList()
        {
            var startPos = _lexer.Position;
            var nodes = new List<ExpressionNode>();
            var separators = new List<Token>();
            nodes.Add( ParseArgumenter() );
            
            
            while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
            {
                separators.Add(_lexer.CurSymbol);
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind == TokenKind.PyRightParen) break;
                nodes.Add(ParseArgumenter());
            }
            

            if (nodes.Count == 1 && separators.Count == 0) return nodes[0];
            
            return new ArgList(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray());
        }

        private ExpressionNode ParseArgumenter()
        {
            var startPos = _lexer.Position;
            if (_lexer.CurSymbol.Kind == TokenKind.PyMul)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseTest();

                return new Argument(startPos, _lexer.Position, null, symbol, right);
            }
            else if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseTest();

                return new Argument(startPos, _lexer.Position, null, symbol, right);
            }
            else
            {
                var left = ParseTest();
                switch (_lexer.CurSymbol.Kind)
                {
                    case TokenKind.PyAsync:
                    case TokenKind.PyFor:
                        var right = ParseCompFor();
                        return new Argument(startPos, _lexer.Position, left, null, right);
                    case TokenKind.PyColonAssign:
                        var symbol = _lexer.CurSymbol;
                        _lexer.Advance();
                        var right2 = ParseTest();
                        return new Argument(startPos, _lexer.Position, left, symbol, right2);
                    case TokenKind.PyAssign:
                        var symbol2 = _lexer.CurSymbol;
                        _lexer.Advance();
                        var right3 = ParseTest();
                        return new Argument(startPos, _lexer.Position, left, symbol2, right3);
                    default:
                        return left;
                }
            }
        }

        private ExpressionNode ParseCompIter()
        {
            if (_lexer.CurSymbol.Kind == TokenKind.PyAsync || _lexer.CurSymbol.Kind == TokenKind.PyFor)
                return ParseCompFor();
            else
                return ParseCompIf();
        }

        private ExpressionNode ParseSyncCompFor()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var left = ParseExprList();
            if (_lexer.CurSymbol.Kind != TokenKind.PyIn) 
                throw new SyntaxError(_lexer.Position, "Expecting 'in' in for expression!", _lexer.CurSymbol);
            var symbol2 = _lexer.CurSymbol;
            _lexer.Advance();
            var right = ParseOrTest();
            if (_lexer.CurSymbol.Kind == TokenKind.PyAsync || _lexer.CurSymbol.Kind == TokenKind.PyFor ||
                _lexer.CurSymbol.Kind == TokenKind.PyIf)
            {
                var next = ParseCompIter();

                return new SyncCompFor(startPos, _lexer.Position, symbol, left, symbol2, right, next);
            }
            
            return new SyncCompFor(startPos, _lexer.Position, symbol, left, symbol2, right, null);
        }
        
        private ExpressionNode ParseCompFor()
        {
            var startPos = _lexer.Position;
            if (_lexer.CurSymbol.Kind == TokenKind.PyAsync)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseSyncCompFor();

                return new CompFor(startPos, _lexer.Position, symbol, right);
            }

            return ParseSyncCompFor();
        }

        private ExpressionNode ParseCompIf()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var left = ParseTestNoCond();
            if (_lexer.CurSymbol.Kind == TokenKind.PyAsync || _lexer.CurSymbol.Kind == TokenKind.PyFor ||
                _lexer.CurSymbol.Kind == TokenKind.PyIf)
            {
                var next = ParseCompIter();

                return new CompIf(startPos, _lexer.Position, symbol, left, next);
            }


            return new CompIf(startPos, _lexer.Position, symbol, left, null);
        }

        private ExpressionNode ParseYieldExpr()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind == TokenKind.PyFrom)
            {
                var symbol2 = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseTest();

                return new YieldFrom(startPos, _lexer.Position, symbol, symbol2, right);
            }

            var rightPlain = ParseTestListStarExpr();

            return new YieldExpr(startPos, _lexer.Position, symbol, rightPlain);
        }

        private ExpressionNode ParseVarArgsList()
        {
            var startPos = _lexer.Position;
            var nodes = new List<ExpressionNode>();
            var separators = new List<Token>();
            Token div = null;
            Token mulOp = null, powerOp = null;
            NameToken mulNode = null, powerNode = null;

            if (_lexer.CurSymbol.Kind == TokenKind.PyMul)
            {
                mulOp = _lexer.CurSymbol;
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.Name)
                    throw new SyntaxError(_lexer.Position, "Missing NAME literal after '*' in argument list!", _lexer.CurSymbol);
                mulNode = _lexer.CurSymbol as NameToken;
                _lexer.Advance();
                while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                {
                    separators.Add( _lexer.CurSymbol );
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                        throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!", _lexer.CurSymbol);
                    if (_lexer.CurSymbol.Kind == TokenKind.PyColon) continue;
                    if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
                    {
                        powerOp = _lexer.CurSymbol;
                        _lexer.Advance();
                        if (_lexer.CurSymbol.Kind != TokenKind.Name)
                            throw new SyntaxError(_lexer.Position, "Missing NAME literal after '**' in argument list!", _lexer.CurSymbol);
                        powerNode = _lexer.CurSymbol as NameToken;
                        _lexer.Advance();
                        if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                        {
                            separators.Add( _lexer.CurSymbol );
                            _lexer.Advance();
                        }
                        if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                            throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!", _lexer.CurSymbol);
                        continue;
                    }
                    nodes.Add( ParseVfpDefAssign() );
                }

            }
            else if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
            {
                powerOp = _lexer.CurSymbol;
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.Name)
                    throw new SyntaxError(_lexer.Position, "Missing NAME literal after '**' in argument list!", _lexer.CurSymbol);
                powerNode = _lexer.CurSymbol as NameToken;
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                {
                    separators.Add( _lexer.CurSymbol );
                    _lexer.Advance();
                }
                if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                    throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!", _lexer.CurSymbol);
            }
            else
            {
                nodes.Add( ParseVfpDefAssign() );
                while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                {
                    separators.Add(_lexer.CurSymbol);
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                        throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!", _lexer.CurSymbol);
                    if (_lexer.CurSymbol.Kind == TokenKind.PyMul 
                        || _lexer.CurSymbol.Kind == TokenKind.PyPower
                        || _lexer.CurSymbol.Kind == TokenKind.PyColon
                        || _lexer.CurSymbol.Kind == TokenKind.PyDiv) continue;
                    nodes.Add( ParseVfpDefAssign() );
                }

                if (_lexer.CurSymbol.Kind == TokenKind.PyDiv)
                {
                    div = _lexer.CurSymbol;
                    _lexer.Advance();
                    var lastToken = div;

                    if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                    {
                        while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                        {
                            separators.Add(_lexer.CurSymbol);
                            lastToken = _lexer.CurSymbol;
                            _lexer.Advance();
                            if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                                throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!", _lexer.CurSymbol);
                            if (_lexer.CurSymbol.Kind == TokenKind.PyMul 
                                || _lexer.CurSymbol.Kind == TokenKind.PyPower
                                || _lexer.CurSymbol.Kind == TokenKind.PyColon) continue;
                            nodes.Add(ParseVfpDefAssign());
                            if (_lexer.CurSymbol.Kind != TokenKind.PyComma)
                                return new VarArgsListStatement(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray(), div, mulOp, mulNode, powerOp, powerNode);
                        }
                        
                        if (lastToken.Kind != TokenKind.PyComma)
                            throw new SyntaxError(_lexer.Position, "Expected ',' in argument list!", _lexer.CurSymbol);

                        if (_lexer.CurSymbol.Kind == TokenKind.PyMul)
                        {
                            mulOp = _lexer.CurSymbol;
                            _lexer.Advance();
                            if (_lexer.CurSymbol.Kind != TokenKind.Name)
                                throw new SyntaxError(_lexer.Position, "Missing NAME literal after '*' in argument list!", _lexer.CurSymbol);
                            mulNode = _lexer.CurSymbol as NameToken;
                            _lexer.Advance();
                            while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                            {
                                separators.Add(_lexer.CurSymbol);
                                _lexer.Advance();
                                if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                                    throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!",
                                        _lexer.CurSymbol);
                                if (_lexer.CurSymbol.Kind == TokenKind.PyColon) continue;
                                if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
                                {
                                    powerOp = _lexer.CurSymbol;
                                    _lexer.Advance();
                                    if (_lexer.CurSymbol.Kind != TokenKind.Name)
                                        throw new SyntaxError(_lexer.Position,
                                            "Missing NAME literal after '**' in argument list!", _lexer.CurSymbol);
                                    powerNode = _lexer.CurSymbol as NameToken;
                                    _lexer.Advance();
                                    if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                                    {
                                        separators.Add(_lexer.CurSymbol);
                                        _lexer.Advance();
                                    }

                                    if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                                        throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!",
                                            _lexer.CurSymbol);
                                    continue;
                                }

                                nodes.Add(ParseVfpDefAssign());
                            }
                        }
                        else if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
                        {
                            powerOp = _lexer.CurSymbol;
                            _lexer.Advance();
                            if (_lexer.CurSymbol.Kind != TokenKind.Name)
                                throw new SyntaxError(_lexer.Position, "Missing NAME literal after '**' in argument list!", _lexer.CurSymbol);
                            powerNode = _lexer.CurSymbol as NameToken;
                            _lexer.Advance();
                            if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                            {
                                separators.Add( _lexer.CurSymbol );
                                _lexer.Advance();
                            }
                            if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                                throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!", _lexer.CurSymbol);
                        }
                    }
                }
            }
            
            return new VarArgsListStatement(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray(), div, mulOp, mulNode, powerOp, powerNode);
        }

        private ExpressionNode ParseVfpDefAssign()
        {
            var startPos = _lexer.Position;
            Token left = null, assignment = null;
            ExpressionNode right = null;
            if (_lexer.CurSymbol.Kind != TokenKind.Name)
                throw new SyntaxError(_lexer.Position, "Missing NAME literal in argument list!", _lexer.CurSymbol);
            left = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind == TokenKind.PyAssign)
            {
                assignment = _lexer.CurSymbol;
                _lexer.Advance();
                right = ParseTest();
            }

            return new VfpDefAssignStatement(startPos, _lexer.Position, left as NameToken, assignment, right);
        }
        
        
#endregion


#region Statement rules

        private StatementNode ParseCompound()
        {
            switch (_lexer.CurSymbol.Kind)
            {
                case TokenKind.PyIf:
                    return ParseIf();
                case TokenKind.PyWhile:
                    return ParseWhile();
                case TokenKind.PyFor:
                    return ParseFor();
                case TokenKind.PyTry:
                    return ParseTry();
                case TokenKind.PyWith:
                    return ParseWith();
                case TokenKind.PyDef:
                    return ParseFuncDef();
                case TokenKind.PyClass:
                    return ParseClass();
                case TokenKind.PyMatrice:
                    return ParseDecorated();
                case TokenKind.PyAsync:
                    return ParseAsync();
                default:
                    throw new SyntaxError(_lexer.Position, "Illegal statement!", _lexer.CurSymbol);
            }
        }

        private StatementNode ParseIf()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            var nodes = new List<StatementNode>();
            StatementNode node = null;
            _lexer.Advance();
            var left = ParseNamedExpr();
            if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                throw new SyntaxError(_lexer.Position, "Missing ':' in 'if' statement!", _lexer.CurSymbol);
            var symbol2 = _lexer.CurSymbol;
            _lexer.Advance();
            var right = ParseSuite();
            while (_lexer.CurSymbol.Kind == TokenKind.PyElif) nodes.Add( ParseElif() );
            if (_lexer.CurSymbol.Kind == TokenKind.PyElse) node = ParseElse();

            return new IfStatement(startPos, _lexer.Position, symbol, left, symbol2, right, nodes.ToArray(), node);
        }
        
        private StatementNode ParseElif()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var left = ParseNamedExpr();
            if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                throw new SyntaxError(_lexer.Position, "Missing ':' in 'elif' statement!", _lexer.CurSymbol);
            var symbol2 = _lexer.CurSymbol;
            _lexer.Advance();
            var right = ParseSuite();

            return new ElifStatement(startPos, _lexer.Position, symbol, left, symbol2, right);
        }
        
        private StatementNode ParseElse()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                throw new SyntaxError(_lexer.Position, "Missing ':' in 'else' statement!", _lexer.CurSymbol);
            var symbol2 = _lexer.CurSymbol;
            _lexer.Advance();
            var right = ParseSuite();

            return new ElseStatement(startPos, _lexer.Position, symbol, symbol2, right);
        }
        
        private StatementNode ParseWhile()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var left = ParseNamedExpr();
            if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                throw new SyntaxError(_lexer.Position, "Missing ':' in 'while' statement!", _lexer.CurSymbol);
            var symbol2 = _lexer.CurSymbol;
            _lexer.Advance();
            var right = ParseSuite();
            if (_lexer.CurSymbol.Kind == TokenKind.PyElse)
            {
                var next = ParseElse();

                return new WhileStatement(startPos, _lexer.Position, symbol, left, symbol2, right, next);
            }
            
            return new WhileStatement(startPos, _lexer.Position, symbol, left, symbol2, right, null);
        }
        
        private StatementNode ParseFor()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var left = ParseExprList();
            if (_lexer.CurSymbol.Kind != TokenKind.PyIn)
                throw new SyntaxError(_lexer.Position, "Missing 'in' in 'for' statement!", _lexer.CurSymbol);
            var symbol2 = _lexer.CurSymbol;
            _lexer.Advance();
            var right = ParseTestList();
            if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                throw new SyntaxError(_lexer.Position, "Missing ':' in 'for' statement!", _lexer.CurSymbol);
            var symbol3 = _lexer.CurSymbol;
            _lexer.Advance();
            Token tc = null;
            if (_lexer.CurSymbol.Kind == TokenKind.TypeComment)
            {
                tc = _lexer.CurSymbol;
                _lexer.Advance();
            }

            var next = ParseSuite();
            if (_lexer.CurSymbol.Kind == TokenKind.PyElse)
            {
                var node = ParseElse();

                return new ForStatement(startPos, _lexer.Position, symbol, left, symbol2, right, symbol3, tc, next, node);
            }
            
            return new ForStatement(startPos, _lexer.Position, symbol, left, symbol2, right, symbol3, tc, next, null);
        }
        
        private StatementNode ParseWith()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var nodes = new List<StatementNode>();
            var separators = new List<Token>();
            nodes.Add( ParseWithItem() );
            while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
            {
                separators.Add(_lexer.CurSymbol);
                _lexer.Advance();
                nodes.Add( ParseWithItem() );
            }
            if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                throw new SyntaxError(_lexer.Position, "Missing ':' in 'with' statement!", _lexer.CurSymbol);
            var symbol2 = _lexer.CurSymbol;
            _lexer.Advance();
            
            Token tc = null;
            if (_lexer.CurSymbol.Kind == TokenKind.TypeComment)
            {
                tc = _lexer.CurSymbol;
                _lexer.Advance();
            }

            var right = ParseSuite();

            return new WithStatement(startPos, _lexer.Position, symbol, nodes.ToArray(), separators.ToArray(), symbol2, tc, right);
        }

        private StatementNode ParseWithItem()
        {
            var startPos = _lexer.Position;
            var left = ParseTest();
            if (_lexer.CurSymbol.Kind == TokenKind.PyAs)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseOr();

                return new WithItemStatement(startPos, _lexer.Position, left, symbol, right);
            }
            
            return new WithItemStatement(startPos, _lexer.Position, left, null, null);
        }
        
        private StatementNode ParseTry()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                throw new SyntaxError(_lexer.Position, "Missing ':' in 'try' statement!", _lexer.CurSymbol);
            var symbol2 = _lexer.CurSymbol;
            _lexer.Advance();
            var left = ParseSuite();
            if (_lexer.CurSymbol.Kind == TokenKind.PyFinally)
            {
                var symbol3 = _lexer.CurSymbol;
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                    throw new SyntaxError(_lexer.Position, "Missing ':' in 'finally' statement!", _lexer.CurSymbol);
                var symbol4 = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseSuite();

                return new TryStatement(startPos, _lexer.Position, 
                    symbol, symbol2, left, new StatementNode[] {}, null, symbol3, symbol4, right);
            }
            else
            {
                if (_lexer.CurSymbol.Kind != TokenKind.PyExcept)
                    throw new SyntaxError(_lexer.Position, "Missing 'except' in 'try' statement!", _lexer.CurSymbol);
                var nodes = new List<StatementNode>();
                while (_lexer.CurSymbol.Kind == TokenKind.PyExcept) nodes.Add( ParseExcept() );
                StatementNode node = null;
                if (_lexer.CurSymbol.Kind == TokenKind.PyElse) node = ParseElse();
                if (_lexer.CurSymbol.Kind == TokenKind.PyFinally)
                {
                    var symbol3 = _lexer.CurSymbol;
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                        throw new SyntaxError(_lexer.Position, "Missing ':' in 'finally' statement!", _lexer.CurSymbol);
                    var symbol4 = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = ParseSuite();

                    return new TryStatement(startPos, _lexer.Position, 
                        symbol, symbol2, left, nodes.ToArray(), node, symbol3, symbol4, right);
                }

                return new TryStatement(startPos, _lexer.Position, 
                    symbol, symbol2, left, nodes.ToArray(), node, null, null, null);
            }
        }

        private StatementNode ParseExcept()
        {
            var startPos = _lexer.Position;
            var left = ParseExceptClause();
            if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                throw new SyntaxError(_lexer.Position, "Missing ':' in 'except' statement!", _lexer.CurSymbol);
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var right = ParseSuite();

            return new ExceptStatement(startPos, _lexer.Position, left, symbol, right);
        }

        private StatementNode ParseExceptClause()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
            {
                var left = ParseTest();
                if (_lexer.CurSymbol.Kind == TokenKind.PyAs)
                {
                    var symbol2 = _lexer.CurSymbol;
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind != TokenKind.Name)
                        throw new SyntaxError(_lexer.Position, "Missing Name after 'as' in 'except' statement!", _lexer.CurSymbol);
                    var right = _lexer.CurSymbol as NameToken;
                    _lexer.Advance();

                    return new ExceptClauseStatement(startPos, _lexer.Position, symbol, left, symbol2, right);
                }
                
                return new ExceptClauseStatement(startPos, _lexer.Position, symbol, left, null, null);
            }
            
            return new ExceptClauseStatement(startPos, _lexer.Position, symbol, null, null, null);
        }
        
        private StatementNode ParseDecorated()
        {
            var startPos = _lexer.Position;
            var left = ParseDecorators();
            switch (_lexer.CurSymbol.Kind)
            {
                case TokenKind.PyClass:
                    var rightClass = ParseClass();
                    return new DecoratedStatement(startPos, _lexer.Position, left, rightClass);
                case TokenKind.PyDef:
                    var rightDef = ParseClass();
                    return new DecoratedStatement(startPos, _lexer.Position, left, rightDef);
                case TokenKind.PyAsync:
                    var rightAsync = ParseAsyncFuncDef();
                    return new DecoratedStatement(startPos, _lexer.Position, left, rightAsync);
                default:
                    throw new SyntaxError(_lexer.Position, "Expecting 'async', 'class' or 'def' after '@' decorators!", _lexer.CurSymbol);
            }
        }

        private StatementNode ParseDecorators()
        {
            var startPos = _lexer.Position;
            var nodes = new List<StatementNode>();
            nodes.Add( ParseDecorator() );
            while (_lexer.CurSymbol.Kind == TokenKind.PyMatrice) nodes.Add( ParseDecorator() );

            return new DecoratorsStatement(startPos, _lexer.Position, nodes.ToArray());
        }
        
        private StatementNode ParseDecorator()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var left = ParseDottedName();
            Token symbol2 = null, symbol3 = null;
            ExpressionNode right = null;
            if (_lexer.CurSymbol.Kind == TokenKind.PyLeftParen)
            {
                symbol2 = _lexer.CurSymbol;
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen) right = ParseArgList();
                if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen)
                    throw new SyntaxError(_lexer.Position, "Expecting ')' in decorator!", _lexer.CurSymbol);
                symbol3 = _lexer.CurSymbol;
                _lexer.Advance();
            }
            if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen)
                throw new SyntaxError(_lexer.Position, "Expecting Newkine after decorator!", _lexer.CurSymbol);
            var symbol4 = _lexer.CurSymbol;
            _lexer.Advance();

            return new DecoratorStatement(startPos, _lexer.Position, symbol, left, symbol2, right, symbol3, symbol4);
        }

        private StatementNode ParseAsyncFuncDef()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind != TokenKind.PyDef)
                throw new SyntaxError(_lexer.Position, "Expecting 'def' after 'async'", _lexer.CurSymbol);
            var right = ParseFuncDef();

            return new AsyncStatement(startPos, _lexer.Position, symbol, right);
        }
        
        private StatementNode ParseFuncDef()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind != TokenKind.Name)
                throw new SyntaxError(_lexer.Position, "Missing Name of functiomn!", _lexer.CurSymbol);
            var symbol2 = _lexer.CurSymbol;
            _lexer.Advance();
            var left = ParseParameterStmt();
            Token symbol3 = null;
            ExpressionNode right = null;
            if (_lexer.CurSymbol.Kind == TokenKind.PyArrow)
            {
                symbol3 = _lexer.CurSymbol;
                _lexer.Advance();
                right = ParseTest();
            }
            if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                throw new SyntaxError(_lexer.Position, "Expecting ':' in function declaration!", _lexer.CurSymbol);
            var symbol4 = _lexer.CurSymbol;
            _lexer.Advance();
            Token tc = null;
            if (_lexer.CurSymbol.Kind == TokenKind.TypeComment)
            {
                tc = _lexer.CurSymbol;
                _lexer.Advance();
            }

            var next = ParseFuncBodySuite();

            return new FuncDefStatement(startPos, _lexer.Position, 
                symbol, symbol2, left, symbol3, right, symbol4, tc, next);
        }
        
        private StatementNode ParseParameterStmt()
        {
            var startPos = _lexer.Position;
            if (_lexer.CurSymbol.Kind == TokenKind.PyLeftParen)
                throw new SyntaxError(_lexer.Position, "Expecting '(' in function declaration!", _lexer.CurSymbol);
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            StatementNode right = null;
            if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen) right = ParseTypedArgsList();
            if (_lexer.CurSymbol.Kind == TokenKind.PyLeftParen)
                throw new SyntaxError(_lexer.Position, "Expecting ')' in function declaration!", _lexer.CurSymbol);
            var symbol2 = _lexer.CurSymbol;
            _lexer.Advance();

            return new ParametersStatement(startPos, _lexer.Position, symbol, right, symbol2);
        }

        private StatementNode ParseFuncBodySuite()
        {
            if (_lexer.CurSymbol.Kind == TokenKind.Newline)
            {
                var startPos = _lexer.Position;
                var newline = _lexer.CurSymbol;
                _lexer.Advance();

                Token tc = null, nl = null; 

                if (_lexer.CurSymbol.Kind == TokenKind.TypeComment)
                {
                    tc = _lexer.CurSymbol;
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind != TokenKind.Newline)
                        throw new SyntaxError(_lexer.Position, "Expecting Newline after typecomment!", _lexer.CurSymbol);
                    nl = _lexer.CurSymbol;
                    _lexer.Advance();
                }
                
                if (_lexer.CurSymbol.Kind == TokenKind.Indent)
                    throw new SyntaxError(_lexer.Position, "Missing indentation!", _lexer.CurSymbol);
                var indent = _lexer.CurSymbol;
                _lexer.Advance();
                var nodes = new List<StatementNode>();
                nodes.Add(ParseStmt());
                while (_lexer.CurSymbol.Kind != TokenKind.Dedent) nodes.Add(ParseStmt());
                var dedent = _lexer.CurSymbol;
                _lexer.Advance();

                return new FuncBodySuiteStatement(startPos, _lexer.Position, 
                    newline,  tc, nl, indent, nodes.ToArray(), dedent);
            }

            return ParseSimpleStmt();
        }
        
        private StatementNode ParseTypedArgsList()
        {
            var startPos = _lexer.Position;
            var nodes = new List<StatementNode>();
            var separators = new List<Token>();
            var tc = new List<Token>();
            Token div = null;
            Token mulOp = null, powerOp = null;
            StatementNode mulNode = null, powerNode = null;

            if (_lexer.CurSymbol.Kind == TokenKind.PyMul)
            {
                mulOp = _lexer.CurSymbol;
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.Name)
                    throw new SyntaxError(_lexer.Position, "Missing NAME literal after '*' in argument list!", _lexer.CurSymbol);
                mulNode = ParseTfpDef();
                while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                {
                    separators.Add( _lexer.CurSymbol );
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                        throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!", _lexer.CurSymbol);
                    if (_lexer.CurSymbol.Kind == TokenKind.PyColon) continue;
                    if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
                    {
                        powerOp = _lexer.CurSymbol;
                        _lexer.Advance();
                        if (_lexer.CurSymbol.Kind != TokenKind.Name)
                            throw new SyntaxError(_lexer.Position, "Missing NAME literal after '**' in argument list!", _lexer.CurSymbol);
                        powerNode = ParseTfpDef();
                        if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                        {
                            separators.Add( _lexer.CurSymbol );
                            _lexer.Advance();
                        }
                        if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                            throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!", _lexer.CurSymbol);
                        continue;
                    }
                    nodes.Add( ParseTypedAssign() );
                }

            }
            else if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
            {
                powerOp = _lexer.CurSymbol;
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.Name)
                    throw new SyntaxError(_lexer.Position, "Missing NAME literal after '**' in argument list!", _lexer.CurSymbol);
                powerNode = ParseTfpDef();
                if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                {
                    separators.Add( _lexer.CurSymbol );
                    _lexer.Advance();
                }
                if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                    throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!", _lexer.CurSymbol);
            }
            else
            {
                nodes.Add( ParseTypedAssign() );
                while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                {
                    separators.Add(_lexer.CurSymbol);
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                        throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!", _lexer.CurSymbol);
                    if (_lexer.CurSymbol.Kind == TokenKind.PyMul 
                        || _lexer.CurSymbol.Kind == TokenKind.PyPower
                        || _lexer.CurSymbol.Kind == TokenKind.PyColon
                        || _lexer.CurSymbol.Kind == TokenKind.PyDiv) continue;
                    nodes.Add( ParseTypedAssign() );
                }

                if (_lexer.CurSymbol.Kind == TokenKind.PyDiv)
                {
                    div = _lexer.CurSymbol;
                    _lexer.Advance();
                    var lastToken = div;

                    if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                    {
                        while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                        {
                            separators.Add(_lexer.CurSymbol);
                            lastToken = _lexer.CurSymbol;
                            _lexer.Advance();
                            if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                                throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!", _lexer.CurSymbol);
                            if (_lexer.CurSymbol.Kind == TokenKind.PyMul 
                                || _lexer.CurSymbol.Kind == TokenKind.PyPower
                                || _lexer.CurSymbol.Kind == TokenKind.PyColon) continue;
                            nodes.Add(ParseTypedAssign());
                            if (_lexer.CurSymbol.Kind != TokenKind.PyComma)
                                return new TypedArgsListStatement(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray(), div, mulOp, mulNode, powerOp, powerNode, tc.ToArray());
                        }
                        
                        if (lastToken.Kind != TokenKind.PyComma)
                            throw new SyntaxError(_lexer.Position, "Expected ',' in argument list!", _lexer.CurSymbol);

                        if (_lexer.CurSymbol.Kind == TokenKind.PyMul)
                        {
                            mulOp = _lexer.CurSymbol;
                            _lexer.Advance();
                            if (_lexer.CurSymbol.Kind != TokenKind.Name)
                                throw new SyntaxError(_lexer.Position, "Missing NAME literal after '*' in argument list!", _lexer.CurSymbol);
                            mulNode = ParseTfpDef();
                            while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                            {
                                separators.Add(_lexer.CurSymbol);
                                _lexer.Advance();
                                if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                                    throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!",
                                        _lexer.CurSymbol);
                                if (_lexer.CurSymbol.Kind == TokenKind.PyColon) continue;
                                if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
                                {
                                    powerOp = _lexer.CurSymbol;
                                    _lexer.Advance();
                                    if (_lexer.CurSymbol.Kind != TokenKind.Name)
                                        throw new SyntaxError(_lexer.Position,
                                            "Missing NAME literal after '**' in argument list!", _lexer.CurSymbol);
                                    powerNode = ParseTfpDef();
                                    if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                                    {
                                        separators.Add(_lexer.CurSymbol);
                                        _lexer.Advance();
                                    }

                                    if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                                        throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!",
                                            _lexer.CurSymbol);
                                    continue;
                                }

                                nodes.Add(ParseTypedAssign());
                            }
                        }
                        else if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
                        {
                            powerOp = _lexer.CurSymbol;
                            _lexer.Advance();
                            if (_lexer.CurSymbol.Kind != TokenKind.Name)
                                throw new SyntaxError(_lexer.Position, "Missing NAME literal after '**' in argument list!", _lexer.CurSymbol);
                            powerNode = ParseTfpDef();
                            if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                            {
                                separators.Add( _lexer.CurSymbol );
                                _lexer.Advance();
                            }
                            if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                                throw new SyntaxError(_lexer.Position, "Unexpected ',' in argument list!", _lexer.CurSymbol);
                        }
                    }
                }
            }
            
            return new TypedArgsListStatement(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray(), div, mulOp, mulNode, powerOp, powerNode, tc.ToArray());
        }
        
        private StatementNode ParseTypedAssign()
        {
            var startPos = _lexer.Position;
            var left = ParseTfpDef();
            if (_lexer.CurSymbol.Kind == TokenKind.PyAssign)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseTest();

                return new TfpDefAssignStatement(startPos, _lexer.Position, left, symbol, right);
            }

            return left;
        }
        
        private StatementNode ParseTfpDef()
        {
            var startPos = _lexer.Position;
            if (_lexer.CurSymbol.Kind == TokenKind.Name)
                throw new SyntaxError(_lexer.Position, "Expecting Name literal in argument!", _lexer.CurSymbol);
            var left = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind == TokenKind.PyColon)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseTest();

                return new TfpDefStatement(startPos, _lexer.Position, left, symbol, right);
            }
            
            return new TfpDefStatement(startPos, _lexer.Position, left, null, null);
        }
        
        private StatementNode ParseClass()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind != TokenKind.Name)
                throw new SyntaxError(_lexer.Position, "Expecting Name of class statement!", _lexer.CurSymbol);
            var symbol2 = _lexer.CurSymbol;
            _lexer.Advance();
            Token symbol3 = null, symbol4 = null;
            ExpressionNode left = null;
            if (_lexer.CurSymbol.Kind == TokenKind.PyLeftParen)
            {
                symbol3 = _lexer.CurSymbol;
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen) left = ParseArgList();
                if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen)
                    throw new SyntaxError(_lexer.Position, "Expecting ')' in class statement!", _lexer.CurSymbol);
                symbol4 = _lexer.CurSymbol;
                _lexer.Advance();
            }
            if (_lexer.CurSymbol.Kind != TokenKind.PyColon)
                throw new SyntaxError(_lexer.Position, "Expecting ':' in class statement!", _lexer.CurSymbol);
            var symbol5 = _lexer.CurSymbol;
            _lexer.Advance();
            var right = ParseSuite();

            return new ClassStatement(startPos, _lexer.Position, symbol, symbol2 as NameToken, symbol3, left, symbol4, symbol5, right);
        }

        private StatementNode ParseSuite()
        {
            if (_lexer.CurSymbol.Kind == TokenKind.Newline)
            {
                var startPos = _lexer.Position;
                var newline = _lexer.CurSymbol;
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind == TokenKind.Indent)
                    throw new SyntaxError(_lexer.Position, "Missing indentation!", _lexer.CurSymbol);
                var indent = _lexer.CurSymbol;
                _lexer.Advance();
                var nodes = new List<StatementNode>();
                nodes.Add( ParseStmt() );
                while (_lexer.CurSymbol.Kind != TokenKind.Dedent) nodes.Add( ParseStmt() );
                var dedent = _lexer.CurSymbol;
                _lexer.Advance();

                return new SuiteStatement(startPos, _lexer.Position, newline, indent, nodes.ToArray(), dedent);
            }

            return ParseSimpleStmt();
        }
        
        private StatementNode ParseAsync()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            StatementNode right = null;
            switch (_lexer.CurSymbol.Kind)
            {
                case TokenKind.PyDef:
                    right = ParseFuncDef();
                    return new AsyncStatement(startPos, _lexer.Position, symbol, right);
                case TokenKind.PyWith:
                    right = ParseWith();
                    return new AsyncStatement(startPos, _lexer.Position, symbol, right);
                case TokenKind.PyFor:
                    right = ParseFor();
                    return new AsyncStatement(startPos, _lexer.Position, symbol, right);
            }

            throw new SyntaxError(_lexer.Position, "Expecting 'def', 'with' or 'for' after 'async' statement!", _lexer.CurSymbol);
        }

        private StatementNode ParseStmt()
        {
            switch (_lexer.CurSymbol.Kind)
            {
                case TokenKind.PyIf:
                case TokenKind.PyWhile:
                case TokenKind.PyFor:
                case TokenKind.PyTry:
                case TokenKind.PyWith:
                case TokenKind.PyAsync:
                case TokenKind.PyDef:
                case TokenKind.PyClass:
                case TokenKind.PyMatrice:
                    return ParseCompound();
                default:
                    return ParseSimpleStmt();
            }
        }

        private StatementNode ParseSimpleStmt()
        {
            var startPos = _lexer.Position;
            var nodes = new List<StatementNode>();
            var separators = new List<Token>();
            nodes.Add( ParseSmallStmt() );
            while (_lexer.CurSymbol.Kind == TokenKind.PySemiColon)
            {
                separators.Add( _lexer.CurSymbol );
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.Newline) nodes.Add( ParseSmallStmt() );
            }

            if (_lexer.CurSymbol.Kind != TokenKind.Newline)
                throw new SyntaxError(_lexer.Position, "Expecting newline or ';' in statement kist!", _lexer.CurSymbol);
            var newline = _lexer.CurSymbol;
            _lexer.Advance();
            
            return new SimpleStatement(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray(), newline);
        }

        private StatementNode ParseSmallStmt()
        {
            switch (_lexer.CurSymbol.Kind)
            {
                case TokenKind.PyDel:
                    return ParseDelStmt();
                case TokenKind.PyPass:
                    return ParsePassStmt();
                case TokenKind.PyBreak:
                    return ParseBreakStmt();
                case TokenKind.PyContinue:
                    return ParseContinueStmt();
                case TokenKind.PyRaise:
                    return ParseRaiseStmt();
                case TokenKind.PyYield:
                    return ParseYieldStmt();
                case TokenKind.PyReturn:
                    return ParseReturnStmt();
                case TokenKind.PyImport:
                case TokenKind.PyFrom:
                    return ParseImportStmt();
                case TokenKind.PyGlobal:
                    return ParseGlobalStmt();
                case TokenKind.PyNonLocal:
                    return ParseNonLocalStmt();
                case TokenKind.PyAssert:
                    return ParseAssertStmt();
                default:
                    return ParseExprStmt();
            }
        }

        private StatementNode ParseExprStmt()
        {
            var startPos = _lexer.Position;
            var left = ParseTestListStarExpr();
            switch (_lexer.CurSymbol.Kind)
            {
                case TokenKind.PyPlusAssign:
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = _lexer.CurSymbol.Kind == TokenKind.PyYield ? ParseYieldExpr() : ParseTestList();
                    return new PlusAssignStatement(startPos, _lexer.Position, left, symbol, right);
                }
                case TokenKind.PyMinusAssign:
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = _lexer.CurSymbol.Kind == TokenKind.PyYield ? ParseYieldExpr() : ParseTestList();
                    return new MinusAssignStatement(startPos, _lexer.Position, left, symbol, right);
                }
                case TokenKind.PyMulAssign:
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = _lexer.CurSymbol.Kind == TokenKind.PyYield ? ParseYieldExpr() : ParseTestList();
                    return new MulAssignStatement(startPos, _lexer.Position, left, symbol, right);
                }
                case TokenKind.PyMatriceAssign:
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = _lexer.CurSymbol.Kind == TokenKind.PyYield ? ParseYieldExpr() : ParseTestList();
                    return new MatriceAssignStatement(startPos, _lexer.Position, left, symbol, right);
                }
                case TokenKind.PyDivAssign:
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = _lexer.CurSymbol.Kind == TokenKind.PyYield ? ParseYieldExpr() : ParseTestList();
                    return new DivAssignStatement(startPos, _lexer.Position, left, symbol, right);
                }
                case TokenKind.PyModuloAssign:
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = _lexer.CurSymbol.Kind == TokenKind.PyYield ? ParseYieldExpr() : ParseTestList();
                    return new ModuloAssignStatement(startPos, _lexer.Position, left, symbol, right);
                }
                case TokenKind.PyBitAndAssign:
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = _lexer.CurSymbol.Kind == TokenKind.PyYield ? ParseYieldExpr() : ParseTestList();
                    return new BitAndAssignStatement(startPos, _lexer.Position, left, symbol, right);
                }
                case TokenKind.PyBitOrAssign:
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = _lexer.CurSymbol.Kind == TokenKind.PyYield ? ParseYieldExpr() : ParseTestList();
                    return new BitOrAssignStatement(startPos, _lexer.Position, left, symbol, right);
                }
                case TokenKind.PyBitXorAssign:
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = _lexer.CurSymbol.Kind == TokenKind.PyYield ? ParseYieldExpr() : ParseTestList();
                    return new BitXorAssignStatement(startPos, _lexer.Position, left, symbol, right);
                }
                case TokenKind.PyShiftLeftAssign:
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = _lexer.CurSymbol.Kind == TokenKind.PyYield ? ParseYieldExpr() : ParseTestList();
                    return new ShiftLeftAssignStatement(startPos, _lexer.Position, left, symbol, right);
                }
                case TokenKind.PyShiftRightAssign:
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = _lexer.CurSymbol.Kind == TokenKind.PyYield ? ParseYieldExpr() : ParseTestList();
                    return new ShiftRightAssignStatement(startPos, _lexer.Position, left, symbol, right);
                }
                case TokenKind.PyPowerAssign:
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = _lexer.CurSymbol.Kind == TokenKind.PyYield ? ParseYieldExpr() : ParseTestList();
                    return new PowerAssignStatement(startPos, _lexer.Position, left, symbol, right);
                }
                case TokenKind.PyFloorDivAssign:
                {
                    var symbol = _lexer.CurSymbol;
                    _lexer.Advance();
                    var right = _lexer.CurSymbol.Kind == TokenKind.PyYield ? ParseYieldExpr() : ParseTestList();
                    return new FloorDivAssignStatement(startPos, _lexer.Position, left, symbol, right);
                }
                case TokenKind.PyColon:
                    return ParseAnnAssign(startPos, left);
                case TokenKind.PyAssign:
                {
                    var operators = new List<Token>();
                    var nodes = new List<Node>();
                    while (_lexer.CurSymbol.Kind == TokenKind.PyAssign)
                    {
                        operators.Add(_lexer.CurSymbol);
                        var symbol = _lexer.CurSymbol;
                        _lexer.Advance();
                        nodes.Add(_lexer.CurSymbol.Kind == TokenKind.PyYield
                            ? ParseYieldExpr() as Node
                            : ParseTestListStarExpr() as Node);
                    }

                    Token tc = null;
                    if (_lexer.CurSymbol.Kind == TokenKind.TypeComment)
                    {
                        tc = _lexer.CurSymbol;
                        _lexer.Advance();
                    }

                    return new AssignStatement(startPos, _lexer.Position, left, operators.ToArray(), nodes.ToArray(), tc);
                }
                default:
                    return left;
            }
        }

        private StatementNode ParseAnnAssign(UInt32 startPos, StatementNode left)
        {
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var right = ParseTest();
            if (_lexer.CurSymbol.Kind == TokenKind.PyAssign)
            {
                var symbol2 = _lexer.CurSymbol;
                _lexer.Advance();
                var next = _lexer.CurSymbol.Kind == TokenKind.PyYield
                    ? ParseYieldExpr() as Node
                    : ParseTestListStarExpr() as Node;
                
                return new AnnAssignStatement(startPos, _lexer.Position, left, symbol, right, symbol2, next);
            }

            return new AnnAssignStatement(startPos, _lexer.Position, left, symbol, right, null, null);
        }
        
        private StatementNode ParseTestListStarExpr()
        {
            var startPos = _lexer.Position;
            var nodes = new List<ExpressionNode>();
            var separators = new List<Token>();
            nodes.Add( _lexer.CurSymbol.Kind == TokenKind.PyMul ? ParseStarExpr() : ParseTest() );
            while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
            {
                separators.Add(_lexer.CurSymbol);
                _lexer.Advance();
                switch (_lexer.CurSymbol.Kind)
                {
                    case TokenKind.PyPlusAssign:
                    case TokenKind.PyMinusAssign:
                    case TokenKind.PyMulAssign:
                    case TokenKind.PyDivAssign:
                    case TokenKind.PyPowerAssign:
                    case TokenKind.PyFloorDivAssign:
                    case TokenKind.PyBitAndAssign:
                    case TokenKind.PyBitOrAssign:
                    case TokenKind.PyBitXorAssign:
                    case TokenKind.PyShiftLeftAssign:
                    case TokenKind.PyShiftRightAssign:
                    case TokenKind.PyMatriceAssign:
                    case TokenKind.PyModuloAssign:
                    case TokenKind.PyColon:
                    case TokenKind.PyAssign:
                    case TokenKind.PySemiColon:
                    case TokenKind.Newline:
                        break;
                    default:
                        nodes.Add( _lexer.CurSymbol.Kind == TokenKind.PyMul ? ParseStarExpr() : ParseTest() );
                        break;
                }
            }
            
            return new TestListStarExprStatement(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray());
        }

        private StatementNode ParseDelStmt()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var right = ParseExprList();

            return new DelStatement(startPos, _lexer.Position, symbol, right);
        }

        private StatementNode ParsePassStmt()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();

            return new PassStatement(startPos, _lexer.Position, symbol);
        }
        
        private StatementNode ParseBreakStmt()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();

            return new BreakStatement(startPos, _lexer.Position, symbol);
        }
        
        private StatementNode ParseContinueStmt()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();

            return new ContinueStatement(startPos, _lexer.Position, symbol);
        }

        private StatementNode ParseReturnStmt()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind != TokenKind.Newline && _lexer.CurSymbol.Kind == TokenKind.PySemiColon)
            {
                var right = ParseTestListStarExpr();

                return new ReturnStatement(startPos, _lexer.Position, symbol, right);
            }
            
            return new ReturnStatement(startPos, _lexer.Position, symbol, null);
        }
        
        private StatementNode ParseYieldStmt()
        {
            var startPos = _lexer.Position;
            var right = ParseYieldExpr();
            
            return new YieldStatement(startPos, _lexer.Position, right);
        }
        
        private StatementNode ParseRaiseStmt()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            ExpressionNode left = null, right = null;
            Token symbol2 = null;
            if (_lexer.CurSymbol.Kind != TokenKind.Newline && _lexer.CurSymbol.Kind != TokenKind.PySemiColon)
            {
                left = ParseTest();
                if (_lexer.CurSymbol.Kind == TokenKind.PyFrom)
                {
                    symbol2 = _lexer.CurSymbol;
                    _lexer.Advance();
                    right = ParseTest();
                }
            }
            
            return new RaiseStatement(startPos, _lexer.Position, symbol, left, symbol2, right);
        }
        
        private StatementNode ParseImportStmt()
        {
            if (_lexer.CurSymbol.Kind == TokenKind.PyImport) return ParseImportNameStmt();
            else return ParseImportFromStmt();
        }

        private StatementNode ParseImportNameStmt()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var right = ParseDottedAsNamesStmt();
            
            return new ImportNameStatement(startPos, _lexer.Position, symbol, right);
        }
        
        private StatementNode ParseImportFromStmt()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var dots = new List<Token>();
            StatementNode left = null, right = null;

            while (_lexer.CurSymbol.Kind == TokenKind.PyElipsis || _lexer.CurSymbol.Kind == TokenKind.PyDot)
            {
                dots.Add(_lexer.CurSymbol);
                _lexer.Advance();
            }

            if (_lexer.CurSymbol.Kind == TokenKind.PyImport && dots.Count == 0)
                throw new SyntaxError(_lexer.Position, "Missing 'from' part of import statement!", _lexer.CurSymbol);
            else if (_lexer.CurSymbol.Kind != TokenKind.PyImport) left = ParseDottedName();

            if (_lexer.CurSymbol.Kind != TokenKind.PyImport)
                throw new SyntaxError(_lexer.Position, "Expected 'import' in 'from' statement!", _lexer.CurSymbol);
            var symbol2 = _lexer.CurSymbol;
            _lexer.Advance();

            if (_lexer.CurSymbol.Kind == TokenKind.PyMul)
            {
                var symbol5 = _lexer.CurSymbol;
                _lexer.Advance();
                
                return new ImportFromStatement(startPos, _lexer.Position, symbol, dots.ToArray(), left, symbol2, symbol5, null, null);
            }
            else if (_lexer.CurSymbol.Kind == TokenKind.PyLeftParen)
            {
                var symbol3 = _lexer.CurSymbol;
                _lexer.Advance();
                right = ParseImportAsNamesStmt();
                if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen)
                    throw new SyntaxError(_lexer.Position, "Missing ')' in import statement!", _lexer.CurSymbol);
                var symbol4 = _lexer.CurSymbol;
                _lexer.Advance();

                return new ImportFromStatement(startPos, _lexer.Position, symbol, dots.ToArray(), left, symbol2, symbol3, right, symbol4);
            }

            right = ParseImportAsNamesStmt();

            return new ImportFromStatement(startPos, _lexer.Position, symbol, dots.ToArray(), left, symbol2, null, right, null);
        }
        
        private StatementNode ParseImportAsNameStmt()
        {
            var startPos = _lexer.Position;
            Token symbol = null, symbol2 = null, symbol3 = null;
            if (_lexer.CurSymbol.Kind != TokenKind.Name)
                throw new SyntaxError(_lexer.Position, "Expecting Name literal in import statement!", _lexer.CurSymbol);
            symbol = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind == TokenKind.PyAs)
            {
                symbol2 = _lexer.CurSymbol;
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.Name)
                    throw new SyntaxError(_lexer.Position, "Expecting Name literal in import statement!", _lexer.CurSymbol);
                symbol3 = _lexer.CurSymbol;
                _lexer.Advance();
            }

            return new ImportAsNameStatement(startPos, _lexer.Position, symbol, symbol2, symbol3);
        }
        
        private StatementNode ParseDottedAsNameStmt()
        {
            var startPos = _lexer.Position;
            var left = ParseDottedName();
            if (_lexer.CurSymbol.Kind == TokenKind.PyAs)
            {
                var symbol = _lexer.CurSymbol;
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.Name)
                    throw new SyntaxError(_lexer.Position, "Expecting Name literal in import statement!", _lexer.CurSymbol);
                var right = _lexer.CurSymbol;
                _lexer.Advance();

                return new DottedAsNameStatement(startPos, _lexer.Position, left, symbol, right as NameToken);
            }

            return left;
        }
        
        private StatementNode ParseImportAsNamesStmt()
        {
            var startPos = _lexer.Position;
            var nodes = new List<StatementNode>();
            var separators = new List<Token>();
            nodes.Add( ParseImportAsNameStmt() );
            while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
            {
                separators.Add(_lexer.CurSymbol);
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind == TokenKind.PyRightParen || _lexer.CurSymbol.Kind == TokenKind.Newline ||
                    _lexer.CurSymbol.Kind == TokenKind.PySemiColon)
                {
                    continue;
                }
                nodes.Add( ParseImportAsNameStmt() );
            }

            return new ImportAsNamesStatement(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray());
        }
        
        private StatementNode ParseDottedAsNamesStmt()
        {
            var startPos = _lexer.Position;
            var nodes = new List<StatementNode>();
            var separators = new List<Token>();
            nodes.Add( ParseDottedAsNameStmt() );
            while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
            {
                separators.Add(_lexer.CurSymbol);
                _lexer.Advance();
                nodes.Add( ParseDottedAsNameStmt() );
            }

            if (nodes.Count == 1 && separators.Count == 0) return nodes[0];

            return new DottedAsNamesStatement(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray());
        }
        
        private StatementNode ParseDottedName()
        {
            var startPos = _lexer.Position;
            var nodes = new List<Token>();
            var dots = new List<Token>();
            if (_lexer.CurSymbol.Kind != TokenKind.Name)
                throw new SyntaxError(_lexer.Position, "Expecting Name literal in import statement!", _lexer.CurSymbol);
            nodes.Add(_lexer.CurSymbol);
            _lexer.Advance();
            while (_lexer.CurSymbol.Kind == TokenKind.Name)
            {
                dots.Add(_lexer.CurSymbol);
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.Name)
                    throw new SyntaxError(_lexer.Position, "Expecting Name literal in import statement!", _lexer.CurSymbol);
                nodes.Add(_lexer.CurSymbol);
                _lexer.Advance();
            }

            return new DottedNameStatement(startPos, _lexer.Position, nodes.ToArray(), dots.ToArray());
        }
        
        private StatementNode ParseGlobalStmt()
        {
            var startPos = _lexer.Position;
            var nodes = new List<Token>();
            var separators = new List<Token>();
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind != TokenKind.Name)
                throw new SyntaxError(_lexer.Position, "Expecting Name literal in global statement!", _lexer.CurSymbol);
            nodes.Add(_lexer.CurSymbol);
            _lexer.Advance();
            while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
            {
                separators.Add(_lexer.CurSymbol);
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.Name)
                    throw new SyntaxError(_lexer.Position, "Expecting Name literal in global statement!", _lexer.CurSymbol);
                nodes.Add(_lexer.CurSymbol);
                _lexer.Advance();
            }

            return new GlobalStatement(startPos, _lexer.Position, symbol, nodes.ToArray(), separators.ToArray());
        }
        
        private StatementNode ParseNonLocalStmt()
        {
            var startPos = _lexer.Position;
            var nodes = new List<Token>();
            var separators = new List<Token>();
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind != TokenKind.Name)
                throw new SyntaxError(_lexer.Position, "Expecting Name literal in nonlocal statement!", _lexer.CurSymbol);
            nodes.Add(_lexer.CurSymbol);
            _lexer.Advance();
            while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
            {
                separators.Add(_lexer.CurSymbol);
                _lexer.Advance();
                if (_lexer.CurSymbol.Kind != TokenKind.Name)
                    throw new SyntaxError(_lexer.Position, "Expecting Name literal in nonlocal statement!", _lexer.CurSymbol);
                nodes.Add(_lexer.CurSymbol);
                _lexer.Advance();
            }

            return new NonlocalStatement(startPos, _lexer.Position, symbol, nodes.ToArray(), separators.ToArray());
        }
        
        private StatementNode ParseAssertStmt()
        {
            var startPos = _lexer.Position;
            var symbol = _lexer.CurSymbol;
            _lexer.Advance();
            var left = ParseTest();
            if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
            {
                var symbol2 = _lexer.CurSymbol;
                _lexer.Advance();
                var right = ParseTest();

                return new AssertStatement(startPos, _lexer.Position, symbol, left, symbol2, right);
            }
            
            return new AssertStatement(startPos, _lexer.Position, symbol, left, null, null);
        }
        
        private TypeNode ParseFuncType()
        {
            var startPos = _lexer.Position;
            Token symbol1 = null, symbol2 = null, symbol3 = null;
            TypeNode left = null;
            if (_lexer.CurSymbol.Kind != TokenKind.PyLeftParen)
                throw new SyntaxError(_lexer.Position, "Expecting '(' in func type!", _lexer.CurSymbol);
            symbol1 = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen) left = ParseTypeList();
            if (_lexer.CurSymbol.Kind != TokenKind.PyRightParen)
                throw new SyntaxError(_lexer.Position, "Expecting ')' in func type!", _lexer.CurSymbol);
            symbol2 = _lexer.CurSymbol;
            _lexer.Advance();
            if (_lexer.CurSymbol.Kind != TokenKind.PyArrow)
                throw new SyntaxError(_lexer.Position, "Expecting '->' in func type!", _lexer.CurSymbol);
            symbol3 = _lexer.CurSymbol;
            _lexer.Advance();
            var right = ParseTest();

            return new FuncType(startPos, _lexer.Position, symbol1, left, symbol2, symbol3, right);
        }

        private TypeNode ParseTypeList()
        {
            var startPos = _lexer.Position;
            var nodes = new List<ExpressionNode>();
            var separators = new List<Token>();
            Token opMul = null, opPower = null;
            ExpressionNode mulNode = null, powerNode = null;

            if (_lexer.CurSymbol.Kind == TokenKind.PyMul)
            {
                opMul = _lexer.CurSymbol;
                _lexer.Advance();
                mulNode = ParseTest();
                while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                {
                    separators.Add(_lexer.CurSymbol);
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
                    {
                        opPower = _lexer.CurSymbol;
                        _lexer.Advance();
                        powerNode = ParseTest();
                        if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                            throw new SyntaxError(_lexer.Position, "Unexpected ',' after ** argument!", _lexer.CurSymbol);
                    }
                    else
                    {
                        nodes.Add( ParseTest() );
                    }
                }
            }
            else if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
            {
                opPower = _lexer.CurSymbol;
                _lexer.Advance();
                powerNode = ParseTest();
            }
            else
            {
                nodes.Add( ParseTest() );
                while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                {
                    separators.Add(_lexer.CurSymbol);
                    _lexer.Advance();
                    if (_lexer.CurSymbol.Kind == TokenKind.PyMul)
                    {
                        opMul = _lexer.CurSymbol;
                        _lexer.Advance();
                        mulNode = ParseTest();
                        while (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                        {
                            separators.Add(_lexer.CurSymbol);
                            _lexer.Advance();
                            if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
                            {
                                opPower = _lexer.CurSymbol;
                                _lexer.Advance();
                                powerNode = ParseTest();
                                if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                                    throw new SyntaxError(_lexer.Position, "Unexpected ',' after ** argument!", _lexer.CurSymbol);
                            }
                            else
                            {
                                nodes.Add( ParseTest() );
                            }
                        }
                    }
                    else if (_lexer.CurSymbol.Kind == TokenKind.PyPower)
                    {
                        opPower = _lexer.CurSymbol;
                        _lexer.Advance();
                        powerNode = ParseTest();
                        if (_lexer.CurSymbol.Kind == TokenKind.PyComma)
                            throw new SyntaxError(_lexer.Position, "Unexpected ',' after ** argument!", _lexer.CurSymbol);
                    }
                    else
                    {
                        nodes.Add( ParseTest() );
                    }
                }
            }

            return new TypeList(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray(), opMul, mulNode, opPower, powerNode);
        }

        public StatementNode ParseEvalInput()
        {
            var startPos = _lexer.Position;
            var right = ParseTestList();
            var newlines = new List<Token>();
            while (_lexer.CurSymbol.Kind == TokenKind.Newline)
            {
                newlines.Add( _lexer.CurSymbol );
                _lexer.Advance();
            }

            if (_lexer.CurSymbol.Kind != TokenKind.EndOfFile)
                throw new SyntaxError(_lexer.Position, "Expecting end of file!", _lexer.CurSymbol);

            return new EvalInputNode(startPos, _lexer.Position, right, newlines.ToArray(), _lexer.CurSymbol);
        }

        public StatementNode ParseFileInput()
        {
            var startPos = _lexer.Position;
            var nodes = new List<StatementNode>();
            var newlines = new List<Token>();

            while (_lexer.CurSymbol.Kind != TokenKind.EndOfFile)
            {
                if (_lexer.CurSymbol.Kind == TokenKind.Newline)
                {
                    newlines.Add(_lexer.CurSymbol);
                    _lexer.Advance();
                }
                else
                {
                    nodes.Add( ParseStmt() );
                }
            }

            return new FileInputNode(startPos, _lexer.Position, newlines.ToArray(), nodes.ToArray(), _lexer.CurSymbol);
        }

        public StatementNode ParseSingleInput()
        {
            var startPos = _lexer.Position;

            switch (_lexer.CurSymbol.Kind)
            {
                case TokenKind.PyIf:
                case TokenKind.PyWhile:
                case TokenKind.PyFor:
                case TokenKind.PyTry:
                case TokenKind.PyWith:
                case TokenKind.PyAsync:
                case TokenKind.PyDef:
                case TokenKind.PyClass:
                case TokenKind.PyMatrice:
                {
                    var right = ParseCompound();
                    if (_lexer.CurSymbol.Kind != TokenKind.Newline)
                        throw new SyntaxError(_lexer.Position, "Expecting NEWLINE after compound statement!",
                            _lexer.CurSymbol);

                    return new SingleInputNode(startPos, _lexer.Position, _lexer.CurSymbol, right);
                }
                case TokenKind.Newline:
                    return new SingleInputNode(startPos, _lexer.Position, _lexer.CurSymbol, null);
                default:
                {
                    var right = ParseSimpleStmt();
                    return new SingleInputNode(startPos, _lexer.Position, null, right);
                }
            }
        }

        public TypeNode ParseFuncTypeInput()
        {
            var startPos = _lexer.Position;
            var newlines = new List<Token>();
            var right = ParseFuncType();
            while (_lexer.CurSymbol.Kind == TokenKind.Newline)
            {
                newlines.Add(_lexer.CurSymbol);
                _lexer.Advance();
            }

            if (_lexer.CurSymbol.Kind != TokenKind.EndOfFile)
                throw new SyntaxError(_lexer.Position, "Expecting end of file!", _lexer.CurSymbol);

            return new TypeInput(startPos, _lexer.Position, right, newlines.ToArray(), _lexer.CurSymbol);
        }
        
#endregion

    }
}