using UnityEngine;

public class ParalaxBG : MonoBehaviour
{
    private Transform camPos;
    private float startingPos;
    private float lengthOfSprite;
    private SpriteRenderer sr;

    [SerializeField] private float amountOfParalax;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startingPos = transform.position.x;
        lengthOfSprite = sr.bounds.size.x;

        camPos = Camera.main.transform;
    }

    private void Update()
    {
        float temp = camPos.position.x * (1 - amountOfParalax);
        float distance = camPos.position.x * amountOfParalax;

        Vector3 newPos = new Vector3(startingPos + distance , transform.position.y, transform.position.z);

        transform.position = newPos;

        if(temp > startingPos + (lengthOfSprite / 2))
        {
            startingPos += lengthOfSprite;
        }
        else if(temp < startingPos - (lengthOfSprite / 2))
        {
            startingPos -= lengthOfSprite;
        }
    }

}
