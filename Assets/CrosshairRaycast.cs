using UnityEngine;

public class CrosshairRaycast : MonoBehaviour
{
    public Transform gunBarrelEnd;  // Silahın ucu (namlu)
    public Camera cam;              // Aktif kamera
    public RectTransform crosshairUI;  // Crosshair'in UI objesi (World Space Canvas içindeki Image)

    void Update()
    {
        Ray ray = new Ray(gunBarrelEnd.position, gunBarrelEnd.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Vector3 screenPoint = cam.WorldToScreenPoint(hit.point);
            crosshairUI.position = screenPoint;
        }
        else
        {
            // Uzak noktaya sabitle (boşluğa nişan alındıysa)
            Vector3 pointFar = ray.GetPoint(100f);
            Vector3 screenPoint = cam.WorldToScreenPoint(pointFar);
            crosshairUI.position = screenPoint;
        }
    }
}
