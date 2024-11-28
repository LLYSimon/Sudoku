//******************************************
// FileName: UIRoot.cs
// Mail: 877121737@qq.com
// Author: Simon
// Version:#Version#
// Description:
//******************************************

using System.Collections;
using System.Collections.Generic;
using Codice.Client.Common.GameUI;
using UnityEngine;
using UnityEngine.UI;

namespace EightQueen
{
    public class UIRoot : MonoBehaviour
    {
        private const int QUEEN_COUNT = 8;
        private const int CELL_SPACE = 20;
        private const int CELL_SIZE = 100;
        public Transform m_kGridRoot;
        public Button m_kGridTemplate;
        [HideInInspector]
        public Button[,] m_kGridArray = new Button[QUEEN_COUNT, QUEEN_COUNT];
        public Text m_kPageDisplay;
        private List<int[]> m_kQueenList;
        private int m_iPageIndex;
        public Button m_kNextBtn;
        public Button m_kLastBtn;
        private void Awake()
        {
            var kGridComp = this.m_kGridRoot.GetComponent<GridLayoutGroup>();
            kGridComp.spacing = new Vector2(CELL_SPACE, CELL_SPACE);
            kGridComp.cellSize = new Vector2(CELL_SIZE, CELL_SIZE);
            this.m_kGridRoot.GetComponent<RectTransform>().sizeDelta = Vector2.one * (QUEEN_COUNT * CELL_SIZE + (QUEEN_COUNT - 1) * CELL_SPACE);

            this.m_kLastBtn.onClick.AddListener(this._btnLastPage);
            this.m_kNextBtn.onClick.AddListener(this._btnNextPage);
            this.m_kPageDisplay.text = default;
        }

        private void Start()
        {
            for (int i = 0; i < this.m_kGridArray.GetLength(0); i++)
            {
                for (int j = 0; j < this.m_kGridArray.GetLength(1); j++)
                {
                    var kObj = GameObject.Instantiate(this.m_kGridTemplate, this.m_kGridRoot);
                    kObj.gameObject.SetActive(true);
                    kObj.transform.GetChild(0).gameObject.SetActive(false);
                    this.m_kGridArray[i, j] = kObj;
                }
            }

            this._getResult();
        }

        private void _getResult()
        {
            this.m_kQueenList = EightQueenSolutionTool.GetEightQueenSolution(QUEEN_COUNT);
            this.m_iPageIndex = 0;
            this._setText();
            this._refreshPage();
        }

        private void _setText()
        {
            this.m_kPageDisplay.text = $"共{this.m_kQueenList.Count}页， 当前第{this.m_iPageIndex + 1}页";
        }

        private void _btnNextPage()
        {
            this.m_iPageIndex += 1;
            this._setText();
            this._refreshPage();
        }

        private void _btnLastPage()
        {
            this.m_iPageIndex -= 1;
            this._setText();
            this._refreshPage();
        }

        private void _refreshPage()
        {
            int iQueenIndex = 0;
            var kSolution = this.m_kQueenList[this.m_iPageIndex];

            for (int i = 0; i < this.m_kGridArray.GetLength(0); i++)
            {
                for (int j = 0; j < this.m_kGridArray.GetLength(1); j++)
                {
                    if (i == iQueenIndex && kSolution[iQueenIndex] == j)
                    {
                        this.m_kGridArray[i, j].transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        this.m_kGridArray[i, j].transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                iQueenIndex++;
            }
        }
    }
}
