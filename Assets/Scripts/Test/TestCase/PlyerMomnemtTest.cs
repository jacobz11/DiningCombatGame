using Abstraction.DiningCombat.Player;
using Abstraction.Player.DiningCombat;
using UnityEngine;

public class PlyerMomnemtTest : TestCase
{
    private void Start()
    {
        GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        PlayerMovement.Builder(gameObject, DiningCombat.GameGlobal.ePlayerModeType.OfflineTestPlayer,
            out PlayerMovement o_Movement, out PlayerMovementImplementor o_Implementor);
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
