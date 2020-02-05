using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Craft))]
public class PlayerControl : MonoBehaviour
{
	public static PlayerControl instance { get; private set; }
    
	public Craft craft { get; private set; }

	void Awake()
	{
		instance = this;
        
		this.SetLayer(Layers.Player);
		craft = GetComponent<Craft>();
	}
	
	void Start()
	{
		craft.RotateTo(Crosshair.instance.transform);

		StartCoroutine(FireCannon());
		StartCoroutine(ProcessInput());
	}
 
	void OnEnable()
	{
		Messenger.AddListener(MessageTypes.MissileButtonIsDown, LaunchMissile);

		Messenger.AddListener(MessageTypes.CannonPowerUp, PowerUp);
		Messenger.AddListener(MessageTypes.CannonPowerDown, PowerDown);
		Messenger.AddListener(MessageTypes.OpenEnergyShield, OpenEnergyShield);
		Messenger.AddListener(MessageTypes.CloseEnergyShield, CloseEnergyShield);
		Messenger.AddListener(MessageTypes.StartInfiniteMissile, StartInfiniteMissiles);
		Messenger.AddListener(MessageTypes.StopInfiniteMissile, StopInfiniteMissiles);
	}

	void OnDisable()
	{
		Messenger.RemoveListener(MessageTypes.MissileButtonIsDown, LaunchMissile);

		Messenger.AddListener(MessageTypes.CannonPowerUp, PowerUp);
		Messenger.AddListener(MessageTypes.CannonPowerDown, PowerDown);
		Messenger.RemoveListener(MessageTypes.OpenEnergyShield, OpenEnergyShield);
		Messenger.RemoveListener(MessageTypes.CloseEnergyShield, CloseEnergyShield);
		Messenger.RemoveListener(MessageTypes.StartInfiniteMissile, StartInfiniteMissiles);
		Messenger.RemoveListener(MessageTypes.StopInfiniteMissile, StopInfiniteMissiles);
	}
	
	void LateUpdate()
	{
		if (LevelHandler.instance.paused) {
			return;
		}
		
		var targetPos = Crosshair.instance.transform.position;
		targetPos.z = craft.transform.position.z;
		craft.transform.position = Vector3.Lerp(craft.transform.position, targetPos, Time.deltaTime * 2);
	}

	public void Move(Vector3 movement)
	{
		craft.transform.Translate(movement, Space.World);
	}

	IEnumerator ProcessInput()
	{
		yield return new WaitForSeconds(0.5f);

		while (true) {
			while (LevelHandler.instance.paused) {
				yield return null;
			}

			var speed = 320;
	
			if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor) {
				var x = Input.GetAxis("Horizontal");
				var y = Input.GetAxis("Vertical");
				Joystick.instance.joystickPosition = new Vector2(x, y);

				if (Input.GetKeyDown(KeyCode.Space)) {
					craft.parts[0].ApplyDamage(1000);
				}
			}

			var movement = Joystick.instance.joystickPosition * speed * Time.deltaTime;

			if (movement != Vector2.zero) {
				Crosshair.instance.Move(movement.x, movement.y);
			}

			yield return null;
		}
	}

	IEnumerator FireCannon()
	{
		yield return new WaitForSeconds(0.5f);

		while (true) {
			while (LevelHandler.instance.paused) {
				yield return null;
			}

			foreach (var cannon in craft.cannons) {
				cannon.Fire();
			}
			yield return null;
		}
	}

	void LaunchMissile()
	{
		if (Crosshair.instance.target != null) {
			craft.LaunchMissile(Crosshair.instance.target);
		}
	}

	void PowerUp()
	{
		var bullet = (GameObject)Resources.Load("fake_bulletx2");
		foreach (var cannon in craft.cannons) {
			cannon.SetBullet(bullet);
		}
	}

	void PowerDown()
	{
		var bullet = (GameObject)Resources.Load("fake_bullet");
		foreach (var cannon in craft.cannons) {
			cannon.SetBullet(bullet);
		}
	}

	void OpenEnergyShield()
	{
		craft.OpenShield();
	}

	void CloseEnergyShield()
	{
		craft.CloseShield();
	}

	void StartInfiniteMissiles()
	{
		craft.missileLauncher.reloadSpeed = 0;
	}

	void StopInfiniteMissiles()
	{
		craft.missileLauncher.ResetReloadSpeed();
	}
}
