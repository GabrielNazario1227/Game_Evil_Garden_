using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [Range(0f, 1f)][SerializeField] private float parallaxEffectX = 0.5f;
    [Range(0f, 1f)][SerializeField] private float parallaxEffectY = 0.0f;

    private Vector2 startPos;

    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        startPos = new Vector2(transform.position.x, transform.position.y);
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        float distanceX = cameraTransform.position.x * (1 - parallaxEffectX);
        float distanceY = cameraTransform.position.y * (1 - parallaxEffectY);

        transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, transform.position.z);
    }
}