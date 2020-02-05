using UnityEngine;
using System.Collections;

public class LogoScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Delay(1));
    }
    
    IEnumerator Delay(float sec)
    {
        yield return new WaitForSeconds(sec);
//        yield return StartCoroutine(ScreenFade.FadeOut(1));
        Application.LoadLevel("MainMenu");
    }
}
