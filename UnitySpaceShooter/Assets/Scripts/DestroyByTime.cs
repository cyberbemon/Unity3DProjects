using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    public float lifeTime;

    /// <summary>
    /// Destroy the game object after a given time.
    /// Attached to player explosion/asteroid explosion.
    /// </summary>
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
