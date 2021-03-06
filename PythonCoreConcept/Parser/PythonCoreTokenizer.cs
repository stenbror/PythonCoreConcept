
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
        private Stack<UInt32> _indentStack;
        private Int32 _pending;
        private bool _isInteractiveMode;

        public Token CurSymbol { get; private set; }
        public UInt32 Position { get; private set; }
        public UInt32 TabSize { get; set; }
        
        
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
            _indentStack = new Stack<UInt32>();
            _indentStack.Push(0);
            _pending = 0;
            _isInteractiveMode = false;
            
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

            var triviaList = new List<Trivia>();
          
_nextLine:
            isBlankline = false;

            if (_atBol)
            {
                _atBol = false;
                uint col = 0;
                var triviaStart = _index;
                while (_sourceBuffer[_index] == ' ' || _sourceBuffer[_index] == '\v' || _sourceBuffer[_index] == '\t')
                {
                    if (_sourceBuffer[_index] == ' ') col++;
                    else if (_sourceBuffer[_index] == '\t') col = (col / TabSize + 1) * TabSize;
                    else col = 0;
                    _index++;
                }

                if (_index != triviaStart)
                {
                    var trivia = new Trivia(triviaStart, _index, TriviaKind.Whitespace, new string(_sourceBuffer[(int)triviaStart .. (int)_index]));
                    triviaList.Add(trivia);
                }
                
                if (_sourceBuffer[_index] == '#' || _sourceBuffer[_index] == '\r' || _sourceBuffer[_index] == '\n' ||
                    _sourceBuffer[_index] == '\\')
                {
                    if (col == 0 && (_sourceBuffer[_index] == '\r' || _sourceBuffer[_index] == '\n') && _isInteractiveMode)
                      isBlankline = false;
                    else if (_isInteractiveMode) // lineno = 1 in interactive mode
                    {
                      col = 0;
                      isBlankline = false;
                    }
                    else isBlankline = true;
                }

                if (!isBlankline && _levelStack.Count == 0)
                {
                    if (col == _indentStack.Peek())
                    {
                        // No change
                    }
                    else if (col > _indentStack.Peek())
                    {
                        _indentStack.Push(col);
                        _pending++;
                    }
                    else
                    {
                        while (_indentStack.Count > 0 && col < _indentStack.Peek())
                        {
                          _pending--;
                          _indentStack.Pop();
                        }

                        if (col != _indentStack.Peek()) throw new LexicalError(_index, "Inconsistant indentation level!");
                    }
                }
            }

            Position = _index;

            if (_pending != 0)
            {
                if (_pending < 0)
                {
                    _pending++;
                    CurSymbol = new Token(Position, _index, TokenKind.Dedent, triviaList.ToArray());
                }
                else
                {
                    _pending--;
                    CurSymbol = new Token(Position, _index, TokenKind.Indent, triviaList.ToArray());
                }

                return;
            }
            
_again:
            Position = _index;
            
            /* Handle whitespace and other trivia */
            while (_sourceBuffer[_index] == ' ' || _sourceBuffer[_index] == '\v' || _sourceBuffer[_index] == '\t')
            {
                _index++;
            }
            
            if (_index != Position)
            {
              var trivia = new Trivia(Position, _index, TriviaKind.Whitespace, new string(_sourceBuffer[(int)Position .. (int)_index]));
              triviaList.Add(trivia);
            }

            Position = _index;
            
            /* Handle comment, unless it is a type comment */
            if (_sourceBuffer[_index] == '#')
            {
                while (_sourceBuffer[_index] != '\r' && _sourceBuffer[_index] != '\n' &&
                       _sourceBuffer[_index] != '\0') _index++;

                var sr = new string(_sourceBuffer[(int)Position .. (int)_index]);

                if (sr.StartsWith("# type: "))
                {
                    CurSymbol = new TypeCommentToken(Position, _index,  triviaList.ToArray(), sr);
                    return;
                }

                if (_index != Position)
                {
                  var trivia = new Trivia(Position, _index, TriviaKind.Comment, new string(_sourceBuffer[(int)Position .. (int)_index]));
                  triviaList.Add(trivia);
                }
                
                goto _again;
            }
            
            /* Handle End of File */
            if (_sourceBuffer[_index] == '\0')
            {
                // TODO: Fix interactive mode first
                CurSymbol = new Token(Position, _index, TokenKind.EndOfFile, triviaList.ToArray());
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
                    CurSymbol = new Token(Position, _index, _reservedKeywordTable[key], triviaList.ToArray());
                    return;
                }
                if (_sourceBuffer[_index] == '\'' || _sourceBuffer[_index] == '"')
                {
                    // Check for valid prefix, or exception  TODO!
                    goto _letterQuote;
                }
                
                CurSymbol = new NameToken(Position, _index, triviaList.ToArray(), key);
                return;
            }
            
            /* Handle newline */
            if (_sourceBuffer[_index] == '\r' || _sourceBuffer[_index] == '\n')
            {
                _atBol = true;
                var triviaStart = _index;
                if (_sourceBuffer[_index] == '\r') _index++;
                if (_sourceBuffer[_index] == '\n') _index++;

                if (_sourceBuffer[_index] != '\0' &&  (isBlankline || _levelStack.Count > 0))
                {
                  
                    if (_index != triviaStart)
                    {
                      var trivia = new Trivia(triviaStart, _index, TriviaKind.Newline, new string(_sourceBuffer[(int)triviaStart .. (int)_index]));
                      triviaList.Add(trivia);
                    }
                  
                    goto _nextLine;
                }
                
                CurSymbol = new Token(Position, _index, TokenKind.Newline, triviaList.ToArray());
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
                        CurSymbol = new Token(Position, _index, TokenKind.PyElipsis, triviaList.ToArray());
                        return;
                    }

                    _index--;
                }
                else if (!Char.IsDigit(_sourceBuffer[_index]))
                {
                    CurSymbol = new Token(Position, _index, TokenKind.PyDot, triviaList.ToArray());
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
                CurSymbol = new NumberToken(Position, _index, triviaList.ToArray(), text);
                return;
            }
            
_letterQuote:
            /* Handle String */
            if (_sourceBuffer[_index] == '\'' || _sourceBuffer[_index] == '\"')
            {
                var quote = _sourceBuffer[_index++];
                var quoteSize = 1;
                var quoteEndsize = 0;

                if (_sourceBuffer[_index] == quote)
                {
                    _index++;
                    if (_sourceBuffer[_index] == quote) quoteSize = 3;
                    else quoteEndsize = 1;
                }
                else _index--;

                while (quoteEndsize != quoteSize)
                {
                    _index++;
                    if (_sourceBuffer[_index] == '\0')
                    {
                        
                    }

                    if (quoteSize == 1 && (_sourceBuffer[_index] == '\r' || _sourceBuffer[_index] == '\n'))
                        throw new LexicalError(_index, "Newline inside single quote string!");
                    if (quote == _sourceBuffer[_index])
                    {
                        quoteEndsize++;
                        if (quoteEndsize == quoteSize) _index++;
                    }
                    else
                    {
                        quoteEndsize = 0;
                        if (_sourceBuffer[_index] == '\\')
                        {
                            _index++;
                            
                            // Fix!
                        }
                    }
                }

                var text = new string(_sourceBuffer[(int)Position .. (int)_index]);
                CurSymbol = new StringToken(Position, _index, triviaList.ToArray(), text);
                return;
            }
            
            /* Handle Line continuation */
            if (_sourceBuffer[_index] == '\\')
            {
                _index++;
                triviaList.Add(new Trivia(_index - 1, _index, TriviaKind.LineContinuation, "\\"));

                var triviaStart = _index;
                
                if (_sourceBuffer[_index] == '\r' || _sourceBuffer[_index] == '\n')
                {
                    if (_sourceBuffer[_index] == '\r')
                    {
                        _index++;
                    }

                    if (_sourceBuffer[_index] == '\n')
                    {
                      _index++;
                    }
                    
                    if (_index != triviaStart)
                    {
                        var trivia = new Trivia(triviaStart, _index, TriviaKind.Newline, new string(_sourceBuffer[(int)triviaStart .. (int)_index]));
                        triviaList.Add(trivia);
                    }
                    
                    goto _again;
                }

                throw new LexicalError(_index, "Line continuation must be followed by newline!");
            }
            
            /* Handle Operators or Delimiters */
            switch (_sourceBuffer[_index])
            {
                case '(':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyLeftParen, triviaList.ToArray());
                    break;
                case '[':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyLeftBracket, triviaList.ToArray());
                    break;
                case '{':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyLeftCurly, triviaList.ToArray());
                    break;
                case ')':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyRightParen, triviaList.ToArray());
                    break;
                case ']':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyRightBracket, triviaList.ToArray());
                    break;
                case '}':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyRightCurly, triviaList.ToArray());
                    break;
                case ';':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PySemiColon, triviaList.ToArray());
                    break;
                case ',':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyComma, triviaList.ToArray());
                    break;
                case '~':
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyBitInvert, triviaList.ToArray());
                    break;
                case '+':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyPlusAssign, triviaList.ToArray());
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyPlus, triviaList.ToArray());
                    }

                    break;
                case '-':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyMinusAssign, triviaList.ToArray());
                    }
                    else if (_sourceBuffer[_index] == '>')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyArrow, triviaList.ToArray());
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyMinus, triviaList.ToArray());
                    }

                    break;
                case '*':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyMulAssign, triviaList.ToArray());
                    }
                    else if (_sourceBuffer[_index] == '*')
                    {
                        _index++;
                        if (_sourceBuffer[_index] == '=')
                        {
                            _index++;
                            CurSymbol = new Token(Position, _index, TokenKind.PyPowerAssign, triviaList.ToArray());
                        }
                        else
                        {
                            CurSymbol = new Token(Position, _index, TokenKind.PyPower, triviaList.ToArray());
                        }
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyMul, triviaList.ToArray());
                    }

                    break;
                case '/':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyDivAssign, triviaList.ToArray());
                    }
                    else if (_sourceBuffer[_index] == '/')
                    {
                        _index++;
                        if (_sourceBuffer[_index] == '=')
                        {
                            _index++;
                            CurSymbol = new Token(Position, _index, TokenKind.PyFloorDivAssign, triviaList.ToArray());
                        }
                        else
                        {
                            CurSymbol = new Token(Position, _index, TokenKind.PyFloorDiv, triviaList.ToArray());
                        }
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyDiv, triviaList.ToArray());
                    }

                    break;
                case '<':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyLessEqual, triviaList.ToArray());
                    }
                    else if (_sourceBuffer[_index] == '>')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyNotEqual, triviaList.ToArray());
                    }
                    else if (_sourceBuffer[_index] == '<')
                    {
                        _index++;
                        if (_sourceBuffer[_index] == '=')
                        {
                            _index++;
                            CurSymbol = new Token(Position, _index, TokenKind.PyShiftLeftAssign, triviaList.ToArray());
                        }
                        else
                        {
                            CurSymbol = new Token(Position, _index, TokenKind.PyShiftLeft, triviaList.ToArray());
                        }
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyLess, triviaList.ToArray());
                    }

                    break;
                case '>':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyGreaterEqual, triviaList.ToArray());
                    }
                    else if (_sourceBuffer[_index] == '>')
                    {
                        _index++;
                        if (_sourceBuffer[_index] == '=')
                        {
                            _index++;
                            CurSymbol = new Token(Position, _index, TokenKind.PyShiftRightAssign, triviaList.ToArray());
                        }
                        else
                        {
                            CurSymbol = new Token(Position, _index, TokenKind.PyShiftRight, triviaList.ToArray());
                        }
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyGreater, triviaList.ToArray());
                    }

                    break;
                case '%':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyModuloAssign, triviaList.ToArray());
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyModulo, triviaList.ToArray());
                    }

                    break;
                case '@':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyMatriceAssign, triviaList.ToArray());
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyMatrice, triviaList.ToArray());
                    }

                    break;
                case '&':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyBitAndAssign, triviaList.ToArray());
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyBitAnd, triviaList.ToArray());
                    }

                    break;
                case '|':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyBitOrAssign, triviaList.ToArray());
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyBitOr, triviaList.ToArray());
                    }

                    break;
                case '^':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyBitXorAssign, triviaList.ToArray());
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyBitXor, triviaList.ToArray());
                    }

                    break;
                case ':':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyColonAssign, triviaList.ToArray());
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyColon, triviaList.ToArray());
                    }

                    break;
                case '=':
                    _index++;
                    if (_sourceBuffer[_index] == '=')
                    {
                        _index++;
                        CurSymbol = new Token(Position, _index, TokenKind.PyEqual, triviaList.ToArray());
                    }
                    else
                    {
                        CurSymbol = new Token(Position, _index, TokenKind.PyAssign, triviaList.ToArray());
                    }
                    break;
                case '!':
                    _index++;
                    if (_sourceBuffer[_index] != '=')
                        throw new LexicalError(_index, "Expecting '!=' but found only '!'");
                    _index++;
                    CurSymbol = new Token(Position, _index, TokenKind.PyNotEqual, triviaList.ToArray());
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
