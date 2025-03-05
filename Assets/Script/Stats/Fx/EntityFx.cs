using System.Collections;
using UnityEngine;

public class EntityFx : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Material hitMat;
    private Material originalMat;
    private float timeEffect;
    private Entity entity;


    [Header("Apply Ailment info")]
    [SerializeField] private Color[] chill;
    [SerializeField] private Color[] ingnite;
    [SerializeField] private Color[] shock;

    [Header("Effect hit impact infor")]
    [SerializeField] private GameObject hitImpact;
    [SerializeField] private GameObject hitCrital;

    [Header("Popup fx infor")]
    [SerializeField] private GameObject popUpFx;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        entity = GetComponent<Entity>();
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

    public void CreatPopUp(Transform target , int dame , DameColor color)
    {
        GameObject _popupFx = GameManager.Instance.GetObjFromPool(popUpFx);
        if(_popupFx != null)
        {
            _popupFx.transform.position = target.position;
            _popupFx.transform.rotation = Quaternion.identity;
            _popupFx.GetComponent<PopUpFx>().setDame(dame , color);
        }
    }


    #region Fx HitImpact
    IEnumerator FlashFx()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sr.color = currentColor;
        sr.material = originalMat;
    }

    public void CreatHitImpact(Transform posTarget, bool canCital)
    {
        float randomX = Random.Range(-.5f, .5f);
        float randomY = Random.Range(-.5f, .5f);
        GameObject _hitImpact = hitImpact;

        if (canCital)
        {
            _hitImpact = hitCrital;
        }

        GameObject hitEffect = GameManager.Instance.GetObjFromPool(_hitImpact);
        if (hitEffect != null)
        {
            hitEffect.transform.position = posTarget.position + new Vector3(randomX , randomY);
            hitEffect.transform.rotation = Quaternion.identity;
            hitEffect.transform.rotation = posTarget.rotation;
            StartCoroutine(DeactiveMe(hitEffect));
        }
    }

    IEnumerator DeactiveMe(GameObject objMe)
    {
        yield return new WaitForSeconds(0.3f);
        objMe.SetActive(false);
    }

    #endregion

    #region Fx Stun
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
    #endregion

    #region Fx Aliment
    public void chillColorFor(float _second)
    {
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
    #endregion
}
