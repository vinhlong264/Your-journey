using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_EqipmentInfor uiEquipmentInfo;
    public UI_StatsInfo uiStatsInfo;
    public UI_SkillInformation uiSkillInfo;
    public UI_CraftWindow uiCanCraftWindow;
    


    [SerializeField] private GameObject UI_Character;
    [SerializeField] private GameObject UI_Atribute;
    [SerializeField] private GameObject UI_SkillTree;
    [SerializeField] private GameObject UI_Setting;
    [SerializeField] private GameObject UI_InGame;

    private void Start()
    {
        swicthOptions(UI_InGame);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchOptionByKey(UI_Character);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchOptionByKey(UI_Atribute);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchOptionByKey(UI_SkillTree);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            switchOptionByKey(UI_Setting);
        }
    }

    public void swicthOptions(GameObject _entity)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_entity != null)
        {
            _entity.SetActive(true);
        }
    }

    private void switchOptionByKey(GameObject _entityWindow)
    {
        if(_entityWindow != null && _entityWindow.activeSelf) // kiểm tra xem nếu _entityWindow != null và đang được active thì tắt đi
        {
            _entityWindow.SetActive(false);
            checkForInGame();
            return;
        }

        swicthOptions(_entityWindow);
    }

    private void checkForInGame()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                return;
            }
        }

        swicthOptions(UI_InGame);
    }
}
