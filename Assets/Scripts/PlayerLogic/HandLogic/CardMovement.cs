using DependencyInjection;
using UnityEngine;
using UnityEngine.Events;

namespace SlayTheKing
{
    public class CardMovement : MonoBehaviour, IDraggable
    {
        [SerializeField] private float dragSpeed = .1f;
        [Inject(InjectSource.Parent)] private IObserverData<CardInteractionState> cardInteractionStateObserverData;
        private bool isDragStarted = false;
        private Vector3 direction;
        private Vector3 velocity;
        private float startingYPos;

        public void Highlight()
        {
            cardInteractionStateObserverData.Data = CardInteractionState.HIGHLIGHTED;
        }

        public void StartDrag()
        {
            startingYPos = transform.position.y;
            isDragStarted = true;
            cardInteractionStateObserverData.Data = CardInteractionState.SELECTED;
        }

        public void Drag(Vector3 initialPoint)
        {
            direction = initialPoint;
            cardInteractionStateObserverData.Data = CardInteractionState.DRAGGING;
        }

        public void EndDrag()
        {
            isDragStarted = false;
            cardInteractionStateObserverData.Data = CardInteractionState.DROP;
        }

        private void Update()
        {
            if (!isDragStarted)
            {
                return;
            }

            var nextPosition = Vector3.SmoothDamp(transform.position, direction, ref velocity, dragSpeed);
            nextPosition.y = startingYPos;
            transform.position = nextPosition;
        }
    }
}