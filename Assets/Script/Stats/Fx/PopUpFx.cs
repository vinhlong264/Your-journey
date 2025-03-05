using TMPro;
using UnityEngine;

public class PopUpFx : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dameTxt;
    private DameColor dameColor;

    [Header("Setting infor")]
    [SerializeField] private float lifeTime;
    [SerializeField] private float speedDisappear;
    [SerializeField] private float speedDefault;
    [SerializeField] private float colorAplaSpeed;
    private float speed;
    private float timer;
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

    private void OnEnable()
    {
        speed = speedDefault;
        timer = lifeTime;
        dameTxt.color = new Color(1, 1, 1, 1);
    }

    public void setDame(int _dame, DameColor _dameColor)
    {
        dameColor = _dameColor;
        dameTxt.text = _dame.ToString();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position , new Vector3(transform.position.x , transform.position.y + 1) , speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            float alpha = dameTxt.color.a - colorAplaSpeed * Time.deltaTime;
            dameTxt.color = new Color(1,1,1,alpha); 
            if(dameTxt.color.a < 50)
            {
                speed = speedDisappear;
            }

            if(dameTxt.color.a <= 0)
            {
                gameObject.SetActive(false);
            }

        }
    }
}

public enum DameColor
{
    DEFAULT, CRITICAL, FIRE, ICE, LiGHTING
}
