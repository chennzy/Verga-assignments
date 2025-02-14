using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private enum state
    {
        Pace,
        Follow,
    }

    private state currentState = state.Pace;

    [SerializeField]
    GameObject[] route;
    GameObject target;
    int routeIndex = 0;
    float speed = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case state.Pace:
                OnPace();
                break;

            case state.Follow:
                OnFollow();
                break;

        }
    }

    void OnPace()
    {
        //What do we do when we're pacing
        print("im pacing");
        target = route[routeIndex];

        MoveTo(target);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.001f)
        {
            routeIndex += 1;
            if (routeIndex >= route.Length)
            {
                routeIndex = 0;
            }

        }



        // On what condition do we switch states 
        GameObject obstacle = CheckForward();
        if (obstacle != null)
        {
            target = obstacle;
            currentState = state.Follow;
        }
    }






    
    void OnFollow()
    {
    print(" im following");
        GameObject obstacle = CheckForward();
        if (obstacle == null)
        {
            currentState = state.Pace;
        }
    }

    


    void MoveTo(GameObject t)
    {
    transform.LookAt(t.transform, Vector3.up);
    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        
    }
    GameObject CheckForward()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 10, Color.green);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))

        {
            FirstPersonController player = hit.transform.gameObject.GetComponent<FirstPersonController>();

            if (player != null)
            {
                print(hit.transform.gameObject.name);
                return hit.transform.gameObject;
            }
        }

        return null;

    }
}




