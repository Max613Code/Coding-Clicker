using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public Camera cam4;

    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    public Canvas canvas4;

    private void Start()
    {
        EnableCamera(cam1);
        DisableCamera(cam2);
        DisableCamera(cam3);
        DisableCamera(cam4);
        canvas1.enabled = true;
        canvas2.enabled = false;
        canvas3.enabled = false;
        canvas4.enabled = false;
    }

    public void SwitchToCam1()
    {
        EnableCamera(cam1);
        DisableCamera(cam2);
        DisableCamera(cam3);
        DisableCamera(cam4);
        canvas1.enabled = true;
        canvas2.enabled = false;
        canvas3.enabled = false;
        canvas4.enabled = false;
        canvas2.gameObject.transform.position = new Vector3(5000, 5000, canvas2.gameObject.transform.position.z);
        canvas3.gameObject.transform.position = new Vector3(5000, 9000, canvas1.gameObject.transform.position.z);
        canvas4.gameObject.transform.position = new Vector3(6000, 7000, canvas4.gameObject.transform.position.z);
    }

    public void SwitchToCam2()
    {
        EnableCamera(cam2);
        DisableCamera(cam1);
        DisableCamera(cam3);
        DisableCamera(cam4);
        canvas1.enabled = false;
        canvas2.enabled = true;
        canvas3.enabled = false;
        canvas4.enabled = false;
        canvas1.gameObject.transform.position = new Vector3(5000, 5000, canvas1.gameObject.transform.position.z);
        canvas3.gameObject.transform.position = new Vector3(5000, 9000, canvas3.gameObject.transform.position.z);
        canvas4.gameObject.transform.position = new Vector3(6000, 7000, canvas4.gameObject.transform.position.z);
    }

    public void SwitchToCam3()
    {
        DisableCamera(cam1);
        DisableCamera(cam2);
        EnableCamera(cam3);
        DisableCamera(cam4);
        canvas1.enabled = false;
        canvas2.enabled = false;
        canvas3.enabled = true;
        canvas4.enabled = false;
        canvas1.gameObject.transform.position = new Vector3(5000, 5000, canvas1.gameObject.transform.position.z);
        canvas2.gameObject.transform.position = new Vector3(5000, 7000, canvas2.gameObject.transform.position.z);
        canvas4.gameObject.transform.position = new Vector3(6000, 7000, canvas4.gameObject.transform.position.z);
    }
    
    public void SwitchToCam4()
    {
        DisableCamera(cam1);
        DisableCamera(cam2);
        DisableCamera(cam3);
        EnableCamera(cam4);
        canvas1.enabled = false;
        canvas2.enabled = false;
        canvas3.enabled = false;
        canvas4.enabled = true;
        canvas1.gameObject.transform.position = new Vector3(5000, 5000, canvas1.gameObject.transform.position.z);
        canvas2.gameObject.transform.position = new Vector3(5000, 7000, canvas2.gameObject.transform.position.z);
        canvas3.gameObject.transform.position = new Vector3(6000, 7000, canvas4.gameObject.transform.position.z);
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
