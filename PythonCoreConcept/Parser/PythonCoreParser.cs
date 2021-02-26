
// PythonCore - This is the Recursive descend parser for Python 3.9 grammar.
// Written by Richard Magnor Stenbro. (C) 2021 By Richard Magnor Stenbro
// Free to use for none commercial uses.

using System;
using System.Collections.Generic;
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



        private ExpressionNode ParseTrailer()
        {
            throw new NotImplementedException();
        }
        
        
#endregion
    }
}