using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Skill Infor")]
    [SerializeField] private GameObject swordPrefabs; // sword clone
    [SerializeField] private Vector2 laughDirection; // Hướng ném của cây kiếm
    [SerializeField] private float swordGravity; // trọng lực
}
