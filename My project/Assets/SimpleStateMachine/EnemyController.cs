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
    GameObject[] route;  // Waypoints for pacing
    GameObject target;    // Target to follow (either player or waypoint)
    int routeIndex = 0;   // Index for the waypoints
    float speed = 3.0f;   // Speed of the enemy movement
    float followTime = 5.0f; // Time to follow player before switching back to path
    float followTimer = 0f;  // Timer to keep track of follow state

    private State currentState = State.Pace;  // Start by pacing (following waypoints)

    void Start()
    {

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
        print("I'm pacing");

        if (routeIndex < 0 || routeIndex >= route.Length)
        {
            routeIndex = 0;  // Reset to the first waypoint if out of bounds
        }

        target = route[routeIndex];  // Set the current waypoint as the target
        MoveTo(target);

        // If the enemy reaches the waypoint, move to the next one
        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            routeIndex++;
            if (routeIndex >= route.Length)
            {
                routeIndex = 0; 
            }
        }

        // Check if the player is detected in front of the enemy (using raycasting)
        GameObject detectedPlayer = CheckForPlayer();
        if (detectedPlayer != null)
        {
            target = detectedPlayer; 
            currentState = State.Follow;  
            followTimer = 0f;  
            Debug.Log("Switching to Follow state!");
        }
    }

    void OnFollow()
    {
        print("I'm following the player");

        // Move the enemy towards the player
        MoveTo(target);

        // Increment follow timer
        followTimer += Time.deltaTime;

        // If the player is no longer detected and the timer has exceeded the threshold, switch back to pacing
        GameObject detectedPlayer = CheckForPlayer();
        if (detectedPlayer == null && followTimer > followTime)
        {
            currentState = State.Pace;  // Switch back to Pace state
            Debug.Log("Switching back to Pace state!");
        }
    }

    void MoveTo(GameObject t)
    {
        // Move the enemy towards the target (either the player or the waypoint)
        transform.position = Vector3.MoveTowards(transform.position, t.transform.position, speed * Time.deltaTime);
        transform.LookAt(t.transform, Vector3.up);  // Make the enemy face the target
    }

    GameObject CheckForPlayer()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 1.0f;  // Raycast from above to avoid hitting the ground
        Debug.DrawRay(rayOrigin, transform.forward * 10, Color.green);  // Debug ray for player detection

        if (Physics.Raycast(rayOrigin, transform.forward, out hit, 10))
        {
            // Check if the raycast hits the player (FirstPersonController component)
            FirstPersonController player = hit.transform.gameObject.GetComponent<FirstPersonController>();
            if (player != null)
            {
                return hit.transform.gameObject;  // Return the player as the target if detected
            }
        }

        // If no player is detected, return null
        return null;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<FirstPersonController>() != null)
        {
            Debug.Log("Enemy touched the player. Resetting scene...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        }
    }
}
