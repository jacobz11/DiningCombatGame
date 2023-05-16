using System;
using UnityEngine;

namespace Assets.DataObject
{
    /// <summary>
    /// Change only on Prifab - On Run time A change will do nothing
    /// Contains all the necessary fields for building any type of object,
    /// not all fields are relevant for each of the types
    /// </summary>
    [Serializable]
    // TODO Replace the class to scriptable objects
    internal struct ThrownActionTypesBuilder
    {

        // Summary:
        //     Purpose : create the object State Thrown by type
        public enum eThrownActionTypes
        {
            //
            // Summary:
            //      The normal type, the player makes a throw and the object is thrown in the direction of the hand
            //      Animation - Throwing
            //      Action -> OnColisenEnter
            //      successful action takes a life -> Yes
            //      Element-Special-By-Name : Not required
            Throwing,
            //
            // Summary:
            //      The Smoke Grenade, Does no damage any A player who is in the vicinity of the explosion, only creates an effect, and makes it difficult to move
            //      Animation -> Throwing
            //      Action -> On Timer
            //      successful action takes a life -> No
            //      Element-Special-By-Name : required
            SmokeGrenade,
            //
            // Summary:
            //      The Grenade, few seconds explodes and disturbs points to everyone who is around
            //      Animation - Throwing
            //      Action -> On Timer
            //      successful action takes a life -> Yes
            //      Element-Special-By-Name : required
            Grenade,
            //
            // Summary:
            //      The Mine, falls to the floor, and waits for another player to step on him
            //      Animation - Droping
            //      Action -> On Triger Enter 
            //      successful action takes a life -> Yes
            //      Element-Special-By-Name : required
            Mine
        }
        internal enum eElementSpecialByName
        {
            None,
            // SmokeGrenade
            FlourSmokeGrenade,
            // Grenade 
            PomegranateGrenade,
            // mine
            BananaMine,
            NpcCorn,
        }

        public eThrownActionTypes m_Type;
        public eElementSpecialByName m_ElementName;
        [Range(0f, 100f)]
        public float m_MaxDamagePoint;
        [Range(0f, 5f)]
        public float m_MinDamagePoint;
        public Rigidbody Rigidbody { get; set; }
        public Transform Transform { get; set; }
        public GameObject m_GameFoodObj;
        public MineData m_MinData;
        public GrenadeData m_GrenadeData;
        public ThrowingData m_ThrowingData;
        public GameFoodObj.eThrowAnimationType m_AnimationType;

        [Serializable]
        public struct MineData
        {
            public Collider m_Triger;
            [Range(0f, 5f)]
            public float m_InpactRadius;
            [Range(0f, 5f)]
            public float m_EffctTime;
            [Range(0f, 5f)]
            public float m_ForceHitExsplostin;
            [Range(0f, 5f)]
            public float m_CountdownTime;
            public GameObject m_GameObjectVisal;
            public GameObject m_AlmostTransparent;
        }
        [Serializable]
        public struct GrenadeData
        {
            public float InpactRadius;
            public float EffctTime;
            public float LifeTimeUntilAction;
            public float ForceHitExsplostin;
        }

        [Serializable]
        public struct ThrowingData
        { /* Not-Implemented */}

        public ThrownActionTypesBuilder SetTransform(Transform transform)
        {
            Transform = transform;
            return this;
        }

        public ThrownActionTypesBuilder SetRigidbody(Rigidbody i_Rigidbody)
        {
            Rigidbody = i_Rigidbody;
            return this;
        }

        public ThrownActionTypesBuilder GetGameObject(GameObject i_GameObject)
        {
            return this;
        }

        public ThrownActionTypesBuilder SetGameFoodObj(GameObject i_GameFoodObj)
        {
            m_GameFoodObj = i_GameFoodObj;
            return this;
        }

        public IThrownState Build()
        {
            switch (m_Type)
            {
                case eThrownActionTypes.Throwing:
                    return new ThrownState(this);
                case eThrownActionTypes.SmokeGrenade:
                    return new SmokeGrenade(this);
                case eThrownActionTypes.Grenade:
                    return new GrenadeLike(this);
                case eThrownActionTypes.Mine:
                    return new MineLike(this);
            }

            Debug.LogWarning("Building a ThrownState without type");
            return new ThrownState(this);
        }

        internal Type GetBuildType()
        {
            switch (m_Type)
            {
                case eThrownActionTypes.Throwing: return typeof(ThrownState);
                case eThrownActionTypes.Grenade: return typeof(ThrownState);
                case eThrownActionTypes.SmokeGrenade: return typeof(SmokeGrenade);
                case eThrownActionTypes.Mine: return typeof(MineLike);
            }

            return typeof(ThrownState);
        }

        public static implicit operator IThrownState(ThrownActionTypesBuilder i_Builder)
        {
            switch (i_Builder.m_Type)
            {
                case eThrownActionTypes.Throwing:
                    return new ThrownState(i_Builder);
                case eThrownActionTypes.SmokeGrenade:
                    return new SmokeGrenade(i_Builder);
                case eThrownActionTypes.Grenade:
                    return new GrenadeLike(i_Builder);
                case eThrownActionTypes.Mine:
                    return new MineLike(i_Builder);
            }

            Debug.LogWarning("Building a ThrownState without type");
            return new ThrownState(i_Builder);
        }
    }
}