using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameTiles : MonoBehaviour {
	public static GameTiles instance;
	public Tilemap Tilemap;
	public Dictionary<Vector3Int, WorldTile> tilesLocal = new Dictionary<Vector3Int, WorldTile>();
    public Dictionary<Vector3, WorldTile> tilesWorld = new Dictionary<Vector3, WorldTile>();
	private WorldTile tile;


	private void Awake() 
	{
		// CREATE INSTANCE
		if      (instance == null) { instance = this; }
		else if (instance != this) { Destroy(gameObject); }

		// FIND TERRAIN TILEMAP
		this.Tilemap = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Tilemap>();

		// SETUP
		GetWorldTiles();
	}

	// INITIALISE ALL TILES
	private void GetWorldTiles() 
	{
		foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin)
		{
			// SET LOCAL POSITION
			var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

			// APPLY VALUES
			if (!Tilemap.HasTile(localPlace)) continue;
			var tile = new WorldTile
			{
				posLocal = localPlace,
				posWorld = Tilemap.CellToWorld(localPlace),

				TileBase = Tilemap.GetTile(localPlace),
				Tile = Tilemap.GetTile<Tile>(localPlace),
				TilemapParent = Tilemap
			};

			// ADD TILE TO DICTIONARIES
			tilesLocal.Add(tile.posLocal, tile);
            tilesWorld.Add(tile.posWorld, tile);

		}
	}


    public WorldTile GetTileByLocalPos(Vector3Int v)
    {
        if (tilesLocal.TryGetValue(v, out tile)) 
		{
			return tile;
		}
        return null;
    }


    public WorldTile GetTileByWorldPos(Vector3 v)
    {
        foreach(var item in tilesWorld)
        {
            if(V3Equal(v, item.Value.posWorld))
            {
                return item.Value;
            }
        }
        
		// COULD NOT FIND TILE
		Debug.LogError("An Entity could not find a tile to inhabit!");
        return null;
    }

    public static bool V3Equal(Vector3 a, Vector3 b){
        return Vector3.SqrMagnitude(a - b) < 0.4;
    }


}