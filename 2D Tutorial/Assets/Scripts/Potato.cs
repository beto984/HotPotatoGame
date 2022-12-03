using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] public float speed = 5;
    [SerializeField] public float maxLifeTime = 20; 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);
        
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
            Debug.Log("Potato got sliced");
            Destroy(this.gameObject);
        }
        
        // if the potato hits the PolygonCollider which refers to the collision with the enemies body
        // the potato destroys the enemy
        if (col.collider == col.gameObject.GetComponent<PolygonCollider2D>())
        {
            Debug.Log("Enemy got hit");
            Destroy(col.gameObject);
            this._rigidbody.velocity = new Vector2(0, 0);
            Vector2 bounceDirection = new Vector2(0f, 0.5f);
            
            Project(bounceDirection);
        }
    }
}
