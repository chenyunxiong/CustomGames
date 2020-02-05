using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    Rect boundary = Rect.MinMaxRect(-20, 12, 12, -40);
    float speed = 0.12f;
 
    void Start()
    {
        var pos = transform.position;
        pos.x = boundary.xMin;
        pos.y = boundary.yMin;
        transform.position = pos;
     
        StartCoroutine(Move());
    }
 
    IEnumerator Move()
    {
        while (true) {
            while (transform.position.x < boundary.xMax) {
                transform.Translate(speed * Time.deltaTime, 0, 0);
                yield return null;
            }
            while (transform.position.y > boundary.yMax) {
                transform.Translate(0, -speed * Time.deltaTime, 0);
                yield return null;
            }
            while (transform.position.x > boundary.xMin) {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
                yield return null;
            }
            while (transform.position.y < boundary.yMin) {
                transform.Translate(0, speed * Time.deltaTime, 0);
                yield return null;
            }
            yield return null;
        }
    }
}
