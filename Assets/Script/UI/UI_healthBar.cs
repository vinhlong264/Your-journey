using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_healthBar : MonoBehaviour
{
    private Entity entity;
    private RectTransform myTransform;
    private CharacterStats myCharacter;
    private Slider mySlider;
    void Start()
    {
        entity = GetComponentInParent<Entity>();
        myTransform = GetComponent<RectTransform>();
        mySlider = GetComponentInChildren<Slider>();
        myCharacter = GetComponentInParent<CharacterStats>();

        entity.onFliped += onFliped;
        myCharacter.onUiHealth += updateUiHealth;

        updateUiHealth();

    }

    private void onFliped()
    {
        myTransform.Rotate(0, 180, 0);
    }


    private void OnDisable()
    {
        entity.onFliped -= onFliped;
        myCharacter.onUiHealth -= updateUiHealth;
    }

    private void updateUiHealth()
    {
        mySlider.maxValue = myCharacter.getMaxHealth();
        mySlider.value = myCharacter.currentHealth;

        if(mySlider.value <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
