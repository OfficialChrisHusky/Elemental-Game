using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenuSlot : MonoBehaviour {

    [SerializeField] private Ability ability;
    [SerializeField] private Image image;

    public void Initialize(Ability ability) {

        this.ability = ability;
        this.image.sprite = ability.sprite;

    }

    public void Use() {

        AbilityMenu.instance.ShowAbilityInfo(ability);

    }
    
}
