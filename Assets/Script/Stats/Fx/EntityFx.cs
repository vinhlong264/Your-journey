using System.Collections;
using UnityEngine;

public class EntityFx : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Material hitMat;
    private Material originalMat;
    private float timeEffect;


    [Header("Apply Ailment info")]
    [SerializeField] private Color[] chill;
    [SerializeField] private Color[] ingnite;
    [SerializeField] private Color[] shock;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    private void Update()
    {
        timeEffect -= Time.deltaTime;
    }

    public void setTimeDuration(float _time)
    {
        timeEffect = _time;
    }

    IEnumerator FlashFx()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sr.color = currentColor;
        sr.material = originalMat;
    }


    public void StunColorFor()
    {
        StartCoroutine(StopStunCrountine());
    }

    IEnumerator StunCrountine()
    {
        while (true)
        {
            if (timeEffect < 0)
            {
                Debug.Log("Stop StunCrountine");
                break;
            }

            RedColorBlink();
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator StopStunCrountine()
    {
        yield return StartCoroutine(StunCrountine());
        Debug.Log("Call StopStunCrountine");
        canCelRedBlink();
    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    private void canCelRedBlink()
    {
        sr.color = Color.white;
    }

    public void chillColorFor(float _second)
    {
        //InvokeRepeating("chillColor", 0, 0.3f);
        //Invoke("canCelRedBlink", _second);
        StartCoroutine(effectAiliment(_second, chill));
    }

    IEnumerator effectAiliment(float _second, Color[] _color)
    {
        yield return StartCoroutine(colorEffect(_color));
        yield return new WaitForSeconds(_second);
        canCelRedBlink();
    }

    IEnumerator colorEffect(Color[] _color)
    {
        yield return new WaitForSeconds(0.3f);
        if (sr.color != _color[0])
        {
            sr.color = _color[0];
        }
        else
        {
            sr.color = _color[1];
        }
    }
    public void shockColorFor(float _second)
    {
        StartCoroutine(effectAiliment(_second, shock));
    }

    public void ingniteColorFor(float _second)
    {
        StartCoroutine(effectAiliment(_second, ingnite));
    }
}
