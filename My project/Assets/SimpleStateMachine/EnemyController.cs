using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        Pace,
        Follow,
    }

    [SerializeField]
    GameObject[] route;
    GameObject target;
    int routeIndex = 0;
    float speed = 3.0f;
    float followTime = 5.0f;
    float followTimer = 0f;

    private State currentState = State.Pace;
    Animator anim;

    public int health = 3;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Pace:
                OnPace();
                break;
            case State.Follow:
                OnFollow();
                break;
        }
    }

    void OnPace()
    {
        if (routeIndex < 0 || routeIndex >= route.Length)
        {
            routeIndex = 0;
        }

        target = route[routeIndex];
        MoveTo(target);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            routeIndex++;
            if (routeIndex >= route.Length)
            {
                routeIndex = 0;
            }
        }

        GameObject detectedPlayer = CheckForPlayer();
        if (detectedPlayer != null)
        {
            target = detectedPlayer;
            currentState = State.Follow;
            followTimer = 0f;
        }
    }

    void OnFollow()
    {
        MoveTo(target);
        followTimer += Time.deltaTime;

        GameObject detectedPlayer = CheckForPlayer();
        if (detectedPlayer == null && followTimer > followTime)
        {
            currentState = State.Pace;
        }
    }

    void MoveTo(GameObject t)
    {
        transform.position = Vector3.MoveTowards(transform.position, t.transform.position, speed * Time.deltaTime);
        transform.LookAt(t.transform, Vector3.up);
    }

    GameObject CheckForPlayer()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 1.0f;
        Debug.DrawRay(rayOrigin, transform.forward * 10, Color.green);

        if (Physics.Raycast(rayOrigin, transform.forward, out hit, 10))
        {
            FirstPersonController player = hit.transform.gameObject.GetComponent<FirstPersonController>();
            if (player != null)
            {
                return hit.transform.gameObject;
            }
        }
        return null;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<FirstPersonController>() != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
