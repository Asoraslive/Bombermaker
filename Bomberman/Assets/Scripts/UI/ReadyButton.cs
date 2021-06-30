using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class ReadyButton : MonoBehaviour
{
    public Tilemap tilemap;
    public GameTiles gametileScript;
    public GameObject[] prefabs;

    /// <summary>
    /// change wall order so objects cant be seen anymore
    ///
    /// </summary>
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

            List<Entity> tileInhabs = tile.GetInhabitants();
            //look for wall
            foreach(Entity listElement in tileInhabs)
            {
                DestructibleBox wall;
                //if is wall 
                if(listElement.CompareTag("DesWall"))
                {
                    wall = (DestructibleBox)listElement;
                    //get mates and add them to the spawnon death list delete after
                    List<Entity> mates = listElement.GetMates();
                    foreach(Entity mate in mates)
                    {
                        wall.spawnOnDeath.Add(findPrefabByTag(mate.tag));
                        mate.Death();
                    }
                    //change sorting order
                    wall.GetComponent<SpriteRenderer>().sortingOrder = 2;
                }            }
        }
    }

    private GameObject findPrefabByTag(string tag)
    {
        foreach(GameObject arrayElement in prefabs)
        {
            if(arrayElement.tag == tag)
            {
                return arrayElement;
            }
        }

        return null;
    }
}
