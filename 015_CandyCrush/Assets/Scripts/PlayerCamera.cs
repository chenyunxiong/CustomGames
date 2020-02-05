using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
	float depth;
	
	void Start()
	{
		depth = camera.depth;
	}
	
	void OnEnable()
	{
		Messenger.AddListener(MessageTypes.GameOver, DecreaseDepth);
		Messenger.AddListener(MessageTypes.GamePause, DecreaseDepth);
		Messenger.AddListener(MessageTypes.GameResume, IncreaseDepth);
	}
	
	void OnDisable()
	{
		Messenger.RemoveListener(MessageTypes.GameOver, DecreaseDepth);
		Messenger.RemoveListener(MessageTypes.GamePause, DecreaseDepth);
		Messenger.RemoveListener(MessageTypes.GameResume, IncreaseDepth);
	}
	
	void DecreaseDepth()
	{
		camera.depth = 0;
	}
	
	void IncreaseDepth()
	{
		camera.depth = depth;
	}
}
