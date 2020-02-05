using UnityEngine;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    private static ScreenFade instance;
    
    void Start()
    {
        instance = this;
        
        guiTexture.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
        
        Debug.Log("Screen.width:" + Screen.width + ",Screen.height:" + Screen.height);
    }
    
    public static IEnumerator FadeOut(float fadeTime)
    {
        var c = instance.guiTexture.color;
        float lerp = 0;
        
        while (c.a < 1) {
            lerp += Time.deltaTime / fadeTime;
            c.a = Mathf.Pow(lerp, 4);
            if (c.a > 1) {
                c.a = 1;
            }
            instance.guiTexture.color = c;
            yield return null;
        }
    }
    
    public static IEnumerator FadeIn(float fadeTime)
    {
        var c = instance.guiTexture.color;
        
        while (c.a > 0) {
            c.a -= Time.deltaTime / fadeTime;
            if (c.a < 0) {
                c.a = 0;
            }
            instance.guiTexture.color = c;
            yield return null;
        }
    }
}
