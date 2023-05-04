
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Material[] m_Materials;
    [SerializeField]
    private SkinnedMeshRenderer m_Mesh;
    [SerializeField]
    private Button m_Prefab;
    [SerializeField]
    private HorizontalLayoutGroup m_HorizontalLayoutGroup;
    [SerializeField]
    private StaringData m_GameObjectData;
    private void Awake()
    {
        foreach (Material material in m_Materials)
        {
            Button btm = Instantiate(m_Prefab);
            btm.name = material.name;
            btm.image.material = material;
            btm.onClick.AddListener(() => m_Mesh.material = material);
            btm.transform.SetParent(m_HorizontalLayoutGroup.transform);
        }
        //m_HorizontalLayoutGroup.
    }
    public void ChangeColor(int colorIndex)
    {
        m_Mesh.material = m_Materials[colorIndex];
    }
    public void PlayGame()
    {
        DontDestroyOnLoad(m_GameObjectData);
        SceneManager.LoadScene("V3DiningCombat");
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
