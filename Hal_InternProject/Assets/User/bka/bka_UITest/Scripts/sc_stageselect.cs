using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sc_stageselect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("UIPlay");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ChangeSceneToPlay()
    {
        SceneManager.LoadScene("UIPlay");
    }
}
