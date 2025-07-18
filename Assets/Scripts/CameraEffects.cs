using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraEffects : MonoBehaviour
{
    public Volume postProcessVolume;
    private DepthOfField dof;

    void Start()
    {
        if (postProcessVolume == null)
        {
            postProcessVolume = GetComponent<Volume>();
        }

        if (postProcessVolume != null && postProcessVolume.profile != null)
        {
            // DOF override'ını alıyoruz
            if (!postProcessVolume.profile.TryGet(out dof))
            {
                Debug.LogWarning("DOF efekti profile içinde bulunamadı!");
            }
        }
        else
        {
            Debug.LogWarning("PostProcessVolume veya Profile atanmadı!");
        }
    }

    public void TriggerBlur()
    {
        Debug.Log("TriggerBlur çalıştı!");

        if (dof != null)
        {
            dof.active = true;
            dof.focusDistance.value = 0.1f; // Yakın odak (bulanık)
            Invoke(nameof(ResetBlur), 1f);   // 1 saniye sonra blur sıfırlanacak
        }
        else
        {
            Debug.LogWarning("DOF bulunamadı! (null)");
        }
    }

    void ResetBlur()
    {
        if (dof != null)
        {
            dof.focusDistance.value = 10f; // Uzak odak (net)
        }
    }
}
