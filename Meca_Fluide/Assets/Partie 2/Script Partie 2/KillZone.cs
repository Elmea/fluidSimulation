using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private ParticleManager particleManager;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ParticuleEau"))
        {
            collision.gameObject.GetComponent<Particle>().isActive = false;
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
            //particleManager.particles.Remove(collision.gameObject.GetComponent<Particle>());
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
