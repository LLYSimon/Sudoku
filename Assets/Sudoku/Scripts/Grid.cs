//******************************************
// FileName: Grid.cs
// Mail: 877121737@qq.com
// Author: Simon
// Version:#Version#
// Description:
//******************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sudoku
{
    public class Grid
    {
        public int m_iPosX;
        public int m_iPosY;
        public int m_iNum;
        public Text m_kText;
        public GameObject m_kGrid;

        public void UpdateText()
        {
            this.m_kText.text = this.m_iNum.ToString();
            if (this.m_iNum <= 0)
            {
                this.m_kText.text = default;
            }
        }

        public void SetNum(int iNum)
        {
            this.m_iNum = iNum;
        }
    }
}
