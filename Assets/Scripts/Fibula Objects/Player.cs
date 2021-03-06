﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FibulaPlayer : FibulaObject {

    public int hp { get; set; }
    public int mana { get; set; }
    public int speed;

    public enum Classes { Warrior, Mage, Druid, Shaman, Rogue};
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

    public FibulaPlayer()
    {
        PlayerClass = Classes.Warrior;
        hp = 100;
        mana = 100;
        speed = 5;
    }

}
