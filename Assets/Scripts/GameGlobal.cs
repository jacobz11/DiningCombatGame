namespace DiningCombat
{
    public static class GameGlobal
    {
        public static class GameObjectName
        {
            public const string k_Player = "Player";
            public const string k_PickUpPoint = "PickUpPoint";
            public const string k_Ground = "Ground";
        }

        public static class TagNames
        {
            public const string k_Ground = "Ground";
            public const string k_FoodObj = "FoodObj";
            public const string k_ThrowFoodObj = "ThrownObject";
            public const string k_Player = "Player";
            public const string k_Picked = "Picked";
            public const string k_Ui = "UI";
            public const string k_Environment = "environment";
            public const string k_Water = "water";
            public const string k_DontDestroyOnLoad = "DontDestroyOnLoad";
            public const string k_Hide = "hide";
        }

        public static class ScenesName
        {
            public const string k_GamePlay = "V3Copy";
            public const string k_GameOver = "GameOver";
            public const string k_Menu = "Menu";
        }

        public enum ePlayerModeType
        {
            OfflinePlayer,
            OfflineAiPlayer,
            OfflineTestPlayer,
            MLTrining,
            OnlinePlayer,
            OnlineAiPlayer,
            OnlineTestPlayer,
        }
    }
}