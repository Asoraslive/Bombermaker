using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    public float delay = 0.4f;

    private void Start() 
    {
        StartCoroutine(DespawnAfterDelay());
    }

    public IEnumerator DespawnAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
        yield return null;
    }
}
