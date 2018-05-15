using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioControl : MonoBehaviour {
    public GameObject sound;

    public void PlayClip()
    {
        GameObject go = Instantiate(sound, transform);
        Destroy(go, 4);
    }
}
