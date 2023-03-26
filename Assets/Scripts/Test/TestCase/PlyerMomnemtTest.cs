using Abstraction.Player.DiningCombat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyerMomnemtTest : TestCase
{
    private void Start()
    {
        GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PlayerMovement.Builder(gameObject, DiningCombat.GameGlobal.ePlayerModeType.OfflineTestPlayer);
        m_Object = gameObject;
    }

    public override void OnTestCaseFinish()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTestCaseStart()
    {
        Debug.LogError("NotImplementedException");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
