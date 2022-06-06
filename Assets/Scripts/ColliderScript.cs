using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour {
    GameObject mutter;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            mutter.SendMessage("GotDown", SendMessageOptions.DontRequireReceiver);
        }
    }
}
