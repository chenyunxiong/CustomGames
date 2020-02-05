using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour
{
    public Aerolite[] aerolites;
    public Enemy[] enemies;
    public float interval = 1;
    public float speed = 20;
    public float radius = 30;
    int destroyedNumber;
    int aeroliteTypeLimited = 3;
    
    float endurationAddition = 0;
    
    public AudioClip levelUp;
    
    UIText text;
    UITextInstance levelText;
    
    public int difficultLevel { get; private set; }

    void Start()
    {
        text = UIHelper.getText();
        
        difficultLevel = 1;
    }
 
    void OnEnable()
    {
        Messenger<Aerolite>.AddListener(MessageTypes.AeroliteDestroyed, AdjustDifficulty);
        Messenger.AddListener(MessageTypes.PortalOpened, OnPortalOpened);
        Messenger.AddListener(MessageTypes.PortalDestroyed, OnPortalDestroyed);
    }
 
    void OnDisable()
    {
        Messenger<Aerolite>.RemoveListener(MessageTypes.AeroliteDestroyed, AdjustDifficulty);
        Messenger.RemoveListener(MessageTypes.PortalOpened, OnPortalOpened);
        Messenger.RemoveListener(MessageTypes.PortalDestroyed, OnPortalDestroyed);
    }
    
    void OnPortalOpened()
    {
        StartCoroutine("Generate");
    }
 
    IEnumerator Generate()
    {
        yield return new WaitForSeconds(1.5f);
        
        while (LevelHandler.instance.paused) {
            yield return null;
        }
         
        StartCoroutine(ShowLevel());
        
        bool generateEnemy = false;
        
        while (true) {
            while (LevelHandler.instance.paused) {
                yield return null;
            }
            
            if (difficultLevel > 3) {
                generateEnemy = (Random.value < 0.05f * difficultLevel);
            }
         
            var rand = Random.insideUnitCircle * radius;
            
            if (!generateEnemy) {
                // generate aerolites
                
                var index = Random.Range(0, aeroliteTypeLimited);
                if (difficultLevel > 3 && index == 5) {
                    if (Random.value < Mathf.Clamp01(difficultLevel / 20f) - 0.1f) {
                        index = 6;
                    }
                }
             
                var pos = new Vector3(rand.x, rand.y, transform.position.z);
                var aero = (Aerolite)Instantiate(aerolites[index], pos, Quaternion.identity);
                aero.speed = speed;
                aero.endurance *= 1 + endurationAddition;
                
            } else {

                // generate enemy
                var index = Random.Range(0, enemies.Length);
                var pos = new Vector3(rand.x, rand.y, transform.position.z);
                var enemy = (Enemy)Instantiate(enemies[index], pos, transform.rotation);
                enemy.craft.speed = speed;
            }
            
         
            yield return new WaitForSeconds(interval);
        }
    }
 
    void AdjustDifficulty(Aerolite aerolite)
    {
        destroyedNumber++;
        
        if (destroyedNumber % 30 == 0) {
            if (aeroliteTypeLimited < aerolites.Length) {
                aeroliteTypeLimited += 2;
                if (aeroliteTypeLimited > aerolites.Length) {
                    aeroliteTypeLimited = aerolites.Length;
                }
            }
            
            if (difficultLevel < 20) {
                interval *= 0.9f;
                speed *= 1.1f;
            } else {
                endurationAddition += 0.1f;
            }
            
            ++difficultLevel;
            Messenger<float>.Broadcast(MessageTypes.DifficultLevelUp, speed, MessengerMode.DONT_REQUIRE_LISTENER);

            StartCoroutine(ShowLevel());
        }
    }
    
    IEnumerator ShowLevel()
    {
//        SoundPlayer.PlayFX(levelUp);
        
        var color = Colors.HighlightText;
        
        if (levelText == null) {
            var size = text.sizeForText("Level " + difficultLevel);
            levelText = text.addTextInstance("Level " + difficultLevel, (Screen.width - size.x) / 2, (Screen.height - size.y) / 2);
//            color.a = 0.8f;
            levelText.setColorForAllLetters(color);
            levelText.alignMode = UITextAlignMode.Center;
            levelText.verticalAlignMode = UITextVerticalAlignMode.Middle;
        } else {
            var size = text.sizeForText("Level " + difficultLevel);
            levelText = text.addTextInstance("Level " + difficultLevel, (Screen.width - size.x) / 2, (Screen.height - size.y) / 2);
//            color.a = 0.8f;
            levelText.setColorForAllLetters(color);
            levelText.alignMode = UITextAlignMode.Center;
            levelText.verticalAlignMode = UITextVerticalAlignMode.Middle;
        }
        
        levelText.scaleFromTo(0.5f, Vector3.one * 2, Vector3.one * 1.5f, Easing.Quartic.easeOut);
//        levelText.alphaFromTo(0.5f, 0, 1, Easing.Quartic.easeOut);
        
        yield return new WaitForSeconds(0.5f);
        
        levelText.scaleFromTo(0.5f, Vector3.one * 1.5f, Vector3.one * 3, Easing.Quartic.easeIn);
//        levelText.alphaFromTo(0.5f, 1, 0, Easing.Linear.easeOut);

        yield return new WaitForSeconds(0.5f);
        levelText.clear();
    }
    
    void OnPortalDestroyed()
    {
        StopCoroutine("Generate");
    }
 
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 3);
    }
}
