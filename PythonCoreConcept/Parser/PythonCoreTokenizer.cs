
// PythonCore - Tokenizer for Python 3.9 grammar.
// Written by Richard Magnor Stenbro. (C) 2021 By Richard Magnor Stenbro
// Free to use for none commercial uses.

using System;
using System.Collections.Generic;

namespace PythonCoreConcept.Parser
{
    public class PythonCoreTokenizer
    {
        
        private Dictionary<string, TokenKind> _reservedKeywordTable = new Dictionary<string, TokenKind>()
        {
            { "False", TokenKind.PyFalse },
            { "None", TokenKind.PyNone },
            { "True", TokenKind.PyTrue },
            { "and", TokenKind.PyAnd },
            { "as", TokenKind.PyAs },
            { "assert", TokenKind.PyAssert },
            { "async", TokenKind.PyAsync },
            { "await", TokenKind.PyAwait },
            { "break", TokenKind.PyBreak },
            { "class", TokenKind.PyClass },
            { "continue", TokenKind.PyContinue },
            { "def", TokenKind.PyDef },
            { "del", TokenKind.PyDel },
            { "elif", TokenKind.PyElif },
            { "else", TokenKind.PyElse },
            { "except", TokenKind.PyExcept },
            { "finally", TokenKind.PyFinally },
            { "for", TokenKind.PyFor },
            { "from", TokenKind.PyFrom },
            { "global", TokenKind.PyGlobal },
            { "if", TokenKind.PyIf },
            { "import", TokenKind.PyImport },
            { "in", TokenKind.PyIn },
            { "is", TokenKind.PyIs },
            { "lambda", TokenKind.PyLambda },
            { "nonlocal", TokenKind.PyNonLocal },
            { "not", TokenKind.PyNot },
            { "or", TokenKind.PyOr },
            { "pass", TokenKind.PyPass },
            { "raise", TokenKind.PyRaise },
            { "return", TokenKind.PyReturn },
            { "try", TokenKind.PyTry },
            { "while", TokenKind.PyWhile },
            { "with", TokenKind.PyWith },
            { "yield", TokenKind.PyYield }
        };

        private char[] _sourceBuffer;
        private UInt32 _index;
        private bool _atBol;
        private Stack<TokenKind> _levelStack;

        public Token CurSymbol { get; private set; }
        public UInt32 Position { get; private set; }
        
        
        public PythonCoreTokenizer(char[] sourceCode)
        {
            Array.Resize(ref sourceCode, sourceCode.Length + 1);
            sourceCode[^1] = (char) 0x00;
            _sourceBuffer = sourceCode;
            Position = 0;
            _index = 0;
            CurSymbol = null;
            _atBol = true;
            _levelStack = new Stack<TokenKind>();
            
            this.Advance();
        }

        private bool IsHexDigit(char ch)
        {
            if (_sourceBuffer[_index] >= 'a' && _sourceBuffer[_index] <= 'f') return true;
            if (_sourceBuffer[_index] >= 'A' && _sourceBuffer[_index] <= 'F') return true;
            return Char.IsDigit(ch);
        }

        public void Advance()
        {
            bool isBlankline = false;
            
            
            
_again:
            Position = _index;
            
            /* Handle whitespace and other trivia */
            while (_sourceBuffer[_index] == ' ' || _sourceBuffer[_index] == '\v' || _sourceBuffer[_index] == '\t')
            {
                _index++;
            }

            Position = _index;
            
            /* Handle comment, unless it is a type comment */
            if (_sourceBuffer[_index] == '#')
            {
                while (_sourceBuffer[_index] != '\r' && _sourceBuffer[_index] != '\n' &&
                       _sourceBuffer[_index] != '\0') _index++;

                var sr = new string(_sourceBuffer[(int)Position .. (int)_index]);

                if (sr.StartsWith("#type: "))
                {
                    CurSymbol = new TypeCommentToken(Position, _index, new Trivia[] {}, sr);
                    return;
                }

                // TODO! Fix comment as trivia.
                goto _again;
            }
            
            /* Handle End of File */
            if (_sourceBuffer[_index] == '\0')
            {
                // TODO: Fix interactive mode first
                CurSymbol = new Token(Position, _index, TokenKind.EndOfFile, new Trivia[] {});
                return;
            }
            
            /* Handle Name literal or Reserved keywords */
            if (Char.IsLetter(_sourceBuffer[_index]) || _sourceBuffer[_index] == '_')
            {
                // TODO! Prefix to string testing here!
                
                Position = _index;
                while (Char.IsLetterOrDigit(_sourceBuffer[_index]) || _sourceBuffer[_index] == '_') _index++;
                var key = new string(_sourceBuffer[(int)Position .. (int)_index]);

                if (_reservedKeywordTable.ContainsKey(key))
                {
                    CurSymbol = new Token(Position, _index, _reservedKeywordTable[key], new Trivia[] {});
                    return;
                }
                else
                {
                    CurSymbol = new NameToken(Position, _index, new Trivia[] {}, key);
                    return;
                }
            }
            
            /* Handle newline */
            if (_sourceBuffer[_index] == '\r' || _sourceBuffer[_index] == '\n')
            {
                _atBol = true;
                if (_sourceBuffer[_index] == '\r') _index++;
                if (_sourceBuffer[_index] == '\n') _index++;
                
                // TODO! Check for trivia.
                CurSymbol = new Token(Position, _index, TokenKind.Newline, new Trivia[] {});
                return;
            }
            
            /* Handle Period or start of Number */
            if (_sourceBuffer[_index] == '.')
            {
                Position = _index;
                _index++;
                if (_sourceBuffer[_index] == '.')
                {
                    _index++;
                    if (_sourceBuffer[_index] == '.')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyElipsis, new Trivia[] {});
                        return;
                    }

                    _index--;
                }
                else if (!Char.IsDigit(_sourceBuffer[_index]))
                {
                    CurSymbol = new Token(Position, _index, TokenKind.PyDot, new Trivia[] {});
                    return;
                }

                _index = Position;
            }
            
            /* Handle Number */
            if (Char.IsDigit(_sourceBuffer[_index]) || _sourceBuffer[_index] == '.')
            {
                Position = _index;

                if (_sourceBuffer[_index] == '0')
                {
                    _index++;
                    if (_sourceBuffer[_index] == 'x' || _sourceBuffer[_index] == 'X')
                    {
                        _index++;
                        do
                        {
                            if (_sourceBuffer[_index] == '_') _index++;
                            if (!IsHexDigit(_sourceBuffer[_index]))
                                throw new LexicalError(_index, "Expecting hex digits!");
                            do
                            {
                                _index++;
                            } while (IsHexDigit(_sourceBuffer[_index]));

                        } while (_sourceBuffer[_index] == '_');

                    }
                    else if (_sourceBuffer[_index] == 'o' || _sourceBuffer[_index] == 'O')
                    {
                        _index++;
                        do
                        {
                            if (_sourceBuffer[_index] == '_') _index++;
                            if (_sourceBuffer[_index] > '7' || _sourceBuffer[_index] < '0')
                            {
                                if (Char.IsDigit(_sourceBuffer[_index]))
                                {
                                    throw new LexicalError(_index, "Expecting octet digit only!");
                                }
                                else
                                {
                                    throw new LexicalError(_index, "Expecting digits between '0' and '7' in octet Number!");
                                }
                            }

                            do
                            {
                                _index++;
                            } while (_sourceBuffer[_index] >= '0' && _sourceBuffer[_index] <= '7');
                            
                        } while (_sourceBuffer[_index] == '_');
                        
                        if (Char.IsDigit(_sourceBuffer[_index]))
                        {
                            throw new LexicalError(_index, "Expecting octet digit only!");
                        }

                    }
                    else if (_sourceBuffer[_index] == 'b' || _sourceBuffer[_index] == 'B')
                    {
                        _index++;
                        do
                        {
                            if (_sourceBuffer[_index] == '_') _index++;
                            if (_sourceBuffer[_index] != '0' && _sourceBuffer[_index] != '1')
                            {
                                if (Char.IsDigit(_sourceBuffer[_index]))
                                {
                                    throw new LexicalError(_index, "Expecting binary digit only!");
                                }
                                else
                                {
                                    throw new LexicalError(_index, "Expecting '0' or '1' in binary Number!");
                                }
                            }

                            do
                            {
                                _index++;
                            } while (_sourceBuffer[_index] == '0' || _sourceBuffer[_index] == '1');
                        } while (_sourceBuffer[_index] == '_');

                        if (Char.IsDigit(_sourceBuffer[_index]))
                        {
                            throw new LexicalError(_index, "Expecting binary digit only!");
                        }
                    }
                    else
                    {
                        var nonZero = false;
                        if (_sourceBuffer[_index] != '.')
                        {
                            while (true)
                            {
                                while (Char.IsDigit(_sourceBuffer[_index])) _index++;
                                if (_sourceBuffer[_index] != '_') break;
                                _index++;
                                if (!Char.IsDigit(_sourceBuffer[_index]))
                                    throw new LexicalError(_index, "Expecting digits after '_' in Number!");
                            }
                        }

                        if (_sourceBuffer[_index] == '.')
                        {
                            _index++;
                            while (true)
                            {
                                while (Char.IsDigit(_sourceBuffer[_index])) _index++;
                                if (_sourceBuffer[_index] != '_') break;
                                _index++;
                                if (!Char.IsDigit(_sourceBuffer[_index]))
                                    throw new LexicalError(_index, "Expecting digits after '_' in Number!");
                            }
                        }

                        if (_sourceBuffer[_index] == 'e' || _sourceBuffer[_index] == 'E')
                        {
                            _index++;
                            if (_sourceBuffer[_index] == '+' || _sourceBuffer[_index] == '-')
                            {
                                _index++;
                                if (!Char.IsDigit(_sourceBuffer[_index])) 
                                    throw new LexicalError(_index, "Expecting digit after '+' or '-' in Number!");
                            }
                            else if (!Char.IsDigit(_sourceBuffer[_index]))
                                throw new LexicalError(_index, "Expecting digit after 'e' in Number!");
                            while (true)
                            {
                                while (Char.IsDigit(_sourceBuffer[_index])) _index++;
                                if (_sourceBuffer[_index] != '_') break;
                                _index++;
                                if (!Char.IsDigit(_sourceBuffer[_index]))
                                    throw new LexicalError(_index, "Expecting digits after '_' in Number!");
                            }
                        }
                        
                        if (_sourceBuffer[_index] == 'j' || _sourceBuffer[_index] == 'J')
                        {
                            _index++;
                        }
                        else if (nonZero) throw new LexicalError(_index, "Unexpected digit found in Number!");
                    }
                }
                else // Decimal
                {
                    if (_sourceBuffer[_index] != '.')
                    {
                        while (true)
                        {
                            while (Char.IsDigit(_sourceBuffer[_index])) _index++;
                            if (_sourceBuffer[_index] != '_') break;
                            _index++;
                            if (!Char.IsDigit(_sourceBuffer[_index]))
                                throw new LexicalError(_index, "Expecting digits after '_' in Number!");
                        }
                    }

                    if (_sourceBuffer[_index] == '.')
                    {
                        _index++;
                        while (true)
                        {
                            while (Char.IsDigit(_sourceBuffer[_index])) _index++;
                            if (_sourceBuffer[_index] != '_') break;
                            _index++;
                            if (!Char.IsDigit(_sourceBuffer[_index]))
                                throw new LexicalError(_index, "Expecting digits after '_' in Number!");
                        }
                    }

                    if (_sourceBuffer[_index] == 'e' || _sourceBuffer[_index] == 'E')
                    {
                        _index++;
                        if (_sourceBuffer[_index] == '+' || _sourceBuffer[_index] == '-')
                        {
                            _index++;
                            if (!Char.IsDigit(_sourceBuffer[_index])) 
                                throw new LexicalError(_index, "Expecting digit after '+' or '-' in Number!");
                        }
                        else if (!Char.IsDigit(_sourceBuffer[_index]))
                            throw new LexicalError(_index, "Expecting digit after 'e' in Number!");
                        while (true)
                        {
                            while (Char.IsDigit(_sourceBuffer[_index])) _index++;
                            if (_sourceBuffer[_index] != '_') break;
                            _index++;
                            if (!Char.IsDigit(_sourceBuffer[_index]))
                                throw new LexicalError(_index, "Expecting digits after '_' in Number!");
                        }
                    }
                    
                    if (_sourceBuffer[_index] == 'j' || _sourceBuffer[_index] == 'J')
                    {
                        _index++;
                    }
                }
                
                var text = new string(_sourceBuffer[(int)Position .. (int)_index]);
                CurSymbol = new NumberToken(Position, _index, new Trivia[]{}, text);
                return;
            }
            
_letterQuote:
            /* Handle String */
            
            /* Handle Line continuation */
            
            /* Handle Operators or Delimiters */
            switch (_sourceBuffer[_index])
            {
                case '(':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyLeftParen, new Trivia[] { });
                    break;
                case '[':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyLeftBracket, new Trivia[] { });
                    break;
                case '{':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyLeftCurly, new Trivia[] { });
                    break;
                case ')':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyRightParen, new Trivia[] { });
                    break;
                case ']':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyRightBracket, new Trivia[] { });
                    break;
                case '}':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyRightCurly, new Trivia[] { });
                    break;
                case ';':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PySemiColon, new Trivia[] { });
                    break;
                case ',':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyComma, new Trivia[] { });
                    break;
                case '~':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyBitInvert, new Trivia[] { });
                    break;
                case '+':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyPlusAssign, new Trivia[] { });
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyPlus, new Trivia[] { });
                    }

                    break;
                case '-':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyMinusAssign, new Trivia[] { });
                    }
                    else if (_sourceBuffer[_index] == '>')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyArrow, new Trivia[] { });
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyMinus, new Trivia[] { });
                    }

                    break;
                case '*':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyMulAssign, new Trivia[] { });
                    }
                    else if (_sourceBuffer[_index] == '*')
                    {
                        _index++;
                        if (_sourceBuffer[_index] == '=')
                        {
                            _index++;
                            CurSymbol = new Token(Position, _index, TokenKind.PyPowerAssign, new Trivia[] { });
                        }
                        else
                        {
                            CurSymbol = new Token(Position, _index, TokenKind.PyPower, new Trivia[] { });
                        }
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyMul, new Trivia[] { });
                    }

                    break;
                case '/':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyDivAssign, new Trivia[] { });
                    }
                    else if (_sourceBuffer[_index] == '/')
                    {
                        _index++;
                        if (_sourceBuffer[_index] == '=')
                        {
                            _index++;
                            CurSymbol = new Token(Position, _index, TokenKind.PyFloorDivAssign, new Trivia[] { });
                        }
                        else
                        {
                            CurSymbol = new Token(Position, _index, TokenKind.PyFloorDiv, new Trivia[] { });
                        }
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyDiv, new Trivia[] { });
                    }

                    break;
                case '<':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyLessEqual, new Trivia[] { });
                    }
                    else if (_sourceBuffer[_index] == '>')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyNotEqual, new Trivia[] { });
                    }
                    else if (_sourceBuffer[_index] == '<')
                    {
                        _index++;
                        if (_sourceBuffer[_index] == '=')
                        {
                            _index++;
                            CurSymbol = new Token(Position, _index, TokenKind.PyShiftLeftAssign, new Trivia[] { });
                        }
                        else
                        {
                            CurSymbol = new Token(Position, _index, TokenKind.PyShiftLeft, new Trivia[] { });
                        }
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyLess, new Trivia[] { });
                    }

                    break;
                case '>':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyGreaterEqual, new Trivia[] { });
                    }
                    else if (_sourceBuffer[_index] == '>')
                    {
                        _index++;
                        if (_sourceBuffer[_index] == '=')
                        {
                            _index++;
                            CurSymbol = new Token(Position, _index, TokenKind.PyShiftRightAssign, new Trivia[] { });
                        }
                        else
                        {
                            CurSymbol = new Token(Position, _index, TokenKind.PyShiftRight, new Trivia[] { });
                        }
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyGreater, new Trivia[] { });
                    }

                    break;
                case '%':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyModuloAssign, new Trivia[] { });
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyModulo, new Trivia[] { });
                    }

                    break;
                case '@':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyMatriceAssign, new Trivia[] { });
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyMatrice, new Trivia[] { });
                    }

                    break;
                case '&':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyBitAndAssign, new Trivia[] { });
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyBitAnd, new Trivia[] { });
                    }

                    break;
                case '|':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyBitOrAssign, new Trivia[] { });
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyBitOr, new Trivia[] { });
                    }

                    break;
                case '^':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyBitXorAssign, new Trivia[] { });
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyBitXor, new Trivia[] { });
                    }

                    break;
                case ':':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyColonAssign, new Trivia[] { });
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyColon, new Trivia[] { });
                    }

                    break;
                case '=':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyEqual, new Trivia[] { });
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyAssign, new Trivia[] { });
                    }
                    break;
                case '!':
                    _index++;
                    if (_sourceBuffer[_index] != '=')
                        throw new LexicalError(_index, "Expecting '!=' but found only '!'");
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyNotEqual, new Trivia[] { });
                    break;
                default:
                    throw new LexicalError(_index, $"Found '{_sourceBuffer[_index]}' in source code!");
            }
              
            /* Finally we check for matching parenthezis if any */
            if (CurSymbol.Kind == TokenKind.PyLeftParen || CurSymbol.Kind == TokenKind.PyLeftBracket ||
                CurSymbol.Kind == TokenKind.PyLeftCurly)
            {
                _levelStack.Push(CurSymbol.Kind);
            }
            else if (CurSymbol.Kind == TokenKind.PyRightParen)
            {
                if (_levelStack.Peek() == TokenKind.PyLeftParen) _levelStack.Pop();
                else
                {
                    throw new LexicalError(_index, "Inconsistent ')' parenthesis matching!");
                }
            }
            else if (CurSymbol.Kind == TokenKind.PyRightBracket)
            {
                if (_levelStack.Peek() == TokenKind.PyLeftBracket) _levelStack.Pop();
                else
                {
                    throw new LexicalError(_index, "Inconsistent ']' parenthesis matching!");
                }
            }
            else if (CurSymbol.Kind == TokenKind.PyRightCurly)
            {
                if (_levelStack.Peek() == TokenKind.PyLeftCurly) _levelStack.Pop();
                else
                {
                    throw new LexicalError(_index, "Inconsistent '}' parenthesis matching!");
                }
            }
        }
    }
}
