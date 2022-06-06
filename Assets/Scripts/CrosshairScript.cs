using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairScript : MonoBehaviour {
    public Transform from;
    public Transform to;
    public float speed = 0.1F;
    public static float roty = 140;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        to.rotation = Quaternion.Euler(0, roty, 0);                                             //Analogdan gelen değerler doğrultusunda nişangahın yönünü ayarlar
        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, Time.time * speed);
    }
}
