using UnityEngine;
using System.Collections;

public class RotateAroundTarget : MonoBehaviour {

	public Transform targetPos;

	public float distance = 10f;
	public float height = 10f;
	public Vector3 direction;
	public float rotateSpeed = 1.5f;

	public float minAngle = 0;
	public float maxAngle = 360;
	public float angle = 0;

	void Start()
	{
		Init();
	}

	void Update()
	{
		RotateTarget();
	}

	void Init()
	{
		direction = new Vector3(0, height, -distance);
		minAngle = Mathf.Clamp ( minAngle, minAngle, minAngle);
		maxAngle = Mathf.Clamp ( minAngle, minAngle, maxAngle);
	}

	void RotateTarget()
	{
		Quaternion rotateAngle = Quaternion.Euler ( 0, angle, 0);

		direction = new Vector3(0, height, -distance);	

		transform.position = targetPos.position + rotateAngle * direction;

		transform.LookAt ( targetPos );

		angle += rotateSpeed;

		Debug.Log ("Angle = " + angle );

		if( angle > maxAngle || angle < minAngle)
		{
			angle = 0;
		}

	}
}
