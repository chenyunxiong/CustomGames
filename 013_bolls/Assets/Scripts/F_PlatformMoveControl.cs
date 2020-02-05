using UnityEngine;
using System.Collections;

public enum MoveDirection
{
	UpDown,
	LeftRight
}

public class F_PlatformMoveControl : MonoBehaviour {

	public MoveDirection moveDirection;

	private Vector3 leftPos = Vector3.zero;
	private Vector3 rightPos = Vector3.zero;
	private Vector3 upPos = Vector3.zero;
	private Vector3 downPos = Vector3.zero;
	private bool isArrived = false;

	private Rigidbody m_rigidbody;

	void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
		leftPos = transform.position - transform.right * 50;
		rightPos = transform.position + transform.right * 50;
		upPos = transform.position + transform.up * 3;
		downPos = transform.position - transform.up * 3;
		Debug.Log("leftPos = " + leftPos);
		Debug.Log("rightPos = " + rightPos);
	}

	void Update()
	{
		MoveTo();
	}

	#region
	void MoveTo()
	{
		if( moveDirection == MoveDirection.LeftRight )
		{
			if( Vector3.Distance( transform.position, rightPos ) >= 0.1f && isArrived == false)
			{
//				Debug.Log("enter");
//				m_rigidbody.velocity = new Vector3(30, 0, 0);
				transform.position += Vector3.right*0.5f;
				if( Vector3.Distance( transform.position, rightPos ) <= 0.1f )
					isArrived = true;
			}
			else if( Vector3.Distance( transform.position, leftPos ) >= 0.1f && isArrived )
			{
				transform.position -= Vector3.right*0.5f;
				if( Vector3.Distance( transform.position, leftPos ) <= 0.1f )
					isArrived = false;
//				Debug.Log("enter000000000000");
//				Vector3.MoveTowards( transform.position, leftPos, Time.deltaTime * 3 );
			}
		}
	}
	#endregion
}
