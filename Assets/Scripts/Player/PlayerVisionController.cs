using UnityEngine;

public class PlayerVisionController : MonoBehaviour
{
    static public PlayerVisionController instance;

    [Header("Vision Settings")]
    [SerializeField]
    private Material visionMaterial;
    [SerializeField]
    private float initialFovAngle = 33.3f;
    public float fovAngle = 33.3f;
    [SerializeField]
    private float viewDistance = 5f;
    [SerializeField]
    private float circleRadius = 2f;
    [SerializeField]
    private float shadowAlpha = 0.8f;

    //To pause the vision if escape menu is open
    private bool isVisionActive = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogWarning("There is more than one instance of PlayerVisionController");
            Destroy(gameObject);
        }

    }


    void Update()
    {
        if (isVisionActive)
        {
            Vector3 playerPos = Camera.main.WorldToViewportPoint(transform.position);
            Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            visionMaterial.SetVector("_PlayerPosition", new Vector4(playerPos.x, playerPos.y, 0, 0));
            visionMaterial.SetVector("_MousePosition", new Vector4(mousePos.x, mousePos.y, 0, 0));
            visionMaterial.SetFloat("_FOVAngle", fovAngle);
            visionMaterial.SetFloat("_ViewDistance", viewDistance);
            visionMaterial.SetFloat("_CircleRadius", circleRadius);
            visionMaterial.SetFloat("_Alpha", shadowAlpha);
        }
    }

    public static float GetViewRadius()
    {
        return instance.viewDistance;
    }

    public void ResetVision()
    {
        fovAngle = initialFovAngle;
    }

    public void SetVisionActive(bool isActive)
    {
        isVisionActive = isActive;
    }
}
