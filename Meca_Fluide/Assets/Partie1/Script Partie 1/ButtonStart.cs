using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStart : MonoBehaviour
{
    [SerializeField] NiveauEau water;
    public void OnClickStartSim()
    {
        water.startSimulation = true;
    }

    public void OnClickReset()
    {
        water.startSimulation = false;
        water.Reset();
    }
}
