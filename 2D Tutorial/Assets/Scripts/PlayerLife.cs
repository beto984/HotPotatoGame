using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerLife : MonoBehaviour
{
    
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private Image[] lives;
    private int livesRemaining;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        livesRemaining = lives.Length;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }   
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy collider");
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Oven"))
        {
            anim.SetBool("potatoInHand", true);
        }
        if (collision.gameObject.CompareTag("fire"))
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("ENTERS DIE METHOD");
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        anim.SetInteger("state", 0);

        Invoke("RestartLevel",2);
    }

    private void LoseLife()
    {
        //Lose a life and remove it from the screen
        livesRemaining--;
        lives[livesRemaining].enabled = false;
        if (livesRemaining == 0)
        {
            Die();
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
