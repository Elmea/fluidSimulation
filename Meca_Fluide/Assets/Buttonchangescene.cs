using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonchangescene : MonoBehaviour
{
    [SerializeField] GameObject GM;
    bool open = true;

    // Start is called before the first frame update
    void Start()
    {
        GM.SetActive(open);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickScene1()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickScene2()
    {
        SceneManager.LoadScene(2);
    }

    public void OnClickOpen()
    {
        open = !open;
        GM.SetActive(open);
    }
}
