using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class QuantumConsole : MonoBehaviour
{
    [SerializeField]
    private Text m_LogText;
    [SerializeField]
    private ScrollRect m_ScrollRect;

    void Start()
    {
        //scrollRect = GetComponent<ScrollView>();
        //logText = GetComponentInChildren<Text>();
        m_LogText.text = "it work";
        Debug.Assert(m_LogText != null);
        Application.logMessageReceived += HandleLog;
    }
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        m_LogText.text += logString + "\n";
        ScrollToBottom();
    }

    void ScrollToBottom()
    {
        //scrollRect.scrollOffset= Vector2.zero;
        m_ScrollRect.verticalScrollbar.value = 0f;
    }
}
