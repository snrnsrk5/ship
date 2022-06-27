using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCastCtrl : MonoBehaviour
{
    //함포 사거리
    public Camera cam;
    public float direction;
    public float rDirection;
    public Transform ship;
    public Transform cach;
    public Text text;
    public CameraCtrl cameraCtrl;
    public RaycastHit hit;

    void Start()
    {

    }

    void Update()
    {
        rDirection = direction - (direction * ((cameraCtrl.ymove - 2.75f) / (22.5f - 2.75f)));

        /*Debug.DrawRay(transform.position, transform.up, Color.blue, 0.1f);
        transform.position = new Vector3(0, 0, rDirection);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit))
        {
            text.text = string.Format($"{hit.point}");
            g.transform.position = hit.point;
        }*/

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        if (Physics.Raycast(ray, out hit))
        {
            float distance = Vector3.Distance(hit.point, ship.transform.position);


            text.text = string.Format($"{distance}");
            cach.transform.position = hit.point;

            // Do something with the object that was hit by the raycast.
        }

    }
}
