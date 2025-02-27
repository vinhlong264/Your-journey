using TMPro;
using UnityEngine;

public class PopUpFx : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dameTxt;
    private DameColor dameColor;
    private void Start()
    {
        if(dameTxt != null)
        {
            switch (dameColor)
            {
                case DameColor.DEFAULT:
                    dameTxt.color = Color.white; 
                    break;
                case DameColor.CRITICAL:
                    dameTxt.color = Color.yellow;
                    break;
                case DameColor.FIRE:
                    dameTxt.color = Color.red;
                    break;
                case DameColor.ICE:
                    dameTxt.color = Color.blue;
                    break;
                case DameColor.LiGHTING:
                    dameTxt.color = Color.green;
                    break;
            }
        }
    }

    public void setDame(int _dame, DameColor _dameColor)
    {
        dameColor = _dameColor;
        dameTxt.text = _dame.ToString();
    }
}

public enum DameColor
{
    DEFAULT, CRITICAL, FIRE, ICE, LiGHTING
}
