using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour {

    public static Player instance;

    private void Awake() {
        
        if(instance == null) { instance = this; }

    }

    [Header("Health System")]
    public float health = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private bool isAlive;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private AudioSource damageAudio;
    [SerializeField] private GameObject damageVisual;

    [Header("Regeneration")]
    [SerializeField] private float regeneration;
    [SerializeField] private bool canRegenerate;

    [Header("Movement")]
    public bool canMove = true;
    public bool canLook = true;

    [Header("Scripts")]
    public PlayerMovement movement;
    public PlayerLook look;

    private void Start() {

        damageVisual.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        damageVisual.SetActive(true);

        UpdateUI();

    }

    private void Update() {
        
        if(canRegenerate) { Heal(regeneration * 0.01f); }
        if(Input.GetKeyDown(KeyCode.KeypadPlus)) { Heal(10.0f); }
        if(Input.GetKeyDown(KeyCode.KeypadMinus)) { Damage(15.0f); }

    }

    public void Damage(float amount) {

        if(health - amount <= 0) { Die(); return; }
        health -= amount;

        if (damageAudio) damageAudio.Play();
        if (damageVisual) {

            damageVisual.GetComponent<Image>().canvasRenderer.SetAlpha(0.55f);
            damageVisual.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.5f, false);

        }

        UpdateUI();

    }

    public void Heal(float amount) {

        if(health + amount >= maxHealth) { health = maxHealth; UpdateUI(); return; }
        health += amount;



        UpdateUI();

    }

    private void Die() {

        health = 0;

        isAlive = false;
        canRegenerate = false;

        canMove = false;
        canLook = false;

        UpdateUI();

    }

    private void UpdateUI() {

        healthText.text = "Health: " + (int)health;

    }

}
