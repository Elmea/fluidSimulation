using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NiveauWater : MonoBehaviour
{
    [SerializeField] GameObject flecheu;
    [SerializeField] GameObject Reservoir;
    public float hauteurReservoir;
    public float H;
    public float diamtru;
    private float ht;
    private float tailleFleche;
    private float speedWater;
    private float g = 9.81f;
    private float SurfaceEau;
    private float VolumeEau;
    private float Debit;
    private float deltatime;

    // Start is called before the first frame update
    void Start()
    {
        deltatime = 0.01f;
        SurfaceEau = 16;
        ht = H;
        VolumeEau = SurfaceEau * ht;
        speedWater = Mathf.Sqrt(2*g*ht);
    }

    // Update is called once per frame
    void Update()
    {
        speedWater = Mathf.Sqrt(2*g*ht);
        Debit = diamtru * speedWater;
        VolumeEau -= Debit * deltatime;
        ht = VolumeEau / SurfaceEau;

        RectTransform rectTrans = GetComponent<RectTransform>();

        rectTrans.sizeDelta=new Vector2(4.5f, ht);

        Debug.Log(ht);
    }
}
