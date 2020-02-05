using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelHandler : MonoBehaviour
{
    public static LevelHandler instance { get; private set; }

    public float farest = 1000;

    public float sqrFarest { get; private set; }
 
    public int level { get { return enemyGenerator.difficultLevel; } }

    public int score { get; private set; }
 
    public int cleanCount { get; private set; }
 
    public int missileCleanCount { get; private set; }

    public bool isPlaying { get { return PlayerControl.instance.craft != null; } }
 
    public float timer { get; private set; }
 
    public string formattedTimer { get { return timer.getTimeFormat(); } }
 
    public bool paused { get; private set; }
 
    EnemyGenerator enemyGenerator;
    bool delayShowStats;
 
    public void Pause()
    {
        paused = true;
        Messenger.Broadcast(MessageTypes.GamePause);
    }
 
    public void Resume()
    {
        paused = false;
        Messenger.Broadcast(MessageTypes.GameResume);
    }

    void Awake()
    {
        instance = this;
        sqrFarest = Mathf.Pow(farest, 2);
    }

    void Start()
    {
        enemyGenerator = (EnemyGenerator)FindObjectOfType(typeof(EnemyGenerator));
     
        timer = 0;
     
        if (Player.playTimes < 1) {
            Player.playTimes += 1;
            ShowHelpScreen();
        }
    }
 
    public void ShowHelpScreen()
    {
        Instantiate(Resources.Load("help_screen"));
    }
 
    void Update()
    {
        if (isPlaying && !paused) {
            timer += Time.deltaTime;
        }
    }

    void OnEnable()
    {
        Messenger<Aerolite>.AddListener(MessageTypes.AeroliteDestroyed, OnAeroliteDestroyed);
        Messenger<Enemy>.AddListener(MessageTypes.EnemyDestroyed, OnEnemyDestroyed);   
        Messenger.AddListener(MessageTypes.DestroyAeroliteByMissile, OnDestroyAeroliteByMissile);   
        Messenger<Craft>.AddListener(MessageTypes.CraftDestroyed, OnCraftDestroyed);
        Messenger<int, int>.AddListener(MessageTypes.ComboEnd, GainComboScore);
    }

    void OnDisable()
    {
        Messenger<Aerolite>.RemoveListener(MessageTypes.AeroliteDestroyed, OnAeroliteDestroyed);
        Messenger<Enemy>.RemoveListener(MessageTypes.EnemyDestroyed, OnEnemyDestroyed);   
        Messenger.RemoveListener(MessageTypes.DestroyAeroliteByMissile, OnDestroyAeroliteByMissile);      
        Messenger<Craft>.RemoveListener(MessageTypes.CraftDestroyed, OnCraftDestroyed);
        Messenger<int, int>.RemoveListener(MessageTypes.ComboEnd, GainComboScore);
    }
 
    void OnDestroyAeroliteByMissile()
    {
        missileCleanCount++;
    }
 
    void OnCraftDestroyed(Craft craft)
    {
        if (craft == PlayerControl.instance.craft) {
            StartCoroutine(GameOver());
        } else if (craft.gameObject.layer == Layers.Enemy) {
            Messenger<Enemy>.Broadcast(MessageTypes.EnemyDestroyed, craft.GetComponent<Enemy>());
        }
    }
 
    IEnumerator GameOver()
    {
        Messenger.Broadcast(MessageTypes.GameOver, MessengerMode.DONT_REQUIRE_LISTENER);
        
        Messenger<Aerolite>.AddListener(MessageTypes.AeroliteDestroyed, DelayStatsBoard);
        
        yield return new WaitForSeconds(3);
        while (delayShowStats) {
            delayShowStats = false;
            yield return new WaitForSeconds(3);
        }
        GetComponent<LevelStats>().Show();
     
        Messenger<Aerolite>.RemoveListener(MessageTypes.AeroliteDestroyed, DelayStatsBoard);   
    }
 
    void DelayStatsBoard(Aerolite aerolite)
    {
        delayShowStats = true;
    }

    void OnAeroliteDestroyed(Aerolite aerolite)
    {
        cleanCount++;
        GainAeroliteScore(aerolite);
    }

    void OnEnemyDestroyed(Enemy enemy)
    {
        cleanCount++;
        GainEnemyScore(enemy);
    }
 
    public void GainAeroliteScore(Aerolite aerolite)
    {
        score += aerolite.score;
        Messenger<Aerolite>.Broadcast(MessageTypes.GainAeroliteScore, aerolite);
    }
 
    public void GainEnemyScore(Enemy enemy)
    {
        score += enemy.score;
        Debug.Log("gain enemy score");
        Messenger<Enemy>.Broadcast(MessageTypes.GainEnemyScore, enemy);
    }
 
    public void GainComboScore(int baseScore, int comboCount)
    {
        var credit = (score - baseScore) * comboCount;
        score += credit;
        Messenger<int>.Broadcast(MessageTypes.GainComboScore, credit);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Vector3.zero, Vector3.forward * farest);
    }
}
