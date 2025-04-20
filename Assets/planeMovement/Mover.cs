using UnityEngine;

public class Mover : MonoBehaviour
{
    public Transform[] waypoints;
    public bool[] stopAtWaypoint;
    public float speed = 3f;
    public KeyCode continueKey = KeyCode.Space;

    public int currentIndex = 0;
    private bool waitingForInput = false;

    void Update()
    {
        if(waypoints.Length == 0 || currentIndex >= waypoints.Length)
            return;
        
        if(waitingForInput)
        {
            if(Input.GetKeyDown(continueKey))
            {
                waitingForInput = false;
                currentIndex++;
            }
            return;
        }

        Transform target = waypoints[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            if (stopAtWaypoint.Length > currentIndex && stopAtWaypoint[currentIndex])
            {
                waitingForInput = true;
            }
            else
            {
                currentIndex++;
            }
        }
    }
}
