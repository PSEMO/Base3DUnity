using System.Collections.Generic;
using UnityEngine;

namespace PSEMO.Environment.Movement
{
    public class VelocityOffsetter : MonoBehaviour
    {
        [SerializeField] private GameObject _mover;
        private IMover mover;

        private List<IVelocityOffsettable> offsettables = new();

        void Awake()
        {
            mover = _mover.GetComponent<IMover>();
        }

        private void OnCollisionEnter(Collision col)
        {
            HandleEnter(col.collider);
        }
        private void OnTriggerEnter(Collider col)
        {
            HandleEnter(col);
        }

        private void OnCollisionExit(Collision col)
        {
            HandleExit(col.collider);
        }
        private void OnTriggerExit(Collider col)
        {
            HandleExit(col);
        }

        private void HandleEnter(Collider col)
        {
            if (col.TryGetComponent(out IVelocityOffsettable offsettable))
            {
                if (!offsettables.Contains(offsettable))
                {
                    offsettables.Add(offsettable);
                    offsettable.AddVelocityOffset(mover);
                }
            }
        }

        private void HandleExit(Collider col)
        {
            if (col.TryGetComponent(out IVelocityOffsettable offsettable))
            {
                if (offsettables.Contains(offsettable))
                {
                    offsettables.Remove(offsettable);
                    offsettable.RemoveVelocityOffset(mover);
                }
            }
        }
    }
}
