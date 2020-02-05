using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Destructible))]
public class ExplodeOnDestroy : MonoBehaviour
{
	public float radius = 30;
	
	Destructible destructible;
	
	void Start()
	{
		destructible = GetComponent<Destructible>();
		destructible.OnDamage += OnDamage;
	}

	void OnDamage (Destructible obj, float damage)
	{
		if (obj.destroyed) {
			Instantiate(Resources.Load("fire_explode"), transform.position, Quaternion.identity);
			LevelHandler.instance.StartCoroutine(DelayExplode(0.5f, radius, transform.position));
		}
		
	}
	
	static IEnumerator DelayExplode(float delay, float radius, Vector3 position)
	{
		yield return new WaitForSeconds(delay);
		
		var aerolites = Physics.OverlapSphere(position, radius, 1 << Layers.Enemy);
		foreach (var col in aerolites) {
            var destructible = col.GetComponent<Destructible>();
            if (destructible) {
                destructible.ApplyDamage(Mathf.Infinity);
            }
		}
	}
}
