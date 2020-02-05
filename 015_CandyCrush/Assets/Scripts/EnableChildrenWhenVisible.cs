using UnityEngine;
using System.Collections;

public class EnableChildrenWhenVisible : MonoBehaviour
{
    public GameObject[] children;
    public float visibleDistance = 50;
    
    bool visible;
    
    void Start()
    {
        if (transform.position.z > visibleDistance) {
            foreach (var child in children) {
                child.SetActiveRecursively(false);
            }
            visible = false;
        } else {
            visible = true;
        }
    }
    
    void Update()
    {
        if (!visible && transform.position.z <= visibleDistance) {
            foreach (var child in children) {
                child.SetActiveRecursively(true);
            }
        } else if (visible && transform.position.z > visibleDistance) {
            foreach (var child in children) {
                child.SetActiveRecursively(false);
            }
        }
    }
}
