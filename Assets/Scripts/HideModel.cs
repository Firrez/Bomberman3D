using UnityEngine;
using UnityEngine.Rendering;

public class HideModel : MonoBehaviour
{
    void Start()
    {
        GetComponent<SkinnedMeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;
    }
}
