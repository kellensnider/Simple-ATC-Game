using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Only trigger if the other object also has a collider
        if (other.CompareTag("Airplane"))
        {
            Debug.Log("Collision Detected! Game Over.");
            Time.timeScale = 0; // Pause the game
            // Or call your GameOver logic here
        }
    }
}
