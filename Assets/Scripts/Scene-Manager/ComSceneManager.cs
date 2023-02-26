using UnityEngine;
using UnityEngine.SceneManagement;

public class ComSceneManager : MonoBehaviour
{

    public void ButtumNewGame_OnClick()
    {
        SceneManager.LoadScene("DiningCombat", LoadSceneMode.Single);
    }
}
