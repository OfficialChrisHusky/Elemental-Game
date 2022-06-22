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

    [Header("Element System")]
    [SerializeField] private Element element;
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
        
        if(damageVisual) {
            
            damageVisual.canvasRenderer.SetAlpha(0.0f);
            damageVisual.gameObject.SetActive(true);
        
        }

        if (element) {
            
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

        if(Input.GetKeyDown(KeyCode.E)) {

            element.abilities[0].Use();

        }

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

    }

    private void UpdateUI() {

        healthText.text = "Health: " + ((int)health).ToString();

    }

}