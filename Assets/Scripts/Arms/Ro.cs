using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ro : MonoBehaviour
{
    [Header("스크립트")]
    public CameraCtrl cameraCtrl;

    [Header("기본수치")]
    public float degSec = 18f;

    private void Update()
    {
        Rot();
    }

    protected void Rot()
    {
        transform.rotation = Quaternion.RotateTowards
        (transform.rotation, Quaternion.Euler
        (new Vector3(-90, 0, cameraCtrl.transform.eulerAngles.y + 180)),
        180 * Time.deltaTime / degSec);
    }
}

