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

        if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            Vector2 bounceDirection = new Vector2(-0.3f, 0.5f);
            
            Project(bounceDirection);
        }
    }
}
