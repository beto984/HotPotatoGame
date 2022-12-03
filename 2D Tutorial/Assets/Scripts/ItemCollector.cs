using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioSource collectSoundEffect;
    private int cherries = 0;

    [SerializeField] private TextMeshProUGUI cherriesText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            collectSoundEffect.Play();
            cherries ++;
            cherriesText.text = "Cherries : " + cherries;
        }
    }
}
