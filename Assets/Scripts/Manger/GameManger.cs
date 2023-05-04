using DiningCombat;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Manger
{
    internal class GameManger : NetworkBehaviour
    {
        [SerializeField]
        private GameObject m_AiPrifab;
        [SerializeField]
        private Room m_RoomDimension;
        [SerializeField]
        private NetworkBtnStrting m_NetworkBtn;
        [SerializeField]
        private GameStrting m_GameStrting;
        private GameOverLogic m_GameOverLogic;

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
            m_GameOverLogic = gameObject.GetComponent<GameOverLogic>();
            Cuntter = 0;
        }

        private void Start()
        {
            TryStartOffline();
        }

        private void TryStartOffline()
        {
            GameObject[] data = GameObject.FindGameObjectsWithTag(GameGlobal.TagNames.k_DontDestroyOnLoad);

            if (data.Length == 0)
            {
                Debug.Log("Data is empty");
                return;
            }
            else
            {
                Debug.Log("Data " + data.Length);
            }

            if (!data[0].TryGetComponent<StaringData>(out StaringData o_StaringData))
            {
                Debug.Log("StaringData cant get ");
                return;
            }

            if (!o_StaringData.IsOnline)
            {
                Debug.Log("StaringData Is not Online ");

                // remove on ckile 
                m_NetworkBtn.StartHost();
                GameStrting.Instance.AddNumOfPlyers(o_StaringData.m_NumOfAi);

                // instint Ai
                for (int i = 0; i < o_StaringData.m_NumOfAi; i++)
                {
                    Debug.Log("StaringData ai ");

                    GameObject ai = GameObject.Instantiate(m_AiPrifab, GameStrting.Instance.GatIntPosForPlayer(), Quaternion.identity);
                }
            }
            else
            {
                Debug.Log("StaringData IsOnline ");
                m_GameStrting.AddNumOfPlyers(1);
            }
        }

        public void AddCamera(GameObject i_Player)
        {
            Camera cam = i_Player.AddComponent<Camera>();
            cam.targetDisplay = Cuntter++;
        }

        internal int GetTargetDisplay()
        {
            int targetDisplay = Cuntter;
            Cuntter++;
            return targetDisplay;
        }

        public IEnumerable<Vector3> GetPlayerPos(Transform i_Player)
        {
            return GameObject.FindGameObjectsWithTag(GameGlobal.TagNames.k_Player).Where(player => player.transform.position != i_Player.position).Select(player => player.transform.position);
        }
    }
}
