using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Solver
    {
        public string SortDates(string datesString)
        {
            var sb = new StringBuilder(datesString.Length);
            var first = true;

            foreach (var s in datesString.Split(' ')
                .OrderByDescending(x => x))
            {
                if (!first)
                {
                    sb.Append(' ');
                }

                sb.Append(s);
                first = false;
            }

            return sb.ToString();
        }
    }
}
