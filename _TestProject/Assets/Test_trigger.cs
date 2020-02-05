using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class Test_trigger : MonoBehaviour,IPointerClickHandler {

	public SpriteCreateManager spriteCreateManager;
//	public int type = 0;

	void Start()
	{
		Init();
	}

	void Init()
	{
		spriteCreateManager = GameObject.Find("SpriteCreateManager").GetComponent<SpriteCreateManager>();
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		Debug.Log("work");
		spriteCreateManager.tempImage = this.gameObject;

	}
}
