using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour
{
	public float endurance = 100;
	
	public bool autoDestroy = false;
	
	public float fullEndurance { get; private set; }
	
	public bool destroyed { get { return endurance <= 0; } }
	
	public event System.Action<Destructible, float> OnDamage;
	
	void Awake()
	{
		tag = Tags.Destructible;
		fullEndurance = endurance;
	}
	
	public void ApplyDamage(float damage)
	{
		if (destroyed) {
			return;
		}
		
		endurance -= damage;
		
		if (endurance < 0) {
			endurance = 0;
			collider.enabled = false;
		}
			
		if (OnDamage != null) {
			OnDamage(this, damage);
		}
		
		if (autoDestroy && destroyed) {
			Destroy(gameObject);
		}
	}
}
