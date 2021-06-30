using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Entity
{
    [SerializeField] private AudioClip victorySound;
    protected override void Awake() 
    {
        // CALL ENTITY'S AWAKE FUNCTION
        base.Awake();
        
        // SET OBSTACLE
        this.isObstacle = false;
        this.isDestructible = false;
    }

    public override void OnMateEnter(Entity e)
    {
        if(e is Player)
        {
            AudioSource.PlayClipAtPoint(victorySound, Camera.main.transform.position);
            GameController.instance.Win();
        }
        base.OnMateEnter(e);
    }
}
