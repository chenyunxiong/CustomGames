using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Test_mouseClick_down : MonoBehaviour, IPointerClickHandler{

	public void OnPointerClick (PointerEventData eventData)
	{
		Debug.Log("this.name ");
	}
}
