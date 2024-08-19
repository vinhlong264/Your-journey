using TMPro;
using UnityEngine;

public class BlackHole_HotKey_Controller : MonoBehaviour
{
    private SpriteRenderer sr;


    private KeyCode myHotKey;
    private TextMeshProUGUI myHotKeyText;
    private Transform enemiesTransform;
    private BlackHole_Skill_Controller myBlackHole;

    public void setUpKeyCode(KeyCode _myNewHotKey , Transform _enemiesTransform , BlackHole_Skill_Controller _blackHole)
    {
        sr = GetComponent<SpriteRenderer>();
        myHotKeyText = GetComponentInChildren<TextMeshProUGUI>();
        enemiesTransform = _enemiesTransform;
        myBlackHole = _blackHole;
        myHotKey = _myNewHotKey;
        myHotKeyText.text = myHotKey.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(myHotKey))
        {
            myBlackHole.addEnemy(enemiesTransform);

            myHotKeyText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
