using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour
{
    UISprite root;
    UISprite scoreButton;
    UISprite timerButton;
    UIButton helpButton;
    UIButton pauseButton;
    UISprite alertIcon;
    UISpriteAnimation alertAnim;
    UISprite missileIcon;
    UISprite[] missileCountIcons;
    UIText text;
    UITextInstance timerText;
    UITextInstance scoreText;
    float defaultTextScale = 1;
    float scoreXPos;
    float scoreYPos = 12;
    Vector2 scoreSize;
    TargetMark lockingMark;
    float lastTimer;
 
    UIToolkit spriteManager { get { return UIToolkit.instance; } }
 
    void Start()
    {
        text = UIHelper.getText();
        text.alignMode = UITextAlignMode.Right;
     
        scoreSize = text.sizeForText("00000", defaultTextScale);
     
        DrawScore();
        DrawTimer();
        DrawPauseButton();
        DrawMissileCount();
        DrawAlert();
    }
 
    void OnEnable()
    {
        Messenger<Aerolite>.AddListener(MessageTypes.GainAeroliteScore, OnGainAeroliteScore);
        Messenger<Enemy>.AddListener(MessageTypes.GainEnemyScore, OnGainEnemyScore);
        Messenger<int>.AddListener(MessageTypes.GainComboScore, OnGainComboScore);
        Messenger.AddListener(MessageTypes.CollisionAlert, StartAlert);
        Messenger.AddListener(MessageTypes.CollisionAlertLifted, StopAlert);
        Messenger.AddListener(MessageTypes.LaunchMissile, UpdateMissileCount);
        Messenger.AddListener(MessageTypes.MissileLauncherReloaded, UpdateMissileCount);
        Messenger.AddListener(MessageTypes.GameOver, Hide);
        Messenger.AddListener(MessageTypes.ShowHelpScreen, HideForHelp);      
        Messenger.AddListener(MessageTypes.HideHelpScreen, Show);      
    }
 
    void OnDisable()
    {
        Messenger<Aerolite>.RemoveListener(MessageTypes.GainAeroliteScore, OnGainAeroliteScore);
        Messenger<Enemy>.RemoveListener(MessageTypes.GainEnemyScore, OnGainEnemyScore);
        Messenger<int>.RemoveListener(MessageTypes.GainComboScore, OnGainComboScore);
        Messenger.RemoveListener(MessageTypes.CollisionAlert, StartAlert);
        Messenger.RemoveListener(MessageTypes.CollisionAlertLifted, StopAlert);
        Messenger.RemoveListener(MessageTypes.LaunchMissile, UpdateMissileCount);
        Messenger.RemoveListener(MessageTypes.MissileLauncherReloaded, UpdateMissileCount);
        Messenger.RemoveListener(MessageTypes.GameOver, HideForHelp);
        Messenger.RemoveListener(MessageTypes.HideHelpScreen, Show);      
    }
 
    void Hide()
    {
        scoreButton.hidden = true;
        timerButton.hidden = true;
        helpButton.hidden = true;
        pauseButton.hidden = true;
        scoreText.hidden = true;
        timerText.hidden = true;
     
        alertIcon.hidden = true;
        missileIcon.hidden = true;
        foreach (var icon in missileCountIcons) {
            icon.hidden = true;
        }
    }
    
    void HideForHelp()
    {
        scoreButton.hidden = true;
        timerButton.hidden = true;
        helpButton.hidden = true;
        pauseButton.hidden = true;
        scoreText.hidden = true;
        timerText.hidden = true;
    }
    
    void Show()
    {
        scoreButton.hidden = false;
        timerButton.hidden = false;
        helpButton.hidden = false;
        pauseButton.hidden = false;
        scoreText.hidden = false;
        timerText.hidden = false;
     
        alertIcon.hidden = false;
        missileIcon.hidden = false;
        foreach (var icon in missileCountIcons) {
            icon.hidden = false;
        }
    }
 
    void Update()
    {
        UpdateTimer();
    }
 
    void DrawScore()
    {
        var x = Screen.width / 2 - 10;
        scoreButton = spriteManager.addSprite("button_score.png", x, 0);
     
        scoreXPos = x + 62;
        scoreText = text.addTextInstance(LevelHandler.instance.score.ToString(), scoreXPos, scoreYPos, defaultTextScale, 1, Colors.HighlightText);
        scoreText.autoRefreshPositionOnScaling = false;
    }
 
    void DrawTimer()
    {
        var x = Screen.width / 2 - 180;
        timerButton = spriteManager.addSprite("button_time.png", x, 0);
     
        x += 62;
        timerText = text.addTextInstance(LevelHandler.instance.formattedTimer, x, scoreYPos);
        timerText.textScale = defaultTextScale;
        timerText.setColorForAllLetters(Colors.HighlightText);
//       timer.alignMode = UITextAlignMode.Center;
    }
 
    void UpdateTimer()
    {
        if (LevelHandler.instance.timer - lastTimer > 1) {
            timerText.text = LevelHandler.instance.formattedTimer;
            lastTimer = Mathf.Floor(LevelHandler.instance.timer);
        }
    }
 
    void OnGainAeroliteScore(Aerolite aerolite)
    {
        var pos = Camera.main.WorldToScreenPoint(aerolite.transform.position);
        StartCoroutine(ShowScoreTip(aerolite.score, pos));
     
        UpdateScoreText();
    }
 
    void OnGainEnemyScore(Enemy enemy)
    {
        var pos = Camera.main.WorldToScreenPoint(enemy.transform.position);
        StartCoroutine(ShowScoreTip(enemy.score, pos));
     
        UpdateScoreText();
    }
    
    void OnGainComboScore(int score)
    {
        StartCoroutine(ShowComboScoreTip(score));
        UpdateScoreText();
    }
 
    void UpdateScoreText()
    {
        var txt = LevelHandler.instance.score.ToString();
     
        var scale = Mathf.Clamp01(5f / txt.Length);
        
        scoreText.text = txt;
        scoreText.textScale = scale * defaultTextScale;
        scoreText.yPos = scoreYPos + (scoreSize.y - scoreText.height) / 2;

//        Debug.Log("yPos: " + scoreYPos + ", ypos:" + scoreText.yPos + ", scale:" + scale);
    }
 
    void DrawPauseButton()
    {
        var x = Screen.width - 65;
        pauseButton = UIButton.create("button_pause.png", "button_pause.png", x, 5);
        pauseButton.onTouchUpInside += (UIButton btn) => {
            LevelHandler.instance.Pause();
        };
        
        x -= 65;
        helpButton = UIButton.create("button_help.png", "button_help.png", x, 5);
        helpButton.onTouchUpInside += (UIButton btn) => {
            LevelHandler.instance.ShowHelpScreen();
        };
    }
 
    void DrawMissileCount()
    {
        int x = Screen.width / 2 - 3;
        int y = 120;
        missileIcon = spriteManager.addSprite("missile_count_on.png", x, y);
     
        x += 32;
        y += 7;
        missileCountIcons = new UISprite[8];
        for (int i = 0; i < 8; i++) {
            missileCountIcons[i] = spriteManager.addSprite("missile_count_1.png", x + i % 4 * 8, y + i / 4 * 11);
        }
    }
 
    void UpdateMissileCount()
    {
        var count = PlayerControl.instance.craft.missileLauncher.availableCount;
     
        for (int i = 0; i < missileCountIcons.Length; i++) {
            missileCountIcons[i].setSpriteImage(i < count ? "missile_count_1.png" : "missile_count_0.png");
        }
     
        if (count > 0) {
            missileIcon.setSpriteImage("missile_count_on.png");
        } else {
            missileIcon.setSpriteImage("missile_count_off.png");
        }
    }
 
    void DrawAlert()
    {
        int x = Screen.width / 2 - 77;
        int y = 120;
        alertIcon = spriteManager.addSprite("alert_normal.png", x, y);
        alertAnim = alertIcon.addSpriteAnimation("alert", 0.1f, "alert_highlight1.png", "alert_highlight2.png");
        alertAnim.loopReverse = true;
    }
 
    void StartAlert()
    {
        alertIcon.playSpriteAnimation(alertAnim, 10);
    }
 
    void StopAlert()
    {
        alertAnim.stop();
    }
 
    IEnumerator ShowScoreTip(int score, Vector2 pos)
    {
        var tip = text.addTextInstance("+" + score, pos.x, Screen.height - pos.y, 0.8f, 10, Colors.Gold);
        tip.xPos -= tip.width / 2;
        var targetPos = tip.position;
        targetPos.y += 50;
        tip.positionTo(1, targetPos, Easing.Linear.easeIn);
        yield return new WaitForSeconds(0.8f);
        tip.clear();
    }
 
    IEnumerator ShowComboScoreTip(int score)
    {
        var pos = Vector2.zero;
        if (LevelHandler.instance.isPlaying) {
            pos = Crosshair.instance.screenPosition;
            pos.y = Screen.height - pos.y;
        } else {
            pos = new Vector2(Screen.width / 2, Screen.height / 2);
        }
     
        var tipText = "+" + score + " Bonus";
        var tipSize = text.sizeForText(tipText);
        var tip = text.addTextInstance(tipText, pos.x - tipSize.x / 2, pos.y - tipSize.y, 1, 1, Colors.HighlightText);
        var targetPos = tip.position;
        targetPos.y += 50;
        tip.positionTo(1, targetPos, Easing.Linear.easeIn);
        yield return new WaitForSeconds(1);
        tip.clear();
    }
}
