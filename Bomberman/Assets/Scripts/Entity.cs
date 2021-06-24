using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public WorldTile inhabitedTile;
    public bool isObstacle = false;
    public bool isMoving = false;
    public bool isDestructible = false;
    public bool isFreezable = false;
    public bool isFrozen = false;
    public float speed = 1f;


    protected virtual void Awake() 
    {
        // FIND STARTING TILE, SET INHABITED TILE
        WorldTile startingTile = GameTiles.instance.GetTileByWorldPos(this.gameObject.transform.position);
        Inhabit(startingTile);
        // startingTile.DebugTile();
        this.transform.position = startingTile.posWorld;
    }

    public void Inhabit(WorldTile tile)
    {
        // DISINHABITE OLD TILE
        Disinhabit();

        // INHABIT NEW TILE
        inhabitedTile = tile;
        tile.AddInhabitant(this);
    }

    public void Disinhabit()
    {
        if (inhabitedTile != null){ inhabitedTile.RemoveInhabitant(this); }
    }

    public virtual void Death()
    {
        Disinhabit();
        Destroy(this.gameObject);
    }


    public List<Entity> GetMates() // Mates are entities on the same tile
    {
        List<Entity> mates = new List<Entity>(this.inhabitedTile.inhabitants);
        if (mates.Contains(this)) { mates.Remove(this); }
        return mates;
    }

    public void Move(Vector3Int v)
    {
        WorldTile destinationTile = inhabitedTile.FindNeighbourTileByOffset(v);

        // EXIT IF THERE IS NO TILE TO MOVE TO
        if(destinationTile == null){ Debug.Log("No tile to move to"); return;}

        // EXIT IF THERE IS AN OBSTACLE
        if(destinationTile.IsBlocked()){ Debug.Log("Destination Tile is blocked"); return; }

        // EXIT IF ENTITY IS FROZEN
        if(isFrozen){ return; }

        StartCoroutine(MoveRoutine(destinationTile));
    }

    public IEnumerator MoveRoutine(WorldTile destinationTile)
    {
        // DISABLE INPUT
        isMoving = true;
        InputController.instance.DisableInput();

        // EXECUTE MOVEMENT (LERP)
        var startPosition = this.transform.position;
        var endPosition = destinationTile.posWorld;
        float lerpTime = 1f;
        float t = 0f;

        while (t < 1f) 
        {
            // LERP
            t += Time.deltaTime * this.speed;
            if (t > lerpTime) { t = lerpTime; }
            this.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            // HALT IF FROZEN
            while(isFrozen){ yield return null; }
            yield return null;
        }

        // SET NEW INHABITED TILE
        Inhabit(destinationTile);

        // ENABLE INPUT
        InputController.instance.EnableInput();
        yield return null;
    }

    public virtual void OnMateEnter(Entity e)
    {

    }

    public void Freeze(float duration)
    {
        StartCoroutine(FreezeRoutine(duration));
    }

    public IEnumerator FreezeRoutine(float duration)
    {
        isFrozen = true;
        this.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.72f, 0.78f, 1f);

        yield return new WaitForSeconds(duration);

        isFrozen = false;
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
}
