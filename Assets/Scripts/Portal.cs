using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;

    Vector2 newPosition;

    void Start()
    {
        newPosition = new Vector2(-2, -2);
        ChangePosition();
    }

    void Update()
    {
        Weapon weapon = FindObjectOfType<Weapon>();

        Vector2 currentPosition = transform.position;

        if (Vector2.Distance(currentPosition, newPosition) < 0.5f)
        {
            ChangePosition();
            Debug.Log("New position chosen: " + newPosition);
        }

        if (weapon == null)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Weapon weapon = FindObjectOfType<Weapon>();
        if (other.gameObject.CompareTag("Player") && weapon != null)
        {
            Debug.Log("Player passed through the portal!");
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
        else
        {
            Debug.Log("Not a player");
        }
    }

    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
        speed = 0.4f;
        rotateSpeed = Random.Range(50f, 200f);
    }
}
