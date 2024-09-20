using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/ItemEffect")]
public class itemEffectSO : ScriptableObject // class base effectSO
{
    [SerializeField] protected GameObject objEffect;
    protected Player player => PlayerManager.Instance.player;
    public virtual void excuteEffect(Transform _enemyPos)
    {
        Debug.Log("effect excute");
    }
}
