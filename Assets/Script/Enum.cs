public enum StatType
{
    //Major type status
    Strength,
    Ability,
    inteligent,
    vitality,
    //Defend type status
    Health,
    Armor,
    Evasion,
    MagicResitance,

    //Dame physical status
    Dame,
    CritRate,
    CritPower,

    //Dame magical status
    FireDame,
    IceDame,
    LightingDame
}

public enum SkillType
{
    //Skill sword
    Sword_Skill,// Ném thanh kiếm về phía trước với sức tấn công của nhân vật +20%ST
    Stop_Time, // Đóng băng thời gian của mục tiêu
    Hurt_Target, // Tạo hiệu ứng đốt máu cho mục tiêu
    Bounce, // Thanh kiếm nảy qua lại qua nhiều mục tiêu
    Spin, // Thanh kiếm quay liên tục về phía trước
    Drill, // Thanh kiếm lao xuyên qua các mục tiêu

    //Skill Mirage
    Mirage_Skill,
    Aggresive_Mirage, // Mirage được tạo ra với sức tấn công của nhân vật + 30%ST
    Impact_Mirage, //Mirage được tạo ra với sức tấn công của nhân vật + 60%ST
    Crystal_Mirage, // Tạo ra 4 viên pha lê, sau đó tạo ra 4 Mirage cùng tấn công của nhân vật + 60%ST

    //Skill_Crystal
    Crystal_Skill, //Tạo ra pha lên và nếu kích hoạt lần nữa sẽ hoán đổi vị trí với pha lê
    //Nhánh 1 của skill Crystal
    Crystal_Stun, // Pha lê tiến tới vị trí mục tiêu và gây choáng vói những mục tiêu ở gần với ST phép bản thân +30%ST

    //Nhánh 2 của skill Crystal
    Crystal_Explosion, // Pha lê sẽ phát nổ với ST phép của thân +30%ST
    Crysral_Destruction, // Pha lê sẽ di chuyển tới mục tiêu gần nhất và phát nổ với ST phép của bản thân +40%ST
    Crystal_Multipler, // tạo ra 3 viên pha lê cùng 1 lúc di chuyển tới mục tiêu và phát nổi với ST phép của bản thân +40%ST

    //Skill Dash
    Dash_Skill,
    Dash_SpaceCrack, // gây dame lên các mục tiêu trên đường đi

    //Skill Parry
    Parry_Skill,
    Parry_Restore, // Gây choáng và hồi lại 5% máu
    Parry_Explore, // Tạo ra 1 vụ nổ tại thời điểm triển khai skill khiến các mục tiêu xung quanh văng xa

    Skill_Dodge, // tăng 10% Evasion
    Dodge_Lucky, // Tăng thêm 10% - 40% Evasion (Chỉ số ở lần nâng cấp sẽ là ngẫu nhiên)

    Skill_BlackHole // Tạo ra hố đen và dừng toàn bộ hoạt động của các vật ở bên trong hỗ đen, sau đó dịch chuyển tới và tấn công các mục tiêu từ các vị trí khác
}


public enum ItemType
{
    Material,
    Equipment
}

public enum EqipmentType
{
    Sword,
    Armor,
    Helmet,
    Bottle,
    Necklace
}

public enum Skill_HotKey
{
    Dash,
    Parry,
    Crystal,
    ThrowSword,
    Ultimate,
    Bottle
}


public enum EnemyType
{
    SLIME,
    WOLF,
    SEKELETON,
    BOSS
}
