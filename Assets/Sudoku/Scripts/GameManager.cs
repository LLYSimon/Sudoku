//******************************************
// FileName: GameManage.cs
// Mail: 877121737@qq.com
// Author: Simon
// Version:#Version#
// Description:
//******************************************

using UnityEngine;
using UnityEngine.UI;

namespace Sudoku
{
    public class GameManager
    {
        public const int GRID_LENGTH = 9;
        public const int SUB_GRIDLENGTH = 3;
        public const int CELL_LENGTH = 3;

        public static GameManager s_kInstance;
        public static GameManager Instance
        {
            get
            {
                if (s_kInstance == null)
                {
                    s_kInstance = new GameManager();
                }
                return s_kInstance;
            }
        }
        private const int MAX_DIFFICULTY = 100;
        private const int MIN_DIFFICULTY = 1;
        private int m_iDifficulty;
        private Grid[,] m_kGridArray;
        private Grid m_kCurSelectGrid;
        public GameUI m_kGameUI;
        public MainUI m_kMainUI;

        public void Awake()
        {
            this.m_kMainUI = GameObject.Find("Canvas/MainUI").GetComponent<MainUI>();
            this.m_kGameUI = GameObject.Find("Canvas/GameUI").GetComponent<GameUI>();
            this.m_kGameUI.gameObject.SetActive(false);
        }

        public void Init(GridLayoutGroup kLayout, GameObject kTemplate)
        {
            if (this.m_kGridArray == null)
            {
                this.m_kGridArray = new Grid[9, 9];
                for (int i = 0; i < this.m_kGridArray.GetLength(0); i++)
                {
                    for (int j = 0; j < this.m_kGridArray.GetLength(1); j++)
                    {
                        GameObject kObj = GameObject.Instantiate(kTemplate, kLayout.transform);
                        kObj.SetActive(true);
                        this.m_kGridArray[i, j] = new Grid()
                        {
                            m_iPosX = i,
                            m_iPosY = j,
                            m_kGrid = kObj,
                            m_iNum = SudokuMgr.Instance.m_kPuzzleBakNumArray[i, j],
                            m_kText = kObj.transform.GetChild(0).GetComponent<Text>(),
                        };
                        this.m_kGridArray[i, i].UpdateText();
                    }
                }
            }
        }

        public void Play()
        {
            this.m_kMainUI.gameObject.SetActive(false);
            this.m_kGameUI.gameObject.SetActive(true);
            SudokuMgr.Instance.CreateGrid();
            this.m_kGameUI.OnStartGame();
        }

        public void SetDifficulty(float fValue)
        {
            this.m_iDifficulty = (int)(fValue * (MAX_DIFFICULTY - MIN_DIFFICULTY) + MIN_DIFFICULTY);
        }

        public void ResetGame()
        {

        }

        public void StartNewGame()
        {

        }
    }
}
