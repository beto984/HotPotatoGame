using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // 2 full rotation of the image per second
    private void Update()
    {
        transform.Rotate(0,0, Time.deltaTime * speed * 360);
    }
}
