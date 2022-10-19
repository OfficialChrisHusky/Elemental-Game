using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComboSystem : MonoBehaviour
{

    public Text debugText;
    private string buffer = ""; //to accumulate the various input of the current combo, if the timing is wrong this would be cleared
    private Tuple<string,Action> spells; //bucket where put correct combo and their association to the method that would invoke the spell
    private float timing = 0.3f; //threshold to continue combo
    private float offset = 0.2f;
    private float currentTime = -1;
    private bool notRemove = false;

    private PlayerElementalPower elementalPowers;
    // Start is called before the first frame update
    void Start()
    {
        elementalPowers = this.GetComponent<PlayerElementalPower>();
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = buffer;
        var input = Input.inputString;

        if (!string.IsNullOrEmpty(input))
        {
            if (input.Contains("w") || input.Contains("a") || input.Contains("d") || input.Contains("s")) return;

            if (currentTime == -1)
            {
                currentTime = Time.time;
                buffer = "";
                buffer += input;
            }
            else
            {
                var t = Time.time;
                if(t - currentTime < timing && t -currentTime > timing-offset)
                {
                    currentTime = t;
                    buffer += input;

                    if(buffer.Length == 4)
                    {
                        checkCombo();
                    }
                }
            }
        }
        else
        {
            if (!notRemove)
            {
                var t = Time.time;
                if (t - currentTime > timing)
                {
                    currentTime = -1;
                    buffer = "";
                }
            }
        }
    }

    private void checkCombo()
    {

        switch(buffer)
        {
            case "rtyu":
                notRemove = true;
                buffer = "";
                elementalPowers.throwFireball();
                StartCoroutine(wait());
                break;
            case "rtyh":
                notRemove = true;
                buffer = "FireAbility";
                StartCoroutine(wait());
                break;

        }

    }

    IEnumerator wait()
    {

        yield return new WaitForSeconds(2);
        notRemove = false;

    }
}
