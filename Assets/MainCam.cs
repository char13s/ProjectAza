using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
public class MainCam : MonoBehaviour
{
    void Start()
    {
        GameManager.setCam += SetCam;
    }
    private void SetCam(Transform target) {
        GetComponent<CinemachineFreeLook>().m_Follow = target;
        GetComponent<CinemachineFreeLook>().m_LookAt = target;
    }
}
