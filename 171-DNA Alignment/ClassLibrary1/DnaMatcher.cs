using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{

    /// <summary>
    /// Based on "FastLSA – A Fast Linear-Space Algorithm for Sequence Alignment".
    /// </summary>
    public class DnaMatcher
    {
        const int GapStart = -8;

        const int Gap = -1;

        const int Match = 3;

        const int Mismatch = -3;

        struct Entry
        {
            public Entry(int maxScore, int vScore, int hScore)
            {
                this.maxScore = maxScore;
                this.hScore = hScore;
                this.vScore = vScore;
            }

            public int hScore;

            public int vScore;

            public int maxScore;

        }

        internal int Score(string input)
        {
            var split = input.IndexOf(" | ");
            var a = input.Substring(0, split);
            var b = input.Substring(split + 3);

            if (a.Length < b.Length)
            {
                var t = a;
                a = b;
                b = t;
            }

            var al = a.Length;
            var bl = b.Length;

            var matrix = new Entry[al + 1, bl + 1];

            // add gap edges
            for (int ai = 1; ai <= al; ai++)
            {
                var baseCost = GapStart + (ai - 1) * Gap;
                matrix[ai, 0] = new Entry(baseCost, baseCost, baseCost + GapStart);
            }

            for (int bi = 1; bi <= bl; bi++)
            {
                var baseCost = GapStart + (bi - 1) * Gap;
                matrix[0, bi] = new Entry(baseCost, baseCost + GapStart, baseCost);
            }

            matrix[0, 0] = new Entry(0, -8, -8);

            // build main area
            for (int ai = 0; ai < al; ai++)
            {
                for (int bi = 0; bi < bl; bi++)
                {
                    var matchScore = a[ai] == b[bi] ? Match : Mismatch;

                    // check diagonal
                    var dScore = matrix[ai, bi].maxScore + matchScore;

                    // check vertical
                    var above = matrix[ai, bi + 1];
                    var vScore = Max(
                        above.hScore + GapStart,
                        above.vScore + Gap,
                        above.maxScore + GapStart);

                    // check horisontal
                    var left = matrix[ai + 1, bi];
                    var hScore = Max(
                        left.hScore + Gap,
                        left.vScore + GapStart,
                        left.maxScore + GapStart);

                    // create entry
                    var maxScore = Max(dScore, vScore, hScore);
                    matrix[ai + 1, bi + 1] = new Entry(maxScore, vScore, hScore);
                }
            }

            //printMatrix(matrix);


            return matrix[al, bl].maxScore;


            // Optimeringar:
            // Nummertyper (max 400 tecken).
            // Ha bara föreg rad och nuvarande i minnet.
        }

        //static void printMatrix(Entry[,] matrix)
        //{
        //    var sb = new StringBuilder();

        //    for (int y = 0; y <= matrix.GetUpperBound(0); y++)
        //    {
        //        sb.Clear();
        //        for (int x = 0; x <= matrix.GetUpperBound(1); x++)
        //        {
        //            sb.AppendFormat("{0:000} ", matrix[y, x].maxScore);
        //        }

        //        Debug.WriteLine(sb.ToString());
        //    }
        //}

        static int Max(int a, int b, int c)
        {
            return Math.Max(Math.Max(a, b), c);
        }
    }
}
