using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFx : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField]private Material hitMat;
    private Material originalMat;


    [Header("Apply Ailment info")]
    [SerializeField] private Color[] chill;
    [SerializeField] private Color[] ingnite;
    [SerializeField] private Color[] shock;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    public IEnumerator FlashFx()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sr.color = currentColor;
        sr.material = originalMat;
    }

    private void RedColorBlink()
    {
        if(sr.color != Color.white)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    public void StunColorFor(float _second)
    {

    }


    private void canCelRedBlink()
    {
        //CancelInvoke(); 
        // hàm hủy tất cả các Invoke được gọi
        sr.color = Color.white;
    }

    public void chillColorFor(float _second)
    {
        //InvokeRepeating("chillColor", 0, 0.3f);
        //Invoke("canCelRedBlink", _second);
        StartCoroutine(effectAiliment(_second, chill));
    }

    IEnumerator effectAiliment(float _second , Color[] _color)
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
