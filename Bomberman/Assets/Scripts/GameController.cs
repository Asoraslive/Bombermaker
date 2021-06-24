using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Player player;

    // UPGRADES
    public bool useUpgrades = true;
    public static string upgrade_bomb_strength = "BombStrength";
    public static string upgrade_move_speed = "MoveSpeed";
    public static string upgrade_bomb_range = "BombRange";

    public int currentBombStrength;
    public float currentPlayerSpeed;
    public int currentBombRange;

    public int initialBombStrength = 1;
    public float initialPlayerSpeed = 4;
    public int initialBombRange = 1;

    private void Awake() 
	{
		// MAKE INSTANCE
		if      (instance == null) { instance = this; }
		else if (instance != this) { Destroy(gameObject); }

        // SET INITIAL VALUES
        ResetUpgrades();
    }

    private void Start() 
    {
        // FIND PLAYER
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void ResetUpgrades()
    {
        currentBombRange = initialBombRange;
        currentBombStrength = initialBombStrength;
        currentPlayerSpeed = initialPlayerSpeed;
    }

    private void ApplyUpgrades()
    {
        if(!useUpgrades){ return; }
        player.speed = currentPlayerSpeed;
    }

    public void Upgrade(string type)
    {
        if      (type == upgrade_bomb_range){ currentBombRange += 1; }
        else if (type == upgrade_bomb_strength){ currentBombStrength += 1; }
        else if (type == upgrade_move_speed){ currentPlayerSpeed *= 1.5f; }
        ApplyUpgrades();
    }
    
    public void Win()
    {
        Debug.Log("You Win!");
    }

    public void Lose()
    {
        Debug.Log("You Lose!");
    }
}
