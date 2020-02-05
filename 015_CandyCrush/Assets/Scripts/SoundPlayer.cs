using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour
{
	static AudioSource musicSource;
	static AudioSource soundfxSource;
	
	public static bool musicOn = true;
	public static bool soundfxOn = true;

	public AudioClip backgroundMusic;
	public AudioClip lockedTarget;
	public AudioClip aeroliteExplosion;
	public AudioClip launchMissile;
	public AudioClip gainScore;
	
	static bool available;

	void Awake()
	{
		musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.volume = 1;
		musicSource.loop = true;
		
		soundfxSource = gameObject.AddComponent<AudioSource>();
		soundfxSource.loop = true;
        soundfxSource.volume = 0.8f;
		
		available = true;
	}
	
	void OnDestroy()
	{
		available = false;
	}

	void Start()
	{
		musicSource.enabled = musicOn;
		soundfxSource.enabled = soundfxOn;
		
		if (musicOn) {
			PlayMusic(backgroundMusic);
		}
	}

	public static void PlayFX(AudioClip clip, float volume, bool loop)
	{
		if (soundfxSource.enabled) {
			if (loop) {
				soundfxSource.Stop();
				soundfxSource.loop = true;
				soundfxSource.clip = clip;
				soundfxSource.Play();
			} else {
				soundfxSource.PlayOneShot(clip, volume);
			}
		}
	}
	
	public static void StopFX(AudioClip clip)
	{
		if (available && soundfxSource.clip == clip) {
			soundfxSource.Stop();
		}
	}

	public static void PlayFX(AudioClip clip, float volume)
	{
		if (soundfxSource.enabled)
			soundfxSource.PlayOneShot(clip, volume);
	}

	public static void PlayFX(AudioClip clip)
	{
		if (soundfxSource.enabled)
			soundfxSource.PlayOneShot(clip, 1);
	}

	public static void PlayMusic(AudioClip clip)
	{
		if (musicSource.enabled) {
			musicSource.Stop();
			musicSource.clip = clip;
			musicSource.Play();
		}
	}
	
	public static void SwitchMusic()
	{
		musicOn = !musicOn;
		musicSource.enabled = musicOn;
  
        if (musicOn && musicSource.clip != null) {
            musicSource.Play();
        }
	}
	
	public static void SwitchFX()
	{
		soundfxOn = !soundfxOn;
		soundfxSource.enabled = soundfxOn;
	}

	void OnEnable()
	{
		Messenger<Aerolite>.AddListener(MessageTypes.AeroliteDestroyed, OnAeroliteDestroyed);
		Messenger<Destructible>.AddListener(MessageTypes.LockedTarget, OnLockedTarget);
		Messenger.AddListener(MessageTypes.LaunchMissile, OnLaunchMissile);
		Messenger<Aerolite>.AddListener(MessageTypes.GainAeroliteScore, OnGainAeroliteScore);
	}

	void OnDisable()
	{
		Messenger<Aerolite>.RemoveListener(MessageTypes.AeroliteDestroyed, OnAeroliteDestroyed);
		Messenger<Destructible>.RemoveListener(MessageTypes.LockedTarget, OnLockedTarget);
		Messenger.RemoveListener(MessageTypes.LaunchMissile, OnLaunchMissile);
		Messenger<Aerolite>.RemoveListener(MessageTypes.GainAeroliteScore, OnGainAeroliteScore);
	}

	void OnAeroliteDestroyed(Aerolite aerolite)
	{
		PlayFX(aeroliteExplosion);
	}

	void OnLockedTarget(Destructible target)
	{
		if (target != null) {
			PlayFX(lockedTarget);
		}
	}

	void OnGainAeroliteScore(Aerolite aerolite)
	{
		PlayFX(gainScore);
	}
	
	void OnLaunchMissile()
	{
		PlayFX(launchMissile);
	}
}
