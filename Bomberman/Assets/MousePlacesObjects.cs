using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MousePlacesObjects : MonoBehaviour
{
    public GameTiles gametileScript;
    public Tilemap tilemap;
    public DestructibleBox destructible;
    public int LimitExit = 1;
    public int CurrentExit = 0;
    public int LimitPowerUp = 16;
    public int CurrentPowerUp = 0;
    public int LimitWall = 80;
    public int CurrentWall = 0;
    public int LimitTrap = 2;
    public int CurrentTrap = 0;
    public int LimitEnemy = 10;
    public int CurrentEnemy = 0;


    private GameObject placing;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            try
            {
                spawnObject(GetTileMouse());
                
            }
            catch(NullReferenceException e)
            {
                Debug.Log("Can't Place outside tilemap");
            }
        }
    }

    private WorldTile GetTileMouse()
    {
        //get right tile with mouse
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cell = tilemap.WorldToCell(pos);
        Vector3 centeredPos = tilemap.GetCellCenterWorld(cell);

        //get actual tile
        return  gametileScript.GetTileByWorldPos(centeredPos);
    }

    public void spawnObject(WorldTile tile)
    {
        //check if its a Indestructable Wall
        foreach (Entity inhab in tile.inhabitants)
        {
            if (inhab.CompareTag("Wall") || inhab.CompareTag("Win") || inhab.CompareTag("Trap"))
            {
                Debug.Log("Can't Place in a Wall");
                return;
            }
            else if(inhab == placing)
            {
                Debug.Log("There is an Object on this Tile already");
                return;
            }
            
        }
        //
        if (placing) 
        {
            foreach (Entity inhab in tile.inhabitants)
            {
                if (inhab.CompareTag(placing.tag))
                {
                    Debug.Log("Another Type of that Object cant sit on the same Tile");
                    return;
                }
            }
            //keep limits of gameobjects
            if (placing.CompareTag("Win"))
            {
                if(CurrentExit < LimitExit)
                {
                    Instantiate(placing, tile.posWorld, Quaternion.identity);
                    CurrentExit++;
                }
                else
                {
                    Debug.Log("Limit Reached");
                }
            }
            else if (placing.CompareTag("PowerUp"))
            {
                if (CurrentPowerUp < LimitPowerUp)
                {
                    Instantiate(placing, tile.posWorld, Quaternion.identity);
                    CurrentPowerUp++;
                }
                else
                {
                    Debug.Log("Limit Reached");
                }
            }
            else if (placing.CompareTag("Trap"))
            {
                if (CurrentTrap < LimitTrap)
                {
                    Instantiate(placing, tile.posWorld, Quaternion.identity);
                    CurrentTrap++;
                }
                else
                {
                    Debug.Log("Limit Reached");
                }
            }
            else if (placing.CompareTag("Enemy"))
            {
                if (CurrentEnemy < LimitEnemy)
                {
                    Instantiate(placing, tile.posWorld, Quaternion.identity);
                    CurrentEnemy++;
                }
                else
                {
                    Debug.Log("Limit Reached");
                }
            }
            else if (placing.CompareTag("DesWall"))
            {
                if (CurrentWall < LimitWall)
                {
                    Instantiate(placing, tile.posWorld, Quaternion.identity);
                    CurrentWall++;
                }
                else
                {
                    Debug.Log("Limit Reached");
                }
            }
        }
        //unselected Placing
        else
        {
            Debug.Log("No Placement selected");
        }
    }

    public void SetPlacingObject(GameObject obj)
    {
        placing = obj;
    }
    public void UnsetPlacingObject()
    {
        placing = null;
    }

    
}
