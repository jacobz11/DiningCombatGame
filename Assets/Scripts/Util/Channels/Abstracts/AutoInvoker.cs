using UnityEngine;
using System.Collections;

namespace Util
{
    namespace DiningCombat
    {
        internal class AutoInvoker<T> : MonoBehaviour
        {
            private ChannelObserver<T> m_Channel;
            public bool m_IsRunnig = false;
            private float m_time = 0;

            public bool Statr
            {
                get => m_IsRunnig;
                set => changeState(true);
            }

            public static AutoInvoker<T> Statrer(ChannelObserver<T> channel, float time)
            {
                AutoInvoker<T> invoker = new AutoInvoker<T>();
                invoker.m_Channel = channel;
                invoker.SetTimeWiting(time);
                invoker.Statr = true;

                return invoker;
            }

            public bool Run => Statr;
            public bool IsRuning => Statr;

            public bool Stop
            {
                get => !m_IsRunnig;
                set => changeState(false);
            }

            private void changeState(bool i_NewState)
            {
                bool isEqualState = i_NewState == m_IsRunnig;

                if (isEqualState)
                {
                    string errorMsgg = i_NewState ? "You can't start multiple times" : "You didn't start it";
                    Debug.LogError(errorMsgg);
                }
                else
                {
                    if (i_NewState)
                    {
                        StartCoroutine(invokerCoroutine());
                    }
                    else
                    {
                        StopCoroutine(invokerCoroutine());
                    }
                    m_IsRunnig = i_NewState;
                }
            }

            public void SetTimeWiting(float time)
            {
                if (time >= 0)
                {
                    m_time = time;
                }
                else
                {
                    Debug.LogError("the time must be none nagative number");
                }
            }

            private IEnumerator invokerCoroutine()
            {
                while (m_IsRunnig)
                {
                    m_Channel.Invoke();
                    bool isWait = m_time != 0;
                    yield return isWait ? new WaitForSeconds(m_time) : null;
                }
            }
        }
    }
}
