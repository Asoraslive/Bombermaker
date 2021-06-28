using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Coroutine moveRoutine;
    public float startDelay = 1f;
    List<Vector3Int> directions = new List<Vector3Int>();
    public Animator death;
    protected override void Awake() 
    {
        // CALL ENTITY'S AWAKE FUNCTION
        base.Awake();

        // SET PARAMETERS
        this.isDestructible = true;
        this.isFreezable = true;

        // SET DIRECTIONS
        directions.Add(new Vector3Int(0,1,0)); // N, 0
        directions.Add(new Vector3Int(1,0,0)); // E, 1
        directions.Add(new Vector3Int(0,-1,0)); // S, 2
        directions.Add(new Vector3Int(-1,0,0)); // W, 3

        // START MOVE ROUTINE
        moveRoutine = StartCoroutine(MoveRoutine());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Death();
        }
    }

    private IEnumerator MoveRoutine()
    {
        yield return new WaitForSeconds(startDelay);
        Vector3Int dirPrev = new Vector3Int(0,0,0);
        Vector3Int dir;
        while(true)
        {
            if(!isMoving)
            {
                dir = directions[Random.Range(0, 4)];

                if(dir == dirPrev)
                {
                    if(Random.Range(0f, 1f) < 0.98f){ continue; }
                }

                Move(dir);
                dirPrev = dir * -1;
                yield return null;
            }

            yield return null;
        }
    }

    public override void Death()
    {
        StopCoroutine(moveRoutine);
        //base.Death();
        death.SetBool("Dead", true);
        StartCoroutine(DeathDelayed(0.9f));
    }



}
