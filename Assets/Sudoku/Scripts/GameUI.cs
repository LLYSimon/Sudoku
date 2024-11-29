//******************************************
// FileName: MainUI.cs
// Mail: 877121737@qq.com
// Author: Simon
// Version:#Version#
// Description:
//******************************************

using UnityEngine;
using UnityEngine.UI;

namespace Sudoku
{
    public class GameUI : MonoBehaviour
    {
        public Button m_kBackBtn;
        public Button m_kNewBtn;
        public Button m_kRestartBtn;
        public Button m_kSubmitBtn;
        public Text m_kTimerText;
        public GridLayoutGroup m_kGridLayoutGroup;
        public GameObject m_kGridTemplate;
        public float m_fTimer;
        public GameObject m_kNumerRoot;

        public void OnStartGame()
        {
            this.m_kBackBtn.onClick.AddListener(this._backBtnClick);
            this.m_kNewBtn.onClick.AddListener(this._newBtnClick);
            this.m_kSubmitBtn.onClick.AddListener(this._submitBtnClick);
            this.m_kRestartBtn.onClick.AddListener(this._restartBtnClick);
            this.m_fTimer = 0;
            for (int i = 0; i < this.m_kNumerRoot.transform.childCount; i++)
            {
                var kChild = this.m_kNumerRoot.transform.GetChild(i);
                kChild.name = (i + 1).ToString();
                kChild.transform.GetChild(0).GetComponent<Text>().text = i == 9 ? "" : kChild.name;
                kChild.GetComponent<Button>().onClick.AddListener(() =>
                {

                });
            }
            GameManager.Instance.Init(this.m_kGridLayoutGroup, this.m_kGridTemplate);
        }

        private void _backBtnClick()
        {

        }

        private void _newBtnClick()
        {

        }

        private void _submitBtnClick()
        {

        }

        private void _restartBtnClick()
        {

        }

        private void Update()
        {
            this.m_fTimer += Time.deltaTime;
            this.m_kTimerText.text = this.m_fTimer.ToString();
        }

        public void OnEndGame()
        {

        }
    }
}
