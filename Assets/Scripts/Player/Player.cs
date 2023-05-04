using Assets.scrips;
using Assets.Scripts.Manger;
using System;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public event Action<Collider> OnExitCollisionFoodObj;
    public event Action<Collider> OnEnterCollisionFoodObj;
    public event Action OnPickUpAction;
    public event Action OnChargingAction;

    [SerializeField] 
    private GameInput gameInput;
    [SerializeField] 
    private Transform m_PickUpPoint;
    private PlayerMovment m_Movment;
    private Rigidbody m_Rigidbody;
    [SerializeField]
    private bool isAi;

    //private AcitonStateMachine m_AcitonHand;

    public Transform PicUpPoint { get; internal set; }

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        GameStrting.Instance.AddNumOfPlyers(1);
    }

    private void Start()
    {
        if (!isAi)
        {
            gameInput.OnBostRunnigAction += GameInput_OnBostRunnigAction;
        }
        gameObject.transform.position = GameStrting.Instance.GatIntPosForPlayer();
    }

    private void GameInput_OnBostRunnigAction(object sender, System.EventArgs e)
    {
        Debug.Log("GameInput_OnChargingAction");
    }
}
