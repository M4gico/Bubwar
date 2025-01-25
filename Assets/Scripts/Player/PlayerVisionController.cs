using UnityEngine;

public class VisionController : MonoBehaviour
{
    public Material visionMaterial;
    public float fovAngle = 60f;
    public float viewDistance = 5f;
    public float circleRadius = 2f;
    public float shadowAlpha = 0.8f;

    void Update()
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
