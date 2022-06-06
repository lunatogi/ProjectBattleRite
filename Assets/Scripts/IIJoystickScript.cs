using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IIJoystickScript : MonoBehaviour           //AS Analog (Area Shot)
{
    private Vector3 startPos;
    private Vector3 currentPos;
    private Vector3 newPos;

    private Vector3 anPos;
    private Vector3 nisPos;

    public GameObject analog;
    public GameObject m_character;
    /*
    public GameObject skillWay;
    public GameObject i_Button;        //İlgili buton xd
    public GameObject canvas;
    public GameObject character;
    */
    public GameObject nisangah;
    private GameObject moveAnalog;
    
    public float distance;
    float x;
    float z;

    private string nisanTag = "nisangah";
    private string backMsg;

    public int radius;
    public static int i_touchNumber;
    int touchCheck;
    
    // Use this for initialization
    void Start()
    {

        moveAnalog = GameObject.FindGameObjectWithTag("movanalog");
        m_character = GameObject.FindGameObjectWithTag("character");
        nisangah = GameObject.FindGameObjectWithTag(nisanTag);
        nisangah.transform.position = m_character.transform.position;
        nisPos = nisangah.transform.position;
        startPos = transform.position;
        /*
        canvas = GameObject.FindGameObjectWithTag("canvas");
        i_Button = GameObject.FindGameObjectWithTag("jump");
        character = GameObject.FindGameObjectWithTag("character");
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (touchCheck == 2)                    //Dokunuş sayısının azalıp artması itaat edilen dokunuşu değiştirdiğinden burada onun ayarı yapılıyor
        {
            if (Input.touchCount == 1)
            {
                i_touchNumber = 0;
            }
            else
            {
                i_touchNumber = 1;
            }
        }

        anPos = transform.position;
        currentPos = Input.GetTouch(i_touchNumber).position;
        distance = Vector3.Distance(startPos, currentPos);


        if (distance < radius)                                                            //Analoğun küresinde olduğumuzda mouse ile aynı yerde olsun diye distance radius ilişkisi var
        {
            analog.transform.position = currentPos;
        }
        else
        {                                                                               //Kürenin dışında da mousei takip eder
            int delta = (int)(Input.GetTouch(i_touchNumber).position.x - startPos.x);
            newPos.x = delta;

            int delta1 = (int)(Input.GetTouch(i_touchNumber).position.y - startPos.y);
            newPos.y = delta1;

            analog.transform.position = Vector3.ClampMagnitude(new Vector3(newPos.x, newPos.y, newPos.z), radius) + startPos;
        }
        

        if (Input.GetTouch(i_touchNumber).phase == TouchPhase.Ended)                    //Analog ile işimiz bittiğinde yapılması gerekenleri yapması için mesaj yollar
        {
            m_character.SendMessage(backMsg, SendMessageOptions.DontRequireReceiver);
        }

        /*
        if (Input.GetTouch(i_touchNumber).phase == TouchPhase.Ended)                    //Analog ile işimiz bittiğinde yapılması gerekenleri yapması için mesaj yollar
        {
            m_character.SendMessage(backMsg, SendMessageOptions.DontRequireReceiver);
        }*/

        x = anPos.x - startPos.x;
        if (x > 90)
            x = 80;
        else if (x < -90)
            x = -80;

        z = anPos.y - startPos.y;
        if (z > 90)
            z = 80;
        else if (z < -90)
            z = -80;
        denemeScript.pos = new Vector3(nisangah.transform.position.x - (z/2), nisangah.transform.position.y, nisangah.transform.position.z + (x/2));  //Alan range'inin hareket etmesi için gereken değerleri gönderir
        Debug.Log("x: " + x);
        Debug.Log("z: " + z);


    }

    public void NisanTag(string tag)
    {
        nisanTag = tag;
    }

    public void BackMessage(string msg)
    {
        backMsg = msg + "Back";
    }

    public void TouchNumber(int i)      //Hangi dokunuşa itaat etmesi gerektiği kodunu girer
    {
        i_touchNumber = i;
        touchCheck = i + 1;
    }
}
