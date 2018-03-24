using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float spawnDelay = 0.5f;
    public float width = 15;
    public float height = 5;
    public float speed = 0.1f;
    public float padding = 1f;
    private bool moveRight = true;
    private float minX;
    private float maxX;
    private Vector3 leftMost;
    private Vector3 rightMost;

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    void Start ()
    {
        SetCamera();
        SpawnUntilFull();
    }
    void Update()
    {

        if (moveRight)
        {
            transform.position += Vector3.right * (speed * Time.deltaTime);
        }
        else
        {
            transform.position += Vector3.left * (speed * Time.deltaTime);
        }
        float rEdgeOfForm = transform.position.x + (0.5f * width);
        float lEdgeOfForm = transform.position.x - (0.5f * width);
        if (lEdgeOfForm < minX)
        {
            moveRight = true;
        }
        else if (rEdgeOfForm > maxX)
        {
            moveRight = false;
        }
        if (AllMembersDead())
        {
            SpawnUntilFull();
        }
    }

    private void SetCamera()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        minX = leftMost.x + padding;
        maxX = rightMost.x - padding;
    }

    private void SpawnUntilFull()
    {
        Transform freePos = NextFreePosition();
        if(freePos)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePos.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePos;
        }
        if(NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay); //Recursion!!!!!! Wooooo!!!! I used recursion!!!!
        }
    }

    private Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0  )
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    private bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if(childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

}
