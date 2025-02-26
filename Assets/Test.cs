using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private CinemachineCollisionImpulseSource _cameraShakeCollision;
    void Start()
    {
        _cameraShakeCollision = GetComponent<CinemachineCollisionImpulseSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _cameraShakeCollision.m_DefaultVelocity = new Vector3(1, 1, 0) * 0.5f;
            _cameraShakeCollision.GenerateImpulse();
        }
    }
}
