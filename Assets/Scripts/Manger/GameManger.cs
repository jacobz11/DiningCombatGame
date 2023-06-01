using DiningCombat.Environment;
using DiningCombat.Player;
using DiningCombat.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DiningCombat.Manger
{
    //TODO : arrange the code
    //TODO : Delete what you don't need
    public class GameManger : Util.DesignPatterns.Singleton<GameManger>
    {
        [SerializeField]
        private AllPlayerSkinsSO m_Skins;
        [SerializeField]
        private GameObject m_AiPrifab;
        [SerializeField]
        private Room m_RoomDimension;
        [SerializeField]
        private NetworkBtnStrting m_NetworkBtn;
        [SerializeField]
        private GameStrting m_GameStrting;
        [SerializeField]
        public LifePointsVisual m_LifePointsVisualScreen;
        [SerializeField]
        public PoweringVisual m_PoweringVisualScreen;
        private readonly List<string> r_AiName = new List<string>() {
            "Botzilla",
            "Byte Meister",
            "Cogsworth",
            "Electric Boogaloo",
            "GizmoTron",
            "Machina Man",
            "Pixel Poppins",
            "Robo Rascal",
            "Sparky McSparkface",
            "Technotron",
            "Whirly Gigglebot",
            "Circuit Breaker",
            "Quirkotron",
            "Byte-sized Bandit",
            "TinkerTot",
        };

        public GameOverLogic GameOverLogic { get; private set; }
        public int Cuntter { get; private set; }
        public LifePointsVisual LifePointsVisual { get => m_LifePointsVisualScreen; }
        public PoweringVisual PoweringVisual { get => m_PoweringVisualScreen; }

        protected override void Awake()
        {
            base.Awake();
            GameOverLogic = gameObject.GetComponent<GameOverLogic>();
            Cuntter = 0;
        }

        private void Start()
        {
            TryStartOffline();
            gameObject.tag = GameGlobal.TagNames.k_DontDestroyOnLoad;
        }

        private void TryStartOffline()
        {
            GameObject[] data = GameObject.FindGameObjectsWithTag(GameGlobal.TagNames.k_DontDestroyOnLoad);

            if (data.Length == 0)
            {
                Debug.LogWarning($"No tagged object found in {GameGlobal.TagNames.k_DontDestroyOnLoad}");
                return;
            }

            GameObject staringDataGO = data[0];
            if (!staringDataGO.TryGetComponent<StaringData>(out StaringData o_StaringData))
            {
                Debug.Log("StaringData cant get");
            }
            else if (!o_StaringData.IsOnline)
            {
                m_NetworkBtn.StartHost();

                // instint Ai
                for (int i = 0; i < o_StaringData.m_NumOfAi; i++)
                {
                    GameObject ai = GameObject.Instantiate(m_AiPrifab, m_RoomDimension.GetRendonPos(), Quaternion.identity);
                    ai.name = r_AiName[i];
                    ai.GetComponentInChildren<Renderer>().material = m_Skins;
                }
            }

            Destroy(staringDataGO);
        }

        public void AddCamera(GameObject i_Player)
        {
            Camera cam = i_Player.AddComponent<Camera>();
            cam.targetDisplay = Cuntter++;
        }

        public int GetTargetDisplay()
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
