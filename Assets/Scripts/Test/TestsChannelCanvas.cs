using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TestsChannelCanvas : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    [SerializeField]
    private TMP_Text m_TextActine;
    [SerializeField]
    private float m_TimeToShowActine;

    private Dictionary<string, TestCase> m_Dictionary = new Dictionary<string, TestCase>();
    //private Dictionary<int, string> m_KeysInDropDropdown;

    private void Awake()
    {
        List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
        foreach (string test in m_Dictionary.Keys)
        {
            list.Add(new TMP_Dropdown.OptionData(test));
        }

        dropdown.options = list;
        dropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    private void OnDropdownChanged(int id)
    {
        Debug.Log("OnDropdownChanged :" + id);
    }

    public void OnActionHappens(string action)
    {
        StartCoroutine(TextActionHappens(action));
    }

    private IEnumerator TextActionHappens(string action)
    {
        m_TextActine.text = action;
        m_TextActine.enabled= true;
        yield return new WaitForSeconds(m_TimeToShowActine);
        m_TextActine.enabled = false;
    }
}
