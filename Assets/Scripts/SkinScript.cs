using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinScript : MonoBehaviour {
    //Karakteri çevirmek için
    Transform from;
    public Transform to;
    public float speed = 0.1F;
    public static float roty = 140;

    private Animator anim;
    private Vector3 pos;
    private float animTimer = 0.02f;

    // Use this for initialization
    void Start () {
        from = transform;
        anim = transform.gameObject.GetComponent<Animator>();
        pos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        animTimer -= Time.deltaTime;
        if (animTimer < 0)
        {
            if (transform.position != pos)
            {
                anim.SetBool("forward", true);
                pos = transform.position;
            }
            else
            {
                anim.SetBool("forward", false);
            }
            animTimer = 0.02f;
        }

        to.rotation = Quaternion.Euler(0, roty, 0);                                             //Analogdan gelen değerler doğrultusunda karakterin yönünü değiştirir
        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, Time.time * speed);
    }

    public void Roty(float angle)
    {
        roty = angle;
    }

    public void BasicAttack()
    {
        anim.SetBool("b_attack", true);
        Invoke("BasicAttackBack", 0.5f);
        
    }

    public void BasicAttackBack()
    {
        anim.SetBool("b_attack", false);
    }
}
