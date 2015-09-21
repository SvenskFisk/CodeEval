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
        [InlineData("02:26:31 14:44:45 09:53:27", "14:44:45 09:53:27 02:26:31")]
        [InlineData("05:33:44 21:25:41", "21:25:41 05:33:44")]
        public void SortDates(string input, string expected)
        {
            var t = new Solver();
            var result = t.SortDates(input);

            Assert.Equal(expected, result);
        }
    }
}
