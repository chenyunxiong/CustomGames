using UnityEngine;
using System.Collections;

public static class MonoBehaviorExtensions
{
	public static void SetLayer(this MonoBehaviour behavior, int layer)
	{
		foreach (var trans in behavior.GetComponentsInChildren<Transform>(true)) {
			trans.gameObject.layer = layer;
		}
	}
}
