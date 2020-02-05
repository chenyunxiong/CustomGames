using UnityEngine;
using System.Collections;

public class HelpScreen : MonoBehaviour
{
    public Texture2D texture;
    public Texture2D padTexture;
    
    void Start()
    {
        if (!LevelHandler.instance.paused) {
            LevelHandler.instance.Pause();
        }
        Messenger.Broadcast(MessageTypes.ShowHelpScreen);
        
        if (Screen.width > 960) {
            guiTexture.texture = padTexture;
            guiTexture.pixelInset = new Rect(0, 0, 1024, 768);
        } else {
            guiTexture.texture = texture;
            guiTexture.pixelInset = new Rect(0, 0, 960, 640);
        }
    }
 
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER
        if (Input.anyKeyDown) {
#else
        if (Input.touchCount > 0) {
#endif
            Messenger.Broadcast(MessageTypes.HideHelpScreen);
            LevelHandler.instance.Resume();
            
            Destroy(gameObject);
        }
    }
    
}
