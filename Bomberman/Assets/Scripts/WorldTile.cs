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
        return null;
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