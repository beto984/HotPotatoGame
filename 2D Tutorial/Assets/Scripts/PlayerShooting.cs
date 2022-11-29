using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float shootingDistance = 0f; 
    public Potato potatoPrefab;
    private bool potatoInHand = true;
    private float dirX = 0f;
    
    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown("space"))
        {
            Shoot(dirX, shootingDistance);
        }
    }

    private void Shoot(float dirX, float shootingDistance)
    {
        Vector3 spawnPosition = new Vector3(this.transform.position.x + shootingDistance, this.transform.position.y,
            this.transform.position.z);

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
}
