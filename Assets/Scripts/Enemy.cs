using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject projectile;
    public float projectileSpeed = 20f;
    public float health = 300;
    public float firingRate = 2f;
    public float sps = 0.5f;
    public float explosionVol = 10;
    public int scoreValue = 125;
    public AudioClip death;
    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();

    }

    private void Update()
    {
        float prob = 0.5f * Time.deltaTime;
        if (Random.value < prob)
        {
            Fire();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if(missile)
        {
            health = health - missile.GetDamage();
            missile.Hit();
            Debug.Log(health);
            if(health <= 0)
            {
                AudioSource.PlayClipAtPoint(death, transform.position, explosionVol);
                Destroy(gameObject);
                scoreKeeper.Score(scoreValue);
            }
        }
    }

    private void Fire()
    {
        Vector3 startPos = transform.position + new Vector3(0, -1, 0);
        GameObject beam = Instantiate(projectile, startPos, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed);
    }

}
