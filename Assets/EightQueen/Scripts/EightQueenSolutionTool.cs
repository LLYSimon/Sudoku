//******************************************
// FileName: EightQueenSolutionTool.cs
// Mail: 877121737@qq.com
// Author: Simon
// Version:#Version#
// Description:
//******************************************

using System.Collections.Generic;
using UnityEngine;

namespace EightQueen
{
    public static class EightQueenSolutionTool
    {
        public static List<int[]> GetEightQueenSolution(int iCount)
        {
            var kQueenList = new List<int>();
            var kSolutionList = new List<int[]>();

            for (int i = 0; i < iCount; i++)
            {
                kQueenList.Add(0);
            }
            _putQueen(iCount, kQueenList, 0, kSolutionList);
            return kSolutionList;
        }

        private static void _putQueen(int iQueenCount, List<int> kQueenList, int iNextY, List<int[]> kSolutionList)
        {
            for (kQueenList[iNextY] = 0; kQueenList[iNextY] < iQueenCount; kQueenList[iNextY]++)
            {
                if (!_checkConflict(kQueenList, iNextY))
                {
                    if (iNextY + 1 < iQueenCount)
                    {
                        _putQueen(iQueenCount, kQueenList, iNextY + 1, kSolutionList);
                    }
                    else
                    {
                        kSolutionList.Add(kQueenList.ToArray());
                    }
                }
            }
        }

        private static bool _checkConflict(List<int> kQueenList, int iNextY)
        {
            for (int i = 0; i < iNextY; i++)
            {
                if (Mathf.Abs(kQueenList[i] - kQueenList[iNextY]) == Mathf.Abs(i - iNextY) || kQueenList[i] == kQueenList[iNextY])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
