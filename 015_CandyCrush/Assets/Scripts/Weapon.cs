using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon : MonoBehaviour
{
    public abstract bool available { get; }
}

public static class WeaponExtensions
{
	public static List<T> Get<T>(this List<Weapon> weapons) where T : Weapon
	{
		var results = new List<T>();
		foreach (var w in weapons) {
			if (w is T) {
				results.Add((T)w);
			}
		}
		return results;
	}
}
