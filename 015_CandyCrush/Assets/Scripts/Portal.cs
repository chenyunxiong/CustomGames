using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public int orbNumber = 3;
    public float maxRadius = 60;
    public float rotSpeed = 10;
    
    public Transform blackhole;
    
    PortalOrb[] orbs;
    float[] angles;
    float radius = 5;
    
//    bool destroyed { get; set; }

    void Start()
    {
        var res = Resources.Load("PortalOrb");
        
        angles = new float[orbNumber];
        
        orbs = new PortalOrb[orbNumber];
        for (int i = 0; i < orbNumber; i++) {
            angles[i] = 360f / orbNumber * (i + 1);
            var obj = (GameObject)Instantiate(res, getCirclePosition(angles[i]), Quaternion.identity);
            orbs[i] = obj.GetComponent<PortalOrb>();
            if (i > 0) {
                orbs[i].nextOrb = orbs[i - 1];
            }
        }
        
        if (orbs.Length > 0) {
            orbs[0].nextOrb = orbs[orbNumber - 1];
        }
        
        StartCoroutine(Open());
    }
    
    IEnumerator Open()
    {
        var tmp = rotSpeed;
        rotSpeed *= 10;
        
        float scale = 1;
        
        while (radius < maxRadius) {
            scale += Time.deltaTime * 2;
            blackhole.localScale = Vector3.one * scale;
            
            radius += Time.deltaTime * 20;
            
            yield return null;
        }
        
        rotSpeed = tmp;
        
        while (scale < 25) {
            scale += Time.deltaTime * 5;
            blackhole.localScale = Vector3.one * scale;
            
            yield return null;
        }
        
        Messenger.Broadcast(MessageTypes.PortalOpened);
    }
    
    IEnumerator Close()
    {
        float scale = 25;
        
        Debug.Log("scale: " + scale);
        
        while (scale > 1) {
            scale -= Time.deltaTime * 8;
            blackhole.localScale = Vector3.one * scale;
            
            yield return null;
        }
        
        Destroy(gameObject);
    }
    
    Vector3 getCirclePosition(float angle)
    {
        var pos = new Vector3();
        pos.x = transform.position.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = transform.position.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = transform.position.z - 20;
        
        return pos;
    }
 
//    void Update()
//    {
//        if (!destroyed) {
//            destroyed = true;
//            for (int i = 0; i < orbNumber; i++) {
//                if (orbs[i] != null) {
//                    angles[i] += rotSpeed * Time.deltaTime;
//                    orbs[i].transform.position = getCirclePosition(angles[i]);
//                    
//                    destroyed = false;
//                }
//            }
//            
//            if (destroyed) {
//                Messenger.Broadcast(MessageTypes.PortalDestroyed);
//                StartCoroutine(Close());
//                Debug.Log("Portal destroyed.");
//            }
//        }
//    }
}
