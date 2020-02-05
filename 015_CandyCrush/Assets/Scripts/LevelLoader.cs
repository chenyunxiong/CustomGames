using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    static string nextLevel;
    
    public static void Load(string levelName) {
        nextLevel = levelName;
        Application.LoadLevel("Loading");
    }

    void Start()
    {
        StartCoroutine(DelayLoad());
    }
    
    IEnumerator DelayLoad()
    {
        yield return new WaitForSeconds(1);
        
        if (nextLevel != null) {
            Application.LoadLevel(nextLevel);
        }
    }
}
