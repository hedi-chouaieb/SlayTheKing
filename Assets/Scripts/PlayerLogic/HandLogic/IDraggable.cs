using UnityEngine;

namespace SlayTheKing
{
    public interface IDraggable
    {
        void StartDrag();
        void Drag(Vector3 initialPoint);
        void EndDrag();
    }
}