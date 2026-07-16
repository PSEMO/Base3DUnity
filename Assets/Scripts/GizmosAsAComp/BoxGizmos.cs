using UnityEngine;

namespace PSEMO.GizmosAsAComp
{
    [RequireComponent(typeof(BoxCollider))]
    public class BoxGizmos : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Color boxColor = new(0, 0, 0, 0.3f);

        BoxCollider boxCollider;

        private void OnDrawGizmos()
        {
            if (boxCollider == null)
                boxCollider = GetComponent<BoxCollider>();
            
            if (boxCollider != null)
            {
                Matrix4x4 oldMatrix = Gizmos.matrix;
                Color oldColor = Gizmos.color;

                Gizmos.color = boxColor;
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
                Gizmos.DrawCube(boxCollider.center, boxCollider.size);
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);

                Gizmos.matrix = oldMatrix;
                Gizmos.color = oldColor;
            }
        }
#endif
    }
}