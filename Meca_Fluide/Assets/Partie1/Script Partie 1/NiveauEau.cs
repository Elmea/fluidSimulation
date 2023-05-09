using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NiveauEau : MonoBehaviour
{
    [SerializeField] Slider SliderTailleCuve;
    [SerializeField] Slider SliderHauteurReservoir;
    [SerializeField] Slider SliderDiamtru;
    [SerializeField] TextMeshProUGUI TextTailleCuve;
    [SerializeField] TextMeshProUGUI TextHauteurReservoir;
    [SerializeField] TextMeshProUGUI TextDiamOrifice;

    [SerializeField] GameObject Eau;

    [SerializeField] GameObject fleche;
    [SerializeField] GameObject Ouverture;
  
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
    private float VEau;
    private float g = 9.81f;
    private float SurfaceEau;
    private float VolumeEau;
    private float Debit;
    private float deltatime;
    private Vector2 baseOuverturePos;
    private float densite = 0;

    private float timer=0;

    private List<Vector3> points;

    private LineRenderer line;
    
    public bool startSimulation = false;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponentInChildren<LineRenderer>();
        baseRectanglePos = Eau.transform.localPosition;
        baseRectangleScale = Eau.transform.localScale;
        BaseArrowPos = fleche.transform.position;
        baseOuverturePos = Ouverture.transform.localPosition;
        Reset();
    }

    public void Reset()
    {
        line.positionCount = 0;

        SliderTailleCuve.value = H;
        SliderHauteurReservoir.value = hauteurReservoir;
        SliderDiamtru.value = diamtru;

        deltatime = 0.01f;
        SurfaceEau = 16;
        ht = H;
        VolumeEau = SurfaceEau * ht;
        VEau = Mathf.Sqrt(2 * g * ht);


        points = new List<Vector3>();
        Eau.transform.localScale = new Vector2(baseRectangleScale.x, ht);
        Eau.transform.localPosition = new Vector3(Eau.transform.localPosition.x, baseRectanglePos.y + 0.5f * Eau.transform.localScale.y, 1.0f);

        fleche.transform.position = new Vector3(BaseArrowPos.x, BaseArrowPos.y, -10.5f);
        fleche.transform.localScale = new Vector2(0.5f, 0.07f);
    }

    // Update is called once per frame
    void Update()
    {
        if(ht != 0)
            timer += Time.deltaTime;

        H = SliderTailleCuve.value;
        hauteurReservoir = SliderHauteurReservoir.value + 0.2f;
        diamtru = SliderDiamtru.value;
        TextTailleCuve.text = "H: " + H;
        TextHauteurReservoir.text = "Hauteur Reservoir: " + hauteurReservoir;
        TextDiamOrifice.text = "Diametre trou: " + diamtru;

        transform.position = new Vector3(transform.position.x, hauteurReservoir - 3.84f);

        Ouverture.transform.localPosition = new Vector3(Ouverture.transform.position.x, baseOuverturePos.y + SliderDiamtru.value);

        if (startSimulation)
        {
            if (tailleFleche <= 0.05f)
            {
                startSimulation = false;
            }

            VEau = Mathf.Sqrt(2 * g * ht);
            Debit = diamtru * VEau;
            VolumeEau -= Debit * deltatime;
            ht = VolumeEau / SurfaceEau;

            Eau.transform.localScale = new Vector2(baseRectangleScale.x, ht);
            Eau.transform.localPosition = new Vector3(Eau.transform.localPosition.x, baseRectanglePos.y + 0.5f * Eau.transform.localScale.y, 1.0f);

            tailleFleche = Mathf.Sqrt(2 * hauteurReservoir / g) * VEau;
            fleche.transform.localScale = new Vector2(tailleFleche, 0.1f);
            fleche.transform.position = new Vector3(positionOrifice.transform.position.x + 0.5f * fleche.transform.localScale.x, BaseArrowPos.y, -10.5f);

            points.Clear(); 
            float yPos;
            float xPos;
            float time = 0;
            float limit = Mathf.Sqrt(2 * hauteurReservoir / g);
            int pointAdded = 0;
            while (time < limit)
            {
                yPos = hauteurReservoir - 0.5f * g * time * time + 0.5f * diamtru;
                xPos = positionOrifice.transform.position.x + time * VEau;

                points.Add(new Vector2(xPos, yPos));
                time += lineDeltaTime;
                pointAdded++;
            }

            
            line.startWidth = diamtru;
            line.endWidth = diamtru;

            line.positionCount = pointAdded;
            line.SetPositions(points.ToArray());
        }
        else
        {
            line.positionCount = 0;
        }
    }
}
