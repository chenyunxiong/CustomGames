using UnityEngine;
using System.Collections;

public class TargetMark : MonoBehaviour
{
	public static TargetMark Create(Transform target)
	{
		var obj = new GameObject("target_mark");
		var mark = obj.AddComponent<TargetMark>();
		mark.target = target;
		
		return mark;
	}
	
	public static void Destroy(TargetMark mark)
	{
		Destroy(mark.gameObject);
	}
	
	public Transform target;
	
	UISprite triangle;
	UISprite box;
	
	float rotAngle;
	
	public bool inited { get; private set; }
	
	public Vector3 position { get { return box.position; } }
	
	public bool locked {
		get { return !box.hidden; }
		set {
			if (box.hidden == !value)
				return;
			
			box.hidden = !value;
			
			if (value) {
				triangle.setSpriteImage("aiming_triangle_green.png");
			} else {
				triangle.setSpriteImage("aiming_triangle_red.png");
			}
		}
	}

	void Start()
	{
		box = UIToolkit.instance.addSprite("locking_box.png", 0, 0, 0, true);
		box.hidden = true;
		
		triangle = UIToolkit.instance.addSprite("aiming_triangle_red.png", 0, 0, 0, true);
		triangle.parentUIObject = box;
		
		inited = true;
	}

	void LateUpdate()
	{
		if (target == null) {
			TargetMark.Destroy(this);
			return;
		}
		
		var pos = Camera.main.WorldToScreenPoint(target.transform.position);
		if (pos.z > 0 && Camera.main.pixelRect.Contains(pos)) {
			pos.y = pos.y - Screen.height;
			pos.z = box.position.z;
		
			triangle.hidden = false;
//			box.hidden = false;
			box.position = pos;
			
		} else {
			triangle.hidden = true;
//			box.hidden = true;
		}
		
		
//		rotAngle += Time.deltaTime * 180;
//		if (rotAngle > 360) rotAngle -= 360;
//		triangle.eulerAngles = new Vector3(0, 0, rotAngle);
	}
	
	public bool HitTest(Vector3 point)
	{
		if (inited && target) {
			var frame = new Rect(triangle.position.x, triangle.position.y, triangle.width, triangle.height);
			frame.x -= frame.width / 2;
			frame.y -= frame.height / 2;
			
			return frame.Contains(point);
		}
		
		return false;
	}
	
	void OnDestroy()
	{
		if (UIToolkit.instance != null) {
			triangle.destroy();
			box.destroy();
		}
	}
}
