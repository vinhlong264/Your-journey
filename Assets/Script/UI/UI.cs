using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_EqipmentInfor uiSlot;
    public UI_StatsInfo uiStatsInfo;
    public UI_CraftWindow uiCanCraftWindow;


    [SerializeField] private GameObject UI_Character;
    [SerializeField] private GameObject UI_Atribute;
    [SerializeField] private GameObject UI_SkillTree;
    [SerializeField] private GameObject UI_Setting;

    private void Start()
    {
        switchOptionByKey(null);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            switchOptionByKey(UI_Character);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            switchOptionByKey(UI_Atribute);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            switchOptionByKey(UI_SkillTree);
        }

        if (Input.GetKeyDown(KeyCode.N))
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
            return;
        }

        swicthOptions(_entityWindow);
    }
}
