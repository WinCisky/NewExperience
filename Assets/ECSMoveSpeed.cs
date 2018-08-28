using System;
using Unity.Entities;

namespace Two.ECS
{
    [Serializable]
    public struct MoveSpeed : IComponentData
    {
        public float value;
    }

    public class ECSMoveSpeed : ComponentDataWrapper<MoveSpeed> { }
}
