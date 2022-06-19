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

    [Header("Bools")]
    public bool isAlive = true;
    public bool canMove = true;
    public bool canLook = true;
    public bool canRegenerate = true;

    [Header("Scripts")]
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerLook look;

    private void Start() {
        
        if(damageVisual) { damageVisual.canvasRenderer.SetAlpha(0.0f); damageVisual.gameObject.SetActive(true); }

    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.KeypadPlus)) Heal(10.0f);
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) Damage(15.0f);
        if (canRegenerate) Heal(regenerateAmount * 0.01f);

    }

    //-----------------------------
    //--------Health System--------
    //-----------------------------
    public void Damage(float amount) {

        if (!isAlive) return;
        if (health - amount <= 0.0f) { health = 0.0f; UpdateUI(); Die(); return; }
        if (damageVisual) { damageVisual.canvasRenderer.SetAlpha(0.7f); damageVisual.CrossFadeAlpha(0.0f, 0.5f, false); }
        if (damageAudio) damageAudio.Play();

        health -= amount;
        UpdateUI();

    }
    public void Heal(float amount) {

        if (health >= maxHealth || !isAlive) return;
        if (health + amount >= maxHealth) { health = maxHealth; UpdateUI(); return; }

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