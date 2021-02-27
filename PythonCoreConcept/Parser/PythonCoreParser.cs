
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
            
            _lexer.Advance();
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
                case TokenKind.PyLeftBracket:
                case TokenKind.PyLeftCurly:
                    throw new NotImplementedException();
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
            if (_lexer.CurSymbol.Kind == TokenKind.PyFor)
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
                ExpressionNode right = null;
                if (_lexer.CurSymbol.Kind != TokenKind.PyRightBracket)
                {
                    right = ParseSubscriptList();
                }

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
                if (_lexer.CurSymbol.Kind != TokenKind.PyColon && _lexer.CurSymbol.Kind != TokenKind.PyRightBracket)
                {
                    second = ParseTest();
                    if (_lexer.CurSymbol.Kind == TokenKind.PyColon)
                    {
                        two = _lexer.CurSymbol;
                        if (_lexer.CurSymbol.Kind != TokenKind.PyRightBracket) third = ParseTest();
                    }
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

            return new TestList(startPos, _lexer.Position, nodes.ToArray(), separators.ToArray());
        }

        private ExpressionNode ParseDictorSetMaker()
        {
            throw new NotImplementedException();
        }
        
        private ExpressionNode ParseArgList()
        {
            throw new NotImplementedException();
        }

        private ExpressionNode ParseArgumenter()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        
        private ExpressionNode ParseCompFor()
        {
            throw new NotImplementedException();
        }

        private ExpressionNode ParseCompIf()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        
        
#endregion


#region Statement rules

        private StatementNode ParseTestListStarExpr()
        {
            throw new NotImplementedException();
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

#endregion

    }
}