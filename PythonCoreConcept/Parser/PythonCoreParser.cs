
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
        private UInt16 _tabSize;

        public PythonCoreParser(PythonCoreTokenizer tokenizer, UInt16 tabSize)
        {
            _lexer = tokenizer;
            _tabSize = tabSize;
            
            _lexer.Advance();
        }





#region Expression rules

        public ExpressionNode ParseAtom()
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
        
        
#endregion
    }
}