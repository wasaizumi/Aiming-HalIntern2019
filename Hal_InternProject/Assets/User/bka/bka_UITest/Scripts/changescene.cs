using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changescene : MonoBehaviour
{
    // Start is called before the first frame update

    //public Animator anima;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //if(Input.GetMouseButtonDown(0))
        //{
        //    anima.SetBool("scenechange", true);

            
        //}
        
    }

    public void scenechange()
    {
        SceneManager.LoadScene("UIplay");
    }
}
