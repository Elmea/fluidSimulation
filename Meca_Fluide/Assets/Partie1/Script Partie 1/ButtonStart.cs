using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStart : MonoBehaviour
{
    [SerializeField] NiveauWater water;
    public void OnClick()
    {
        water.startSimulation = true;
    }
}
