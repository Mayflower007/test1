using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class move3 : MonoBehaviour
{
    // Start is called before the first frame update changetest
    public WheelCollider TL;
    public WheelCollider TR;
    public WheelCollider BL;
    public WheelCollider BR;

    public Transform TTL;
    public Transform TTR;
    public Transform TBL;
    public Transform TBR;
    //是否可移动
    public bool Moveable;
    //速度
    float speed;
    //最大最小速度
    int maxspeed = 50;
    int minspeed = -20;
    //最大转弯角度
    int maxAngle = 90;
    //转弯速度
    float speedOfAngle;
    PlayerControls controls;
    void Start()
    {
        controls = new PlayerControls();

        controls.player.Enable();

        Moveable = true;
        speed = 0;
        speedOfAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log( controls.player.horizontal.ReadValue<float>());
        if (controls.player.disable.ReadValue<float>()!=0)
        {
            Moveable = false;
        }
        if (controls.player.enable.ReadValue<float>() != 0)
        {
            Moveable = true;
        }
        //如果可以移动
        if (Moveable)
        {
            //Debug.Log(Input.GetAxis("move_r"));
            if (controls.player.forward.ReadValue<float>()!=0)
            {
                if (speed < maxspeed)
                {
                    speed += Time.deltaTime * (1+controls.player.move.ReadValue<float>());
                }
            }
            else
            {
                if (speed > 0)
                {
                    speed -= Time.deltaTime * 3;
                }
            }
            //Debug.Log(transform.forward);

            if (controls.player.back_off.ReadValue<float>() != 0)
            {

                if (speed > minspeed)
                {
                    speed -= Time.deltaTime * (1- controls.player.move.ReadValue<float>());
                }
            }
            else
            {
                if (speed < 0)
                {
                    speed += Time.deltaTime * 3;
                }

            }
            //  Debug.Log(Input.GetAxis("hor"));
            if (controls.player.turnright.ReadValue<float>() != 0)
            {
                //transform.Rotate(transform.up * 5 * speed * Time.deltaTime);
                if (speedOfAngle < maxAngle)
                {
                    speedOfAngle = 60*controls.player.horizontal.ReadValue<float>();
                }
            }
            else
            {
                if (speedOfAngle > 0)
                {
                    speedOfAngle -= Time.deltaTime * 30;
                }
            }
            if (controls.player.turnleft.ReadValue<float>() != 0)
            {
                //transform.Rotate(transform.up * -5 * speed * Time.deltaTime);
                if (speedOfAngle > -maxAngle)
                {
                    speedOfAngle = 60 * controls.player.horizontal.ReadValue<float>();
                }
            }
            else
            {
                if (speedOfAngle < 0)
                {
                    speedOfAngle += Time.deltaTime * 30;
                }
            }
            if (controls.player.@break.ReadValue<float>() != 0)
            {
                BR.motorTorque = 0;
                BL.motorTorque = 0;
                TR.motorTorque = 0;
                TL.motorTorque = 0;

                TR.brakeTorque += 10000 * Time.deltaTime;
                BL.brakeTorque += 10000 * Time.deltaTime;
                BR.brakeTorque += 10000 * Time.deltaTime;
                TL.brakeTorque += 10000 * Time.deltaTime;

                if (speed > 0)
                {
                    speed -= Time.deltaTime * 50;
                }
                if (speed < 0.1)
                {
                    speed = 0;
                }

            }
            else
            {
                TR.brakeTorque = 0;
                BL.brakeTorque = 0;
                BR.brakeTorque = 0;
                TL.brakeTorque = 0;
            }

        }
        BR.motorTorque = speed * 1000;
        BL.motorTorque = BR.motorTorque;
        TR.motorTorque = speed * 1000;
        TL.motorTorque = TR.motorTorque;

        TR.steerAngle = speedOfAngle;
        TL.steerAngle = TR.steerAngle;

        updateWheel(TBL, BL);
        updateWheel(TTL, TL);
        updateWheel(TTR, TR);
        updateWheel(TBR, BR);

    }

    void updateWheel(Transform wheel, WheelCollider col)
    {
        Vector3 pos = wheel.position;
        Quaternion rot = wheel.rotation;

        col.GetWorldPose(out pos, out rot);

        wheel.position = pos;
        wheel.rotation = rot;
    }
}
