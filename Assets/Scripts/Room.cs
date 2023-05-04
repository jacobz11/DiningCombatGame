using Assets.DataObject;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Room : MonoBehaviour
    {
        [SerializeField]
        private RoomDimension m_RoomDimension;

        public RoomDimension GetRoomDimension() { return m_RoomDimension; }

        public Vector3 GetRendPos()=> m_RoomDimension.GetRendonPos();
    }
}
