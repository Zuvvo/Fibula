using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public int hp;
    public int mana;
    private Vector3 position;
    public enum Classes { Knight, Paladin, Sorcerer, Druid };
    public Classes PlayerClass;

    public string Name
    {
        get
        {
            return Name;
        }
        set
        {
            if (value.Length > 0 && value.Length < 15)
            {
                Name = value;
            }
            else throw new ArgumentOutOfRangeException("Name must be between 0 and 15 characters length.");
        }
    }

    public Player()
    {
        PlayerClass = Classes.Knight;
        hp = 100;
        mana = 100;
    }
}
