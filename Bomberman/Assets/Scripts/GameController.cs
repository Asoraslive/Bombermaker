using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake() 
	{
		// MAKE INSTANCE
		if      (instance == null) { instance = this; }
		else if (instance != this) { Destroy(gameObject); }
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
