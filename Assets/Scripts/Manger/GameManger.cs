using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Manger
{
    internal class GameManger : NetworkBehaviour
    {
        public static GameManger Instance { get; private set; }
        public int Cuntter { get; private set; }
        private void Awake()
        {
            if (Instance is not null) 
            {
                Destroy(this);
                return;
            }
            Instance = this;
            Cuntter = 0;
        }

        public void AddCamera(GameObject i_Player)
        {
            Camera cam =i_Player.AddComponent<Camera>();
            cam.targetDisplay = Cuntter++;
        }

        internal int GetTargetDisplay()
        {
            int targetDisplay = Cuntter;
            Cuntter++;
            return targetDisplay;
        }
    }
}
