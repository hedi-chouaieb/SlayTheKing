using UnityEngine;
using UnityEngine.InputSystem;

namespace SlayTheKing
{
    public class DraggableController : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        private bool isDragging = false;
        private bool isFirstClick = false;
        private IDraggable currentDraggable;
        private float initialDragDistance;

        private void OnValidate()
        {
            if (mainCamera == null)
            {
                Debug.LogError("Main camera reference is required.", gameObject);
            }
        }

        public void OnDrag(InputAction.CallbackContext context)
        {
            if (!isDragging) return;

            if (isFirstClick)
            {
                isFirstClick = false;
                Ray initialRay = mainCamera.ScreenPointToRay(context.ReadValue<Vector2>());

                if (Physics.Raycast(initialRay, out RaycastHit hit) && hit.collider != null)
                {
                    var clickedObject = hit.collider.gameObject;
                    if (clickedObject.TryGetComponent(out currentDraggable))
                    {
                        initialDragDistance = Vector3.Distance(clickedObject.transform.position, mainCamera.transform.position);
                        currentDraggable.StartDrag();
                    }
                }
            }

            if (currentDraggable != null)
            {
                Ray dragRay = mainCamera.ScreenPointToRay(context.ReadValue<Vector2>());
                currentDraggable.Drag(dragRay.GetPoint(initialDragDistance));
            }
        }

        public void OnStartDrag(InputAction.CallbackContext context)
        {
            isDragging = context.ReadValue<float>() > 0;

            if (!isDragging && currentDraggable != null)
            {
                currentDraggable.EndDrag();
                currentDraggable = null;
            }
            isFirstClick = isDragging;
        }
    }

}