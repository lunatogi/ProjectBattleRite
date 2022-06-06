using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class denemeScript : MonoBehaviour {
    private Vector3 kubsPos;
    private Vector3 newPos;
    private Vector3 curPos;

    private GameObject character;

    public static Vector3 pos;

    float distance;

    public float radius = 20;

	// Use this for initialization
	void Start () {
        character = GameObject.FindGameObjectWithTag("character");
	}
	
	// Update is called once per frame
	void Update () {

        kubsPos = character.transform.position;
        curPos = transform.position;

        distance = Vector3.Distance(kubsPos, curPos);

        if (distance < radius)                                                            //Alan göstergecimiz rangesinin dışına çıkmasın diye radiusla kısıtlıyor
        {
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime/2);
        }
        else
        {                                                                               //Kürenin dışında da mousei takip eder
            int delta = (int)(pos.x - kubsPos.x);
            newPos.x = delta/2;

            int delta2 = (int)(pos.z - kubsPos.z);
            newPos.z = delta2/2;

            transform.position = Vector3.Lerp(transform.position, Vector3.ClampMagnitude(new Vector3(newPos.x, newPos.y, newPos.z), radius) + kubsPos, Time.deltaTime*3);
        }
    }

}
