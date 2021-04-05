using System;
using PythonCoreConcept.Parser;
using PythonCoreConcept.Parser.AST;

namespace PythonCoreConcept
{
    public class PythonFrontEnd
    {
        public static Node ParseEvalInput(string SourceCode, UInt32 TabSize = 8u)
        {
            var lex = new PythonCoreTokenizer(SourceCode.ToCharArray());
            lex.TabSize = TabSize;
            var parser = new PythonCoreParser(lex);
            return parser.ParseEvalInput();
        }
        
        public static Node ParseFuncTypeInput(string SourceCode, UInt32 TabSize = 8u)
        {
          var lex = new PythonCoreTokenizer(SourceCode.ToCharArray());
          lex.TabSize = TabSize;
          var parser = new PythonCoreParser(lex);
          return parser.ParseFuncTypeInput();
        }
        
        public static Node ParseFileInput(string SourceCode, UInt32 TabSize = 8u)
        {
          var lex = new PythonCoreTokenizer(SourceCode.ToCharArray());
          lex.TabSize = TabSize;
          var parser = new PythonCoreParser(lex);
          return parser.ParseFileInput();
        }

        public static Node ParseSingleInput(string SourceCode, UInt32 TabSize = 8u)
        {
            var lex = new PythonCoreTokenizer(SourceCode.ToCharArray());
            lex.TabSize = TabSize;
            var parser = new PythonCoreParser(lex);
            return parser.ParseSingleInput();
        }
    }
}