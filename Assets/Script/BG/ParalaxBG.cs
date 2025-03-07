using UnityEngine;

public class ParalaxBG : MonoBehaviour
{
    private Transform cam;
    private SpriteRenderer sr;
    private float length;
    private float xPostion;

    private Vector3 targetPos;
    [SerializeField] private float paralaxEffect;
    [SerializeField] private float lerpSpeed;
    void Start()
    {
        cam = Camera.main.transform;
        sr = GetComponent<SpriteRenderer>();

        length = sr.bounds.size.x;
        xPostion = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = cam.position.x * (1 - paralaxEffect);
        float distanceToMoved = cam.position.x * paralaxEffect;

        targetPos = new Vector3(xPostion + distanceMoved, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed);
        if (distanceMoved > xPostion + length)
        {
            xPostion = xPostion + length;
        }
        else if (distanceMoved < xPostion - length)
            xPostion = xPostion - length;


    }
}
