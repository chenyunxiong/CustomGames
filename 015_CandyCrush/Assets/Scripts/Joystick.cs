using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour
{
    public static Joystick instance { get; private set; }
 
    Rect joystickTouchFrame = new Rect(0, Screen.height - 400, 400, 400);
 
    public Vector2 joystickPosition {
        get { return joystick.joystickPosition; }
        set { joystick.joystickPosition = value; }
    }
    
    UIJoystick joystick;
    UIContinuousButton missileButton;
 
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        joystick = UIJoystick.create("button_control.png", joystickTouchFrame, 120, -280);
        joystick.position = new Vector3(joystick.position.x, joystick.position.y, 10);
        joystick.maxJoystickMovement = 60;
     
        int x, y;
     
        x = Screen.width - 180;
        y = Screen.height - 180;
        missileButton = UIContinuousButton.create("button_missile.png", "button_missile.png", x, y);
        missileButton.normalTouchOffsets = new UIEdgeOffsets(20, 20, 5, 20);
        missileButton.highlightedTouchOffsets = new UIEdgeOffsets(20, 20, 5, 20);
        missileButton.onTouchIsDown += MissileButtonIsDown;
    }
 
    void OnEnable()
    {
        Messenger.AddListener(MessageTypes.GameOver, Hide);
        Messenger.AddListener(MessageTypes.GamePause, Deactive);
        Messenger.AddListener(MessageTypes.GameResume, Active);
    }
 
    void OnDisable()
    {
        Messenger.RemoveListener(MessageTypes.GameOver, Hide);
        Messenger.RemoveListener(MessageTypes.GamePause, Deactive);
        Messenger.RemoveListener(MessageTypes.GameResume, Active);
    }
 
    void Deactive()
    {
        missileButton.disabled = true;
        joystick.normalTouchOffsets = new UIEdgeOffsets(-(int)joystickTouchFrame.width / 2);
    }
 
    void Active()
    {
        missileButton.disabled = false;
        joystick.normalTouchOffsets = new UIEdgeOffsets(0);
    }
 
    void Hide()
    {
        missileButton.hidden = true;
        joystick.hidden = true;
        Deactive();
    }
 
    void MissileButtonIsDown(UIButton sender)
    {
        Messenger.Broadcast(MessageTypes.MissileButtonIsDown);
    }
}
