using DiningCombat.Player;
using UnityEngine;

namespace Test.Player
{
    public class PlyerMomnemtTest : TestCase
    {
        private void Start()
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BridgeAbstraction3DMovement.Builder(gameObject, DiningCombat.GameGlobal.ePlayerModeType.OfflineTestPlayer,
                out BridgeAbstraction3DMovement o_Movement, out BridgeImplementor3DMovement o_Implementor);
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
}