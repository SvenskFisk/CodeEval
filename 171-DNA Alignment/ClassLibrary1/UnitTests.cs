using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClassLibrary1
{
    public class UnitTests
    {
        [Theory]
        [InlineData("GAAAAAAT | GAAT", 1)]
        [InlineData("GCATGCT | GATTACA", -3)]
        public void Score(string input, int expected)
        {
            var t = new DnaMatcher();
            var result = t.Score(input);

            Assert.Equal(expected, result);
        }
    }
}
