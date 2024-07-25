using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthSkeleton : MonoBehaviour
{
    private Slider healthBar;
    private Skeleton skeleton;
    void Start()
    {
        skeleton = GetComponentInParent<Skeleton>();
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = skeleton.hpMax;
        healthBar.value = skeleton.currentHp;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = skeleton.currentHp;
    }
}
