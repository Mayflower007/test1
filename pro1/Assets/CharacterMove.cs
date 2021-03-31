using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    // Start is called before the first frame update
    //�Ƿ���ƶ�
    public bool Moveable;
    //�ٶ�
    float speed;
    //�����С�ٶ�
    int maxspeed = 50;
    int minspeed = -20;
    //���ת���ٶ�
    int maxAngle = 20;
    //ת���ٶ�
    float speedOfAngle;
    void Start()
    {
        Moveable = true;
        speed = 0;
        speedOfAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //���n�򲻿ɿ����ƶ�
        if (Input.GetKey(KeyCode.N))
        {
            Moveable = false;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            Moveable = true;
        }
        //��������ƶ�
        if (Moveable)
        {
            //Debug.Log(Input.GetAxis("move_r"));
            if (Input.GetKey(KeyCode.W) || Input.GetAxis("move_f") >0)
            {
                if (speed < maxspeed)
                {
                    speed += Time.deltaTime*10;
                }
                if (speedOfAngle < maxAngle)
                {
                    speedOfAngle += Time.deltaTime * 10;
                }
            }
            else
            {
                if (speed > 0)
                {
                    speed -= Time.deltaTime*3;
                }
                if (speedOfAngle > 0)
                {
                    speedOfAngle += Time.deltaTime * 10;
                }
            }
            //Debug.Log(transform.forward);
           
            if (Input.GetKey(KeyCode.S) || Input.GetAxis("move_f") < 0)
            {
                if (speed > minspeed)
                {
                    speed -= Time.deltaTime * 10;
                }
                if (speedOfAngle < maxAngle)
                {
                    speedOfAngle += Time.deltaTime * 10;
                }
            }
            else
            {
                if (speed < 0)
                {
                    speed += Time.deltaTime * 3;
                }
                if (speedOfAngle > 0)
                {
                    speedOfAngle += Time.deltaTime * 10;
                }
            }
          //  Debug.Log(Input.GetAxis("hor"));
            if (Input.GetKey(KeyCode.D) || Input.GetKey("joystick button 5") )
            {
                transform.Rotate(transform.up * 5*speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A)||Input.GetKey("joystick button 4"))
            {
                transform.Rotate(transform.up * -5*speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Space)||Input.GetKey("joystick button 1"))
            {
                if (speed > 0)
                {
                    speed -= Time.deltaTime * 50;
                }
                if (speed < 0)
                {
                    speed = 0;
                }
                
            }
            
        }
        transform.position += transform.forward * speed * Time.deltaTime;

    }
 }
