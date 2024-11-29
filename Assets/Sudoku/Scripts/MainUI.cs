//******************************************
// FileName: GameUI.cs
// Mail: 877121737@qq.com
// Author: Simon
// Version:#Version#
// Description:
//******************************************

using UnityEngine;
using UnityEngine.UI;

namespace Sudoku
{
    public class MainUI : MonoBehaviour
    {
        public Button m_kPlayBtn;
        public Slider m_kDifficultySlider;

        private void Awake()
        {
            GameManager.Instance.Awake();
            this.m_kPlayBtn.onClick.AddListener(this._playBtnClick);
            this.m_kDifficultySlider.onValueChanged.AddListener(this._sliderValueChanged);
        }

        private void _playBtnClick()
        {
            GameManager.Instance.Play();
        }

        private void _sliderValueChanged(float fValue)
        {
            GameManager.Instance.SetDifficulty(fValue);
        }
    }
}
