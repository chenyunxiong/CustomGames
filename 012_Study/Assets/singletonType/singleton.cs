using UnityEngine;
using System.Collections;

public class singleton<T>: MonoBehaviour where T : MonoBehaviour {

	private static T instance;



	public static T Instance
	{
		get
		{
			if( instance == null )
			{
				GameObject singleton2 = new GameObject();
				singleton2.name = "Singleton";
				instance = singleton2.AddComponent<T>();
			}
			return instance;
		}
	}

//	public void Awake()
//	{
//		if(instance == null)
//		{
//			instance = GetComponent<T>();
//		}
//	}

	public void OnApplicationQuit()
	{
		instance = null;
		Debug.Log ("Quit");
	}

}
