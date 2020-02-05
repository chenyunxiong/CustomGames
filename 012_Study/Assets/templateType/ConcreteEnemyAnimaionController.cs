using UnityEngine;
using System.Collections;


public class ConcreteEnemyAnimaionController : AbstractAnimationController
{
	/* 根据父类实现 田伯光的功夫 */

	protected override void PlayAnimation()
	{
		Debug.Log("播放田伯光快刀的动画");
	}
	
	protected override void StopAnimation()
	{
		Debug.Log("停止田伯光快刀的动画");
	}
	
	protected override void PauseAnimation()
	{
		Debug.Log("暂停田伯光快刀的动画");
	}
}
