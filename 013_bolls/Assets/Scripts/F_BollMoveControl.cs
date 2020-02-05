using UnityEngine;
using System.Collections;

public class F_BollMoveControl : MonoBehaviour {

	private Rigidbody m_rigidbody;
	public float upForce = 0;
	public float forwardForce = 0;

	void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		addForceToBoll();
	}

	void addForceToBoll()
	{
		float h = Input.GetAxis("Horizontal") * forwardForce * Time.deltaTime;
		//		float v = Input.GetAxis("Vertical") * upForce * Time.deltaTime;



		if( Input.GetKeyDown( KeyCode.Space ))
		{
			upForce *= Time.deltaTime;
		}

		m_rigidbody.velocity = new Vector3(h, upForce, 0);
	}

}
