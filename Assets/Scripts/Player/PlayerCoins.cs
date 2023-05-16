using UnityEngine;
// TODO : Add a namespace
internal class PlayerCoins : MonoBehaviour
{
    public const string k_Formt = "coins : {0}";
    [SerializeField]
    private TMPro.TextMeshProUGUI m_TextMeshPro;
    private int m_Value;

    public int Value
    {
        get => m_Value;
        private set
        {
            m_Value = value;
            //m_TextMeshPro.text = string.Format(k_Formt, m_Value);
        }
    }
    private void Awake()
    {
        Value = 0;
    }
    internal void AddCoins(CoinsPackage coinsPackage)
    {
        Value += (int)coinsPackage.Amont;
    }
}