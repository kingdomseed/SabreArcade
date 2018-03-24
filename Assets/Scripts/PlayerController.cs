using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed = 0.1f;
    public float padding = 1f;
    public GameObject projectile;
    public float firingRate = 0.5f;
    public float health = 1000;
    public float projectileSpeed = 50f;
    public AudioClip death;
    public float explosionVol = 10;
    public Text text;
    private float minX;
    private float maxX;
    private Vector3 leftMost;
    private Vector3 rightMost;
    private float mouseInput;
    private bool left = true;

    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        speed *= Time.deltaTime;
        minX = leftMost.x + padding;
        maxX = rightMost.x - padding;
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.0001f, firingRate);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            InvokeRepeating("Fire", 0.0001f, firingRate);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("Fire");
        }
        MoveWithKeys();
        MoveWithMouse();
        restrictMove();
    }

    void MoveWithKeys()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed;
        } else if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed;
        }
        
    }

    void MoveWithMouse()
    {
        mouseInput = Input.GetAxis("Mouse X") * speed;
        transform.position += new Vector3(mouseInput, 0f, 0f);
        
    }

    void restrictMove()
    {
        float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void Fire()
    {
        if (left)
        {
            Vector3 startPosR = transform.position + new Vector3(0.46f, 1.01f, 0);
            GameObject beam = Instantiate(projectile, startPosR, Quaternion.identity) as GameObject;
            beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed);
            left = false;
        }
        else
        {
            Vector3 startPosL = transform.position + new Vector3(-0.46f, 1f, 0);
            GameObject beam = Instantiate(projectile, startPosL, Quaternion.identity) as GameObject;
            beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed);
            left = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health = health - missile.GetDamage();
            missile.Hit();
            text.text = health.ToString();
            if (health <= 0)
            {
                AudioSource.PlayClipAtPoint(death, transform.position, explosionVol);
                Destroy(gameObject);
                LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
                man.LoadLevel("Game Over");
            }
        }
    }

}
