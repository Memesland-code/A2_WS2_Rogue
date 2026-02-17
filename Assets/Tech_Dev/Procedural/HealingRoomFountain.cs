using UnityEngine;

namespace Tech_Dev.Procedural
{
    public class HealingRoomFountain : MonoBehaviour
    {
        [SerializeField] private float _healAmount;
        private bool _activated;

        public float GetFountainHeal()
        {
            if (_activated) return 0;
            return _healAmount;
        }
    }
}
