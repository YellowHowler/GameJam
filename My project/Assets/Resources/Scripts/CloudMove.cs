using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    private float moveSpeed;
    void Start()
    {
        Destroy(gameObject, 15f);
        moveSpeed = Random.Range(0.5f, 1.5f);
    }
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + moveSpeed * Time.deltaTime, transform.localPosition.y, 10);
    }
}
