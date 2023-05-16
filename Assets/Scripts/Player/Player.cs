using Assets.scrips;
using System;
using Unity.Netcode;
using UnityEngine;
// TODO : Add a namespace
public class Player : NetworkBehaviour
{
    public event Action<Collider> OnExitCollisionFoodObj;
    public event Action<Collider> OnEnterCollisionFoodObj;
    public event Action OnPickUpAction;
    public event Action OnChargingAction;

    [SerializeField]
    private GameInput m_GameInput;
    [SerializeField]
    private Transform m_PickUpPoint;
    private PlayerMovment m_Movment;
    private Rigidbody m_Rigidbody;

    public Transform PicUpPoint { get; internal set; }

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        GameStrting.Instance.AddNumOfPlyers(1);
    }

    private void GameInput_OnBostRunnigAction(object sender, System.EventArgs e)
    {
        Debug.Log("GameInput_OnChargingAction");
    }
}