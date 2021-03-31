using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ThirdView;
    public GameObject FirstView;
    public GameObject realCar;
    public GameObject CameraFather;
    public float mouseSensitivity;
    float mouseX, mouseY;
    float yAngle;

    PlayerControls controls;
    //��ȡ�����豸��x��yλ�Ʋ�ֵ��[-1,1]
    Vector2 cameraRotate;
    //��һ��Ϊ1�����˳�Ϊ3
    int viewType;
    void Start()
    {
        controls = new PlayerControls();
        controls.Enable();
        viewType = 3;
        controls.player.switchView.performed +=info=> viewSwitch();
    }

    // Update is called once per frame
    void Update()
    {
        //��ȡ�����豸��xyλ�Ʋ�[-1,1]
        cameraRotate = controls.player.camera.ReadValue<Vector2>();
        mouseX = cameraRotate.x * mouseSensitivity * Time.deltaTime;
        mouseY = cameraRotate.y * mouseSensitivity * Time.deltaTime;
        switch (viewType) {
            case 1:First(); break;
            case 3:Third(); break;
        }
    }
    void viewSwitch()
    {
        if (viewType == 1)
        {
            viewType = 3;
        }
        else
        {
            viewType = 1;
            //transform.eulerAngles = Vector3.zero;
        }
    }
    void First()
    {
        //��һ�˳Ƶ���fovΪ����
        Camera mainCamera = GetComponent<Camera>();
        mainCamera.fieldOfView = 72;
       
        //�ƶ����������һ�˳�λ��
        CameraFather.transform.position=FirstView.transform.position;
        //�ƶ����ת���ӽ�
        CameraFather.transform.Rotate(Vector3.up*mouseX);
        Debug.Log(mouseX);
        yAngle += mouseY;
        transform.localRotation = Quaternion.Euler(yAngle, 0, 0);
      
    }
    void Third()
    {
        //�����˳�Ӧ��fov�ϴ�
        Camera mainCamera = GetComponent<Camera>();
        mainCamera.fieldOfView = 90;
        //�ƶ��������˳�����λ��
        CameraFather.transform.position =ThirdView.transform.position;
        //��������
        transform.LookAt(realCar.transform);
    }
}
