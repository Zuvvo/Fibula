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
    private string name;

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

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }
}
