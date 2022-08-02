using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    public float life = 100f;
    public Slider healthBar;
    public Canvas canvas;
    public Camera mainCamera;
    
    private float perpetualDamageQuantity;
    private float perpetualDamageTime;

    private Animator damageAnimation;


    // Start is called before the first frame update
    void Start() {
        damageAnimation = GetComponent<Animator>();
        healthBar.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //first you need the RectTransform component of your canvas
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        
        Vector2 viewportPosition=mainCamera.WorldToViewportPoint(transform.position + transform.up * 3);

        var sizeDelta = canvasRect.sizeDelta;
        var worldObjectScreenPosition = new Vector2(((viewportPosition.x*sizeDelta.x)-(sizeDelta.x*0.5f)), ((viewportPosition.y*sizeDelta.y)-(sizeDelta.y*0.5f)));
        healthBar.GetComponent<RectTransform>().anchoredPosition = worldObjectScreenPosition;
        healthBar.value = life / 100f;
    }

    public void damage(float damageQuantity)
    {
        damageAnimation.SetTrigger("Active");
        if (life - damageQuantity > 0) {
            life -= damageQuantity;
        }
        else {
            life = 0;
            Destroy(healthBar.gameObject);
            Destroy(gameObject);
        }
        
    }
    
}
