using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Camera cam;
    [Header("��ũ��Ʈ")]
    public GameObject player; // �ٶ� �÷��̾� ������Ʈ�Դϴ�.

    [Header("�⺻��ġ")]
    [Range(0, 1)]
    public float smoothTime = 0.2f; // ī�޶� �ε巴�� ������ �ð�
    [Range(1, 8)]
    public float sensitivity = 2f; // ī�޶� �ΰ���
    private float wheelspeed = 250.0f; // �� �ӵ�
    Vector3 cameraNorVec; // ī�޶� �⺻ ��ġ
    protected Vector3 velocity = Vector3.zero; // ��ǥ�� ����

    public float vel = 0;

    [SerializeField] protected int maxZoom;
    [SerializeField] protected int minZoom;

    [Header("�������")]
    public float xmove = 0;// X�� ���� �̵���
    public float ymove = 0;// Y�� ���� �̵���
    public float distance = 110; // ���� ��ġ
    public float[] zoomDistanceLevelCach = { 60, 20, 6.6f, 2.2f, 0.73f }; // fov����
    public int zoomDistanceLevel = 0; // Ȯ����·���

    protected float mouse; // ���� ��ũ�� ����
    // �ִ� �ܻ���, �ܻ���, �ͷ�Ȯ�� ����, �Ϲ� ����, �ִ� ��� ����
    public enum CameraType { MAXZOOM, ZOOM, TARRET, NORMAL, MAXOUT };
    public CameraType cameraTypeState = CameraType.NORMAL;
    public bool check = true;

    public void LateUpdate()
    {
        MouseMove();
        ZoomWheel();
        CheckCameraType();
        CameraPos();
    }

    protected void CheckCameraType()
    {
        if (zoomDistanceLevel != 0)
        {
            cameraTypeState = CameraType.ZOOM;
        }
        else if (distance == minZoom && check)
        {
            check = false;
            StartCoroutine(WaitForIt());
            cameraTypeState = CameraType.TARRET;
        }
        else if (distance > minZoom && distance != maxZoom && check)
        {
            check = false;
            StartCoroutine(WaitForIt());
            cameraTypeState = CameraType.NORMAL;
        }
        else if (distance == maxZoom)
        {
            cameraTypeState = CameraType.MAXOUT;
        }
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(smoothTime * 2);
        check = true;
    }

    protected void CameraPos()
    {
        if (distance == minZoom)
        {
            NormalCamera();
        }
        else if (distance > minZoom && distance != maxZoom)
        {
            if (distance != minZoom)
            {
                SmoothCamera(distance, player);
            }
        }
        else if (distance == maxZoom)
        {
            SmoothCamera(distance, player);
        }
    }

    protected virtual void NormalCamera() { }
    protected void ZoomWheel()
    {
        mouse = Input.GetAxis("Mouse ScrollWheel");

        if (cameraTypeState == CameraType.NORMAL || cameraTypeState == CameraType.MAXOUT)
        {
            distance -= mouse * wheelspeed;
            distance = Mathf.Clamp(distance, minZoom, maxZoom);
        }
        if (cameraTypeState == CameraType.TARRET)
        {
            distance -= mouse * wheelspeed;
            distance = Mathf.Clamp(distance, minZoom, maxZoom);
        }
        if (cameraTypeState == CameraType.TARRET || cameraTypeState == CameraType.ZOOM)
        {
            if (mouse > 0)
            {
                zoomDistanceLevel++;
            }
            if (mouse < 0)
            {
                zoomDistanceLevel--;
            }

            zoomDistanceLevel = Mathf.Clamp(zoomDistanceLevel, 0, zoomDistanceLevelCach.Length - 1);
        }

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomDistanceLevelCach[zoomDistanceLevel], smoothTime * 2);
    }

    protected void MouseMove()
    {
        if (Input.GetKey(KeyCode.LeftControl) == false)
        {
            xmove += Input.GetAxis("Mouse X") * (zoomDistanceLevelCach[zoomDistanceLevel] / 60);
            ymove -= Input.GetAxis("Mouse Y") * ((zoomDistanceLevelCach[zoomDistanceLevel] + 10) / 60);
        }

        switch (zoomDistanceLevel)
        {
            case 0:
                //if (ymove < 2.75f) { ymove = 2.75f; }
                vel = 0f;
                break;
            case 1:
                //if (ymove < 1.425f) { ymove = 1.425f; }
                vel = 1.32f;
                break;
            case 2:
                //if (ymove < 0.835f) { ymove = 0.835f; }
                vel = 1.92f;
                break;
            case 3:
                //if (ymove < 0.525f) { ymove = 0.525f; }
                vel = 2.24f;
                break;
            case 4:
                //if (ymove < 0.35f) { ymove = 0.35f; }
                vel = 2.4f;
                break;
            case 5:
                //if (ymove < 0.25f) { ymove = 0.25f; }
                vel = 2.5f;
                break;
        }
        if (ymove < 2.75f) { ymove = 2.75f; }
        if (ymove > 22.5f) { ymove = 22.5f; }
        if (xmove > 180) { xmove = 0; }
        if (xmove < -180) { xmove = 0; }

        transform.rotation = Quaternion.Euler(ymove * sensitivity - (vel * 2), xmove * sensitivity, 0);
    }

    /// <summary>
    /// ī�޶� �ε巴�� �ϴ� �Լ�
    /// </summary>
    /// <param name="pos">���߾����� ���� �̸�ŭ �ڷ� ���� ������</param>
    /// <param name="obj">�̿�����Ʈ�� ������ �̿��ϰ� ��</param>
    protected void SmoothCamera(float pos, GameObject obj)
    {
        cameraNorVec = new Vector3(0.0f, 0.0f, -pos);
        transform.position = Vector3.SmoothDamp(transform.position,
                            obj.transform.position + (transform.rotation * cameraNorVec),
                            ref velocity, smoothTime);
        /*transform.position = Vector3.Lerp(transform.position,
                            obj.transform.position + (transform.rotation * cameraNorVec),
                            smoothTime);*/
    }


}
