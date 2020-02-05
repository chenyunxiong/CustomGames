using UnityEngine;
using System.Collections;

public class LocalizedTexture : MonoBehaviour
{
    public Material mat;
    public string textureName;
    
    void Awake()
    {
        var langSuffix = Application.systemLanguage.ToString().ToLower();
        var localizedTextureName = textureName + "." + langSuffix;
        mat.mainTexture = (Texture2D)Resources.Load("localization/" + localizedTextureName);
    }
}
