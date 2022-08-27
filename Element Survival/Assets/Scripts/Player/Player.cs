using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour {

    public static Player instance;

    private void Awake() {
        
        if(instance == null) {

            instance = this;

        }

    }

    [Header("Health System")]
    public float health = 100.0f;
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float regenerateAmount = 0.1f;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private AudioSource damageAudio;
    [SerializeField] private Image damageVisual;

    [SerializeField] private GameObject DeathScreen;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private Transform Character;
    [SerializeField] private Transform respawnPoint;


    [Header("Element System")]
    public Ability currentAbility;
    [SerializeField] private Element element;
    [SerializeField] private GameObject elementSelectionGO;
    [SerializeField] private KeyCode abilityMenuKey = KeyCode.Q;
    [SerializeField] private TMP_Text elementText;

    [Header("Bools")]
    public bool isAlive = true;
    public bool canMove = true;
    public bool canLook = true;
    public bool canRegenerate = true;

    [Header("Body Parts")]
    public GameObject body;
    public Camera cam;

    [Header("Scripts")]
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerLook look;
    [SerializeField] private AbilityMenu abilityMenu;

    private void Start() {

        if (!element) {

            elementSelectionGO.SetActive(true);

            canMove = false;
            canLook = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        } else {

            elementSelectionGO.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
        
        if(damageVisual) {
            
            damageVisual.canvasRenderer.SetAlpha(0.0f);
            damageVisual.gameObject.SetActive(true);
        
        }

        if(element) {

            elementText.text = "Element: " + element.name;
            elementText.color = element.color;

        }

    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.KeypadPlus)) Heal(10.0f);
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) Damage(15.0f);
        if (canRegenerate) Heal(regenerateAmount * 0.01f);

        if ((Input.GetKeyDown(abilityMenuKey)
            || (abilityMenu.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)))
            && element) {

            abilityMenu.Open(element);

        }

        foreach(Ability ability in element.abilities) {

            if (Input.GetKeyDown(ability.useKey)) ability.Use();

        }

        if(currentAbility && Input.GetKeyDown(KeyCode.Mouse0)) {

            currentAbility.Activate();

        }

    }

    public void SelectElement(Element element) {

        this.element = element;
        elementSelectionGO.SetActive(false);

        canMove = true;
        canLook = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        elementText.text = "Element: " + element.name;
        elementText.color = element.color;

    }

    //-----------------------------
    //--------Health System--------
    //-----------------------------
    public void Damage(float amount) {

        //If Player is Dead don't do anything
        if (!isAlive) return;

        //If the Player will die when damaged by this amount
        if (health - amount <= 0.0f) {
            
            health = 0.0f;
            
            UpdateUI();
            Die();
            
            return;
        
        }

        //If there is a Damage Visual
        if (damageVisual) {
            
            damageVisual.canvasRenderer.SetAlpha(0.7f);
            damageVisual.CrossFadeAlpha(0.0f, 0.5f, false);
        
        }
        
        //If there is a Damage Audio
        if (damageAudio) damageAudio.Play();

        health -= amount;
        UpdateUI();

    }
    public void Heal(float amount) {

        //If the Player already has maxHealth or is Dead, don't do anything
        if (health >= maxHealth || !isAlive) return;

        //If the Heal would exceed the MaxHealth
        if (health + amount >= maxHealth) {

            health = maxHealth;
            
            UpdateUI();
            
            return;
        
        }

        health += amount;
        UpdateUI();

    }
    private void Die() {

        isAlive = false;
        canMove = false;
        canLook = false;
        canRegenerate = false;

        DeathScreen.SetActive(true);
        PauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Unlocks the cursor, and makes it visible to click through the menus//

        Player.instance.canLook = false;
    }

    private void UpdateUI() {

        healthText.text = "Health: " + ((int)health).ToString();

    }

    public void Respawn ()
    {
        Character.transform.position = respawnPoint.transform.position;

        isAlive = true;
        canMove = true;
        canLook = true;
        canRegenerate = true;
        DeathScreen.SetActive(false);
        PauseMenu.SetActive(true);

        health = maxHealth;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Makes the cursor invisible again, and locks it//

        Player.instance.canLook = true;
        //Resumes the camera rotation when the Pause menu fades//
    }

}