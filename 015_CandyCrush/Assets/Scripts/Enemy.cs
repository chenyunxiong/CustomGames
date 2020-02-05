using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Craft))]
public class Enemy : MonoBehaviour
{
    public Craft craft { get; private set; }
    public int score = 5;
    
    public Destructible destructible {
        get {
            if (_destructible == null) {
                _destructible = GetComponentInChildren<Destructible>();
            }
            return _destructible;
        }
    }
    Destructible _destructible;

    void Awake()
    {
        this.SetLayer(Layers.Enemy);
        craft = GetComponent<Craft>();
    }

    void Start()
    {
        StartCoroutine(Move());
        StartCoroutine(Fire());
    }

    IEnumerator Move()
    {
        while (true) {
            while (LevelHandler.instance.paused) {
                yield return null;
            }

            transform.Translate(Vector3.forward * craft.speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Fire()
    {
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
}
