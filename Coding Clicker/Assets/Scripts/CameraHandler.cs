using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;

    public Canvas canvas1;
    public Canvas canvas2;

    private void Start()
    {
        EnableCamera(cam1);
        DisableCamera(cam2);
        canvas1.enabled = true;
        canvas2.enabled = false;
    }

    public void SwitchToCam1()
    {
        EnableCamera(cam1);
        DisableCamera(cam2);
        canvas1.enabled = true;
        canvas2.enabled = false;
        canvas2.gameObject.transform.position = new Vector3(5000, 5000, canvas2.gameObject.transform.position.z);
    }

    public void SwitchToCam2()
    {
        EnableCamera(cam2);
        DisableCamera(cam1);
        canvas1.enabled = false;
        canvas2.enabled = true;
        canvas1.gameObject.transform.position = new Vector3(5000, 5000, canvas1.gameObject.transform.position.z);
    }

    public void EnableCamera(Camera cam)
    {
        cam.enabled = true;
        cam.GetComponent<AudioListener>().enabled = true;
    }

    public void DisableCamera(Camera cam)
    {
        cam.enabled = false;
        cam.GetComponent<AudioListener>().enabled = false;
    }


}
