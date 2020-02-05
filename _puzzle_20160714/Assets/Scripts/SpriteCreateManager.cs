using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SpriteCreateManager :MonoBehaviour {

	// 行列属性
	public int rowNum = 0;
	public int columNum = 0;
	public int width = 0;
	public int height = 0;
	public int index;

	// 图片随机生成和打乱
	public ImageType[] images;
	public GameObject imageParent;
	public RectTransform rect;
	public ArrayList indexArray;

	// 鼠标点击交换
	public bool isSelect = false;
	public ArrayList changeArray;

	// 是否完成两次鼠标点击
	private bool isDoubleClickDone = false;

	void Start()
	{
		Init2();
		BreakSprite();
	}

	void Update()
	{
//		OnMouseClickEvent();
		ChangeIcon();
	}

	/// <summary>
	/// 初始化变量
	/// </summary>
	void Init2()
	{
		rowNum = 3;
		columNum = 3;

		width = 128;
		height = 128;

		indexArray = new ArrayList();
		changeArray = new ArrayList();
		imageParent = GameObject.Find("Canvas");

//		Listener();
	}

	void Listener()
	{
//		Tool_MouseEventListener.Get( images[0].gameObject ).OnPointerClick = onClickButtonHandler;
	}

	/// <summary>
	/// 生成图片
	/// </summary>
	void BreakSprite()
	{
		for (int rowIndex = 0; rowIndex < rowNum; rowIndex++) {
			for (int columnIndex = 0; columnIndex < columNum; columnIndex++) {

				GetIndex();
				Debug.Log("index = " + index);
				GameObject go = Instantiate( images[index].gameObject, images[index].transform.position, Quaternion.identity ) as GameObject;
				go.name = "Image"+(index + 1);
				go.transform.SetParent(imageParent.transform);
				rect = go.GetComponent<RectTransform>();
				rect.anchoredPosition = new Vector2( rowIndex * width - width, columnIndex * height - height);
			}
		}
	}

	/// <summary>
	/// 并去掉重复 取得唯一的Indwx
	/// </summary>
	void GetIndex()
	{
		int tempIndex = 0;
		while(true)
		{
			tempIndex = Random.Range(0,9);
			if( indexArray.Count > 0 )
			{
				bool isLoooNormalOver = true;
				for (int i = 0; i < indexArray.Count; i++) {

					if( (int)indexArray[i] == tempIndex )
					{
						isLoooNormalOver = false;
						break;
					}
				}
				if( isLoooNormalOver )
				{
					index = tempIndex;
					indexArray.Add(index);
					break;
				}
			}
			else 
			{
				index = tempIndex;
				indexArray.Add(index);
				break;
			}

			if( indexArray.Count > 9) 
				return;
		}
	}

	/// <summary>
	/// 点击交换图片
	/// </summary>
	void ChangeIcon()
	{
		if( changeArray.Count == 2 )
		{
//			RectTransform tempRect = 
			Reset();
		}
	}


	/// <summary>
	/// 完成两次点击
	/// </summary>
	/// <returns>The click.</returns>
	private int clickTime = 0;
	public int ClickTime()
	{
		return clickTime;
	}

	public void ClickTimeCount()
	{
		clickTime += 1;	
	}

	public void ClearClickTime()
	{
		clickTime = 0;
	}

	/// <summary>
	/// 是否完成两次点击
	/// </summary>
	/// <returns><c>true</c>, if click done was doubled, <c>false</c> otherwise.</returns>
	public bool GetDoubleClickDone()
	{
		return isDoubleClickDone;
	}

	public void SetDoubleClickDone(bool done)
	{
		isDoubleClickDone = done;
	}

	/// <summary>
	/// 重置所有相关信息
	/// </summary>
	public void Reset()
	{
		if( GetDoubleClickDone () )
		{
			ClearClickTime();
			if( GetDoubleClickDone() )
			{
				SetDoubleClickDone(false);
			}
			ResetChangeArrary();
		}
	}

	/// <summary>
	/// 重置交换函数
	/// </summary>
	public void ResetChangeArrary()
	{
		changeArray.Clear();
	}

	/// <summary>
	/// 判断周边图片的类型
	/// </summary>
	/// <param name="currentType">Current type.</param>
	/// <param name="aroundType">Around type.</param>
	public void AroundImage(int currentType, int aroundType)
	{
//		if( currentType + 1 == aroundType || currentType + 3 == aroundType || 
	}

}
