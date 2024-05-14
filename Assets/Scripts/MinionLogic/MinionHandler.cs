using DependencyInjection;
using UnityEngine;

namespace SlayTheKing
{
    public class MinionHandler : MonoBehaviour, IInitialize, IMinionHandler, IDependencyProvider
    {
        [Inject(InjectSource.Parent)] private IObserverData<CardData> cardObserverData;
        [Inject(InjectSource.Parent)] private IObserverData<MinionData> minionObserverData;

        [Provide]
        private IMinionHandler ProvideMinionHandler()
        {
            return this;
        }

        public void Initialize()
        {
            cardObserverData?.AddListener(InitializeMinion);
        }

        private void OnDestroy()
        {
            cardObserverData?.RemoveListener(InitializeMinion);
        }

        public void InitializeMinion(CardData cardData)
        {
            var minionData = (MinionData)cardData;
            minionObserverData.Data = minionData;
        }

        public bool Attack(IBoardSlotHandler[] targets)
        {
            var attackStrategy = minionObserverData?.Data?.Minion?.AttackStrategy?.CurrentValue?.Value;
            attackStrategy?.ExecuteAttack(this, targets);
            return attackStrategy != null;
        }

        public void DealDamage(IReceiveDamage damageReceiver)
        {
            damageReceiver.ReceiveDamage(minionObserverData.Data.Minion.AttackDamage);
        }

        public void ReceiveDamage(int damage)
        {
            var minion = minionObserverData.Data.Minion;
            var armorLeft = minion.Armor - damage;

            if (armorLeft > 0)
            {
                minion.Armor.UpdateCurrent(armorLeft);
                return;
            }

            minion.Armor.UpdateCurrent(0);
            minion.Health.UpdateCurrent(armorLeft);
        }

        public bool IsAlive()
        {
            if (minionObserverData == null)
            {
                return false;
            }

            if (minionObserverData.Data == null)
            {
                return false;
            }

            if (minionObserverData.Data.Minion == null)
            {
                return false;
            }

            return minionObserverData.Data.Minion.Health > 0;
        }
    }
}