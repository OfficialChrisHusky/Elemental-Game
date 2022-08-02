using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ElementSelection : MonoBehaviour
{
    public bool isOpen;
    public GameObject buttonContainer;
    public Button fireButton;
    public Button airButton;
    public Button earthButton;
    public Button waterButton;

    private PlayerElementalPower powers;
    
    // Start is called before the first frame update
    void Start()
    {
        powers = GetComponent<PlayerElementalPower>();
        fireButton.onClick.AddListener(() => {
            powers.selectElement(PlayerElementalPower.Element.Fire);
            openClose();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            openClose();
        }
    }

    private void openClose()
    {
        isOpen = !isOpen;
        buttonContainer.SetActive(isOpen);
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpen;
    }
    
}
