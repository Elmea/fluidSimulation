using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStart : MonoBehaviour
{
    [SerializeField] NiveauWater water;
    public void OnClickStartSim()
    {
        water.startSimulation = true;
    }

    public void OnClickReset()
    {
        water.startSimulation = false;
        water.Reset();
        Debug.Log("prout");
    }
}
