using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryDog : MonoBehaviour
{
    [SerializeField] public float enemyRunSpeed = 5f;
    
    private Rigidbody2D enemyRigidbody2D;

    public Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        //init EnemyRigidBody
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingLeft())
        {
            //Makes enemy run in the horizontal axis -> just add to x-axis
            enemyRigidbody2D.velocity = new Vector2(-enemyRunSpeed, 0f);
        }
        else
        {
            //Makes enemy run in the horizontal axis -> just add to x-axis
            enemyRigidbody2D.velocity = new Vector2(enemyRunSpeed, 0f);
        }
        
    }

    //Gets triggered when the BoxCollider in front of the enemy leaves the "Floor ground" layer
    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipSprite();
    }

    //Flips/Mirrors the sprite of the enemy on the x-axis -> seems like it changed walking direction  
    private void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(enemyRigidbody2D.velocity.x), 1f);
    }

    //returns true if the enemy is facing to the left side
    private bool IsFacingLeft()
    {
        return transform.localScale.x > 0;
    }

    public void SlicePotato()
    {
        myAnimator.SetTrigger("eat");
    }
}