using UnityEngine;
using System.Collections;


public class ConcreteHeroAnimaionController : AbstractAnimationController
{
	/* 根据父类实现 令狐冲的功夫 */

	protected override void PlayAnimation()
	{
		Debug.Log("播放令狐冲独孤九剑的招式动画");
	}
	
	protected override void StopAnimation()
	{
		Debug.Log("停止令狐冲独孤九剑的招式动画");   
	}
	
	protected override void PauseAnimation()
	{
		Debug.Log("暂停令狐冲独孤九剑的招式动画");
	}
}
