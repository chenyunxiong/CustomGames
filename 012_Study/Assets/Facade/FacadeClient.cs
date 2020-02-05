using UnityEngine;
using System.Collections;

public class FacadeClient:MonoBehaviour
{
	Facade facade;
	MessageModel messageModel;
	
	void Start()
	{
		facade = new Facade();
		
		#region 设置消息模型
		messageModel = new MessageModel();
		messageModel.Name = "蛮牛";
		messageModel.Sex = "男";
		messageModel.Level = 10;
		messageModel.Power = 200;
		#endregion
	}
	
	void OnGUI()
	{
		if(GUILayout.Button("ShowInfo"))
		{
			facade.ShowInfo(messageModel);
		}
		if (GUILayout.Button("Up"))
		{
			facade.Up(101);
		}
		if(GUILayout.Button("PalyAnimaiton"))
		{
			facade.PlayAnimaiton();
		}
		if(GUILayout.Button("StopAnimation"))
		{
			facade.StopAnimation();
		}
		if(GUILayout.Button("PauseAnimation"))
		{
			facade.PauseAnimation();
		}
		if(GUILayout.Button("UpAnimation"))
		{
			facade.UpAnimation(101);
		}
	}
}