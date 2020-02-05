using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageType : MonoBehaviour, IPointerDownHandler{

	private SpriteCreateManager spriteCreateManager;
	public int type;
	public Image image;

	void Start()
	{
		Init();
	}

	void Init()
	{
		spriteCreateManager = GameObject.Find("SpriteCreateManager").GetComponent<SpriteCreateManager>();
//		type = GetComponent<ImageType>().type;
	}

	/// <summary>
	/// 按下鼠标左键 将需要交换的图形加入到队列中
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerDown(PointerEventData eventData)
	{
		spriteCreateManager.ClickTimeCount();
		if( spriteCreateManager.changeArray.Count >= 1 )
		{
			if( type == (int)spriteCreateManager.changeArray[0] )
			{
				return;
			}
			else
			{
				spriteCreateManager.changeArray.Insert(2, type);
				if( spriteCreateManager.changeArray.Count > 2)
				{
					Debug.Log("length = " + spriteCreateManager.changeArray.Count);
					for (int i = 2; i < spriteCreateManager.changeArray.Count; i++) 
					{
						spriteCreateManager.changeArray.RemoveRange(2, spriteCreateManager.changeArray.Count-1);

						Debug.Log("length2222 = " + spriteCreateManager.changeArray.Count);
					}
				}
			}
		}
		else 
		{
			Debug.Log("type2 = " + type);
			spriteCreateManager.changeArray.Add( type );
		}
	}

}
