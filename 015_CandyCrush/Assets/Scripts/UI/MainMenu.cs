using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class MainMenu : MonoBehaviour
{
    UIButton startButton;
    UIButton leaderboardButton;
    UIButton creditsButton;
    UIButton facebookButton;
    UIButton twitterButton;
    
    UIText text;
    float textScale = 0.9f;
    
//    UITextInstance recordText;
    
    UISprite medalSprite;
    UITextInstance medalText;
    
    public AudioClip startSound;

    void Start()
    {
        StartCoroutine(StartProgress());
    }
    
    IEnumerator StartProgress()
    {
        Camera.main.orthographicSize = 24;
        while (Camera.main.orthographicSize > 21) {
            Camera.main.orthographicSize -= 3 * Time.deltaTime;
            yield return null;
        }
        
        InitUI();
    }
    
    void InitUI()
    {
        text = UIHelper.getText();
        text.lineSpacing = 1.6f;
        
        var buttonOffset = new UIEdgeOffsets(5);
     
        var x = Screen.width / 2;
        var y = Screen.height / 2 + 40;
        
        var medals = Player.medals;
        if (medals > 0) {
            medalSprite = UI.firstToolkit.addSprite("medal_highlight.png", x - 28, y + 6);
            medalText = text.addTextInstance(Player.medals.ToString(), x, y, 0.8f, 1, Colors.HighlightText);
//            y += 30;
        }
     
//        var record = Player.getScoreRecord();
//        if (record > 0) {
//            recordText = text.addTextInstance("Record: " + record, x, y, 0.8f, 1, Colors.HighlightText);
//            recordText.xPos -= recordText.width / 2;
//        }
     
        y += 50;
        startButton = UIButton.create("button_start.png", "button_start.png", x - 150, y);
        startButton.onTouchUpInside += delegate(UIButton obj) {
            StartCoroutine(LoadGame());
        };
     
        y += 95;
        leaderboardButton = UIButton.create("button_leaderboard.png", "button_leaderboard.png", x - 127, y);
        leaderboardButton.onTouchUpInside += ShowLeaderboard;
        leaderboardButton.normalTouchOffsets = buttonOffset;
     
        creditsButton = UIButton.create("button_notice.png", "button_notice.png", Screen.width - 70, Screen.height - 70);
        creditsButton.normalTouchOffsets = buttonOffset;
        creditsButton.onTouchUpInside += ShowCredits;
     
//        buttonOffset = new UIEdgeOffsets(2);
     
//        x = Screen.width - 140;
//        y = Screen.height - 70;
//        facebookButton = UIButton.create("facebook.png", "facebook.png", x, y);
//        facebookButton.normalTouchOffsets = buttonOffset;
//        facebookButton.onTouchUpInside += ConnectFacebook;
//     
//        x += 65;
//        twitterButton = UIButton.create("twitter.png", "twitter.png", x, y);
//        twitterButton.normalTouchOffsets = buttonOffset;
//        twitterButton.onTouchUpInside += ConnectTwitter;
     
        if (!Social.localUser.authenticated) {
            Social.localUser.Authenticate(success => {
                if (success) {
                    Achievement.SyncServerAchievements();
                }
            });
        }
    }
    
    void HideUI()
    {
        startButton.hidden = true;
        leaderboardButton.hidden = true;
        creditsButton.hidden = true;
        
        if (medalSprite != null) {
            medalSprite.hidden = true;
            medalText.hidden = true;
        }
        
//        if (recordText != null) {
//            recordText.hidden = true;
//        }
    }
    
    void ShowUI()
    {
        startButton.hidden = false;
        leaderboardButton.hidden = false;
        creditsButton.hidden = false;
        
        if (medalSprite != null) {
            medalSprite.hidden = false;
            medalText.hidden = false;
        }
        
//        if (recordText != null) {
//            recordText.hidden = false;
//        }
    }
    
    IEnumerator LoadGame()
    {
        HideUI();
        SoundPlayer.PlayFX(startSound);
        
        StartCoroutine(ScreenFade.FadeOut(2));
        
        while (Camera.main.orthographicSize < 24) {
            Camera.main.orthographicSize += 1.5f * Time.deltaTime;
            yield return null;
        }
        LevelLoader.Load("Level01");
    }
 
    void ConnectFacebook(UIButton button)
    {
     
    }
 
    void ConnectTwitter(UIButton button)
    {
     
    }
 
    void ShowCredits(UIButton button)
    {
        HideUI();
        StartCoroutine(ShowCredits());
    }
    
    IEnumerator ShowCredits()
    {
        float cameraSpeed = 0;
        while (Camera.main.transform.position.y > 26) {
            cameraSpeed = Mathf.Lerp(cameraSpeed, -30, Time.deltaTime);
            Camera.main.transform.Translate(0, cameraSpeed * Time.deltaTime, 0);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.3f);
        
        var yOffset = 50 + (Screen.height - 640) / 2;
        
        var creditsText = text.addTextInstance("CREDITS", Screen.width / 2, yOffset, textScale, 1, Colors.DisableText);
        creditsText.alignMode = UITextAlignMode.Center;
        creditsText.xPos -= creditsText.width / 2;
        
        var title = "\n\n\n\nDesigners\n\n\n\nArtists\n\n\n\n\nProgrammer";
        var titleText = text.addTextInstance(title, Screen.width / 4, yOffset, textScale, 1, Colors.DisableText);
        titleText.alignMode = UITextAlignMode.Center;
        titleText.xPos -= titleText.width / 2;
        
        var names = "\n\nWhirlblast Studio\n\n\nRaven Wang\nPauland\n\n\nPauland\nCR Zhang\nBlake Seow\n\n\nRaven Wang";
        var nameText = text.addTextInstance(names, Screen.width / 4, yOffset, textScale, 1, Colors.HighlightText);
        nameText.alignMode = UITextAlignMode.Center;
        nameText.xPos -= nameText.width / 2;
        
        var thanksTitle = text.addTextInstance("\n\n\n\nUnity Plugin\n\n\nMusic\n\n\n\nFonts", Screen.width * 3 / 4, yOffset, textScale, 1, Colors.DisableText);
        thanksTitle.alignMode = UITextAlignMode.Center;
        thanksTitle.xPos -= thanksTitle.width / 2;
        
        var thankNames = text.addTextInstance("\n\nThanks\n" +
            "\n\nUIToolkit - Prime[31]\n" +
            "\n\nTitle: Green Sky - DST\nBattle: Znake - Osnoff\n" +
            "\n\nOptimus - Neale Davidson\nNamco - Family Font Mart",
            Screen.width * 3 / 4, yOffset, textScale, 1, Colors.HighlightText);
        thankNames.alignMode = UITextAlignMode.Center;
        thankNames.xPos -= thankNames.width / 2;
        
        while (Input.touchCount == 0 && !Input.anyKeyDown) {
            yield return null;
        }
        
        creditsText.clear();
        titleText.clear();
        nameText.clear();
        thanksTitle.clear();
        thankNames.clear();
        
        yield return StartCoroutine(HideCredits());
    }
    
    IEnumerator HideCredits()
    {
        float cameraSpeed = 0;
        while (Camera.main.transform.position.y < 51) {
            cameraSpeed = Mathf.Lerp(cameraSpeed, 30, Time.deltaTime);
            Camera.main.transform.Translate(0, cameraSpeed * Time.deltaTime, 0);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.3f);
        
        ShowUI();
    }
 
    void ShowLeaderboard(UIButton button)
    {
        Social.ShowLeaderboardUI();
    }
}
