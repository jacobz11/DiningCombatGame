namespace Assets.scrips.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    internal class PoweringVisual : MonoBehaviour
    {
        public static PoweringVisual Instance { get; private set; }

        [SerializeField]
        private Image m_PoweringBar;

        public bool StartingFullAmont => false;

        public Image BarImage => m_PoweringBar;

        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
            }
        }

        public PoweringVisual GetPoweringVisual()
        {
            return this;
        }

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
