using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FibulaObject {

    public int hp { get; set; }
    public float moveSpeed { get; set; }
    public enum Rotation { Left, Right, Up, Down};
    public Rotation currentRotation;

    public int id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }
    public Enemy()
    {
        currentRotation = Rotation.Down;
    }
}
