using UnityEngine;
using System.Collections;

public class GetSetExample : singleton<GetSetExample> {


	[SerializeField]
	private string _name;

	void OnGUI()
	{

		if( GUI.Button (new Rect(10, 10, 10, 10), "名字" ))
		{
			Debug.Log ("name = " + _name);
		}
	}
}
