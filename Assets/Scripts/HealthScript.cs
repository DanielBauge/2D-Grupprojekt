using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] public float maxHealth;  
    [SerializeField] public float Health;

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
