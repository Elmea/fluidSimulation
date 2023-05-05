using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRemplir : MonoBehaviour
{
    [SerializeField] InstantiatFluide rouxbinait;
    public void isClicked()
    {
        rouxbinait.open = !rouxbinait.open;
    }
}
