using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public int amount = 500;

	float speed = 5;
	Transform target;
	Vector3 rotateVelocity;
	
	float findingTime = 0.5f;

	void Start()
	{
		if (PlayerControl.instance.craft == null) {
			Destroy(gameObject);
			return;
		}
		
		target = PlayerControl.instance.craft.transform;
		
		transform.forward = Random.onUnitSphere;
	}

	void LateUpdate()
	{
		if (target == null) {
			Destroy(gameObject);
			return;
		}
		
		var direction = target.position - transform.position;
		if (findingTime > 0) {
			transform.forward = Vector3.SmoothDamp(transform.forward, direction, ref rotateVelocity, 2);
			findingTime -= Time.deltaTime;
		} else {
			transform.forward = direction;
		}
		
		transform.Translate(Vector3.forward * speed);
		
		if (Vector3.Distance(transform.position, target.position) < 5) {
//			LevelHandler.current.GainScore(amount);
			Destroy(gameObject);
		}
	}
}
