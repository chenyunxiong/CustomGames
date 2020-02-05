using UnityEngine;
using System.Collections;

public class example : MonoBehaviour {

	public GUIText text;

	void Start () {
	
		text.text = singletonInstance.Instance.ShowMsg("chenyunxiong");
	}

}
