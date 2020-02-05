using UnityEngine;
using System.Collections;

public class CollisionSiren : MonoBehaviour
{
	public AudioClip alertSound;
	
	float radius = 7;
	float distance = 50;
	
	bool alerting;
    bool disableSiren;
 
    void OnEnable()
    {
        Messenger.AddListener(MessageTypes.OpenEnergyShield, DisableSiren);
        Messenger.AddListener(MessageTypes.CloseEnergyShield, EnableSiren);
    }
    
    void OnDisable()
    {
        Messenger.RemoveListener(MessageTypes.OpenEnergyShield, DisableSiren);
        Messenger.RemoveListener(MessageTypes.CloseEnergyShield, EnableSiren);
    }
    
    void DisableSiren()
    {
        disableSiren = true;
        
        if (alerting) {
             alerting = false;
             Messenger.Broadcast(MessageTypes.CollisionAlertLifted, MessengerMode.DONT_REQUIRE_LISTENER);
             SoundPlayer.StopFX(alertSound);
         }
    }
    
    void EnableSiren()
    {
        disableSiren = false;
    }

	void FixedUpdate ()
	{
        if (disableSiren) {
            return;
        }
        
		var ray = new Ray(transform.position, Vector3.forward);
		if (Physics.SphereCast(ray, radius, distance, 1 << Layers.Enemy)) {
			if (!alerting) {
				alerting = true;
				Messenger.Broadcast(MessageTypes.CollisionAlert, MessengerMode.DONT_REQUIRE_LISTENER);
				SoundPlayer.PlayFX(alertSound, 0.6f, true);
			}
		} else {
			if (alerting) {
				alerting = false;
				Messenger.Broadcast(MessageTypes.CollisionAlertLifted, MessengerMode.DONT_REQUIRE_LISTENER);
				SoundPlayer.StopFX(alertSound);
			}
		}
	}
	
	void OnDestroy()
	{
		SoundPlayer.StopFX(alertSound);
	}
}
