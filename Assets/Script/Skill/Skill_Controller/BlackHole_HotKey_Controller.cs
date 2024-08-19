using TMPro;
using UnityEngine;

public class BlackHole_HotKey_Controller : MonoBehaviour
{
    private KeyCode myHotKey;
    private TextMeshProUGUI myHotKeyText;

    public void setUpKeyCode(KeyCode _myNewHotKey)
    {
        myHotKeyText = GetComponentInChildren<TextMeshProUGUI>();
        myHotKey = _myNewHotKey;
        myHotKeyText.text = myHotKey.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(myHotKey))
        {
            Debug.Log("KeyCode: " + myHotKeyText.text);
        }
    }
}
