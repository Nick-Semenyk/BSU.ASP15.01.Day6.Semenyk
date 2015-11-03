using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static SortArray.ArraySorter;

namespace SortArray.Tests
{
    [TestFixture]
    public class ArraySorterTest
    {
        public class ComparerBySum : IComparer<int[]>
        {
            public int Compare(int[] x, int[] y)
            {
                int sum1 = 0;
                int sum2 = 0;
                if (x == null)
                {
                    sum1 = int.MaxValue;
                }
                else
                {
                    if (y == null)
                    {
                        sum2 = int.MaxValue;
                    }
                    else
                    {
                        for (int i = 0; i < x.Count(); i++)
                        {
                            sum1 += x[i];
                        }
                        for (int i = 0; i < y.Count(); i++)
                        {
                            sum2 += y[i];
                        }
                    }
                }
                if (sum1 > sum2)
                    return 1;
                if (sum1 == sum2)
                    return 0;
                if (sum1 < sum2)
                    return -1;
                return 0;
            }
        }

        public class ComparerByMax : IComparer<int[]>
        {
            public int Compare(int[] x, int[] y)
            {
                int max1 = 0;
                int max2 = 0;
                if (x == null)
                {
                    max1 = int.MaxValue;
                }
                else
                {
                    if (y == null)
                    {
                        max2 = int.MaxValue;
                    }
                    else
                    {
                        for (int i = 0; i < x.Count(); i++)
                        {
                            if (max1 < Math.Abs(x[i]))
                                max1 = Math.Abs(x[i]);
                        }
                        for (int i = 0; i < y.Count(); i++)
                        {
                            if (max2 < Math.Abs(y[i]))
                                max2 = Math.Abs(y[i]);
                        }
                    }
                }
                if (max1 > max2)
                    return 1;
                if (max1 == max2)
                    return 0;
                if (max1 < max2)
                    return -1;
                return 0;
            }
        }


        [Test]
        public void SortTests()
        {
            int[][] testArray = new int[8][] {
                null,
                new int[] {1,6,4,0},
                new int[] {9,9,100,9, -100},
                null,
                null,
                new int[] {-5,9,-20,7,0,0,0,0},
                new int[] {89,38,19,-8},
                new int[] {-91}};
            int[][] result = new int[8][] {
                new int[] {-91},
                new int[] {-5,9,-20,7,0,0,0,0},
                new int[] {1,6,4,0},
                new int[] {9,9,100,9, -100},
                new int[] {89,38,19,-8},
                null,
                null,
                null
                };
            SortArray.ArraySorter.Sort(testArray, new ComparerBySum());
            Assert.AreEqual(result, testArray);
        }

        [Test]
        public void SortByAbsTests()
        {
            int[][] testArray = new int[8][] {
                null,
                new int[] {1,6,4,0},
                new int[] {9,9,100,9, -100},
                null,
                null,
                new int[] {-5,9,-20,7,0,0,0,0},
                new int[] {89,38,19,-8},
                new int[] {-91}};
            int[][] result = new int[8][] {
                new int[] {1,6,4,0},
                new int[] {-5,9,-20,7,0,0,0,0},
                new int[] {89,38,19,-8},
                new int[] {-91},
                new int[] {9,9,100,9, -100},
                null,
                null,
                null
                };
            SortArray.ArraySorter.Sort(testArray, new ComparerByMax());
            Assert.AreEqual(result, testArray);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SortNullTests()
        {
            int[][] testArray = null;
            int[][] result = null;
            SortArray.ArraySorter.Sort(testArray, new ComparerByMax());
            Assert.AreEqual(result, testArray);
        }
    }
}
