using UnityEngine;
using System.Collections;

public class PortalOrb : MonoBehaviour
{
    public PortalOrb nextOrb;
    
    Lightning lightning;

    void Start()
    {
        if (nextOrb != null) {
            var obj = (GameObject)Instantiate(Resources.Load("SimpleLightning"), transform.position, Quaternion.identity);
            lightning = obj.GetComponent<Lightning>();
            lightning.transform.parent = transform;
            lightning.endPoint = nextOrb.transform;
        }
    }
}
