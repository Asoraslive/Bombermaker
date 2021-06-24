using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Entity
{
    protected override void Awake() 
    {
        // CALL ENTITY'S AWAKE FUNCTION
        base.Awake();
        
        // SET OBSTACLE
        this.isObstacle = true;
    }
}
