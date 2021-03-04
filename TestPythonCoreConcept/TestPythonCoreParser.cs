
using System.Linq;
using Xunit;
using PythonCoreConcept.Parser;

namespace TestPythonCoreConcept
{
    public class TestPythonCoreParser
    {
        public class TestPythonCoreTokenizer
        {
            [Fact]
            public void TestAtomFalse()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("False".ToArray()));
                var node = parser.ParseEvalInput();
            }

        }
    }
}