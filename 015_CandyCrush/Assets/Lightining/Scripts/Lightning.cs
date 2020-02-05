// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.
using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour
{
    public int meshDetail = 5;
    public float random = 1;
    public float smoothness = 30;
    public float lightningThickness = 1.5f;
    public Color lightningColor = Color.cyan;
    public Transform endPoint;
    public bool rayCastEndPoint = false;
    public bool oneShot = false;
    public float noHitLightningDistance = 200;
    public float oneShotFadeSpeed = 0.1f;
    public float oneShotLightFadeSpeed = 0.2f;
    private int detail = 20;
    private bool startDestroy = false;
    private bool firstPosSet = false;

    void  Start()
    {
        endPoint.transform.parent = null;
        if (oneShot == true) {
            startDestroy = true;
        }
        
        StartCoroutine("Lightining");
    }
    
    void Update()
    {
        if (endPoint == null) {
            StopCoroutine("Lightining");
            Destroy(gameObject);
        } else {
            transform.forward = endPoint.position - transform.position;
        }
    }

    IEnumerator Lightining()
    {
        while (endPoint) {
 
            var dist = Vector3.Distance(transform.position, endPoint.position);
            detail = (int)dist * meshDetail / 40;
 
            LineRenderer render = GetComponent<LineRenderer>();
 
            render.SetWidth(lightningThickness, lightningThickness);
 
            float positionDistance = 0;
     
            if (detail > 0) {        
                render.SetVertexCount(detail + 1);
                if (firstPosSet == false) {
                    var texOffset = renderer.material.mainTextureOffset;
                    texOffset.x = Random.value;
                    renderer.material.mainTextureOffset = texOffset;
                }
            
                var texScale = renderer.material.mainTextureScale;
                texScale.x = dist / smoothness;
                renderer.material.mainTextureScale = texScale;
     
                positionDistance = dist / detail;
            }
            for (var i = 1; i < detail + 1; i ++) {
                var randomPos = new Vector3(Random.Range(-random, random), Random.Range(-random, random), i * positionDistance);
                if (i == detail) {
                    var pos = new Vector3(0, 0, dist);
                    render.SetPosition(i, pos);
                } else {
                    if (firstPosSet == false) {
                        render.SetPosition(i, randomPos);
                    }
                }
            }
     
            if (oneShot == true) {
                if (startDestroy == true) {
                    lightningColor.a -= oneShotFadeSpeed;
                }
                firstPosSet = true;
            }
        
            yield return new WaitForSeconds(0.05f);
        }
    }
}