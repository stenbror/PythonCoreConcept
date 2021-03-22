
using System;
using Xunit;
using PythonCoreConcept.Parser;
using PythonCoreConcept.Parser.AST;

namespace TestPythonCoreConcept
{
    public class TestPythonCoreErrors
    {
        [Fact]
        public void TestAtomError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer(".\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
            }
            catch (SyntaxError e)
            {
                Assert.Equal(0u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
            }
            catch 
            {
                Assert.True(false);
            }
        }
    }
}