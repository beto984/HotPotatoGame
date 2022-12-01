using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField] private float shootingDistance = 0f; 
    public Potato potatoPrefab;
    private bool potatoInHand = true;

    [SerializeField] private LayerMask jumpableGround; 
    private enum MovementState { idle, running, jumping, falling,idle_no_potato }

    [SerializeField] private AudioSource jumpSoundEffect;
    
    private float dirX = 0f;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
   private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveSpeed*dirX, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(0, jumpForce);
            
        }
        if (Input.GetKeyDown("space"))
        {
            Shoot(dirX, shootingDistance);
        }
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;

        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else if(potatoInHand)
        {
            state = MovementState.idle;
        }
        else
        {
            state = MovementState.idle_no_potato;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state",(int) state);
    }
    
    private void Shoot(float dirX, float shootingDistance)
    {
        Vector3 spawnPosition;
        if (sprite.flipX)
        {
            spawnPosition = new Vector3(this.transform.position.x - shootingDistance, this.transform.position.y,
                this.transform.position.z);
        }
        else
        {
            spawnPosition = new Vector3(this.transform.position.x + shootingDistance, this.transform.position.y,
                this.transform.position.z);
        }
        

        if (potatoInHand)
        {
            if (dirX > 0)
            {
                Potato potato = Instantiate(potatoPrefab, spawnPosition, this.transform.rotation);
                potato.Project(transform.right);
            }
            else if(dirX < 0)
            {
                Potato potato = Instantiate(potatoPrefab, spawnPosition, this.transform.rotation);
                potato.Project(-transform.right);
            }
            else
            {
                if (sprite.flipX)
                {
                    Potato potato = Instantiate(potatoPrefab, spawnPosition, this.transform.rotation);
                    potato.Project(-transform.right);
                }
                else
                {
                    Potato potato = Instantiate(potatoPrefab, spawnPosition, this.transform.rotation);
                    potato.Project(transform.right);
                }
            }
            
            potatoInHand = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Potato"))
        {
            potatoInHand = true; 
        }
    }
    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround); // creates a box around the player that has the same size as the collider of the player 
    }
}
