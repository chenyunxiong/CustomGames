using UnityEngine;
using System.Collections;

#pragma warning disable 168
#pragma warning disable 219

public class LevelStats : MonoBehaviour
{
    UIText text;
    UnityEngine.SocialPlatforms.ILeaderboard leaderboard;
    
    GameObject board;
    
    UISpriteManager spriteManager;
 
    int xOffset = 0;
    int yOffset = 0;
    float textScale = 0.9f;
    
    void Start()
    {
        if (Localize.GetLanguageSuffix() == "chinese") {
            board = (GameObject)Instantiate(Resources.Load("stats_board.chinese"));
        } else {
            board = (GameObject)Instantiate(Resources.Load("stats_board.english"));
        }
        board.guiTexture.enabled = false;
        
        yOffset = (Screen.height - 640) / 2;
        xOffset = (Screen.width - 960) / 2;
        
        spriteManager = UI.firstToolkit;
    }
 
    public void Show()
    {
        board.guiTexture.enabled = true;
     
        text = UIHelper.getText();
        text.alignMode = UITextAlignMode.Right;
        text.verticalAlignMode = UITextVerticalAlignMode.Middle;
        
        int medals = 0;
     
        // star
//       int x = 376;
//       int y = 40;
//       var starNum = Mathf.Clamp(LevelHandler.current.score / 100000, 0, 5);
//       for (int i = 0; i < 5; i++) {
//           UIToolkit.instance.addSprite(i < starNum ? "star.png" : "star_frame.png", x + i * 42, y);
//       }

     
        int firstColumn = 429 + xOffset;
        int secondColumn = 613 + xOffset;
        int medalColumn = 720 + xOffset;
     
        // time
        int y = 190 + yOffset;
        var time = LevelHandler.instance.timer;
        var timeText = text.addTextInstance(LevelHandler.instance.formattedTimer, firstColumn, y, textScale, 1, Colors.HighlightText);
     
        var timeRecord = Player.getTimeRecord();
        if (time > timeRecord) {
            timeRecord = time;
            Player.setTimeRecord(time);
        }
        text.addTextInstance(timeRecord.getTimeFormat(), secondColumn, y, textScale, 1, Colors.HighlightText);
        
        if (time > 120) {
            var timeMedals = 1 + (int)(time - 120) / 60;
            spriteManager.addSprite("medal.png", medalColumn, y);
            text.addTextInstance("x" + timeMedals, medalColumn + 25, y, textScale, 1, Colors.HighlightText);
            
            medals += timeMedals;
        }
        
        // level
        y = 225 + yOffset;
        var level = LevelHandler.instance.level;
        var levelText = text.addTextInstance(level.ToString(), firstColumn - 1, y, textScale, 1, Colors.HighlightText);
     
        var levelRecord = Player.getLevelRecord();
        if (level > levelRecord) {
            levelRecord = level;
            Player.setLevelRecord(level);
        }
        text.addTextInstance(levelRecord.ToString(), secondColumn, y, textScale, 1, Colors.HighlightText);
        
        if (level > 5) {
            var levelMedals = 1 + (level - 5) / 5;
            spriteManager.addSprite("medal.png", medalColumn, y);
            text.addTextInstance("x" + levelMedals, medalColumn + 25, y, textScale, 1, Colors.HighlightText);
            
            medals += levelMedals;
        }
        
        // max combo
        y = 260 + yOffset;
        var maxCombo = ComboHandler.current.maxCombo;
        var comboText = text.addTextInstance(maxCombo.ToString(), firstColumn, y, textScale, 1, Colors.HighlightText);
     
        var comboRecord = Player.getComboRecord();
        if (maxCombo > comboRecord) {
            comboRecord = maxCombo;
            Player.setComboRecord(maxCombo);
        }
        text.addTextInstance(comboRecord.ToString(), secondColumn, y, textScale, 1, Colors.HighlightText);
        
        if (maxCombo > 10) {
            var comboMedals = 1 + (maxCombo - 10) / 5;
            spriteManager.addSprite("medal.png", medalColumn, y);
            text.addTextInstance("x" + comboMedals, medalColumn + 25, y, textScale, 1, Colors.HighlightText);
            
            medals += comboMedals;
        }
     
        // clean count
        y = 295 + yOffset;
        var cleanCount = LevelHandler.instance.cleanCount;
        var cleanCountText = text.addTextInstance(cleanCount.ToString(), firstColumn, y, textScale, 1, Colors.HighlightText);
     
        var cleanCountRecord = Player.getCleanCountRecord();
        if (cleanCount > cleanCountRecord) {
            cleanCountRecord = cleanCount;
            Player.setCleanCountRecord(cleanCount);
        }
        text.addTextInstance(cleanCountRecord.ToString(), secondColumn, y, textScale, 1, Colors.HighlightText);
        
        if (cleanCount > 300) {
            var cleanMedals = cleanCount / 300;
            spriteManager.addSprite("medal.png", medalColumn, y);
            text.addTextInstance("x" + cleanMedals, medalColumn + 25, y, textScale, 1, Colors.HighlightText);
            
            medals += cleanMedals;
        }
        
        var totalClean = Player.getTotalCleanCount();
        Player.setTotalCleanCount(totalClean + cleanCount);
     
        // score
        y = 355 + yOffset;
        var score = LevelHandler.instance.score;
        var scoreText = text.addTextInstance(score.ToString(), 377 + xOffset, y, 1.3f, 1, Colors.HighlightText);
     
        y = 383 + yOffset;
        var scoreRecord = Player.getScoreRecord();
        if (score > scoreRecord) {
            scoreRecord = score;
            Player.setScoreRecord(score);
            text.addTextInstance("New Record", 613 + xOffset, y, 0.8f, 1, Colors.HighlightText);
        } else {
            text.addTextInstance(scoreRecord.ToString(), 613 + xOffset, y, 0.8f, 1, Colors.HighlightText);
        }
     
        Player.medals += medals;
     
//       y = 238;
//       var cleanWeightText = text.addTextInstance("54321", x, y, 1, 1, Colors.PanelText);
//       
//       y = 355;
//       var rankText = text.addTextInstance("439", x, y, 1, 1, Colors.PanelText);
//       
//       y = 400;
//       var record = Player.getLevelRecord(Application.loadedLevel);
//       if (score > record) {
//           record = score;
//           Player.setLevelRecord(Application.loadedLevel, score);
//       }
//       var recordText = text.addTextInstance(record.ToString(), x, y, 1, 1, Colors.PanelText);
//       
//       y = 445;
//       var totalCleanText = text.addTextInstance("24357230", x, y, 1, 1, Colors.PanelText);
     
     
//       y += 30;
//       
     
        var buttonOffset = new UIEdgeOffsets(40);
     
        int x = 170 + xOffset;
        y = 442 + yOffset;
        var backButton = UIButton.create("button_back.png", "button_back.png", x, y);
        backButton.normalTouchOffsets = buttonOffset;
        backButton.onTouchUpInside += delegate(UIButton obj) {
            LevelLoader.Load("MainMenu");
        };
     
//        x = (Screen.width - 395) / 2;
//        var facebookButton = UIButton.create("facebook.png", "facebook.png", x, y);
//     
//        x += 70;
//        var twitterButton = UIButton.create("twitter.png", "twitter.png", x, y);
     
        x = (Screen.width - 255) / 2;
        var leaderboardButton = UIButton.create("button_leaderboard.png", "button_leaderboard.png", x, y);
        leaderboardButton.onTouchUpInside += delegate(UIButton obj) {
			Social.ShowLeaderboardUI();
		};
     
        x = Screen.width - 230 - xOffset;
        var restartButton = UIButton.create("button_restart.png", "button_restart.png", x, y);
        restartButton.normalTouchOffsets = buttonOffset;
        restartButton.onTouchUpInside += delegate(UIButton obj) {
            LevelLoader.Load("Level01");
        };
        
     
        // social report
     
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            if (Social.localUser.authenticated) {
                ReportLeaderboards(score, level);
                ReportAchievements();
            } else {
                Social.localUser.Authenticate(success => {
                    if (success) {
                        ReportLeaderboards(score, level);
                        ReportAchievements();
                    }
                });
            }
        }   
    }
 
    void ReportLeaderboards(int score, int level)
    {
        Debug.Log("Reporting score...");
        Social.ReportScore(score, LeaderboardIDs.HighScores, success => {
            if (success) {
                Debug.Log("Report score success.");
                
                Debug.Log("Reporting level...");
                Social.ReportScore(level, LeaderboardIDs.HighLevels, s => {
                    if (s) {
                        Debug.Log("Report level success.");
                    } else {
                        Debug.Log("Report level failed.");
                    }
                });
                
            } else {
                Debug.Log("Report score failed.");
            }
        });
        
    }
    
    void ReportAchievements()
    {
        var achievements = new Achievement[15];
        int i = 0;
        
        var totalClean = Player.getTotalCleanCount();
        achievements[i++] = UpdateAchievement(AchievementIDs.Clean100, totalClean);
        achievements[i++] = UpdateAchievement(AchievementIDs.Clean500, totalClean);
        achievements[i++] = UpdateAchievement(AchievementIDs.Clean1000, totalClean);
        achievements[i++] = UpdateAchievement(AchievementIDs.Clean2000, totalClean);
        achievements[i++] = UpdateAchievement(AchievementIDs.Clean10000, totalClean);
        
        var totalMissileClean = Player.missileDestroyCount;
        totalMissileClean += LevelHandler.instance.missileCleanCount;
        Player.missileDestroyCount = totalMissileClean;
        achievements[i++] = UpdateAchievement(AchievementIDs.MissileClean100, totalMissileClean);
        achievements[i++] = UpdateAchievement(AchievementIDs.MissileClean200, totalMissileClean);
        achievements[i++] = UpdateAchievement(AchievementIDs.MissileClean300, totalMissileClean);
        achievements[i++] = UpdateAchievement(AchievementIDs.MissileClean400, totalMissileClean);
        achievements[i++] = UpdateAchievement(AchievementIDs.MissileClean500, totalMissileClean);
        
        var level = LevelHandler.instance.level;
        achievements[i++] = UpdateAchievement(AchievementIDs.Level10, level);
        achievements[i++] = UpdateAchievement(AchievementIDs.Level15, level);
        achievements[i++] = UpdateAchievement(AchievementIDs.Level20, level);
        achievements[i++] = UpdateAchievement(AchievementIDs.Level25, level);
        achievements[i++] = UpdateAchievement(AchievementIDs.Level30, level);
        
        StartCoroutine(ReportAchievements(achievements));
    }
    
    Achievement UpdateAchievement(string id, float current)
    {
        var achievement = Player.getAchievement(id);
        achievement.current = current;
        
        if (achievement.status == AchievementStatus.Uncomplete && current >= achievement.target) {
            achievement.status = AchievementStatus.Completed;
            Player.setAchievement(achievement);
            
            Debug.Log("Complete achievement: " + id);
            
            Debug.Log("Rewards " + achievement.medals + " medals.");
            Player.medals += achievement.medals;
        }
        
        return achievement;
    }
    
    IEnumerator ReportAchievements(Achievement[] achievements)
    {
        Debug.Log("Reporting achievements...");
        
        bool returned = false;
        
        foreach (var achievement in achievements) {
            if (achievement.status != AchievementStatus.Reported) {
                if (achievement.reportProgress || achievement.status == AchievementStatus.Completed) {
                    Debug.Log("Reporting achievement: " + achievement.id + "...");
                    
                    returned = false;
                    
                    var progress = Mathf.Clamp01(achievement.current / achievement.target) * 100;
                    
                    Social.ReportProgress(achievement.id, progress, success => {
                        if (success) {
                            Debug.Log("Report achievement: " + achievement.id + " success.");
                            if (achievement.status == AchievementStatus.Completed) {
                                achievement.status = AchievementStatus.Reported;
                                Player.setAchievement(achievement);
                            } 
                        } else {
                            Debug.Log("Report achievement: " + achievement.id + " failed.");
                        }
                        returned = true;
                    });
                    
                    while (!returned) {
                        yield return null;
                    }
                }
            }
        }
    }
 
    void ShowRanks()
    {
        var leaderboard = Social.CreateLeaderboard();
        leaderboard.id = LeaderboardIDs.HighScores;
        leaderboard.userScope = UnityEngine.SocialPlatforms.UserScope.Global;
        leaderboard.timeScope = UnityEngine.SocialPlatforms.TimeScope.AllTime;
     
        leaderboard.LoadScores(success => {
            int x = 429, y = 361 + yOffset;
            if (success) {
                text.addTextInstance(leaderboard.localUserScore.rank.ToString(), x, y, textScale);
            } else {
                ShowNetworkError();
            }
        });
    }
 
    void ShowNetworkError()
    {
        Debug.Log("Network error.");
    }
}
