using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "ScriptableObjects/Element System/Element")]
public class Element : ScriptableObject {

    public string name;
    public Color color;
    public List<Ability> abilities = new List<Ability>();

}