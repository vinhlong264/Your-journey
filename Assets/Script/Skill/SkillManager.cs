using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance { get; private set; }
    public Dash_Skill dash_skill { get ; private set; }
    public Clone_Skill clone_skill { get ; private set; }
    private void Awake()
    {
        if(instance != null)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        dash_skill = GetComponent<Dash_Skill>();
        clone_skill = GetComponent<Clone_Skill>();
    }
}
