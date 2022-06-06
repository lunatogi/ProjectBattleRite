using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillScript : MonoBehaviour {
    //BUTTONLAR
    public GameObject b_Jump;           //Skill 1 Butonu
    public GameObject b_AttackObject;   //Basic Atak Butonu
    public GameObject b_Skill2;
    public GameObject b_battack;

    public GameObject ASanalog;         //Area Shot Analog
    public GameObject SSanalog;         //Skill Shot Analog
    public GameObject canvas;
    private GameObject anlg;
    public GameObject ssRange;          //Karakterin etrafında çıkan range gösteren şey Skill Shot için
    public GameObject asRange;          //Karakterin etrafında çıkan range gösteren şey Area Shot için

    public GameObject animObject;

    public Transform attackTransform;
    public Transform curPosition;
    public Transform endPosition;

    float b_AttackTimer = 0.8f;
    float skill1_cd = 0.8f;

    public static bool timerStart = false;
    public static bool skill1_time = false;
    bool jump = true;

    public Text txt;
    public Text txt1;
    

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        txt.text = "ss: " + JoystickScript.i_touchNumber.ToString();
        txt1.text = "as: " + IIJoystickScript.i_touchNumber.ToString();


        if (timerStart)                         //Düz atağın cooldownı
        {
            b_AttackTimer -= Time.deltaTime;
            if (b_AttackTimer < 0)
                timerStart = false;
        }

        if (skill1_time)
        {
            skill1_cd -= Time.deltaTime;
            if(skill1_cd < 0)
                skill1_time = false;
        }



        if (jump)
        {
            //transform.gameObject.GetComponent<FirstPersonController>().enabled = false;
            //transform.position = Vector3.Slerp(curPosition.position, endPosition.position, Time.deltaTime*3);
        }
    }

    
    








    public void BasicAttack()
    {
        ssRange.SetActive(true);
        anlg = Instantiate(SSanalog, b_Jump.transform.position, b_Jump.transform.rotation);                           //Skillin atılması için analogu açar
        anlg.transform.parent = canvas.gameObject.transform;                                                        //Analogun görünmesi için canvasa ekler
        anlg.transform.position = b_battack.gameObject.transform.position;
        anlg.SendMessage("BackMessage", "BasicAttack", SendMessageOptions.DontRequireReceiver);
        anlg.SendMessage("TouchNumber", Input.touchCount-1, SendMessageOptions.DontRequireReceiver);                  //Sadece bir dokunuşla çalışması için hangi dokunuşa itaat edeceği kodunu gönderir
        b_battack.gameObject.SetActive(false);
        
    }

    public void BasicAttackBack()
    {
        if (!timerStart)                //Ateş ederken olan şeyler bunun içinde
        {
            animObject.SendMessage("BasicAttack", SendMessageOptions.DontRequireReceiver);
            Invoke("ArrowThrow", 0.5f);
        }
        ssRange.SetActive(false);
        Destroy(anlg.gameObject);
        b_battack.gameObject.SetActive(true);
    }







    public void Skill1()
    {
        ssRange.SetActive(true);
        anlg = Instantiate(SSanalog, b_Jump.transform.position, b_Jump.transform.rotation);                           //Skillin atılması için analogu açar
        anlg.transform.parent = canvas.gameObject.transform;                                                        //Analogun görünmesi için canvasa ekler
        anlg.SendMessage("BackMessage", "Skill1", SendMessageOptions.DontRequireReceiver);
        anlg.SendMessage("TouchNumber", Input.touchCount-1, SendMessageOptions.DontRequireReceiver);                  //Sadece bir dokunuşla çalışması için hangi dokunuşa itaat edeceği kodunu gönderir
        b_Jump.gameObject.SetActive(false);                                                                         //Butonu görünmez yapar
    }

    public void Skill1Back()                                                                                          //Jump skilli atılınca buton yeniden görünür olur ve analog yok olur
    {

        if (!skill1_time)                              //Ateş ederken olan şeyler bunun içinde
        {
            animObject.SendMessage("BasicAttack", SendMessageOptions.DontRequireReceiver);
            Invoke("Skill1Throw", 0.5f);
        }
        ssRange.SetActive(false);
        Destroy(anlg.gameObject);
        b_Jump.gameObject.SetActive(true);
    }




    public void SKill2()
    {
        asRange.SetActive(true);
        anlg = Instantiate(ASanalog, b_Skill2.transform.position, b_Skill2.transform.rotation);
        anlg.transform.parent = canvas.gameObject.transform;
        anlg.SendMessage("NisanTag", "nisangah", SendMessageOptions.DontRequireReceiver);
        anlg.SendMessage("BackMessage", "Skill2", SendMessageOptions.DontRequireReceiver);
        anlg.SendMessage("TouchNumber", Input.touchCount-1, SendMessageOptions.DontRequireReceiver);                 //Sadece bir dokunuşla çalışması için hangi dokunuşa itaat edeceği kodunu gönderir
        b_Skill2.gameObject.SetActive(false);
    }

    public void Skill2Back()
    {
        asRange.SetActive(false);
        Destroy(anlg.gameObject);
        b_Skill2.gameObject.SetActive(true);
    }
  

    public void ArrowThrow()
    {
        GameObject attack = Instantiate(b_AttackObject, attackTransform.position, attackTransform.rotation);    //Düz atağı oluştur
        attack.gameObject.GetComponent<Rigidbody>().AddForce(attackTransform.transform.forward * 300);                          //Ateşle
        b_AttackTimer = 1;                                                                                      //Bu ikisi de cooldownı harekete geçirir
        timerStart = true;
    }

    public void Skill1Throw()
    {
        
         GameObject attack = Instantiate(b_AttackObject, attackTransform.position, attackTransform.rotation);                                //Düz atağı oluştur
         attack.gameObject.GetComponent<Rigidbody>().AddForce((attackTransform.transform.forward + new Vector3(0,3,0)) * 300);                          //Ateşle
        
        b_AttackTimer = 1;                                                                                      //Bu ikisi de cooldownı harekete geçirir
        timerStart = true;

    }







}
