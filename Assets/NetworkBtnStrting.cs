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
        Debug.Log("Awake NetworkBtnStrting");
        Debug.Assert(m_ImagemStartAsImage is not null, "m_ImagemStartAsImage is null");
        Debug.Assert(m_StartAsClient is not null, "m_StartAsClient is null");
        Debug.Assert(m_StartAsHost is not null, "m_StartAsHost is null");

        m_StartAsHost.onClick.AddListener(StartHost);
        m_StartAsClient.onClick.AddListener(StartClient);
    }

    public void StartHost()
    {
        Debug.Log("Host");
        NetworkManager.Singleton.StartHost();
        Hide();
    }

    public void StartClient()
    {
        Debug.Log("Client");
        NetworkManager.Singleton.StartClient();
        Hide();
    }

    public void Hide()
    {
        m_ImagemStartAsImage.gameObject.SetActive(false);
    }
}
