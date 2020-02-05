using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour
{
	public float speed;
	public float damage;
	
	public virtual void Impact()
	{
		Destroy(gameObject);
	}
}
