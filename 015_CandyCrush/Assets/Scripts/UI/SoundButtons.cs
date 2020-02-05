using UnityEngine;
using System.Collections;

public class SoundButtons : MonoBehaviour
{
	UIButton musicButton;
	UIButton soundfxButton;
	
	void OnEnable()
	{
		Messenger.AddListener(MessageTypes.GameOver, Hide);
	}
	
	void OnDisable()
	{
		Messenger.RemoveListener(MessageTypes.GameOver, Hide);
	}
	
	void Hide()
	{
		musicButton.hidden = true;
		soundfxButton.hidden = true;
	}

	void Start ()
	{
		int x = 5, y = 5;
		var musicFile = SoundPlayer.musicOn ? "button_music_on.png" : "button_music_off.png";
		musicButton = UIButton.create(musicFile, musicFile, x, y);
		musicButton.onTouchUpInside += SwitchMusic;
		
		x += 65;
		var soundfxFile = SoundPlayer.soundfxOn ? "button_soundfx_on.png" : "button_soundfx_off.png";
		soundfxButton = UIButton.create(soundfxFile, soundfxFile, x, y);
		soundfxButton.onTouchUpInside += SwitchSoundFX;
	}

	void SwitchMusic(UIButton btn)
	{
		SoundPlayer.SwitchMusic();
		musicButton.setSpriteImage(SoundPlayer.musicOn ? "button_music_on.png" : "button_music_off.png");
	}
	
	void SwitchSoundFX(UIButton btn)
	{
		SoundPlayer.SwitchFX();
		soundfxButton.setSpriteImage(SoundPlayer.soundfxOn ? "button_soundfx_on.png" : "button_soundfx_off.png");
	}
}
