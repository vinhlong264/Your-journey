using TMPro;
using UnityEngine;

public class BlackHole_HotKey_Controller : MonoBehaviour
{
    private SpriteRenderer sr;


    private KeyCode myHotKey; // KeyCode
    private TextMeshProUGUI myHotKeyText; // text 
    private Transform enemiesTransform; // vị trí enemy
    private BlackHole_Skill_Controller myBlackHole;


    //Hàm cài đặt các KeyCode
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
            myBlackHole.addEnemy(enemiesTransform);// gán vị trí của Enemy khi mỗi lần ấn nút

            myHotKeyText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
