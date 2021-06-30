using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    #pragma warning disable 0649
    public GameObject bombPrefab;
#pragma warning restore 0649

    public Animator loose;
    public Animator death;

    [SerializeField] private AudioClip loseSound;

    protected override void Awake() 
    {
        //CALL ENTITY'S AWAKE FUNCTION
        base.Awake();

        this.isDestructible = true;
        this.isFreezable = true;
    }

    public void SpawnBomb()
    {
        Debug.Log(inhabitedTile.posWorld);
        GameObject bomb = Instantiate(bombPrefab, inhabitedTile.posWorld, Quaternion.identity);
    }

    public override void Death()
    {
        InputController.instance.DisableInput();
        GameController.instance.Lose();
        loose.SetTrigger("Loose");
        death.SetBool("Dead",true);
        AudioSource.PlayClipAtPoint(loseSound, Camera.main.transform.position);
        //base.Death();
        StartCoroutine(DeathDelayed(0.6f));
    }
}
