using DiningCombat;
using DiningCombat.Manger;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private StaringData m_GameObjectData;

    public void StartGame()
    {
        DontDestroyOnLoad(m_GameObjectData);
        SceneManager.LoadScene(GameGlobal.ScenesName.k_GamePlay);
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
