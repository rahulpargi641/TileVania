using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rigidBody2D;
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }
   

    // Update is called once per frame
    void Update()
    {
        rigidBody2D.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FlipEnemy();
    }

    private void FlipEnemy()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidBody2D.velocity.x)), 1f);
    }
}
