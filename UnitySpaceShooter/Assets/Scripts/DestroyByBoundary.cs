using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    /// <summary>
    /// When shots leave this boundary, they are destroyed.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
