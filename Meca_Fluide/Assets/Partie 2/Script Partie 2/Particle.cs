using UnityEngine;

public class Particle : MonoBehaviour
{
    public float rho = 0.1f;
    public float mass = 0.02f;
    public float pressure;

    public Vector2 acceleration = new Vector3( 0.0f, 0.0f );
    public Vector2 force = new Vector3( 0.0f, 0.0f );
    public Vector2 velocity = new Vector3( 0.0f, 0.0f);

    public float stiffness = 10000;
    public float referenceDensity = 25.0f;
    public float dynamicViscosity = 500.0f;
    
    private void Awake()
    {
        velocity = new Vector3( 0.0f, 0.0f);
    }

    // Gestion de la collision
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
            return;

        Vector2 externalForce = new Vector2();
        Vector2 normal = collision.GetContact(0).normal;
        
        // Calcul de la force de reaction
        float mag = -(mass + 100.0f) * Vector2.Dot(this.velocity, normal);
        externalForce = mag * normal;
        
        // On ajoute la force de reaction au systeme
        force += externalForce;
    }

    // Application de la troisieme loi de newton et du system d'Euler
    public void UpdatePosition(float deltaT)
    {
        acceleration = force * mass;
        velocity += acceleration * deltaT;
        transform.position += deltaT * new Vector3(velocity.x, velocity.y, 0);
        force = new Vector2(0, 0);
    }
}
