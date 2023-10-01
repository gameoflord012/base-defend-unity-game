using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Human : MonoBehaviour
{
    [SerializeField]
    public float speed = 0;

    public UnityEvent onFigureStartMoving;
    public UnityEvent onFigureStopMoving;
    public UnityEvent onFigrueJumping;

    Animator animator;
    SpriteRenderer spriteRenderer;
    Transform model;
    new Rigidbody2D rigidbody2D;
    EffectController effectController;

    bool isFigureMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        effectController = GetComponentInChildren<EffectController>();
        model = transform.Find("model");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = UpdateMovement();

        // transform.position += moveDir * speed * Time.deltaTime;
        rigidbody2D.velocity = moveDir.normalized * speed;

        if (moveDir.x != 0)
        {
            model.localScale = new Vector3(moveDir.x < 0 ? -1 : 1, model.localScale.y, model.localScale.z);
        }

        if (!isFigureMoving)
        {
            if (moveDir.magnitude > 0)
            {
                isFigureMoving = true;
                onFigureStartMoving.Invoke();
            }
        }
        else
        {
            bool isStopping = Mathf.Approximately(moveDir.magnitude, 0);
            if (isStopping)
            {
                isFigureMoving = false;
                onFigureStopMoving.Invoke();
            }
        }
    }

    private Vector3 UpdateMovement()
    {
        Vector3 moveDir = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDir.y -= 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveDir.y += 1;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            GetComponent<Attacker>().AttackTargets();
        }

        return moveDir;
    }
}
