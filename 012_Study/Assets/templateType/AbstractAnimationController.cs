using UnityEngine;
using System.Collections;


public abstract class AbstractAnimationController
{
	/* 功夫 模板 */
	protected abstract void PlayAnimation();
	protected abstract void StopAnimation();
	protected abstract void PauseAnimation();
	
	public  void ShowKongFu()
	{
		PlayAnimation();
		StopAnimation();
		PauseAnimation();
	}
}
