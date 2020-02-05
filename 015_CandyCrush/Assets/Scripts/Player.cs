using UnityEngine;
using System.Collections;

public static class Player
{
    public static float getTimeRecord()
    {
        return PlayerPrefs.GetFloat("TIME_RECORD", 0);
    }
 
    public static void setTimeRecord(float time)
    {
        PlayerPrefs.SetFloat("TIME_RECORD", time);
    }
 
    public static int getLevelRecord()
    {
        return PlayerPrefs.GetInt("LEVEL_RECORD", 0);
    }
     
    public static void setLevelRecord(int level)
    {
        PlayerPrefs.SetInt("LEVEL_RECORD", level);
    }
     
    public static int getScoreRecord()
    {
        return PlayerPrefs.GetInt("SCORE_RECORD", 0);
    }
 
    public static void setScoreRecord(int score)
    {
        PlayerPrefs.SetInt("SCORE_RECORD", score);
    }
 
    public static int getComboRecord()
    {
        return PlayerPrefs.GetInt("COMBO_RECORD", 0);
    }
 
    public static void setComboRecord(int count)
    {
        PlayerPrefs.SetInt("COMBO_RECORD", count);
    }
 
    public static int getCleanCountRecord()
    {
        return PlayerPrefs.GetInt("CLEAN_COUNT_RECORD", 0);
    }
 
    public static void setCleanCountRecord(int count)
    {
        PlayerPrefs.SetInt("CLEAN_COUNT_RECORD", count);
    }
 
    public static int getTotalCleanCount()
    {
        return PlayerPrefs.GetInt("TOTAL_CLEAN_COUNT", 0);
    }
 
    public static void setTotalCleanCount(int count)
    {
        PlayerPrefs.SetInt("TOTAL_CLEAN_COUNT", count);
    }
 
    public static int playTimes {
        get { return PlayerPrefs.GetInt("PLAY_TIMES", 0); }
        set { PlayerPrefs.SetInt("PLAY_TIMES", value); }
    }
    
    public static int medals {
        get { return PlayerPrefs.GetInt("MEDALS", 0); }
        set { PlayerPrefs.SetInt("MEDALS", value); }
    }
    
    public static int missileDestroyCount {
        get { return PlayerPrefs.GetInt("MISSILE_DESTROY_COUND", 0); }
        set { PlayerPrefs.SetInt("MISSILE_DESTROY_COUND", value); }
    }
 
    public static Achievement getAchievement(string id)
    {
        var achievement = new Achievement();
        achievement.id = id;
        achievement.status = (AchievementStatus)PlayerPrefs.GetInt("ACHIEVEMENT_STATUS_" + id, 0);
//        achievement.progress = PlayerPrefs.GetInt("ACHIEVEMENT_PROGRESS_" + id, 0);
        
        return achievement;
    }
    
    public static void setAchievement(Achievement achievement)
    {
        PlayerPrefs.SetInt("ACHIEVEMENT_STATUS_" + achievement.id, (int)achievement.status);
//        PlayerPrefs.SetInt("ACHIEVEMENT_PROGRESS_" + id, (int)achievement.progress);
    }
}
