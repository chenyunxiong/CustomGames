using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    UISprite sprite;
    float distance = 80;

    public static Crosshair instance { get; private set; }

	public Vector3 screenPosition {
		get {
			return new Vector3(sprite.position.x, Screen.height + sprite.position.y, distance + 50);
		}
	}
 
    public Destructible target { get; private set; }

    void Awake()
    {
        instance = this;
    }
 
    void Start()
    {
        int x = (int)UIRelative.xPercentFrom(UIxAnchor.Left, Screen.width, 0.5f);
        int y = (int)UIRelative.yPercentFrom(UIyAnchor.Top, Screen.height, 0.5f);
        sprite = UIToolkit.instance.addSprite("locking_blue.png", x, y, 40, true);
     
        Move(0, 0);
    }
 
    void OnEnable()
    {
        Messenger.AddListener(MessageTypes.GameOver, Hide);
    }
 
    void OnDisable()
    {
        Messenger.RemoveListener(MessageTypes.GameOver, Hide);
    }
 
    void Hide()
    {
        enabled = false;
        sprite.hidden = true;
    }
 
    void FixedUpdate()
    {
        RaycastHit hit;
        int mask = 1 << Layers.Enemy;
        if (Physics.SphereCast(GetRay(), 1, out hit, LevelHandler.instance.farest, mask) && hit.collider.tag == Tags.Destructible) {
            var hitTarget = hit.collider.GetComponent<Destructible>();
            if (target != hitTarget) {
                target = hitTarget;
                Messenger<Destructible>.Broadcast(MessageTypes.LockedTarget, target);
             
                sprite.setSpriteImage("locking_red.png");
            }
        } else {
            target = null;
            Messenger<Destructible>.Broadcast(MessageTypes.LockedTarget, null);
         
            sprite.setSpriteImage("locking_blue.png");
        }
    }
 
    public void Move(float x, float y)
    {
        var pos = sprite.position;
        pos.x += x;
        pos.y += y;
     
        int halfWidth = Screen.width / 2;
        int halfHeight = Screen.height / 2;
        pos.x = Mathf.Clamp(pos.x, halfWidth - 360 / 2, halfWidth + 360 / 2);
        pos.y = Mathf.Clamp(pos.y, -halfHeight - 220 / 2, -halfHeight + 220 / 2);
     
        sprite.position = pos;
     
        transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
    }
 
    public Ray GetRay()
    {
        var ray = Camera.main.ScreenPointToRay(screenPosition);
        return ray;
    }
}
