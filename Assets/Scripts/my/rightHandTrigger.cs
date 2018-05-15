using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightHandTrigger : MonoBehaviour {

    public List<GameObject> gos = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "zarad")
            gos.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (gos.Contains(other.gameObject))
        {
            gos.Remove(other.gameObject);
        }
    }
}
