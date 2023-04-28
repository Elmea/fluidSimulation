using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NiveauWater : MonoBehaviour
{
    [SerializeField] Slider SliderTailleCuve;
    [SerializeField] Slider SliderHauteurReservoir;
    [SerializeField] Slider SliderDiamtru;
    [SerializeField] TextMeshProUGUI TextTailleCuve;
    [SerializeField] TextMeshProUGUI TextHauteurReservoir;
    [SerializeField] TextMeshProUGUI TextDiamtru;


    [SerializeField] GameObject flecheu;
    [SerializeField] GameObject Ouverture;
    [SerializeField] GameObject paroiGauche;
    [SerializeField] GameObject paroiBasse;

    [SerializeField] GameObject positionOrifice;

    public float hauteurReservoir;
    public float H;
    public float diamtru;
    public float lineDeltaTime;
    
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

    private List<Vector3> points;

    private LineRenderer line;
    
    public bool startSimulation = false;

    // Start is called before the first frame update
    void Start()
    {

        SliderTailleCuve.value = H;
        SliderHauteurReservoir.value = hauteurReservoir;
        SliderDiamtru.value = diamtru;

        deltatime = 0.01f;
        SurfaceEau = 16;
        ht = H;
        VolumeEau = SurfaceEau * ht;
        speedWater = Mathf.Sqrt(2*g*ht);
        distanceConstant = Mathf.Sqrt(2 * hauteurReservoir / g);
        BaseArrowPos = flecheu.transform.position;
        baseRectanglePos = transform.position;
        baseRectangleScale = transform.localScale;
        line = GetComponent<LineRenderer>();
        points = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        H = SliderTailleCuve.value;
        hauteurReservoir = SliderHauteurReservoir.value;
        diamtru = SliderDiamtru.value;
        TextTailleCuve.text = "H: " + H;
        TextHauteurReservoir.text = "Hauteur Reservoir: " + hauteurReservoir;
        TextDiamtru.text = "Diametre trou: " + diamtru;
        Ouverture.transform.position = new Vector3(Ouverture.transform.position.x, baseRectanglePos.y + 2.35f + SliderDiamtru.value + hauteurReservoir);
        paroiBasse.transform.position = new Vector3(paroiBasse.transform.position.x, baseRectanglePos.y + hauteurReservoir);
        paroiGauche.transform.position = new Vector3(paroiGauche.transform.position.x, baseRectanglePos.y + 2.35f + hauteurReservoir);


        if (startSimulation)
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
            flecheu.transform.position = new Vector3(BaseArrowPos.x + 0.5f * flecheu.transform.localScale.x, BaseArrowPos.y, -0.5f);

            points.Clear();
            float yPos = 1;
            float xPos;
            float time = 0;
            float limit = Mathf.Sqrt(2 * hauteurReservoir / g);
            int pointAdded = 0;
            while (time < limit)
            {
                yPos = hauteurReservoir - 0.5f * g * time * time;
                xPos = positionOrifice.transform.position.x + time * speedWater;

                points.Add(new Vector2(xPos, yPos));
                time += lineDeltaTime;
                pointAdded++;
            }

            line.positionCount = pointAdded;
            line.SetPositions(points.ToArray());
        }
        else
        {
            line.positionCount = 0;

        }

        transform.position = new Vector3(transform.position.x, baseRectanglePos.y + 2.35f + hauteurReservoir);

    }
}
