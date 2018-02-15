using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Spider : Enemy {

    public float timer = 0;

    public Spider()
    {
        Name = "spider";
        moveSpeed = 2;
        hp = 100;
        Id = 0;
    }


    public void SetPosition(FibulaPosition position)
    {
      //  this.Position = position;
    }
}
