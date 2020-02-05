using UnityEngine;
using System.Collections;

public class ComboHandler : MonoBehaviour
{
	public static ComboHandler current { get; private set; }
	
	int count;
	float interval = 2;
	float timer;
	
	int baseScore;
	
	public int maxCombo { get; private set; }
	
	UIText text;
	UITextInstance comboText;

	void OnEnable()
	{
		Messenger<Aerolite>.AddListener(MessageTypes.AeroliteDestroyed, CalcCombo);
        Messenger<Enemy>.AddListener(MessageTypes.EnemyDestroyed, CalcCombo);   
	}
	
	void OnDisable()
	{
		Messenger<Aerolite>.RemoveListener(MessageTypes.AeroliteDestroyed, CalcCombo);
        Messenger<Enemy>.RemoveListener(MessageTypes.EnemyDestroyed, CalcCombo);   
	}
	
	void Awake()
	{
		current = this;
	}

	void Start()
	{
		text = UIHelper.getText();
		
		var x = Screen.width / 2 + 100;
		var y = Screen.height / 2 - 210;
		comboText = text.addTextInstance("X", x, y);
		comboText.setColorForAllLetters(Colors.HighlightText);
		comboText.alignMode = UITextAlignMode.Center;
		comboText.verticalAlignMode = UITextVerticalAlignMode.Middle;
		comboText.hidden = true;
		comboText.textScale = 2;
	}

	void CalcCombo(Object target)
	{
		if (timer > 0) {
			timer = interval;
         
			if (++count > 1) {
                interval *= 0.9f;      
                
				DrawCombo();
				Messenger<int>.Broadcast(MessageTypes.ComboIncrease, count);
			}
			
		} else {
			timer = interval = 2;
			count = 1;
			baseScore = LevelHandler.instance.score;
		}
	}
	
	void Update()
	{
		if (timer > 0) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				comboText.hidden = true;
				
				if (count > 1) {
					Messenger<int, int>.Broadcast(MessageTypes.ComboEnd, baseScore, count);
					
					if (maxCombo < count) {
						maxCombo = count;
					}
				}
			}
		}
	}
	
	void DrawCombo()
	{
        comboText.clear();
		comboText.hidden = false;
		comboText.text = "x" + count;
		comboText.textScale = 2;
		comboText.xPos = Screen.width - comboText.width / 2;
		comboText.scaleTo(0.5f, Vector3.one * 2, Easing.Elastic.easeIn);
	}
}
