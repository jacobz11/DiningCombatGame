using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerCounter : MonoBehaviour
{
    public static float powerValue = 0;
    Text power;

    void Start()
    {
        power = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        power.text = "Power: " + powerValue;
    }
}