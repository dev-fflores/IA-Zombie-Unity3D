using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float _maxHealth = 100;
    public float _currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage) {
        _currentHealth -= damage;
        Debug.Log("Enemy " + this.name + " has: " + _currentHealth);
        if (_currentHealth <= 0) {
            Die();
        }
    }
    void Die() {
        Debug.Log("Enemy " + this.name + " is died!");
    }
}
