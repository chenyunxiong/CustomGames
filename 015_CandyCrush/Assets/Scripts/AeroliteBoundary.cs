using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AeroliteBoundary : MonoBehaviour
{
	public float radius = 50;
	public float nearest = 0;
	public float farest = 300;
	public float thickness = 5;
	
	public Transform[] stuffs;
 
    float speed = 100;
	
	List<Transform> stuffInstances = new List<Transform>();

	void Start()
	{
		var distance = nearest;
		while (distance < farest) {
			for (int i = 0; i < stuffs.Length; i++) {
				var pos = Random.insideUnitCircle;
				if (pos.sqrMagnitude > 0.25) {
					pos = Vector2.Scale(pos, new Vector2(100, 100));
					var obj = (Transform)Instantiate(stuffs[i], new Vector3(pos.x, pos.y, distance), Random.rotation);
					obj.parent = transform;
					stuffInstances.Add(obj);
				}
			}
			
			distance += thickness;
		}
	}
 
    void OnEnable()
    {
        Messenger<float>.AddListener(MessageTypes.DifficultLevelUp, SpeedUp);
    }
    
    void OnDisable()
    {
        Messenger<float>.RemoveListener(MessageTypes.DifficultLevelUp, SpeedUp);
    }
    
    void SpeedUp(float speed)
    {
        this.speed = speed + 100;
    }

	void Update()
	{
		if (LevelHandler.instance.paused) {
			return;
		}
		
		foreach (var obj in stuffInstances) {
			obj.Translate(0, 0, -speed * Time.deltaTime, Space.World);
			
			if (obj.position.z < nearest) {
				var pos = obj.position;
				pos.z = farest;
				obj.position = pos;
			}
		}
	}
}
