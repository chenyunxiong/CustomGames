using UnityEngine;
using System.Collections;

public class Missile : Projectile
{
	public static Missile Create(Missile original, Vector3 position, Quaternion rotation)
	{
		return (Missile)Instantiate(original, position, rotation);
	}
	
	[HideInInspector]
	public Destructible target;
	
	Destructible damageable;
	
	void Awake()
	{
		damageable = gameObject.AddComponent<Destructible>();
		damageable.endurance = 10;
		damageable.OnDamage += OnDamage;
	}
	
	void Start()
	{
		StartCoroutine(Guide());
	}

	void Update()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		
		if (transform.position.sqrMagnitude > LevelHandler.instance.sqrFarest) {
			Destroy(this.gameObject);
		}
	}
	
	IEnumerator Guide()
	{
		bool locked = false;
		Vector3 rotateVelocity = Vector3.zero;
		
		var timer = 0.3f;
		
		while (target) {
			var direction = target.transform.position - transform.position;
			
			if (!locked && timer > 0) {
				timer -= Time.deltaTime;
				transform.forward = Vector3.SmoothDamp(transform.forward, direction, ref rotateVelocity, 2);
				if (Vector3.Angle(transform.forward, direction) < 5) {
					locked = true;
				}
			} else {
				transform.forward = direction;
			}
			
			yield return null;
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (target && col.transform == target.transform) {
			Impact();
			target.ApplyDamage(damage);
   
            if (target.destroyed && target.GetComponent<Aerolite>()) {
                Messenger.Broadcast(MessageTypes.DestroyAeroliteByMissile);
            }
		}
	}
	
	void OnDamage(Destructible sender, float damage)
	{
		if (sender.destroyed) {
			Impact();
		}
	}
	
	public override void Impact()
	{
		var explosion = Resources.Load("explosion");
		Instantiate(explosion, transform.position, transform.rotation);
		
		Destroy(gameObject);
	}
}
