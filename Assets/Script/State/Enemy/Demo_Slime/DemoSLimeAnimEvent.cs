using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSLimeAnimEvent : AnimationEventBase
{
    [SerializeField] private Transform firePos;
    [SerializeField] private ShockWaveController shockWaveController;
    private void AttackBullet()
    {
        if (shockWaveController == null) return;

        Instantiate<ShockWaveController>(shockWaveController , firePos.position, Quaternion.identity)
            .setUp(enemyBase.isFacingDir , enemyBase.status , 15f);
    }
}
