using UnityEngine;

public class MoverPatternToBlue : MonoBehaviour
{
    public Transform[] waypoints;
    public bool[] stopAtWaypoint;

    public Transform[] landingWaypoints;
    public bool[] landingStopAtWaypoint;

    public float speed = 3f;
    public KeyCode continueKey = KeyCode.Space;

    public int currentIndex = 0;
    private bool waitingForInput = false;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0 || currentIndex >= waypoints.Length)
            return;

        if (waitingForInput)
        {
            if (Input.GetKeyDown(continueKey))
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

    // 🟢 This lets you switch to landing path dynamically
    public void SwitchToNewPath(Transform[] newWaypoints, bool[] newStopAtWaypoint)
    {
        waypoints = newWaypoints;
        stopAtWaypoint = newStopAtWaypoint;
        currentIndex = 0;
        waitingForInput = false;
    }
}