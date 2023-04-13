using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkMangetUI : MonoBehaviour
{
    [SerializeField]
    private Button m_ServerBtn;
    [SerializeField]
    private Button m_ClientBtn;
    [SerializeField]
    private Button m_HostBtn;
    [SerializeField]
    private Button m_EndBtn;

    private void Awake()
    {
        m_ServerBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });

        m_ClientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });

        m_HostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        m_EndBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StopAllCoroutines();
        });

    }

    private void Start()
    {
       
    }
}
