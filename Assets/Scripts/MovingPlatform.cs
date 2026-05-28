using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveDistance = 5f;
    public float moveSpeed = 2f;

    private Vector3 targetPosition;
    private bool activated = false;

    void Start()
    {
        targetPosition = transform.position + Vector3.right * moveDistance;
    }

    void Update()
    {
        if (activated)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
    }

    public void ActivatePlatform()
    {
        activated = true;
    }
}