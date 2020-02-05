using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class EnergyShield : MonoBehaviour
{
    AnimationState absorbAnim;
    
	void Awake()
	{
		tag = Tags.Player;
        absorbAnim = animation["shield_02"];
        absorbAnim.layer = 1;
	}
 
    void OnEnable()
    {
        animation.Rewind("shield_02");
        animation.Play("shield_02");
    }
	
	void OnTriggerEnter(Collider col)
	{
		var destructible = col.GetComponent<Destructible>();
		if (destructible) {
			PlayAbsorbAnimation();
            destructible.ApplyDamage(Mathf.Infinity);
		}
	}
	
	void PlayAbsorbAnimation()
	{
        animation.Rewind("shield_02");
        animation.Play("shield_02");
	}
}
