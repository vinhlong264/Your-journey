using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public Status strength;
    public Status dame;
    public Status maxHealth;

    [SerializeField] private float currentHealth;
    
    protected virtual void Start()
    {
        currentHealth = maxHealth.getValue();
    }


    public virtual void DoDame(CharacterStatus _targetStatus)
    {
        int totalDame = dame.getValue() + strength.getValue();

        _targetStatus.takeDame(totalDame);
    }


    public virtual void takeDame(float _dame) // Attack
    {
        Debug.Log("Dame receive");
        currentHealth -= _dame;
        if(currentHealth <= 0 )
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        
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
