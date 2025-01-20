using UnityEngine;

public class UiManager : MonoBehaviour
{
    public UI_EqipmentInfor equipmentInfor;
    public UI_SkillInformation skillInfor;
    public UI_StatsInfo statsInfo;
    public UI_CraftWindow craftWidow;


    [Header("Active window")]
    [SerializeField] private GameObject craft;
    [SerializeField] private GameObject character;



}
