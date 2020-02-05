using UnityEngine;
using System.Collections;

public enum AchievementStatus
{
    Uncomplete, Completed, Reported
}

public class Achievement
{
    public static void SyncServerAchievements()
    {
        Debug.Log("Sync achievements...");
        
        Social.LoadAchievements(achievements => {
            foreach (var achievement in achievements) {
                var local = Player.getAchievement(achievement.id);
                if (local.status != AchievementStatus.Reported && achievement.completed) {
                    local.status = AchievementStatus.Reported;
                    Player.setAchievement(local);
                    
                    Debug.Log("Sync achievement " + achievement.id + " as reported.");
                }
            }
        });
    }
    
    public string id { get; set; }
    public AchievementStatus status { get; set; }
    public float current { get; set; }

    public float target {
        get {
            switch (id) {
            case AchievementIDs.Clean100: return 100;
            case AchievementIDs.Clean500: return 500;
            case AchievementIDs.Clean1000: return 1000;
            case AchievementIDs.Clean2000: return 2000;
            case AchievementIDs.Clean10000: return 10000;
            
            case AchievementIDs.MissileClean100: return 100;
            case AchievementIDs.MissileClean200: return 200;
            case AchievementIDs.MissileClean300: return 300;
            case AchievementIDs.MissileClean400: return 400;
            case AchievementIDs.MissileClean500: return 500;
                
            case AchievementIDs.Level10: return 10;
            case AchievementIDs.Level15: return 15;
            case AchievementIDs.Level20: return 20;
            case AchievementIDs.Level25: return 25;
            case AchievementIDs.Level30: return 30;
            }
            
            return -1;
        }
    }
    
    public bool reportProgress {
        get {
            switch (id) {
            case AchievementIDs.Clean100:
            case AchievementIDs.Clean500:
            case AchievementIDs.Clean1000:
            case AchievementIDs.Clean2000:
            case AchievementIDs.Clean10000: return true;
            
            case AchievementIDs.MissileClean100:
            case AchievementIDs.MissileClean200:
            case AchievementIDs.MissileClean300:
            case AchievementIDs.MissileClean400:
            case AchievementIDs.MissileClean500: return true;
                
            case AchievementIDs.Level10:
            case AchievementIDs.Level15:
            case AchievementIDs.Level20:
            case AchievementIDs.Level25:
            case AchievementIDs.Level30: return false;
            }
            
            return false;
        }
    }
    
    public int medals {
        get {
            switch (id) {
            case AchievementIDs.Clean100: return 5;
            case AchievementIDs.Clean500: return 10;
            case AchievementIDs.Clean1000: return 20;
            case AchievementIDs.Clean2000: return 50;
            case AchievementIDs.Clean10000: return 100;
            
            case AchievementIDs.MissileClean100: return 10;
            case AchievementIDs.MissileClean200: return 20;
            case AchievementIDs.MissileClean300: return 30;
            case AchievementIDs.MissileClean400: return 40;
            case AchievementIDs.MissileClean500: return 50;
                
            case AchievementIDs.Level10: return 5;
            case AchievementIDs.Level15: return 10;
            case AchievementIDs.Level20: return 20;
            case AchievementIDs.Level25: return 30;
            case AchievementIDs.Level30: return 50;
            }
            
            return -1;
        }
    }
}
