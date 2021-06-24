using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBox : Entity
{
    public List<GameObject> spawnOnDeath = new List<GameObject>();

    protected override void Awake() 
    {
        // CALL ENTITY'S AWAKE FUNCTION
        base.Awake();
        
        // SET OBSTACLE
        this.isObstacle = true;
        this.isDestructible = true;
    }

    public override void Death()
    {
        foreach(GameObject g in spawnOnDeath){ Instantiate(g, inhabitedTile.posWorld, Quaternion.identity); }
        base.Death();
    }
}
