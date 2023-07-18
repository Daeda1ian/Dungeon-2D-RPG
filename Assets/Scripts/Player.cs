using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float speed = 2f;
    [SerializeField] private float speedCoefY = 0.75f;

    private PlayerInput playerInput;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private RaycastHit2D raycastHit2D;
    private GameObject body;

    private void Start () {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        body = transform.Find("Body").gameObject;
    }

    private void Update () {
        Run();
        SetAnimation();
    }

    private void Run() {
        Vector2 pos = transform.position;
        float newX = playerInput.Horizontal * Time.deltaTime * speed;
        float newY = playerInput.Vertical * Time.deltaTime * speed * speedCoefY;
        raycastHit2D = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(newX, 0), 
                                        Mathf.Abs(newX), LayerMask.GetMask("Blocking"));
        if (!raycastHit2D) {
            pos.x += newX;
        }
        raycastHit2D = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(0, newY),
                                        Mathf.Abs(newY), LayerMask.GetMask("Blocking"));
        if (!raycastHit2D) {
            pos.y += newY;
        }

        transform.position = pos;
    }

    private void SetAnimation() {
        bool isRunning = playerInput.Horizontal != 0 || playerInput.Vertical != 0;
        animator.SetBool("IsRunning", isRunning);
    }

    public void FlipSprite(float value) {
        body.transform.localScale = new Vector2(value, transform.localScale.y);
    }
}
