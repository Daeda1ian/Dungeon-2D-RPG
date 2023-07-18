using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField] private int damage = 1;
    [SerializeField] private float delayAttack = 0.5f;

    private List<Health> collidedHealthObjects;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool attackBlocked = false;

    private void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collidedHealthObjects = new List<Health>();
    }

    private void Update () {
        AnimateWeapon();
    }

    private void AnimateWeapon() {
        Attack();
    }

    private void Attack() {
        if (attackBlocked) { return; }
        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("Swing");
            attackBlocked = true;
            StartCoroutine(DelayAttack());
            collidedHealthObjects.Clear();
        }
    }

    IEnumerator DelayAttack() {
        yield return new WaitForSeconds(delayAttack);
        attackBlocked = false;
    }

    public void ChangeSortingLayer(float rotation) {
        if (rotation > 0) {
            spriteRenderer.sortingOrder = -1;
        }
        else if (rotation < 0) {
            spriteRenderer.sortingOrder = 1;
        }
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health == null) { return; }
        if (collidedHealthObjects.Contains(health)) { return; }

        health.Hit(damage);
        collidedHealthObjects.Add(health);
    }

}
