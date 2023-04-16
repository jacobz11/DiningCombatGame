namespace Assets.scrips.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    internal class PoweringVisual : MonoBehaviour
    {
        [SerializeField]
        private Image m_PoweringBar;

        public bool StartingFullAmont => false;

        public Image BarImage => m_PoweringBar;

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void ResetBar()
        {
            float newFillAmount = StartingFullAmont ? 1 : 0;
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void UpdateBar(float i_Adding)
        {
            m_PoweringBar.fillAmount += (float)i_Adding / 100;
        }

        public void UpdateBarNormalized(float i_NewNormalizedValue)
        {
            m_PoweringBar.fillAmount = i_NewNormalizedValue;
        }
    }
}
