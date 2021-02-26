
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



        private ExpressionNode ParseTrailer()
        {
            throw new NotImplementedException();
        }
        
        
#endregion

    }
}