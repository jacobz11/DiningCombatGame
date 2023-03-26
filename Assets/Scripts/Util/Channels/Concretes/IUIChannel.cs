using UnityEngine;
using Util.Abstraction;
using TMPro;

namespace UI
{
    internal class IUIChannel : MonoBehaviour, IChannelGame
    {
        [SerializeField]
        private TextMeshPro m_SingalText;

        protected virtual void OnGameOver()
        {
            m_SingalText.text = "Game Over MF";
            m_SingalText.transform.position = Vector3.zero;
            m_SingalText.enabled = true;
        }

        protected virtual void OnWinner()
        {
            m_SingalText.text = "ידעת ולא שלחת ?";
            m_SingalText.transform.position = Vector3.zero;
            m_SingalText.enabled = true;
        }
        protected virtual void OnLose()
        {
            m_SingalText.text = "Lose";
            m_SingalText.transform.position = Vector3.zero;
            m_SingalText.enabled = true;
        }
    }
}
