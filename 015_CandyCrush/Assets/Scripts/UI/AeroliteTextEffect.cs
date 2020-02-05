using UnityEngine;
using System.Collections;

public class AeroliteTextEffect : MonoBehaviour
{
	Transform[] children;
	Vector3[] axises;
	float[] speeds;

	void Start ()
	{
		children = GetComponentsInChildren<Transform>();
		axises = new Vector3[children.Length];
		speeds = new float[children.Length];
		for (int i = 0; i < children.Length; i++) {
			axises[i] = Random.onUnitSphere;
			speeds[i] = Random.value * 2;
		}
	}

	void Update ()
	{
		for (int i = 0; i < children.Length; i++) {
			if (children[i] == transform) {
				continue;
			}
			children[i].RotateAround(axises[i], speeds[i] * Time.deltaTime);
		}
	}
}
