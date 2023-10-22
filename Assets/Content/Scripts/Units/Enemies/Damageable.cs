using System;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    public event Action Damage;
    
    public void TakeDamage() => Damage?.Invoke();
}