using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MyArPlaneManager : MonoBehaviour
{
    [SerializeField] ARPlaneManager arPlaneManager;

    bool planesAreVisibles = true;
    public void ToggleArPlanes()
    {
        SetArPlanesVisibles(planesAreVisibles = !planesAreVisibles);
    }

    private void SetArPlanesVisibles(bool visible)
    {
        var planes = arPlaneManager.trackables;
        foreach (var plane in planes)
        {
            plane.gameObject.SetActive(visible);
        }

        //On active/désactive la détection de plans
        arPlaneManager.enabled = visible;
    }
}
