using UnityEngine;

namespace SlayTheKing
{
    public class Ability : ScriptableObject
    {
        public string abilityName;
        public int abilityPower;
        public Sprite abilityIcon;

        public void ActivateAbility(ITarget target)
        {
            Debug.Log($"Using {abilityName} on target!");
            target.ReceiveAbility(this);
        }
    }
}