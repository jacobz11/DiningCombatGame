using DiningCombat.Manger;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private const string k_SceneName = "V3DiningCombat";
    [SerializeField]
    private StaringData m_GameObjectData;

    public void StartGame()
    {
        DontDestroyOnLoad(m_GameObjectData);
        SceneManager.LoadScene(k_SceneName);
    }
}
