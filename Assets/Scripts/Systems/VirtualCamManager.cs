using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
public class VirtualCamManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCam;
    [SerializeField] private CinemachineVirtualCamera lockOnCam;
    private void Start() {
        Player.lockOn += SwitchCam;
    }
    private void SwitchCam(bool val) {
        if (val) {
            lockOnCam.m_Priority = 20;
        }
        else {
            lockOnCam.m_Priority = 0;
        }
    }
}
