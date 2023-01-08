using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSoundEffect;
    private bool levelCompleted = false;
    private Animator anim;

    void Start()
    {
        finishSoundEffect = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Potato") && !levelCompleted)
        {   
            Debug.Log("Potato hits flag");
            levelCompleted = true;
            anim.SetBool("potatoDelivered",true);
            Invoke("CompleteLevel", 2f);
            finishSoundEffect.Play();

        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
