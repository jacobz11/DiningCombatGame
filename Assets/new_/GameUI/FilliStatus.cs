//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class FilliStatus : MonoBehaviour
//{
//    public Image m_FilleImge; 
//    private Slider m_Slider;

//    private void Awake()
//    {
//        m_Slider = GetComponent<Slider>();
//    }

//    public float UpdateFilliStatus
//    {
//        get
//        {
//            return m_Slider.value;
//        }
//        set
//        {
//            m_Slider.value = value;
//        }
//    }

//    public float GetSliderCurAndMaxAndMinValue(out float o_Max, out float o_Min)
//    {
//        o_Max = m_Slider.maxValue;
//        o_Min = m_Slider.minValue;

//        return m_Slider.value;
//    }
//}
