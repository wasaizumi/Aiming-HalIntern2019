using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyButton : MonoBehaviour
{
    
    
    
    
    public void Update()
    {
        
    }

    public void OnClick()
    {


        //AddScene();
        Debug.Log("Button [Start] clicked");

        SceneManager.UnloadSceneAsync("UITitle");
    }

    IEnumerator AddScene()
    {
        //シーンを非同期で追加する
        SceneManager.LoadScene("UIStageSelect", LoadSceneMode.Additive);
        //シーン名を指定する
        Scene scene = SceneManager.GetSceneByName("UIStageSelect");
        while (!scene.isLoaded)
        {
            yield return null;
        }
        //指定したシーン名をアクティブにする
        SceneManager.SetActiveScene(scene);
    }
}
