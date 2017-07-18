using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;

    /// <summary>
    /// When the Asteroid is hit, both the bolt and the asteroid is deleted.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        if(other.tag == "player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        }
        
        Destroy(other.gameObject); // Bolt
        Destroy(gameObject); // Asteroid
    }
}
