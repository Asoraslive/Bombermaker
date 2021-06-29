using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : Entity
{
    public float explodeDelay = 2f;
    public int explodeStrength = 1;
    public int explodeRange = 1;
    public List<Vector3Int> directions = new List<Vector3Int>();

    #pragma warning disable 0649
    public GameObject explosionPrefab;
    #pragma warning restore 0649


    protected override void Awake() 
    {
        // CALL ENTITY'S AWAKE FUNCTION
        base.Awake();
        
        // SET OBSTACLE
        this.isObstacle = true;
        this.isFreezable = true;

        // ADD DIRECTIONS
        directions.Add(new Vector3Int(0,1,0)); // N, 0
        directions.Add(new Vector3Int(1,0,0)); // E, 1
        directions.Add(new Vector3Int(0,-1,0)); // S, 2
        directions.Add(new Vector3Int(-1,0,0)); // W, 3

        // OVERRIDE VALUES IF UPGRADES ARE USED
        if(GameController.instance.useUpgrades)
        {
            explodeStrength = GameController.instance.currentBombStrength;
            explodeRange = GameController.instance.currentBombRange;
        }

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

        // FREEZE DELAY
        while(isFrozen){ yield return null; }

        // PREPARE VARIABLES
        int i_range = 1;
        this.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        List<Entity> destroyees = new List<Entity>();

        // INNER EXPLOSION
        GameObject explosionInner = Instantiate(explosionPrefab, inhabitedTile.posWorld, Quaternion.identity);
        foreach(Entity e in inhabitedTile.inhabitants)
        {
            if(e.isDestructible && e != this){destroyees.Add(e);}
        }

        // OUTER EXPLOSIONS
        int[] hits = { 0, 0, 0, 0}; // N E S W
        while(i_range <= explodeRange)
        {
            foreach(Vector3Int dir in directions)
            {
                // FIND TILE
                WorldTile targetTile = inhabitedTile.FindNeighbourTileByOffset(dir * i_range);

                // CHECK IF THERE IS A WALL
                if(targetTile != null && targetTile.IsHardBlocked())
                {
                    if      (dir == directions[0]){ hits[0] = explodeStrength; }
                    else if (dir == directions[1]){ hits[1] = explodeStrength; }
                    else if (dir == directions[2]){ hits[2] = explodeStrength; }
                    else if (dir == directions[3]){ hits[3] = explodeStrength; }
                }

                // SKIP IF EXPLODE STRENGTH IS EXHAUSTED
                if      (dir == directions[0] && hits[0] >= explodeStrength){ continue; }
                else if (dir == directions[1] && hits[1] >= explodeStrength){ continue; }
                else if (dir == directions[2] && hits[2] >= explodeStrength){ continue; }
                else if (dir == directions[3] && hits[3] >= explodeStrength){ continue; }

                if(targetTile != null)
                {
                    // SPAWN EXPLOSION PREFAB
                    GameObject explosion = Instantiate(explosionPrefab, targetTile.posWorld, Quaternion.identity);

                    // KILL INHABITANTS
                    bool hasHit = false;
                    foreach(Entity e in targetTile.inhabitants)
                    {
                        if(e.isDestructible)
                        {
                            destroyees.Add(e);
                            if(e is DestructibleBox) hasHit = true; 
                        }
                    }

                    // INCREMENT HITS 
                    if(hasHit)
                    {
                        if      (dir == directions[0]){ hits[0]++; }
                        else if (dir == directions[1]){ hits[1]++; }
                        else if (dir == directions[2]){ hits[2]++; }
                        else if (dir == directions[3]){ hits[3]++; }
                    }
                }
            }
            i_range++;
        }

        for(int i = destroyees.Count; i > 0; i--)
        {
            destroyees[i-1].Death();
        }

        // DESTROY BOMB OBJECT
        Death();
        yield return null;
    }
}
