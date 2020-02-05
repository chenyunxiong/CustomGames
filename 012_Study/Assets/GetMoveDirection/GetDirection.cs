using UnityEngine;
using System.Collections;

public class GetDirection : MonoBehaviour {

	Vector3 lastPos = Vector3.zero;
	float lastTimer = 0;
	float dtime = 0;
	Vector3 currentDir = Vector3.zero;
	float speed = 0;

	void OnEnable()
	{
		lastTimer = Time.time;
		lastPos = transform.position;
	}

	void Update()
	{
//		Debug.Log ("timer = " + lastTimer);
		transform.position += Vector3.forward * Time.deltaTime;

		dtime = Time.time - lastTimer;
//		Debug.Log ("dtime = " + dtime);
		if( dtime > 0 )
		{
			lastTimer = Time.time;
			currentDir = (transform.position - lastPos).normalized;
			speed = Vector3.Distance(transform.position, lastPos) / lastTimer;
			Debug.Log ("currentDir = " + currentDir);
			Debug.Log ("speed = " + speed);
		}


	}
}
