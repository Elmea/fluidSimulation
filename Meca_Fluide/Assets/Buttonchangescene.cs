using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonchangescene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickScene1()
    {
        //SceneManager.UnloadScene("Scene Partie 2 ");
        SceneManager.LoadScene("Scene Parie 1");
    }

    public void OnClickScene2()
    {
        //SceneManager.UnloadScene("Scene Partie 1 ");
        SceneManager.LoadScene("Scene Parie 2");
    }
}
