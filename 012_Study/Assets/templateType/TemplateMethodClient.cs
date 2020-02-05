using UnityEngine;
using System.Collections;

public class TemplateMethodClient:MonoBehaviour
{
	/* 具体程序实现 */

	AbstractAnimationController enemyAnimationController;
	AbstractAnimationController heroAnimationController;
	
	void Start()
	{
		enemyAnimationController = new ConcreteEnemyAnimaionController();
		heroAnimationController = new ConcreteHeroAnimaionController();
	}

	#region OnGUI
	void OnGUI()
	{
		if (GUILayout.Button("田伯光武功招式"))
		{
			enemyAnimationController.ShowKongFu();
		}
		
		if (GUILayout.Button("令狐冲武功招式"))
		{
			heroAnimationController.ShowKongFu();
		}
	}
	#endregion

	void Update()
	{
		Debug.Log ("Update");
		Debug.Log ("Awake");
	}
}
