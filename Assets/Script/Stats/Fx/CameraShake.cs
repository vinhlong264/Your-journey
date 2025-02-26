using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineImpulseSource _cameraShake;
    [SerializeField] private float shakeMultiliper;

    private void Start()
    {
        _cameraShake = GetComponent<CinemachineImpulseSource>();
    }

    public void ScreenShake(Vector3 shakePower)
    {
        if (_cameraShake == null) return;

        Debug.Log("Camera Shake");
        _cameraShake.m_DefaultVelocity = shakePower * shakeMultiliper;
        _cameraShake.GenerateImpulse();
    }
}
