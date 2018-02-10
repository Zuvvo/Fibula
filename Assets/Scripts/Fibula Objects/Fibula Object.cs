using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct FibulaPosition
{
    public int x;
    public int y;
}

public class FibulaObject {

    public FibulaPosition position;
    private int id;

    public int Id
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
}
