using UnityEngine;

public class UI : MonoBehaviour
{
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
}
