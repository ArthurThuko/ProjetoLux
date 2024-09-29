using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspelhoController : MonoBehaviour
{
    private bool isHholding = false;
    private Transform player;
    private Vector3 offset;
    private Rigidbody2D rb;
    public float pickUpRange = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        if (isHholding)
        {
            transform.position = player.position + offset;

            if (Input.GetButtonDown("Fire2"))
            {
                DropObject();
            }
        }
        else if(distanceToPlayer <= pickUpRange && Input.GetButtonDown("Fire2"))
        {
            PickUpObject();
        }
    }

    void PickUpObject()
    {
        isHholding=true;
        rb.isKinematic = true;
        offset = transform.position - player.position;
    }

    void DropObject()
    {
        isHholding = false;
        rb.isKinematic = false;
    }
}
