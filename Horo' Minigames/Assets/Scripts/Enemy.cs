using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float stopDistance = 1f;
    [SerializeField] private float groundDistance = 0.5f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask terrainLayer;

    public float GroundDistance => groundDistance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayer(PlayerController a)
    {
        player = a;
    }

    public void Move()
    {
        if (rb == null || player == null)
            return;
        Vector3 velocity = rb.linearVelocity;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > stopDistance)
        {
            velocity = (player.transform.position - transform.position).normalized * speed;
        }
        else
        {
            velocity = Vector3.zero;
        }
        velocity.y = 0;
        rb.linearVelocity = velocity;
    }
}
