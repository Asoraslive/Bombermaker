using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : Entity
{
    public string type;
    [SerializeField] private AudioClip upgradeSound;

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
            AudioController.instance.PlayClip(AudioController.instance.powerup);
            GameController.instance.Upgrade(type);
            StartCoroutine(this.DeathDelayed(0.05f));
        }
        base.OnMateEnter(e);
    }
}
