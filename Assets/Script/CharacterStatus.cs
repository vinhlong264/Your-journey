using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public Status dame;
    public Status maxHealth;

    [SerializeField] private float currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth.getValue();
        dame.addModifiers(4);
    }


    public void takeDame(float _dame)
    {
        Debug.Log("Dame receive");
        currentHealth -= _dame;
        if(currentHealth <= 0)
        {
            Debug.Log("Die");
        }
    }

    
}


[System.Serializable]
public class Status
{
    [SerializeField] private int baseValue;
    public List<int> modifiers = new List<int>();

    public int getValue()
    {
        int finalValue = baseValue;
        foreach(var modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue;
    }


    public void addModifiers(int _modifier)
    {
        modifiers.Add( _modifier);
    }

    public void removeModifiers(int _modifier)
    {
        modifiers.Remove( _modifier);
    }
}
