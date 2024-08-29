using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [Header("Major status info")]
    public Status strength; // với mỗi điểm nâng cấp thì dame cơ bản sẽ tăng lên 1
    public Status ability; // với mỗi điểm nâng cấp thì sẽ tăng các kĩ năng ví dụ là 1% né
    public Status inteligent; // với mỗi điểm nâng cấp thì sẽ tăng sức mạnh phép thuật
    public Status vitality; // với mỗi điểm nâng cấp sẽ tăng lượng máu và giáp

    [Header("Defend status info")]
    public Status maxHealth; // chỉ số máu tối đa
    public Status armor; // chỉ số giáp
    public Status evasion; // chỉ số né chiêu


    public Status dame;
   

    [SerializeField] private float currentHealth;
    
    protected virtual void Start()
    {
        currentHealth = maxHealth.getValue();
    }


    public virtual void DoDame(CharacterStatus _targetStatus)
    {
        if (AvoidAttack(_targetStatus))
        {
            Debug.Log("Attack avoid");
            return;
        }


        int totalDame = dame.getValue() + strength.getValue();

        totalDame = CheckTargetArmor(totalDame, _targetStatus);

        _targetStatus.takeDame(totalDame);
    }


    public virtual void takeDame(float _dame) // Attack
    {
        currentHealth -= _dame;
        Debug.Log(_dame);
        Debug.Log(currentHealth);
        if(currentHealth < 0 )
        {
            Die();
        }
    }


    protected virtual void Die()
    {
        
    }


    private int CheckTargetArmor(int dame , CharacterStatus _target)
    {
        dame -= _target.armor.getValue();
        dame  = Mathf.Clamp(dame , 0 , int.MaxValue);

        return dame;
    }

    private bool AvoidAttack(CharacterStatus _target)
    {
        int toltalEvasion = _target.evasion.getValue() + _target.ability.getValue();
        if(Random.Range(0,100) < toltalEvasion)
        {
            return true;
        }

        return false;
    }  
}


[System.Serializable]
public class Status // Class để chứa các thay đổi về chỉ số
{
    [SerializeField] private int baseValue; // chỉ số gốc 
    public List<int> modifiers = new List<int>(); // list các chỉ số dùng để thay đổi baseValue

    public int getValue() // lấy giá trị
    {
        int finalValue = baseValue;
        foreach(var modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue;
    }


    public void addModifiers(int _modifier) // thêm sự thay đổi
    {
        modifiers.Add( _modifier);
    }

    public void removeModifiers(int _modifier) // xóa sự thay đổi
    {
        modifiers.Remove( _modifier);
    }
}
