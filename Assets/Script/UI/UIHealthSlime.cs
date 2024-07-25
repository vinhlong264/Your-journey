using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthSlime : MonoBehaviour
{
    private Enemy_Slime enemy;
    private Slider slider;
    void Start()
    {
        enemy = GetComponentInParent<Enemy_Slime>();
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = enemy.hpMax;
        slider.value = enemy.hpCurrent;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = enemy.hpCurrent;
    }
}
