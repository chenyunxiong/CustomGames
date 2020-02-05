using UnityEngine;
using System.Collections;

public class F_CameraFollow : MonoBehaviour {

	public Transform bollTrans;
	private Vector3 offset = Vector3.zero;

	void Start()
	{
		offset = transform.position - bollTrans.position;
	}

	void Update()
	{
		transform.position = offset + bollTrans.position;
	}

}
