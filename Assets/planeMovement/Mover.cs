using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 5f;
    void Update() {

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, moveY, 0f);
        transform.position += move * speed * Time.deltaTime;
    }
}
