using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

    [SerializeField] public float sensitivity;
    [SerializeField] private Transform body;

    float xRot;
    float yRot;

    private void Update() {

        if (!Player.instance.canLook) return;

        yRot += Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensitivity;
        xRot -= Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensitivity;

        xRot = Mathf.Clamp(xRot, -90.0f, 90.0f);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0.0f);
        body.rotation = Quaternion.Euler(0.0f, yRot, 0.0f);

    }

}
