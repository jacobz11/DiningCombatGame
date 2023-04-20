using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkBtnStrting : MonoBehaviour
{
    [SerializeField]
    private Button m_StartAsHost;
    [SerializeField]
    private Button m_StartAsClient;
    [SerializeField]
    private Image m_ImagemStartAsImage;

    private void Awake()
    {
        m_StartAsHost.onClick.AddListener(() =>
        {
            Debug.Log("Host");
            NetworkManager.Singleton.StartHost();
            Hide();
        });
        m_StartAsClient.onClick.AddListener(() =>
        {
            Debug.Log("Client");
            NetworkManager.Singleton.StartClient();
            Hide();
        });
    }

    public void Hide()
    {
        m_ImagemStartAsImage.gameObject.SetActive(false);
    }
}
