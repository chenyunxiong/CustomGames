using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Craft : MonoBehaviour
{
    public float speed = 20;
    
    float lookDamping = 2;
    Transform body;
    Transform energyShield;
    Transform towardsTarget;
 
    public List<Destructible> parts { get; private set; }
 
    public List<Weapon> weapons { get; private set; }
    
    public List<Cannon> cannons { get; private set; }
 
    public MissileLauncher missileLauncher { get; private set; }
 
    public bool destroyed { get; private set; }

	void Awake()
	{
        body = transform.FindChild("Body");
        energyShield = transform.FindChild("EnergyShield");
        
        cannons = new List<Cannon>(GetComponentsInChildren<Cannon>());
        missileLauncher = GetComponentInChildren<MissileLauncher>();
     
        parts = new List<Destructible>(GetComponentsInChildren<Destructible>());
        foreach (var part in parts) {
            part.OnDamage += OnPartDamage;
        }
     
        weapons = new List<Weapon>(GetComponentsInChildren<Weapon>());
	}

    void OnPartDamage(Destructible part, float damage)
    {
        foreach (var p in parts) {
            if (!p.destroyed) {
                return;
            }
        }
     
        Crash();
    }
 
    void Crash()
    {
        destroyed = true;
     
        var explosion = Resources.Load("explosion");
        Instantiate(explosion, transform.position, transform.rotation);
     
        var sound = (AudioClip)Resources.Load("Sound/explosion");
        AudioSource.PlayClipAtPoint(sound, transform.position);
     
        Messenger<Craft>.Broadcast(MessageTypes.CraftDestroyed, this);
     
        Destroy(gameObject);
    }

    void LateUpdate()
    {
        if (LevelHandler.instance.paused) {
            return;
        }

        if (towardsTarget) {
	        var rotation = Quaternion.LookRotation(towardsTarget.position - transform.position);
	        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * lookDamping);
	
	        if (body) {
	            Slant();
	        }
		}
    }
 
    void Slant()
    {
        var wantedEuler = Vector3.zero;
     
        if (towardsTarget) {
            var direction = towardsTarget.position - transform.position;
            var relative = transform.InverseTransformDirection(direction).normalized;
         
            if (Mathf.Abs(relative.x) > 0.1f) {
                wantedEuler.z = -relative.x * 540;
            }
        }
     
        body.localRotation = Quaternion.Lerp(body.localRotation, Quaternion.Euler(wantedEuler), Time.deltaTime * 2);
    }
    
    public void RotateTo(Transform target)
    {
        towardsTarget = target;
    }
 
    public void FireCannon()
    {
        foreach (var c in cannons) {
            c.Fire();
        }
    }
 
    public void LaunchMissile(Destructible target)
    {
        missileLauncher.Fire(target);
    }
 
    public void OpenShield()
    {
        body.collider.enabled = false;
        energyShield.gameObject.active = true;
    }
    
    public void CloseShield()
    {
        body.collider.enabled = true;
        energyShield.gameObject.active = false;
    }
}
