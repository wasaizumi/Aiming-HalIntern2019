using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class FadeController : MonoBehaviour
{
    private static bool IsFade = false;

    public static bool IsActive
    {
        get { return IsFade; }
    }

    [SerializeField]
    private Image m_fadeImage;
    [SerializeField]
    private float m_waitTime = 1f;

    [SerializeField]
    private Animator m_fadeAnimator;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        m_fadeAnimator = this.GetComponent<Animator>();
        m_fadeImage.color *= new Color(1.0f,1.0f,1.0f,0.0f);
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
    }

    // fadeTime == 0.0f：FadeInOutなしの読み込み
    public void ChangeScene(string sceneName,float fadeTime,bool FullFadeIn,bool FullFadeOut)
    {
        this.gameObject.SetActive(true);
        if (fadeTime <= 0.0f)
        {
            SceneManager.LoadSceneAsync(sceneName);
            Destroy(this);
        }
        else
            StartCoroutine(FadeInOut(sceneName, fadeTime,FullFadeIn,FullFadeOut));
    }

    IEnumerator FadeInOut(string nextScene,float time,bool FullFadeIn,bool FullFadeOut)
    {
        AsyncOperation sceneAsync;
        AnimatorClipInfo[] info;
        Animator animator = m_fadeAnimator;

        IsFade = true;
        //非同期読み込み
        sceneAsync = SceneManager.LoadSceneAsync(nextScene);
        sceneAsync.allowSceneActivation = false;

        if (FullFadeIn) animator.SetTrigger("FullFadeIn");
        if (FullFadeOut) animator.SetTrigger("FullFadeOut");

        //FadeIn
        animator.SetBool("IsFadeIN",true);
        var currentInfo = animator.GetCurrentAnimatorClipInfo(0);
        while (true)
        {
            info = animator.GetCurrentAnimatorClipInfo(0);
            if (currentInfo[0].clip != info[0].clip) break;
            yield return null;
        }
        Debug.Log("FadeIn Clip:" + info[0].clip.name);
        yield return new WaitForSeconds(info[0].clip.length);

        //読み込み完了待機
        while (sceneAsync.progress < 0.8f)
            yield return null;

        //読み込み完了
        sceneAsync.allowSceneActivation = true;
        this.m_fadeImage.transform.SetAsLastSibling();

        //待ち
        //yield return new WaitForSeconds(time);

        //FadeOut
        animator.SetBool("IsFadeIN",false);

        //遷移待ち
        currentInfo = animator.GetCurrentAnimatorClipInfo(0);
        while (true)
        {
            info = animator.GetCurrentAnimatorClipInfo(0);
            if (currentInfo[0].clip != info[0].clip) break;
            yield return null;
        }
        Debug.Log("FadeOut Clip:"+info[0].clip.name);
        yield return new WaitForSeconds(info[0].clip.length);

        //重複インスタンスの削除
        Destroy(this.gameObject);
        IsFade = false;

        yield break;
    }

    private void OnDestroy()
    {
        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
    }
}
