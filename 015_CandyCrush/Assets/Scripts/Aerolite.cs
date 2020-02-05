using UnityEngine;
using System.Collections;

public class Aerolite : MonoBehaviour
{
	public float speed = 40;
	public int score = 5;
	
	Vector3 axis;
	Destructible destructible;
 
    public float endurance {
        get { return destructible.endurance; }
        set { destructible.endurance = value; }
    }
	
	void Awake()
	{
//		tag = Tags.Aerolite;
		gameObject.layer = Layers.Enemy;
		destructible = GetComponent<Destructible>();
	}
	
	void OnEnable()
	{
		destructible.OnDamage += OnDamage;
	}
	
	void OnDisable()
	{
		destructible.OnDamage -= OnDamage;
	}
	
	void OnDamage(Destructible body, float damage)
	{
		if (body.destroyed) {
			Messenger<Aerolite>.Broadcast(MessageTypes.AeroliteDestroyed, this);
			Instantiate(Resources.Load("explosion"), transform.position, Quaternion.identity);
		}
	}

	void Start()
	{
		axis = Random.onUnitSphere;
//		speed = Random.value * 40;
	}

	void Update()
	{
		if (LevelHandler.instance.paused) {
			return;
		}
		
		transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
		transform.RotateAround(axis, 3 * Time.deltaTime);
		
		if (transform.position.z < -50) {
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == Tags.Destructible) {
			col.GetComponent<Destructible>().ApplyDamage(Mathf.Infinity);
		}
	}
}
