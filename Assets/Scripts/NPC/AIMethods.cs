using DiningCombat.Environment;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;
namespace DiningCombat.AI
{

    public class AIMatud
    {
        private const float k_Speed = 15.0f;
        private const float k_MinMovment = 0.01f;
        private const float k_Perpendicular = 90.0f;
        private const float k_WanderRradius = 10f;
        private const float k_WanderDeistance = 20f;
        private const float k_WandrJitter = 1f;
        private const float k_VisionRange = 60.0f;

        /// <summary>
        /// The agent will try to reach the target
        /// </summary>
        public static void Seek(NavMeshAgent i_Agent, Vector3 i_TrgetPos)
        {
            _ = (i_Agent?.SetDestination(i_TrgetPos));
        }
        /// <summary>
        /// The agent will try to escape from the target
        /// </summary>
        public static void Flee(NavMeshAgent i_Agent, Vector3 i_FleeFromPos)
        {
            //Vector3 fleeVector = i_FleeFromPos - i_Agent.transform.position;
            _ = (i_Agent?.SetDestination(i_Agent.transform.position - TargeDir(i_FleeFromPos, i_Agent.transform)));
        }

        /// <summary>
        /// The agent will try to reach the target By looking ahead
        /// </summary>
        public static void Pursuit(NavMeshAgent i_Agent, Transform i_TargeTransform, float i_TargeSpeed)
        {
            float relativePos = Vector3.Angle(i_Agent.transform.forward, i_Agent.transform.TransformVector(i_TargeTransform.forward));
            float toTarge = Vector3.Angle(i_Agent.transform.forward, i_Agent.transform.TransformVector(TargeDir(i_TargeTransform.position, i_Agent.transform)));

            bool isInformt = toTarge > k_Perpendicular && relativePos < 20f;
            bool isMoving = i_TargeSpeed < k_MinMovment;

            Vector3 pursuitPos = (!isInformt || !isMoving) ?
                i_Agent.transform.position + (i_TargeTransform.forward * LookAhad(i_Agent, i_TargeTransform, i_TargeSpeed))
                : i_TargeTransform.position;

            Seek(i_Agent, pursuitPos);
        }

        /// <summary>
        /// The agent will try to escape from the target By looking ahead
        /// </summary>
        public static void Evade(NavMeshAgent i_Agent, Transform i_TargeTransform, float i_TargeSpeed)
        {
            Flee(i_Agent, i_TargeTransform.position + (i_TargeTransform.forward * LookAhad(i_Agent, i_TargeTransform, i_TargeSpeed)));
        }

        public static Vector3 TargeDir(Vector3 i_TargetPos, Transform i_Agent)
        {
            return i_TargetPos - i_Agent.position;
        }

        public static float LookAhad(NavMeshAgent i_Agent, Transform i_Target, float i_TargeSpeed)
        {
            return TargeDir(i_Target.position, i_Agent.transform).magnitude / (i_Agent.speed + i_TargeSpeed);
        }

        /// <summary>
        /// What do we want the character to do, when it has no official goal location to goto 
        /// </summary>
        public static void Wander(ref Vector3 io_WanderTarget, NavMeshAgent i_Agent)
        {
            Wander(ref io_WanderTarget, i_Agent, k_WanderRradius, k_WanderDeistance, k_WandrJitter);
        }
        /// <summary>
        /// What do we want the character to do, when it has no official goal location to goto 
        /// </summary>
        public static void Wander(ref Vector3 io_WanderTarget,
            NavMeshAgent i_Agent,
            float i_WanderRradius,
            float i_WanderDeistance,
            float i_WandrJitter)
        {
            io_WanderTarget += new Vector3(GetJitter(i_WandrJitter), 0, GetJitter(i_WandrJitter));
            io_WanderTarget.Normalize();
            io_WanderTarget *= i_WanderRradius;
            Vector3 targetWord = i_Agent.transform.InverseTransformVector(TargetLocal(io_WanderTarget, i_WanderDeistance));
            Seek(i_Agent, targetWord);

            static float GetJitter(float i_WandrJitter)
            {
                return Random.Range(-1.0f, 1.0f) * i_WandrJitter;
            }

            static Vector3 TargetLocal(Vector3 i_WanderTarget, float i_WanderDeistance)
            {
                return i_WanderTarget + new Vector3(0, 0, i_WanderDeistance);
            }
        }

        public static void Hide(NavMeshAgent i_Agent)
        {
            float distance = Mathf.Infinity;
            Vector3 selectedPoint = Vector3.zero;

            foreach (GameObject hidingPlace in Word.Instance.GetHidigSpot())
            {
                Vector3 hidePos = hidingPlace.transform.position +
                    TargeDir(hidingPlace.transform.position, i_Agent.transform).normalized * 5;

                if (IsCloser(i_Agent.transform.position, hidePos, distance, out float o_NewDistance))
                {
                    selectedPoint = hidePos;
                    distance = o_NewDistance;
                }
            }

            Seek(i_Agent, selectedPoint);
        }
        public static void CleverHide(NavMeshAgent i_Agent)
        {
            float mullHidPoint = 5;
            float distance = Mathf.Infinity;
            Vector3 selectedSpot = Vector3.zero;
            Vector3 selectedDir = Vector3.zero;
            GameObject selectedGO = null;

            foreach (GameObject hidingPlace in Word.Instance.GetHidigSpot())
            {
                Vector3 hideDir = TargeDir(hidingPlace.transform.position, i_Agent.transform);
                Vector3 hidePos = hidingPlace.transform.position + hideDir.normalized * mullHidPoint;

                if (IsCloser(i_Agent.transform.position, hidePos, distance, out float o_NewDistance))
                {
                    selectedSpot = hidePos;
                    selectedDir = hideDir;
                    selectedGO = hidingPlace;
                    distance = o_NewDistance;
                }
            }

            Collider hideCol = selectedGO?.GetComponent<Collider>();
            float dist = mullHidPoint * 20;
            _ = hideCol.Raycast(BackRay(selectedSpot, selectedDir), out RaycastHit o_Info, dist);

            Seek(i_Agent, o_Info.point + selectedDir.normalized * mullHidPoint);
        }

        private static Ray BackRay(Vector3 i_SelectedSpot, Vector3 i_Dir)
        {
            return new Ray(i_SelectedSpot, -i_Dir.normalized);
        }

        public static bool CanSeeTarge(NavMeshAgent i_Agent, Transform i_Targe)
        {
            bool res = false;
            Vector3 reyToTarge = TargeDir(i_Targe.position, i_Agent.transform);
            if (CanSeeAngle(i_Agent.transform, reyToTarge) && Physics.Raycast(i_Agent.transform.position, reyToTarge, out RaycastHit o_Info))
            {
                res = o_Info.transform.CompareTag(GameGlobal.TagNames.k_Player);
            }

            return res;
        }

        public static bool CanSeeAngle(Transform i_Agent, Vector3 i_Trget)
        {
            return Vector3.Angle(i_Agent.forward, i_Trget) < k_VisionRange;
        }

        public static bool CanSeeAgent(NavMeshAgent i_Agent, Transform i_Targe)
        {
            return CanSeeAngle(i_Agent.transform, TargeDir(i_Agent.transform.position, i_Targe));
        }

        public static bool IsCloser(Vector3 i_Agent, Vector3 i_From, float i_CurrentDistance, out float o_NewDistance)
        {
            o_NewDistance = Vector3.Distance(i_Agent, i_From);
            return i_CurrentDistance < o_NewDistance;
        }

        public static void Behaviours()
        {
            // is cool donw = false
        }

        public static bool IsTrgetInRange(NavMeshAgent i_Agent, Transform i_Targe, float i_Range)
        {
            return Vector3.Distance(i_Agent.transform.position, i_Targe.position) < i_Range;
        }
    }

}