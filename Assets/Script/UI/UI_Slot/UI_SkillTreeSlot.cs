using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISave
{
    [Header("Skill information")]
    [SerializeField] private string skillName; // tên skill
    [TextArea]
    [SerializeField] private string skillDescription; // thông tin skill

    [Header("Skill")]
    public bool isUnlocked; //Biến kiểm tra xem skill này đã được unlock chưa
    [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked; // Mảng chứa các skill đã được mở khóa(có thể gọi là kĩ năng tiền đề để nâng cấp skill tiếp theo)
    [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked; // Mảng chứa các skill đang bị khóa
    [SerializeField] private Image skillImage;
    [SerializeField] private Color skillLockColor;

    public System.Action eventUnlockSKill; // Event
    [SerializeField] private UI_SkillInformation skillInfor;
    //[SerializeField] private LevelData skillData;

    private UI ui;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => unlockedSkill());
    }
    private void Start()
    {   
        skillImage = GetComponent<Image>();
        skillImage.color = skillLockColor;

        if (isUnlocked)
        {
            skillImage.color = Color.white;
        } 
    }

    private void OnValidate()
    {
        gameObject.name = "Skill tree - " + skillName;
    }

    private void unlockedSkill()
    {
        for (int i = 0; i < shouldBeUnlocked.Length; i++)
        {
            if (shouldBeUnlocked[i].isUnlocked == false) // Kiểm tra xem các skill tiền đề có được mở khóa chưa, nếu chưa thì thoát luôn
            {
                Debug.Log("Không thể mở skill này");
                return;
            }
        }

        for (int i = 0; i < shouldBeLocked.Length; i++) // Kiểm tra các kĩ năng cần mở khóa đã được mở chưa, nếu rồi thì thoát luôn
        {
            if (shouldBeLocked[i].isUnlocked == true)
            {
                Debug.Log("Không thể mở skill này");
                return;
            }
        }

        isUnlocked = true;
        eventUnlockSKill?.Invoke();
        skillImage.color = Color.white;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        skillInfor.showInformatioSkill(skillName, skillDescription);
    }
        

    public void OnPointerExit(PointerEventData eventData)
    {
       skillInfor.hideInformationWindow();
    }

    public void LoadGame(GameData data)
    {
        if (data == null) return;

        if(data.skills.TryGetValue(skillName, out var value))
        {
            isUnlocked = value;
        }
    }

    public void SaveGame(ref GameData data)
    {
        if(data.skills.TryGetValue(skillName , out var value))
        {
            data.skills.Remove(skillName);
            data.skills.Add(skillName, isUnlocked);
        }
        else
        {
            data.skills.Add(skillName, isUnlocked);
        }
    }
}
