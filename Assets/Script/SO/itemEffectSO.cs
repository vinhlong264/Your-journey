using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/ItemEffect")]
public class itemEffectSO : ScriptableObject // class base effectSO
{
    [SerializeField] protected GameObject objEffect;
    public virtual void excuteEffect(Transform _enemyPos)
    {
        
    }
}
