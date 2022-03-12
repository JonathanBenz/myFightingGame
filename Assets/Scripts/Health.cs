using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float dmgAmount = 10f;
    
    public float GetHealth() { return health; }
    private void Update()
    {
        Die();
    }
    void Die()
    {
        if (health <= 0)
            Destroy(gameObject);
    }
    public void TakeDamage()
    {
        health -= dmgAmount;
    }
}
