using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
	UIButton backButton;
	UIButton restartButton;
	UIButton resumeButton;

	void Start ()
	{
		var buttonOffset = new UIEdgeOffsets(20);
		
		int x = 327;
		int y = Screen.height / 2;
		backButton = UIButton.create("button_back.png", "button_back.png", x, y);
		backButton.normalTouchOffsets = buttonOffset;
		backButton.onTouchUpInside += delegate(UIButton obj) {
            LevelLoader.Load("MainMenu");
		};
		
		x = 449;
		restartButton = UIButton.create("button_restart.png", "button_restart.png", x, y);
		restartButton.normalTouchOffsets = buttonOffset;
		restartButton.onTouchUpInside += delegate(UIButton obj) {
            LevelLoader.Load("Level01");   
		};
		
		x = 571;
		resumeButton = UIButton.create("button_resume.png", "button_resume.png", x, y);
		resumeButton.normalTouchOffsets = buttonOffset;
		resumeButton.onTouchUpInside += delegate(UIButton obj) {
			LevelHandler.instance.Resume();
		};
		
		Hide();
	}
	
	void OnEnable()
	{
		Messenger.AddListener(MessageTypes.GamePause, Show);
		Messenger.AddListener(MessageTypes.GameResume, Hide);
        Messenger.AddListener(MessageTypes.ShowHelpScreen, Hide);      
        Messenger.AddListener(MessageTypes.HideHelpScreen, Show);      
	}
	
	void OnDisable()
	{
		Messenger.RemoveListener(MessageTypes.GamePause, Show);
		Messenger.RemoveListener(MessageTypes.GameResume, Hide);
        Messenger.RemoveListener(MessageTypes.ShowHelpScreen, Hide);         
        Messenger.RemoveListener(MessageTypes.HideHelpScreen, Show);      
	}

	void Show()
	{
		backButton.hidden = false;
		backButton.disabled = false;
		restartButton.hidden = false;
		restartButton.disabled = false;
		resumeButton.hidden = false;
		resumeButton.disabled = false;
	}
	
	void Hide()
	{
		backButton.hidden = true;
		backButton.disabled = true;
		restartButton.hidden = true;
		restartButton.disabled = true;
		resumeButton.hidden = true;
		resumeButton.disabled = true;
	}
}
