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

    [SerializeField] private AudioClip wilhelmSound;
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
        isDead = true;
        AudioController.instance.PlayClip(AudioController.instance.wilhelm);
        InputController.instance.DisableInput();
        GameController.instance.Lose();
        loose.SetTrigger("Loose");
        death.SetBool("Dead",true);
        StartCoroutine(AudioController.instance.PlayClipDelayed(AudioController.instance.lose, 0.6f));
        StartCoroutine(DeathDelayed(0.6f));
    }

}
