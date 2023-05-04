using Assets.scrips;
using System;
using UnityEngine;

public class Player : MonoBehaviour
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
    //private AcitonStateMachine m_AcitonHand;

    public Transform PicUpPoint { get; internal set; }

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        gameInput.OnBostRunnigAction += GameInput_OnBostRunnigAction;
    }

    private void GameInput_OnBostRunnigAction(object sender, System.EventArgs e)
    {
        Debug.Log("GameInput_OnChargingAction");
    }
}
