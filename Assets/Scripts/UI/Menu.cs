using DiningCombat.Manger;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private const string k_SceneName = "MainMenu";
    [SerializeField]
    private StaringData m_GameObjectData;

    public void StartGame()
    {
        DontDestroyOnLoad(m_GameObjectData);
        SceneManager.LoadScene(k_SceneName);
    }

    public void OnOptionsButtonClick()
    {
        Debug.Log("not implement\nJacob why do you let me do a lot of work?????");
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
