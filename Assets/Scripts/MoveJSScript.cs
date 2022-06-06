using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveJSScript : MonoBehaviour {         //Move Joy Stick Script
    public GameObject skin;
    public GameObject character;

    Vector3 startPos;
    Vector3 currentPos;

    float x;
    float y;
    float z;
    float angle;
    float movStopTimer = 1;

    int i_touchNumber;
    int touchCheck;

    bool movement = true;

    public Text txt;
    public Text txt1;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        txt1.text = movStopTimer.ToString();
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

        if (!movement)                         //Ateş etmek için yürüme durduktan bir süre sonra tekrar aktif etmek için kullanılan if komutu
        {
            txt.text = "false";
            movStopTimer -= Time.deltaTime;
            if(movStopTimer < 0)
            {
                character.SendMessage("Move", SendMessageOptions.DontRequireReceiver);   //Bu kodun kendisi karakterin dönmesini kontrol ediyor ve buradaki mesaj da karakterin tekrar hareket etmesini sağlıyor
                movement = true;
                movStopTimer = 1;
            }
        }

        currentPos = Input.GetTouch(i_touchNumber).position;

        if (movement)
        {
            txt.text = "true";
            if (transform.position != startPos)
            {
                x = currentPos.x - startPos.x;                      //Analogun dönüşüyle karakter de dönsün diye önce x ve y değerlerini alıp tanjantı hesaplar        
                y = currentPos.y - startPos.y;                      //
                z = y / x;                                          //
                                                                    //
                if (x < 0 && y > 0 || x < 0 && y < 0)               //
                {                                                   //
                    angle = Mathf.Atan(z);                          //Sonra arctanjant ile o tanjantı hangi açının yapacağını hesaplar
                    SkinScript.roty = -angle * 55 + 180;            ////O açı karakterin rotasyonunu ayarlar ve 360 derece düzgün dönebilmesi için buradaki birkaç küçük işlem gerekir
                }
                else
                {
                    angle = Mathf.Atan(z);
                    SkinScript.roty = -angle * 55;
                }

            }
        }
    }

    public void MovementTouchControl()
    {
        i_touchNumber = Input.touchCount - 1;
        touchCheck = Input.touchCount - 1;
    }

    public void MovementStop()
    {
        movement = false;
    }
}
