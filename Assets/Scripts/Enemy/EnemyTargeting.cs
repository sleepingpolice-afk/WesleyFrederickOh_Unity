using UnityEngine;

public class EnemyTargeting : Enemy
{
    private float speed = 2f;

    private float CameraTop;
    private float CameraBottom;
    private float CameraRight;
    private float CameraLeft;

    private Transform player;

    private void Start()
    {
        // Spawn musuh secara random di kiri atau kanan layar
        CameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;
        CameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        CameraRight = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        CameraLeft = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;

        player = GameObject.Find("Player").transform;
        Vector3 spawn;
        if(Random.value < 0.5f)
        {
            spawn = new Vector3(CameraLeft, Random.Range(CameraBottom, CameraTop), transform.position.z);
        }
        else
        {
            spawn = new Vector3(CameraRight, Random.Range(CameraBottom, CameraTop), transform.position.z);
        }

        transform.position = spawn;
    }

    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UI pointtracker = FindObjectOfType<UI>();
            pointtracker.points -= level;
            Destroy(gameObject);
        }
    }
}