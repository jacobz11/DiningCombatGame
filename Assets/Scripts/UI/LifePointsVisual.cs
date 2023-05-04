using UnityEngine;
using UnityEngine.UI;

public class LifePointsVisual : MonoBehaviour
{
    [SerializeField]
    private Image m_LifePoints;

    public bool StartingFullAmont => true;

    public Image BarImage => m_LifePoints;

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
        m_LifePoints.fillAmount += (float)i_Adding / 100;
    }

    public void UpdateBarNormalized(float i_NewNormalizedValue)
    {
        m_LifePoints.fillAmount = i_NewNormalizedValue;
    }
}
