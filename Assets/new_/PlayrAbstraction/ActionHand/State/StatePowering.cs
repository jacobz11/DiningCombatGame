//using Assets.Scripts.Player.Offline.Player.States;
//using Assets.Scripts.Util.Channels;
//using DiningCombat;
//using DiningCombat.Player;
//using System;
//using System.Collections;
//using UnityEngine;

//namespace Assets.Scripts.Player.PlayrAbstraction.ActionHand
//{
//    //internal abstract class StatePowering : IStatePlayerHand
//    //{
//    //    public event Action<float> OnPower;
//    //    private PlayerAnimationChannel m_AnimationChannel;
//    //    private TimeBuffer m_TimeBuffer;

//    //    public StatePowering(BridgeAbstractionAction i_PickUpItem, BridgeImplementorAcitonStateMachine i_Machine)
//    //        : base(i_PickUpItem, i_Machine)
//    //    {
//    //        m_AnimationChannel = i_PickUpItem.GetComponentInChildren<PlayerAnimationChannel>();
//    //        m_TimeBuffer = new TimeBuffer(5f);
//    //    }

//    //    protected abstract bool IsPassStage();

//    //    public override void OnStateEnter(params object[] list)
//    //    {
//    //        Debug.Log("init state : StatePowering");
//    //        m_TimeBuffer.Clear();
//    //        base.OnStateEnter(list);
//    //    }

//    //    public override void OnStateExit(params object[] list)
//    //    {
//    //        m_AnimationChannel.SetPlayerAnimationToThrow();
//    //        //this.m_PlayrHand.ThrowingAnimator();
//    //    }

//    //    protected abstract bool IsAddPower();

//    //    public override void OnStateUpdate(params object[] list)
//    //    {
//    //        // TODO : 
//    //        if (this.IsAddPower())
//    //        {
//    //            this.AddToForceMulti();
//    //        }
//    //        else if (this.IsPassStage() && IsOverMinForceMulti(m_PlayrHand.ForceMulti) && m_Buffer.IsBufferOver())
//    //        {
//    //            this.m_Machine.StatesIndex++;
//    //        }
//    //        else
//    //        {
//    //            this.StepBack();
//    //        }
            
//    //        static bool IsOverMinForceMulti(float i_Current)
//    //        {
//    //            return i_Current > GameManager.Singlton.MinForce;
//    //        }

//    //}

//    //public IEnumerator OnTringPoint()
//    //    {
//    //        yield return new WaitForEndOfFrame();
//    //        yield return new WaitForEndOfFrame();

//    //        OnPower?.Invoke(float.NegativeInfinity);
//    //    }

//    //    private void StepBack()
//    //    {
//    //        OnPower?.Invoke(float.NegativeInfinity);
//    //        this.m_Machine.StatesIndex--;
//    //    }

//    //    public override string ToString()
//    //    {
//    //        return "StatePowering : ";
//    //    }

//    //    private void AddToForceMulti()
//    //    {
//    //        OnPower?.Invoke(Time.deltaTime * 1400);
//    //    }
//    }
////}
