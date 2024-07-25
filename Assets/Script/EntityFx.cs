using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFx : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField]private Material hitMat;
    private Material originalMat;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    public IEnumerator FlashFx()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(0.2f);
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

    private void canCelRedBlink()
    {
        CancelInvoke(); 
        // hàm hủy tất cả các Invoke được gọi
        sr.color = Color.white;
    }
}
