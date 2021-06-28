using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class ReadyButton : MonoBehaviour
{
    public Tilemap tilemap;
    public GameTiles gametileScript;


    public void changeOrderDesWall()
    {
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(position))
            {
                continue;
            }

            // Tile is not empty; do stuff
            WorldTile tile = gametileScript.GetTileByLocalPos(position);
            foreach(Entity inhab in tile.inhabitants)
            {
                if (inhab.CompareTag("DesWall"))
                {
                    SpriteRenderer render = inhab.GetComponent<SpriteRenderer>();
                    render.sortingOrder = 2;
                    Debug.Log("Changed Wall @ :" + position);
                }
            }
        }
    }
}
