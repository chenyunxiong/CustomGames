using UnityEngine;
using System.Collections;

#region 子系统相关类
public class MessageModel
{
	string name;
	
	public string Name
	{
		get { return name; }
		set { name = value; }
	}
	
	string sex;
	
	public string Sex
	{
		get { return sex; }
		set { sex = value; }
	}
	
	int level;
	
	public int Level
	{
		get { return level; }
		set { level = value; }
	}
	
	float power;
	
	public float Power
	{
		get { return power; }
		set { power = value; }
	}
}

public class MessageInfo
{
	public void ShowAllInfo(MessageModel infoModel)
	{
		Debug.Log(string.Format("姓名:{0}， 性别:{1}， 级别：{2}，力量：{3}",infoModel.Name, infoModel.Sex, infoModel.Level, infoModel.Power));
	}
}

public class UpLevelController
{
	public void UpLevel(float power)
	{
		if (power > 100)
		{
			Debug.Log("升级！");
		}
	}
}

public class UIAnimaiton
{
	public void PlayAnimationWithEffect()
	{
		Debug.Log("播放UI动画！");
	}
	
	public void StopAnimationWithEffect()
	{
		Debug.Log("停止UI动画！");
	}
	
	public void PauseAnimationWithEffect()
	{
		Debug.Log("暂停UI动画");
	}
}

public class UIAnimationWithMessageInfo
{
	UIAnimaiton uiAnimation;
	UpLevelController upLevelController;
	
	public UIAnimationWithMessageInfo()
	{
		uiAnimation = new UIAnimaiton();
		upLevelController = new UpLevelController();
	}
	
	public void UpLevelWithPlayUIAnimation(float power)
	{
		uiAnimation.PlayAnimationWithEffect();
		upLevelController.UpLevel(power);
	}
}

#endregion