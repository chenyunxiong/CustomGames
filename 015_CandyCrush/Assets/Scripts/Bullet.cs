using UnityEngine;
using System.Collections;

public class Bullet : Projectile
{
	void Update()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		
		if (transform.position.sqrMagnitude > LevelHandler.instance.sqrFarest) {
			Destroy(this.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == Tags.Destructible) {
			Impact();
			col.GetComponent<Destructible>().ApplyDamage(damage);
		}
	}
}
