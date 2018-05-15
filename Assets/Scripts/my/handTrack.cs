using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class handTrack : MonoBehaviour {

    public GameObject go;
    public LineRenderer lr;

    List<GameObject> gos = new List<GameObject>();

    Vector3[] pos = new Vector3[2];
    private void Update()
    {
        if (gos.Count > 0 && Time.timeScale ==1)
        {
            float min = 999;
            GameObject obj = null;
            foreach ( var c in gos)
            {
                float d = Vector3.Distance(transform.position, c.transform.position);
                if (d < min)
                {
                    min = d;
                    obj = c;

                }
            }
            go = obj;
            pos[0] = transform.position;
            pos[1] = go.transform.position;
            lr.SetPositions(pos);
            lr.enabled = true;
        }else
        {
            lr.enabled = false;
            go = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "zarad")
            gos.Add(other.gameObject);
    }

    public void Delete(GameObject go)
    {
        if (gos.Contains(go))
        {
            gos.Remove(go);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gos.Contains(other.gameObject))
        {
            gos.Remove(other.gameObject);
        }
    }
}
