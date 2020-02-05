using UnityEngine;
using System.Collections;

public enum DamageType { Once, OverTime }

public class Damager : MonoBehaviour
{
	public float damage = 20;
	public DamageType damageType = DamageType.Once;
}
