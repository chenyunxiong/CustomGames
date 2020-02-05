using UnityEngine;
using System.Collections;

public class singletonInstance : singleton<singletonInstance> {

	public int t
	{
		get { return t; }
		set{ t = value; }
	}

	public string ShowMsg( string msg )
	{
		return msg;
	}

}
