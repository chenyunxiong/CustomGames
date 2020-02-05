using UnityEngine;
using System.Collections;

public class Localize : MonoBehaviour
{
    public static string GetLanguageSuffix()
    {
        return Application.systemLanguage.ToString().ToLower();
    }

    void Awake()
    {
        var langSuffix = GetLanguageSuffix();
        if (langSuffix == "chinese") {
            UIToolkit.instance.texturePackerConfigName = "UI/battle.chinese";
        }
    }
}
