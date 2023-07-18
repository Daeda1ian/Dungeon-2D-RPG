using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField] private int health = 1;

    public void Hit(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
    
}
