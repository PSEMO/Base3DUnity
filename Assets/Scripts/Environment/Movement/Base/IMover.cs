using UnityEngine;

namespace PSEMO.Environment.Movement
{
    public interface IMover
    {
        public virtual Vector3 GetVelocity() => Vector3.zero;
    }
}