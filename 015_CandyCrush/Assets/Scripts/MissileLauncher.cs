using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileLauncher : Weapon
{
	public Missile missile;
	
	public float reloadSpeed = 2;
	public float fireRate = 0.3f;

	List<MissileBarrel> barrels;

	int currentBarrel = 0;
	bool coolingDown;
	bool reloading;
	float origiReloadSpeed;

	public override bool available { get { return !coolingDown && !reloading; } }

	public int totalCount { get { return barrels.Count; } }

	public int availableCount { get { return reloading ? 0 : (barrels.Count - currentBarrel); }}

	public bool empty { get { return currentBarrel == barrels.Count; } }

    void Awake()
    {
        barrels = new List<MissileBarrel>(GetComponentsInChildren<MissileBarrel>());
        origiReloadSpeed = reloadSpeed;
    }

	public void Fire(Destructible target)
	{
		if (!available) {
			return;
		}
		
		var barrel = barrels[currentBarrel];
		var m = Missile.Create(missile, barrel.transform.position, barrel.transform.rotation);
		m.gameObject.layer = gameObject.layer;
		m.target = target;
		
		++currentBarrel;
		
		Messenger.Broadcast(MessageTypes.LaunchMissile);
		
		if (empty) {
			StartCoroutine(Reload());
		} else {
			StartCoroutine(CoolDown());
		}
	}

	public void ResetReloadSpeed()
	{
		reloadSpeed = origiReloadSpeed;
	}

	IEnumerator Reload()
	{
		reloading = true;
        
        if (reloadSpeed > 0) {
    		yield return new WaitForSeconds(reloadSpeed);
        }   
        
		currentBarrel = 0;
		reloading = false;
		
		Messenger.Broadcast(MessageTypes.MissileLauncherReloaded);
	}
	
	IEnumerator CoolDown()
	{
		coolingDown = true;
		yield return new WaitForSeconds(fireRate);
		coolingDown = false;
	}
}
