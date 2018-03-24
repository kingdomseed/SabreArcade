using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }
}
