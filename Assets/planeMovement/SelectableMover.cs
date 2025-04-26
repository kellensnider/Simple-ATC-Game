/**
using UnityEngine;

public class SelectableMover : MonoBehaviour
{
    private bool isSelected = false;
    private Camera mainCamera;
    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        mainCamera = Camera.main;
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (!isSelected && hit.transform == transform)
                {
                    isSelected = true;
                    rend.material.color = Color.green; // Selection color
                    Debug.Log("Object selected!");
                }
                else if (isSelected && hit.transform.CompareTag("Waypoint"))
                {
                    Vector3 targetPosition = hit.transform.position;
                    MoveTo(targetPosition);
                    rend.material.color = originalColor; // Reset color
                    isSelected = false;
                }
            }
        }
    }

    private void MoveTo(Vector3 destination)
    {
        StopAllCoroutines();
        StartCoroutine(MoveCoroutine(destination));
    }

    private System.Collections.IEnumerator MoveCoroutine(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 5);
            yield return null;
        }
    }
}
**/