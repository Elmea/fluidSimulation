using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cuve : MonoBehaviour
{
    [SerializeField] Slider SliderTailleCuve;
    [SerializeField] Slider SliderHauteurReservoir;
    [SerializeField] Slider SliderDiamtru;
    [SerializeField] TextMeshProUGUI TextTailleCuve;
    [SerializeField] TextMeshProUGUI TextHauteurReservoir;
    [SerializeField] TextMeshProUGUI TextDiamOrifice;

    [SerializeField] GameObject Ouverture;

    public float diamOrifice;
    public float hauteurReservoir;
    public float H;
    private Vector2 baseOuverturePos;
    // Start is called before the first frame update
    void Start()
    {
        baseOuverturePos = Ouverture.transform.localPosition;
        SliderTailleCuve.value = H;
        SliderHauteurReservoir.value = hauteurReservoir;
        SliderDiamtru.value = diamOrifice;
    }

    // Update is called once per frame
    void Update()
    {
        H = SliderTailleCuve.value;
        hauteurReservoir = SliderHauteurReservoir.value + 0.2f;
        diamOrifice = SliderDiamtru.value;
        TextTailleCuve.text = "H: " + H;
        TextHauteurReservoir.text = "Hauteur Reservoir: " + hauteurReservoir;
        TextDiamOrifice.text = "Diametre trou: " + diamOrifice;

        transform.position = new Vector3(transform.position.x, hauteurReservoir - 3.0f);

        Ouverture.transform.localPosition = new Vector3(Ouverture.transform.position.x, baseOuverturePos.y + SliderDiamtru.value + hauteurReservoir - 3.1f);
    }
}
