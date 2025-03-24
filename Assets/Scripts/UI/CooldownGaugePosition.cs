using UnityEngine;

public class CooldownGaugePosition : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(target.position + offset);
            transform.position = screenPosition;
        }
    }
}
