using UnityEngine;

public class LandingControlUI : MonoBehaviour
{
    public MoverPatternToBlue targetPlane;
    public Spawner spawner; // Reference to access landing paths

    public void OnLandingButtonPressed()
    {
        if (targetPlane != null && spawner != null)
        {
            targetPlane.SwitchToNewPath(spawner.landingWaypoints, spawner.landingStopAtWaypoint);
        }
    }
}
