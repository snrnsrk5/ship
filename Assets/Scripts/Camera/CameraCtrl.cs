using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Camera cam;
    [Header("스크립트")]
    public GameObject player; // 바라볼 플레이어 오브젝트입니다.

    [Header("기본수치")]
    [Range(0, 1)]
    public float smoothTime = 0.2f; // 카메라가 부드럽게 움직일 시간
    [Range(1, 8)]
    public float sensitivity = 2f; // 카메라 민감도
    private float wheelspeed = 250.0f; // 줌 속도
    Vector3 cameraNorVec; // 카메라 기본 위치
    protected Vector3 velocity = Vector3.zero; // 좌표축 설정

    public float vel = 0;

    [SerializeField] protected int maxZoom;
    [SerializeField] protected int minZoom;

    [Header("현재상태")]
    public float xmove = 0;// X축 누적 이동량
    public float ymove = 0;// Y축 누적 이동량
    public float distance = 110; // 현재 위치
    public float[] zoomDistanceLevelCach = { 60, 20, 6.6f, 2.2f, 0.73f }; // fov조절
    public int zoomDistanceLevel = 0; // 확대상태레벨

    protected float mouse; // 현재 스크롤 상태
    // 최대 줌상태, 줌상태, 터렛확대 상태, 일반 상태, 최대 축소 상태
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
    /// 카메라 부드럽게 하는 함수
    /// </summary>
    /// <param name="pos">정중앙으로 부터 이만큼 뒤로 축이 고정됨</param>
    /// <param name="obj">이오브젝트를 축으로 이용하게 됨</param>
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
