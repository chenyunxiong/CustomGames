using UnityEngine;
using System.Collections;

public class FakeBullet : Bullet
{
	Vector3 destination;
	Ray damageRay;

	void Start()
	{
		damageRay = Crosshair.instance.GetRay();
		damageRay.origin = damageRay.GetPoint(50);

		destination = damageRay.GetPoint(200);
		
		transform.forward = destination - transform.position;
	}

	void Update()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		
		if (transform.position.z > destination.z) {
			ApplyDamage();
		}
	}
	
	void ApplyDamage()
	{
		RaycastHit hit;
		if (Physics.SphereCast(damageRay, 1, out hit, Mathf.Infinity, 1 << Layers.Enemy) && hit.collider.tag == Tags.Destructible) {
			Impact();
			hit.collider.GetComponent<Destructible>().ApplyDamage(damage);
		} else {
			Destroy(gameObject);
		}
	}
}
