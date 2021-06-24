using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    #pragma warning disable 0649
    public GameObject bombPrefab;
    #pragma warning restore 0649

    protected override void Awake() 
    {
        //CALL ENTITY'S AWAKE FUNCTION
        base.Awake();

        this.isDestructible = true;
        this.isFreezable = true;
    }

    public void SpawnBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, inhabitedTile.posWorld, Quaternion.identity);
    }

    public override void Death()
    {
        InputController.instance.DisableInput();
        GameController.instance.Lose();
        base.Death();
    }
}
