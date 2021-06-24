using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldTile {
    public Vector3Int posLocal;
    public Vector3 posWorld;
    public List<Entity> inhabitants = new List<Entity>();

    public TileBase TileBase;
    public Tile Tile;
    public Tilemap TilemapParent;
    

    public void AddInhabitant(Entity ent)
    {
        NotifyNewInhabitant(ent);
        inhabitants.Add(ent);
    }

    public void RemoveInhabitant(Entity ent)
    {
        inhabitants.Remove(ent);
    }

    public void NotifyNewInhabitant(Entity newInhabitant)
    {
        foreach(Entity e in inhabitants){ e.OnMateEnter(newInhabitant); }
    }

    public List<Entity> GetInhabitants()
    {
        List<Entity> returnInhabs = new List<Entity>();
        foreach(Entity inhab in inhabitants)
        {
            if (inhab.gameObject.activeSelf){ returnInhabs.Add(inhab); }
        }
        return returnInhabs;
    }

    public override string ToString()
    {
        return "Local" + posLocal.ToString() + " | | World" + posWorld.ToString() + "";
    }

    public WorldTile FindNeighbourTileByOffset(Vector3Int offset)
    {
        Vector3Int targetPos = posLocal + offset;
        if(GameTiles.instance.tilesLocal.ContainsKey(targetPos))
        {
            return GameTiles.instance.tilesLocal[targetPos];
        }

        return null;
    }

    public List<WorldTile> FindNeighbourTilesInRadius(int radius)
    {
        List<WorldTile> neighbours = new List<WorldTile>();
        int range = 1 + 2 * radius;
        int x_tile = posLocal.x - radius;
        int y_tile = posLocal.y - radius;
        for (int x = 0; x < range; x++)
        {
            for (int y = 0; y < range; y++)
            {
                Vector3Int targetTile = new Vector3Int(x + x_tile,y + y_tile,0);
                if (GameTiles.instance.tilesLocal.ContainsKey(targetTile))
                {
                    neighbours.Add(GameTiles.instance.tilesLocal[targetTile]);
                }
            }
        }

        return neighbours;
    }

    public void DebugTile()
    {
        TilemapParent.SetTile(posLocal, null);
    }

    public bool IsBlocked()
    {
        foreach(Entity e in inhabitants)
        {
            if(e.isObstacle){ return true; }
        }
        return false;
    }

    public bool IsHardBlocked()
    {
        foreach(Entity e in inhabitants)
        {
            if(e is Wall){ return true; }
        }
        return false;
    }
}