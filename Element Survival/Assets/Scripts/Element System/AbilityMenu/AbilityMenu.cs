using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityMenu : MonoBehaviour {

    public static AbilityMenu instance;

    private void Awake() {
        
        if(instance == null) {

            instance = this;

        }

    }

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform content;

    [Header("Info")]
    [SerializeField] private GameObject abilityInfo;
    [SerializeField] private TMP_Text abilityNameText;
    [SerializeField] private TMP_Text abilityDescText;
    [SerializeField] private Image abilityIcon;

    Element element;
    bool hasInitializedAbilities;

    public void Open(Element element) {

       this.element = element;

        abilityInfo.SetActive(false);
        gameObject.SetActive(!gameObject.activeInHierarchy);

        Player.instance.canMove = !gameObject.activeInHierarchy;
        Player.instance.canLook = !gameObject.activeInHierarchy;

        Cursor.lockState = gameObject.activeInHierarchy ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = gameObject.activeInHierarchy;

        if (!hasInitializedAbilities) { InitializeAbilities(); }

    }

    public void ShowAbilityInfo(Ability ability) {

        if (!abilityInfo.activeInHierarchy) abilityInfo.SetActive(true);

        Debug.Log(ability.name);

        abilityNameText.text = ability.name;
        abilityDescText.text = ability.description;
        abilityIcon.sprite = ability.sprite;

    }

    private void InitializeAbilities() {

        foreach(Ability ability in element.abilities) {

            Instantiate(slotPrefab, content).GetComponent<AbilityMenuSlot>().Initialize(ability);


        }

        hasInitializedAbilities = true;

    }
    
}
