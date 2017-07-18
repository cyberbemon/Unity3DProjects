using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
   
    public float speed;
    public float tilt;
    private float nextFire;
    public float fireRate;

    public GameObject shot;
    public Transform shotSpawn;
    private Rigidbody rb;
    public Boundary boundary;
    private AudioSource audioSource;


    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            audioSource = GetComponent<AudioSource>();
           nextFire = Time.time + fireRate;
           Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }
            
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb = GetComponent<Rigidbody>();
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.velocity = movement * speed;

        // Clamp our movement, so the player doesn't go out of play area.
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        // Tilt the aircraft when we move it. 
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt );
    }
}
