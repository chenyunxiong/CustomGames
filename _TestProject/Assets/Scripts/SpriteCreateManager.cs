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
	public GameObject[] images;
	public GameObject imageParent;
	public RectTransform rect;
	public ArrayList indexArray;

	// 鼠标点击交换
	public bool isSelect = false;
	public GameObject tempImage;

	void Start()
	{
		Init2();
		BreakSprite();
	}

	void Update()
	{
//		OnMouseClickEvent();
//		ChangeIcon();
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
//				index = Random.Range(0,9);
				Debug.Log("index = " + index);
				GameObject go = Instantiate( images[index], images[index].transform.position, Quaternion.identity ) as GameObject;
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
		int count = 0;
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
//	void ChangeIcon()
//	{
//		if( Input.GetMouseButtonDown( 0 ) )
//		{
//			Debug.Log("tempImage====111111111 = ");
//			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
//			RaycastHit hitInfo;
//			Physics.Raycast(ray,out hitInfo, 1000);
//			Debug.DrawLine(Input.mousePosition, transform.forward * 1000, Color.red);
//			Debug.Log("position "+ hitInfo.collider.name);

//			if( Physics.Raycast(ray,out hitInfo, 1000))
//			{
//				
//				Debug.Log("tempImage====33333 = ");
//				if( hitInfo.collider.transform == null )
//				{
//					return;
//				}
//				else
//				{
//					if( !isSelect )
//					{
//						tempImage = hitInfo.collider.gameObject;
//						Debug.Log("tempImage =4444 " + tempImage.name);
//						isSelect = true;
//					}
//				}
//			}
//		}
//	}
//	public void OnMouseClickEvent()
//	{
//
//		if( Input.GetMouseButtonDown(0) )
//		{
//			Debug.Log("image = " + tempImage);
//		}
//	}

}
