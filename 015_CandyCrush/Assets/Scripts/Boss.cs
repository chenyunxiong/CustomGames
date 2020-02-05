using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Craft))]
public class Boss : MonoBehaviour
{
	Craft craft;
	
	float speed = 20;
	Vector3 velocity;
	
	void Awake()
	{
		this.SetLayer(Layers.Enemy);
		craft = GetComponent<Craft>();
	}
	
	void Start()
	{
		StartCoroutine(Cruising());
		StartCoroutine(Move());
	}
	
	IEnumerator Cruising()
	{
		Vector3 dir = Vector3.zero;
		while (true) {
			if (Time.frameCount % 100 == 0 && Random.value > 0.5f) {
				var rand = Random.insideUnitCircle;
				dir = transform.TransformDirection(new Vector3(rand.x, rand.y, 0));
			}
			craft.transform.Translate(dir * 20 * Time.deltaTime);
			yield return null;
		}
	}
	
	IEnumerator Move()
	{
		while (true) {
			yield return StartCoroutine(ComeClose());
			yield return StartCoroutine(CloseAttack());
			yield return StartCoroutine(FarAway());
			yield return StartCoroutine(FarAttack());
		}
	}
	
	IEnumerator ComeClose()
	{
		while (craft.transform.position.z > 200) {
			speed = Mathf.Lerp(speed, -20, 0.5f * Time.deltaTime);
			craft.transform.Translate(Vector3.forward * speed * Time.deltaTime);
			yield return null;
		}
	}
	
	IEnumerator CloseAttack()
	{
		var target = PlayerControl.instance.craft;
		var timer = 5f;
		
		while (timer > 0 && target && !target.destroyed) {
			foreach (var cannon in craft.weapons.Get<Cannon>()) {
				if (cannon.available) {
					cannon.Fire();
				}
			}
			
			timer -= Time.deltaTime;
			yield return null;
		}
	}
	
	IEnumerator FarAway()
	{
		while (craft.transform.position.z < 450) {
			speed = Mathf.Lerp(speed, 20, 0.5f * Time.deltaTime);
			craft.transform.Translate(Vector3.forward * speed * Time.deltaTime);
			yield return null;
		}
	}
	
	IEnumerator FarAttack()
	{
		var target = PlayerControl.instance.craft;
		var timer = 10f;
		
		while (timer > 0 && target && !target.destroyed) {
			foreach (var launcher in craft.weapons.Get<MissileLauncher>()) {
				while (!launcher.empty) {
					launcher.Fire(target.parts[0]);
				}
//				launcher.Reload();
			}
			
			timer -= Time.deltaTime;
			yield return null;
		}
	}

	void Update()
	{
		
	}
}
