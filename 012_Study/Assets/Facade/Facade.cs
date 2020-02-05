using UnityEngine;
using System.Collections;

public class Facade
{
	MessageInfo info;
	UIAnimaiton uiAnimation;
	UIAnimationWithMessageInfo uiWithInfo;
	UpLevelController upLevelController;
	
	public Facade()
	{
		info = new MessageInfo();
		uiAnimation = new UIAnimaiton();
		uiWithInfo = new UIAnimationWithMessageInfo();
		upLevelController = new UpLevelController();
	}
	
	public void ShowInfo(MessageModel messageModel)
	{
		info.ShowAllInfo(messageModel);
	}
	
	public void Up(float power)
	{
		upLevelController.UpLevel(power);
	}
	
	public void PlayAnimaiton()
	{
		uiAnimation.PlayAnimationWithEffect();
	}
	
	public void StopAnimation()
	{
		uiAnimation.StopAnimationWithEffect();
	}
	
	public void PauseAnimation()
	{
		uiAnimation.PauseAnimationWithEffect();
	}
	
	public void UpAnimation(float power)
	{
		uiWithInfo.UpLevelWithPlayUIAnimation(power);
	}
}