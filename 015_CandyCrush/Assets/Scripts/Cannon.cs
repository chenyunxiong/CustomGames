using UnityEngine;
using System.Collections;

public class Cannon : Weapon
{
    public float fireRate = 0.2f;
    public int capacity = 0;
    public float reloadSpeed = 1;

    public GameObject bullet;
    public AudioClip soundClip;

	bool coolingDown;
	bool reloading;
	int shootCount;

	public override bool available { get { return !coolingDown && !reloading; } }
    
    public void Fire()
    {
        if (!available) {
            return;
        }
     
        if (soundClip) {
            SoundPlayer.PlayFX(soundClip, 0.3f);
        }
     
        var b = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
        b.layer = gameObject.layer;
     
		if (capacity > 0 && ++shootCount == capacity) {
			StartCoroutine(Reload());
		} else {
	        StartCoroutine(CoolDown());
		}
    }

    IEnumerator CoolDown()
    {
        coolingDown = true;
        yield return new WaitForSeconds(fireRate);
        coolingDown = false;
    }

	IEnumerator Reload()
	{
		reloading = true;
		yield return new WaitForSeconds(reloadSpeed);
		shootCount = 0;
		reloading = false;
	}
    
    public void SetBullet(GameObject bullet)
    {
        this.bullet = bullet;
    }
}
