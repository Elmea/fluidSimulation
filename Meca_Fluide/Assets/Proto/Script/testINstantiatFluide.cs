using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testINstantiatFluide : MonoBehaviour
{
    [SerializeField] GameObject fluideParticle;
    public bool open = false;
    Transform test; 
    // Start is called before the first frame update
    void Start()
    {
        test = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(open)
        {
            Instantiate(fluideParticle, transform.position, transform.rotation);
        }
    }
}
