using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NiveauWater : MonoBehaviour
{
    [SerializeField] GameObject flecheu;
    public float hauteurReservoir;
    public float H;
    public float diamtru;
    private float ht;
    private Vector2 baseRectanglePos;
    private Vector2 baseRectangleScale;
    private float tailleFleche;
    private Vector2 BaseArrowPos;
    private float speedWater;
    private float g = 9.81f;
    private float SurfaceEau;
    private float VolumeEau;
    private float Debit;
    private float deltatime;
    private float distanceConstant;

    public bool startSimulation = false;

    // Start is called before the first frame update
    void Start()
    {
        deltatime = 0.01f;
        SurfaceEau = 16;
        ht = H;
        VolumeEau = SurfaceEau * ht;
        speedWater = Mathf.Sqrt(2*g*ht);
        distanceConstant = Mathf.Sqrt(2 * hauteurReservoir / g);
        BaseArrowPos = flecheu.transform.position;
        baseRectanglePos = transform.position;
        baseRectangleScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(startSimulation)
        {
            if (tailleFleche <= 0.05f)
            {
                startSimulation = false;
            }

            speedWater = Mathf.Sqrt(2 * g * ht);
            Debit = diamtru * speedWater;
            VolumeEau -= Debit * deltatime;
            ht = VolumeEau / SurfaceEau;

            transform.localScale = new Vector2(baseRectangleScale.x, ht);
            transform.position = new Vector3(baseRectanglePos.x, baseRectanglePos.y + 0.5f * transform.localScale.y, 5);

            tailleFleche = distanceConstant * speedWater;
            Debug.Log(tailleFleche);
            flecheu.transform.localScale = new Vector2(tailleFleche, 0.1f);
            flecheu.transform.position = new Vector3(BaseArrowPos.x + 0.5f * flecheu.transform.localScale.x, BaseArrowPos.y);
        }
        

    }
}
