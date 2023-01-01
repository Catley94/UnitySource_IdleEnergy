using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Breach3D/Create Energy", order = 1)]
public class SOEnergy : ScriptableObject
{
    public string _name;
    public int price;
    public int health;
    public int attack;
    public int attackSpeed;
    public int speed;
    public string type;
}