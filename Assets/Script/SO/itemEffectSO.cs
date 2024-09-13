using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/ItemEffect")]
public class itemEffectSO : ScriptableObject
{
    public virtual void excuteEffect()
    {
        Debug.Log("Effect skill");
    }
}
