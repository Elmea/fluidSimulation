using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float rho = 1.0f;//densiter
    public float mass = 0.02f;//mass
    public float pressure;

    public bool isActive = true;

    public Vector2 force = new Vector3( 0.0f, 0.0f );
    public Vector2 velocity = new Vector3( 0.0f, 0.0f);
    private Vector2 g = new Vector2(0.0f, -9.81f); 

    private void Awake()
    {
        velocity = new Vector3( 0.0f, 0.0f);
    }

    public void UpdatePosition(float deltaT)
    {
        velocity += g + force / rho  * deltaT;
        transform.position += deltaT * new Vector3(velocity.x, velocity.y, 0);
    }

    private void LateUpdate()
    {
        force = new Vector3( 0.0f, 0.0f);
    }
}
