using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Ability {

    public override void Use() {

        GameObject instance = Instantiate(gameObject, Player.instance.cam.transform);

        instance.transform.localPosition = new Vector3(0.15f, -0.1f, 0.35f);

    }

}