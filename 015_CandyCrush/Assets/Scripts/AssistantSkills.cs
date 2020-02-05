using UnityEngine;
using System.Collections;

public class AssistantSkills : MonoBehaviour
{
    UISprite[] skillIcons;
    float[] skillIntervals;
    
    public AudioClip skillStart;
    
    void Awake()
    {
        skillIcons = new UISprite[3];
        skillIntervals = new float[3];
    }

    void Start()
    {
//       int x = Screen.width - 140;
//       int y = Screen.height - 380;
//       cannonX2 = UIButton.create("button_missile.png", "button_missile.png", x, y);
//       cannonX2.normalTouchOffsets = new UIEdgeOffsets(20);
//       cannonX2.highlightedTouchOffsets = new UIEdgeOffsets(20);
//       cannonX2.onTouchUpInside += CannonPowerUp;
    }
 
    void OnEnable()
    {
        Messenger<int>.AddListener(MessageTypes.ComboIncrease, CheckAssistantSkills);
        Messenger.AddListener(MessageTypes.GameOver, GameOver);
    }
 
    void OnDisable()
    {
        Messenger<int>.RemoveListener(MessageTypes.ComboIncrease, CheckAssistantSkills);
        Messenger.RemoveListener(MessageTypes.GameOver, GameOver);
    }
 
    void CheckAssistantSkills(int combo)
    {
        if (!LevelHandler.instance.isPlaying) {
            return;
        }
        
        for (int i = 0; i < 3; i++) {
            if (combo == getSkillCombo(i)) {
                ActivateSkill(i);
            }
        }
    }
    
    int getSkillCombo(int index)
    {
        switch (index) {
        case 0: return 10;
        case 1: return LevelHandler.instance.level > 20 ? 15 : 12;
        case 2: return LevelHandler.instance.level > 20 ? 18 : 15;
        }
        return -1;
    }
    
    void ActivateSkill(int index)
    {
        var interval = getSkillInterval(index);
        
        if (skillIntervals[index] > 0) {
            skillIntervals[index] = interval;
        } else {
            skillIntervals[index] = interval;
            StartCoroutine(SkillEffect(index));
        }
    }
    
    float getSkillInterval(int index)
    {
        switch (index) {
        case 0: return 10;
        case 1: return LevelHandler.instance.level > 20 ? Mathf.Clamp(30 - LevelHandler.instance.level, 3, 10) : 10;
        case 2: return 10;
        }
        return -1;
    }
    
    string getSkillStartMessage(int index)
    {
        switch (index) {
        case 0: return MessageTypes.CannonPowerUp;
        case 1: return MessageTypes.OpenEnergyShield;
        case 2: return MessageTypes.StartInfiniteMissile;
        }
        return string.Empty;
    }
    
    string getSkillStopMessage(int index)
    {
        switch (index) {
        case 0: return MessageTypes.CannonPowerDown;
        case 1: return MessageTypes.CloseEnergyShield;
        case 2: return MessageTypes.StopInfiniteMissile;
        }
        return string.Empty;
    }
    
    IEnumerator SkillEffect(int index)
    {
//        Debug.Log("start skill: " + index);
        
        SoundPlayer.PlayFX(skillStart);
        
        StartCoroutine(ShowSkillIcon(index));
     
        Messenger.Broadcast(getSkillStartMessage(index), MessengerMode.DONT_REQUIRE_LISTENER);
        
        while (skillIntervals[index] > 0) {
            skillIntervals[index] -= Time.deltaTime;
            yield return null;
        }
        
//        Debug.Log("stop skill: " + index);
        Messenger.Broadcast(getSkillStopMessage(index), MessengerMode.DONT_REQUIRE_LISTENER);
    }
    
    string getSkillIcon(int index)
    {
        switch (index) {
        case 0:
            return "cannon_powerup.png";
        case 1:
            return "energy_shield.png";
        case 2:
            return "infinite_missile.png";
        }
        return string.Empty;
    }
    
    IEnumerator ShowSkillIcon(int index)
    {
        var posX = 336 + 45 * index;
        var posY = 75;
        
        if (skillIcons[index] == null) {
            skillIcons[index] = UIToolkit.instance.addSprite(getSkillIcon(index), posX, posY);
            skillIcons[index].centerize();
            skillIcons[index].autoRefreshPositionOnScaling = false;
        } else {
            skillIcons[index].hidden = false;
        }
     
        skillIcons[index].positionFromTo(0.3f, new Vector3(posX, -110, 0), new Vector3(posX, -100, 0), Easing.Linear.easeIn);
        skillIcons[index].alphaFromTo(0.3f, 0, 1, Easing.Linear.easeIn);
        yield return new WaitForSeconds(0.3f);
        
        while (skillIntervals[index] > 0) {
            if (skillIcons[index].hidden) {
                skillIcons[index].hidden = false;
            }
            
            while (skillIntervals[index] > 0 && skillIntervals[index] < 3) {
                skillIcons[index].hidden = !skillIcons[index].hidden;
                yield return new WaitForSeconds(0.3f);
            }
            
            yield return null;
        }
     
        skillIcons[index].positionFromTo(0.3f, new Vector3(posX, -100, 0), new Vector3(posX, -90, 0), Easing.Linear.easeIn);
        skillIcons[index].alphaFromTo(0.3f, 1, 0, Easing.Linear.easeIn);
        yield return new WaitForSeconds(0.3f);
        skillIcons[index].hidden = true;
    }
    
    void GameOver()
    {
        for (int i = 0; i < skillIcons.Length; i++) {
            if (skillIcons[i] != null) {
                skillIcons[i].hidden = true;
                skillIntervals[i] = 0;
            }
        }
    }
}

