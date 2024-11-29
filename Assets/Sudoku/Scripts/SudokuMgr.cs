//******************************************
// FileName: SudokuUI.cs
// Mail: 877121737@qq.com
// Author: Simon
// Version:#Version#
// Description:
//******************************************

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sudoku
{
    public class SudokuMgr
    {
        private static SudokuMgr s_kInstance;
        public static SudokuMgr Instance
        {
            get
            {
                if (s_kInstance == null)
                {
                    s_kInstance = new SudokuMgr();
                }
                return s_kInstance;
            }
        }

        public int[,] m_kGridNumArray = new int[GameManager.GRID_LENGTH, GameManager.GRID_LENGTH];
        public int[,] m_kPuzzleNumArray = new int[GameManager.GRID_LENGTH, GameManager.GRID_LENGTH];
        public int[,] m_kPuzzleBakNumArray = new int[GameManager.GRID_LENGTH, GameManager.GRID_LENGTH];

        private int m_iDifficulty;

        public void CreatePuzzle()
        {
            Array.Copy(this.m_kGridNumArray, this.m_kPuzzleNumArray, this.m_kGridNumArray.Length);

            for (int i = 0; i < this.m_iDifficulty; i++)
            {
                int iRow = Random.Range(0, 9);
                int iCol = Random.Range(0, 9);

                while (this.m_kPuzzleNumArray[iRow, iCol] == 0)
                {
                    iRow = Random.Range(0, 9);
                    iCol = Random.Range(0, 9);
                }

                this.m_kPuzzleNumArray[iRow, iCol] = 0;
            }

            List<int> kOnBoardList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            this._randomizeList(kOnBoardList);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int k = 0; k < kOnBoardList.Count; k++)
                    {
                        if (kOnBoardList[k] == this.m_kPuzzleNumArray[i, j])
                        {
                            kOnBoardList.Remove(k);
                        }
                    }
                }
            }

            while (kOnBoardList.Count - 1 > 1)
            {
                int iRow = Random.Range(0, 9);
                int iCol = Random.Range(0, 9);

                if (this.m_kGridNumArray[iRow, iCol] == kOnBoardList[0])
                {
                    this.m_kPuzzleNumArray[iRow, iCol] = this.m_kGridNumArray[iRow, iCol];
                    kOnBoardList.RemoveAt(0);
                }
            }

            Array.Copy(this.m_kPuzzleNumArray, this.m_kPuzzleBakNumArray, this.m_kGridNumArray.Length);
        }

        private void _randomizeList(List<int> kList)
        {
            for (int i = 0; i < kList.Count - 1; i++)
            {
                int iRand = Random.Range(i, kList.Count);
                (kList[i], kList[iRand]) = (kList[iRand], kList[i]);
            }
        }

        public void CreateGrid()
        {
            List<int> kRowList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> kColList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int iValue = kRowList[Random.Range(0, kRowList.Count)];
            this.m_kGridNumArray[0, 0] = iValue;
            kRowList.Remove(iValue);
            kColList.Remove(iValue);

            for (int i = 1; i < GameManager.GRID_LENGTH; i++)
            {
                iValue = kRowList[Random.Range(0, kRowList.Count)];
                this.m_kGridNumArray[i, 0] = iValue;
                kRowList.Remove(iValue);
            }

            for (int i = 1; i < GameManager.GRID_LENGTH; i++)
            {
                iValue = kColList[Random.Range(0, kColList.Count)];
                if (i < 3)
                {
                    while (_squareContainsValue(0, 0, iValue))
                    {
                        iValue = kColList[Random.Range(0, kColList.Count)];
                    }
                }

                this.m_kGridNumArray[0, i] = iValue;
                kColList.Remove(iValue);
            }

            for (int i = 6; i < 9; i++)
            {
                iValue = Random.Range(1, 10);
                while (_squareContainsValue(0, 8, iValue) || _squareContainsValue(8, 0, iValue) || _squareContainsValue(8, 8, iValue))
                {
                    iValue = Random.Range(1, 10);
                }

                this.m_kGridNumArray[i, i] = iValue;
            }

            this._solveSudoku();
        }

        private bool _isValid()
        {
            for (int i = 0; i < GameManager.GRID_LENGTH; i++)
            {
                for (int j = 0; j < GameManager.GRID_LENGTH; j++)
                {
                    if (this.m_kGridNumArray[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool _solveSudoku()
        {
            int iRow = 0;
            int iCol = 0;

            if (this._isValid())
            {
                return true;
            }

            for (int i = 0; i < GameManager.GRID_LENGTH; i++)
            {
                for (int j = 0; j < GameManager.GRID_LENGTH; j++)
                {
                    if (this.m_kGridNumArray[i, j] == 0)
                    {
                        iRow = i;
                        iCol = j;
                        break;
                    }
                }
            }

            for (int i = 1; i <= GameManager.GRID_LENGTH; i++)
            {
                if (_checkAll(iRow, iCol, i))
                {
                    this.m_kGridNumArray[iRow, iCol] = i;

                    if (_solveSudoku())
                    {
                        return true;
                    }
                    else
                    {
                        this.m_kGridNumArray[iRow, iCol] = 0;
                    }
                }
            }

            return false;
        }

        bool _checkAll(int iCol, int iRow, int iValue)
        {
            if (_colContainsValue(iCol, iValue))
            {
                return false;
            }

            if (_rowContainsValue(iRow, iValue))
            {
                return false;
            }

            if (_squareContainsValue(iRow, iCol, iValue))
            {
                return false;
            }
            return true;
        }


        private bool _colContainsValue(int iCol, int iValue)
        {
            for (int i = 0; i < GameManager.GRID_LENGTH; i++)
            {
                if (this.m_kGridNumArray[i, iCol] == iValue)
                {
                    return true;
                }
            }
            return false;
        }

        private bool _rowContainsValue(int iRow, int iValue)
        {
            for (int i = 0; i < GameManager.GRID_LENGTH; i++)
            {
                if (this.m_kGridNumArray[iRow, i] == iValue)
                {
                    return true;
                }
            }
            return false;
        }

        private bool _squareContainsValue(int iRow, int iCol, int iValue)
        {
            for (int i = 0; i < GameManager.SUB_GRIDLENGTH; i++)
            {
                for (int j = 0; j < GameManager.SUB_GRIDLENGTH; j++)
                {
                    if (this.m_kGridNumArray[iRow / GameManager.SUB_GRIDLENGTH * GameManager.CELL_LENGTH + i, iCol / GameManager.SUB_GRIDLENGTH * GameManager.CELL_LENGTH + j] == iValue)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
