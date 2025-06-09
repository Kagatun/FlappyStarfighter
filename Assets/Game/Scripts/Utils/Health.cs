using System;
using UnityEngine;

namespace Scripts.Utils
{
    public class Health
    {
        public event Action <int> Hited;
        public event Action <int> Healed;
        public event Action Deceased;

        public int Value { get; private set; }
        public int MaxValue { get;}

        public Health(int defaultHealth)
        {
            MaxValue = defaultHealth;
            Value = MaxValue;
        }

        public void TakeDamage(int damage)
        {
            Value -= damage;
            Value = Mathf.Clamp(Value, 0, MaxValue);
            Hited?.Invoke(damage);

            if (Value <= 0)
                Die();
        }
        
        public void Die() =>
            Deceased?.Invoke();

        public void Reset()
        {
            Value = MaxValue;
            Healed?.Invoke(MaxValue);
        }
    }
}