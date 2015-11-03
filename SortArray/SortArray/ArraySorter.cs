using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortArray
{
    public class ArraySorter
    {
        public static void Sort(int[][] arr, IComparer<int[]> comparer)
        {
            if (arr == null)
            {
                throw new ArgumentNullException();
            }
            for (int i = 0; i<arr.Count(); i++)
            {
                for (int j = i + 1; j<arr.Count(); j++)
                {
                    if (comparer.Compare(arr[i],arr[j])>0)
                    {
                        int[] swapArr = arr[i];
                        arr[i] = arr[j];
                        arr[j] = swapArr;
                    }
                }
            }
        }

        public static void Sort(int[][] arr, Func<int[], int[], int> predicate)
        {
            for (int i = 0; i<arr.Count(); i++)
            {
                for (int j = i+1; j<arr.Count(); j++)
                {
                    switch(Math.Sign(predicate(arr[i], arr[j])))
                    {
                        case -1:
                            break;
                        case 0:
                            break;
                        case 1:
                            int[] swapArray = arr[i];
                            arr[i] = arr[j];
                            arr[j] = swapArray;
                            break;
                    }
                }
            }
        }
    }
}
