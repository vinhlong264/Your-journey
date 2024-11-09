using TMPro;
using UnityEngine;

public class UI_StatsInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
   
    public void showStatsDes(string _Des)
    {
        if (_Des.Equals(""))
        {
            return;
        }

        descriptionText.text = _Des;
        gameObject.SetActive(true);
    }

    public void hideStatsDes()
    {
        gameObject.SetActive(false);
    }
}
