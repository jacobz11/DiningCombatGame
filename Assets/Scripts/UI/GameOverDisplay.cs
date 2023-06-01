using DiningCombat;
using DiningCombat.Manger;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverDisplay : MonoBehaviour
{
    private const string k_SceneName = "V3Copy";

    [SerializeField]
    private TextMeshProUGUI m_Text;
    public void Start()
    {
        GameObject[] data = GameObject.FindGameObjectsWithTag(GameGlobal.TagNames.k_DontDestroyOnLoad);
        string text = "no text";
        foreach (GameObject item in data)
        {
            if (item.TryGetComponent<GameOverLogic>(out GameOverLogic o_OverLogic))
            {
                text = o_OverLogic.ToString();
            }

            Destroy(item);
        }

        m_Text.text = text;
        Debug.Log(text);
    }

    public void OnClickStartOver()
    {
        SceneManager.LoadScene(k_SceneName);
    }
}
