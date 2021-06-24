using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController instance;
    private bool playerInputEnabled = true;
    private Player player;

    private void Awake() 
	{
		// MAKE INSTANCE
		if      (instance == null) { instance = this; }
		else if (instance != this) { Destroy(gameObject); }
    }

    private void Start() 
    {
        // FIND PLAYER
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.GetComponent<Player>();
    }

    public void Update()
    {
        if(playerInputEnabled)
        {
            // MOVEMENT
            if (Input.GetButtonDown("W")){  player.Move(new Vector3Int(0, 1, 0));  }
            if (Input.GetButtonDown("S")){  player.Move(new Vector3Int(0, -1, 0));  }
            if (Input.GetButtonDown("A")){  player.Move(new Vector3Int(-1, 0, 0));  }
            if (Input.GetButtonDown("D")){  player.Move(new Vector3Int(1, 0, 0));  }

            // SPAWN BOMB
            if (Input.GetButtonDown("PlantBomb")){  player.SpawnBomb();  }
        }

    }

    public void DisableInput()
    {
        playerInputEnabled = false;
    }

    public void EnableInput()
    {
        playerInputEnabled = true;
    }
}
