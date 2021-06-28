using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FreezeTrap : Entity
{
    public float explodeDelay = 2f;
    public int explodeRadius = 2;
    public float freezeDuration = 3f;

    #pragma warning disable 0649
    public GameObject freezePrefab;
    #pragma warning restore 0649


    protected override void Awake() 
    {
        // CALL ENTITY'S AWAKE FUNCTION
        base.Awake();
        
        // SET OBSTACLE
        this.isObstacle = true;

        // INITIATE EXPLODE SEQUENCE

            Explode();
        
    }



    public void Explode()
    {
        StartCoroutine(ExplodeRoutine());
    }

    public IEnumerator ExplodeRoutine()
    {
        // EXPLODE DELAY
        yield return new WaitForSeconds(explodeDelay);

        // PREPARE VARIABLES
        this.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        List<WorldTile> freezees = inhabitedTile.FindNeighbourTilesInRadius(explodeRadius);

        // FREEZE
        foreach(WorldTile t in freezees)
        {
            Instantiate(freezePrefab, t.posWorld, Quaternion.identity);
            foreach(Entity e in t.inhabitants)
            {
                if(e.isFreezable){e.Freeze(freezeDuration);}
            }
        }

        // DESTROY TRAP OBJECT
        Death();
        yield return null;
    }
}
