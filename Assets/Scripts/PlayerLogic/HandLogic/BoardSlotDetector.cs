using System.Collections.Generic;
using DependencyInjection;
using UnityEngine;

namespace SlayTheKing
{
    public class BoardSlotDetector : MonoBehaviour
    {
        [Inject(InjectSource.Parent)] private IObserverData<IBoardSlotInteractions> boardSlotObserverData;
        private IBoardSlotInteractions currentBoardSlot;
        private List<IBoardSlotInteractions> nearbyBoardSlot = new List<IBoardSlotInteractions>();

        public IBoardSlotInteractions CurrentSummoner
        {
            get => currentBoardSlot;
            private set
            {
                currentBoardSlot = value;
                boardSlotObserverData.Data = currentBoardSlot;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            IBoardSlotInteractions summoner = other.GetComponent<IBoardSlotInteractions>();
            if (summoner != null && !nearbyBoardSlot.Contains(summoner))
            {
                nearbyBoardSlot.Add(summoner);
            }

            UpdateClosestSummoner();
        }

        private void OnTriggerExit(Collider other)
        {
            IBoardSlotInteractions summoner = other.GetComponent<IBoardSlotInteractions>();
            if (summoner != null && nearbyBoardSlot.Contains(summoner))
            {
                nearbyBoardSlot.Remove(summoner);
                if (CurrentSummoner == summoner)
                {
                    CurrentSummoner = null;
                    UpdateClosestSummoner();
                }
            }
        }

        private void UpdateClosestSummoner()
        {
            IBoardSlotInteractions closestSummoner = null;
            float closestSqrDistance = float.MaxValue;
            Vector3 currentPosition = transform.position;

            foreach (var summoner in nearbyBoardSlot)
            {
                if (summoner == null || !(summoner is MonoBehaviour))
                {
                    continue;
                }

                Transform summonerTransform = (summoner as MonoBehaviour).transform;
                if (summonerTransform == null)
                {
                    continue;
                }

                Vector3 direction = currentPosition - summonerTransform.position;
                float sqrDistance = direction.sqrMagnitude;
                if (sqrDistance < closestSqrDistance)
                {
                    closestSqrDistance = sqrDistance;
                    closestSummoner = summoner;
                }
            }

            if (closestSummoner != null && closestSummoner != CurrentSummoner)
            {
                CurrentSummoner = closestSummoner;
            }
        }
    }
}