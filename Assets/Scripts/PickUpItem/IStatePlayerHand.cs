
using static UnityEditor.ShaderData;

namespace Assets.Scripts.PickUpItem
{
    public interface IStatePlayerHand
    {
        public void UpdateByState();
        public void InitState();

        public bool IsPassStage();
    }
}
