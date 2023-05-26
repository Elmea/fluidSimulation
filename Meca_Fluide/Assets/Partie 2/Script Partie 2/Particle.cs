using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float rho = 0.1f;
    public float mass = 0.02f;
    public float pressure;

    private Vector2 lastVelocity = new Vector3( 0.0f, 0.0f);

    public Vector2 acceleration = new Vector3( 0.0f, 0.0f );
    public Vector2 force = new Vector3( 0.0f, 0.0f );
    public Vector2 velocity = new Vector3( 0.0f, 0.0f);

    private void Awake()
    {
        velocity = new Vector3( 0.0f, 0.0f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
            return;

        Vector2 externalForce = new Vector2();
        Vector2 normal = collision.GetContact(0).normal;
        float mag = -(mass + 100.0f) * Vector2.Dot(this.lastVelocity, normal);
        Debug.Log(mag);

        externalForce = mag * normal;
        
        force += externalForce;
    }

    public void UpdatePosition(float deltaT)
    {
        acceleration = force * mass;
        lastVelocity = velocity;  
        velocity += acceleration * deltaT;
        transform.position += deltaT * new Vector3(velocity.x, velocity.y, 0);
        force = new Vector2(0, 0);
    }
}
