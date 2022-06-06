using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickScript : MonoBehaviour {           //SS Analog (Skill Shot)
    private Vector3 startPos;
    private Vector3 currentPos;
    private Vector3 newPos;

    public GameObject analog;
    public GameObject skillWay;
    public GameObject i_Button;        //İlgili buton 
    public GameObject canvas;
    public GameObject character;
    private GameObject moveAnalog;

    public float distance;

    float x;
    float y;
    float z;
    float angle;

    private string nisanTag = "nisangah";
    private string backMsg;

    public int radius;
    public static int i_touchNumber;
    int touchCheck;
    
	// Use this for initialization
	void Start () {
        startPos = analog.transform.position;
        moveAnalog = GameObject.FindGameObjectWithTag("movanalog");
        canvas = GameObject.FindGameObjectWithTag("canvas");
        i_Button = GameObject.FindGameObjectWithTag("jump");
        character = GameObject.FindGameObjectWithTag("character");
	}
	
	// Update is called once per frame
	void Update () {
#if MOBILE_INPUT
        if(touchCheck == 2)                         //Dokunuş sayısının azalıp artması itaat edilen dokunuşu değiştirdiğinden burada onun ayarı yapılıyor
        {
            if(Input.touchCount == 1)
            {
                i_touchNumber = 0;
            }
            else
            {
                i_touchNumber = 1;
            }
        }



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
        


        x = currentPos.x - startPos.x;                                                      //Analogun dönüşüyle nişangah da dönsün diye önce x ve y değerlerini alıp tanjantı hesaplar
        y = currentPos.y - startPos.y;                                                      //
        z = y / x;                                                                          //
                                                                                            //
        if(x < 0 && y > 0 || x < 0 && y < 0)                                                //
        {                                                                                   //
            angle = Mathf.Atan(z);                                                          //Sonra arctanjant ile o tanjantı hangi açının yapacağını hesaplar
            CrosshairScript.roty = -angle * 55 + 180;                                       //O açı nişangahın rotasyonunu ayarlar ve 360 derece düzgün dönebilmesi için buradaki birkaç küçük işlem gerekir
        }
        else
        {
            angle = Mathf.Atan(z);
            CrosshairScript.roty = -angle * 55;
        }

        if (Input.GetTouch(i_touchNumber).phase == TouchPhase.Ended)                        //Analog ile işimiz bittiğinde yapılması gerekenleri yapması için mesaj yollar
        {
            moveAnalog.SendMessage("MovementStop", SendMessageOptions.DontRequireReceiver); //Ateş edildiğinde sağa sola dönme dursun diye bu mesajı gönderir
            character.SendMessage("DontMove", SendMessageOptions.DontRequireReceiver);      //Ateş edildiğinde hareket etme dursun diye bu mesajı gönderir
            character.SendMessage(backMsg, SendMessageOptions.DontRequireReceiver);
            if (!SkillScript.timerStart)                                                    //Burası saldırırken karakteri saldırdığı tarafa döndürüyor
            {                        
                if (x < 0 && y > 0 || x < 0 && y < 0)
                {
                    angle = Mathf.Atan(z);
                    SkinScript.roty = -angle * 55 + 180;
                }
                else
                {
                    angle = Mathf.Atan(z);
                    SkinScript.roty = -angle * 55;
                }
            }
        }
#endif
#if !MOBILE_INPUT

        currentPos = Input.mousePosition;
        distance = Vector3.Distance(startPos, currentPos);


        if (distance < radius)                                                            //Analoğun küresinde olduğumuzda mouse ile aynı yerde olsun diye distance radius ilişkisi var
        {
            analog.transform.position = currentPos;
        }
        else
        {                                                                               //Kürenin dışında da mousei takip eder
            int delta = (int)(Input.mousePosition.x - startPos.x);
            newPos.x = delta;

            int delta1 = (int)(Input.mousePosition.y - startPos.y);
            newPos.y = delta1;

            analog.transform.position = Vector3.ClampMagnitude(new Vector3(newPos.x, newPos.y, newPos.z), radius) + startPos;
        }



        x = currentPos.x - startPos.x;                                                      //Analogun dönüşüyle nişangah da dönsün diye önce x ve y değerlerini alıp tanjantı hesaplar
        y = currentPos.y - startPos.y;                                                      //
        z = y / x;                                                                          //
                                                                                            //
        if (x < 0 && y > 0 || x < 0 && y < 0)                                               //
        {                                                                                   //
            angle = Mathf.Atan(z);                                                          //Sonra arctanjant ile o tanjantı hangi açının yapacağını hesaplar
            CrosshairScript.roty = -angle * 55 + 180;                                       //O açı nişangahın rotasyonunu ayarlar ve 360 derece düzgün dönebilmesi için buradaki birkaç küçük işlem gerekir
        }
        else
        {
            angle = Mathf.Atan(z);
            CrosshairScript.roty = -angle * 55;
        }

        if (Input.GetButtonUp("Fire1"))                        //Analog ile işimiz bittiğinde yapılması gerekenleri yapması için mesaj yollar
        {
            //moveAnalog.SendMessage("MovementStop", SendMessageOptions.DontRequireReceiver); //Ateş edildiğinde sağa sola dönme dursun diye bu mesajı gönderir
            character.SendMessage("DontMove", SendMessageOptions.DontRequireReceiver);      //Ateş edildiğinde hareket etme dursun diye bu mesajı gönderir
            character.SendMessage(backMsg, SendMessageOptions.DontRequireReceiver);
            if (!SkillScript.timerStart)                                                    //Burası saldırırken karakteri saldırdığı tarafa döndürüyor
            {
                if (x < 0 && y > 0 || x < 0 && y < 0)
                {
                    angle = Mathf.Atan(z);
                    SkinScript.roty = -angle * 55 + 180;
                }
                else
                {
                    angle = Mathf.Atan(z);
                    SkinScript.roty = -angle * 55;
                }
            }
        }
#endif
    }

    public void BackMessage(string msg)
    {
        backMsg = msg + "Back";
    }

    public void TouchNumber(int i)      //Hangi dokunuşa itaat etmesi gerektiği kodunu girer
    {
        i_touchNumber = i;
        touchCheck = i+1;
    }
}
