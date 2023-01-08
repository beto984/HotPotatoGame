using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private BoxCollider2D coll;
    [SerializeField] public float speed = 5;
    [SerializeField] public float maxLifeTime = 20; 
    [SerializeField] private LayerMask jumpableGround;
    private GameObject player;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);
        
    }

    public void Update()
    {
        if (IsGrounded())
        {
            Debug.Log("enter is grounded");
            player.gameObject.GetComponent<PlayerLife>().Die();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //destroys the potato when it touches the target
        if (col.gameObject.tag == "Finish")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Debug.Log("Destroy potato");
            Destroy(this.gameObject);
        }
        

        
        // if the potato hits the CapsuleCollider which refers to the collision with knives
        // the potato gets destroyed but the enemy survives
        if (col.collider == col.gameObject.GetComponent<CapsuleCollider2D>())
        { 
            Destroy(this.gameObject);
            player.gameObject.GetComponent<PlayerLife>().Die();
            //Calls the methods in the EnemyChef script that shows the animation for slicing the potato
            col.gameObject.GetComponent<EnemyChef>().SlicePotato();
        }
        
        // if the potato hits the PolygonCollider which refers to the collision with the enemies body
        // the potato destroys the enemy
        if (col.collider == col.gameObject.GetComponent<PolygonCollider2D>())
        {
            Debug.Log("Enemy got hit");
            Destroy(col.gameObject);
            this._rigidbody.velocity = new Vector2(0, 0);
            Vector2 bounceDirection = new Vector2(0f, 0.34f);
            
            Project(bounceDirection);
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround); // creates a box around the player that has the same size as the collider of the player 
    }
}
