using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTargetBehaviour : MonoBehaviour
{
    public GameObject ArObject;
    public SpriteRenderer ImageSprite;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        if (ArObject != null)
            ArObject.SetActive(false);

        if (ImageSprite != null)
            ImageSprite.gameObject.SetActive(false);
    }

    private TrackingState previousState = TrackingState.None;

    public void OnTargetCreated(ARTrackedImage trackedImage)
    {
        previousState = trackedImage.trackingState;
        ArObject.SetActive(true);
        RefreshTransform(trackedImage.transform);
    }

    public void OnTargetRemoved(ARTrackedImage trackedImage)
    {
        ArObject.SetActive(false);
    }

    public void OnTargetUpdated(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            if (previousState != TrackingState.Tracking)
            {
                ArObject.SetActive(true);
            }
            //RefreshTransform(trackedImage.transform);
        }
        else if (previousState == TrackingState.Tracking)
        {
            //ArObject.SetActive(false);
        }

        previousState = trackedImage.trackingState;
    }

    protected void RefreshTransform(Transform trackedTargetTransform)
    {
        ArObject.transform.position = trackedTargetTransform.position;
        //ArObject.transform.rotation = trackedTargetTransform.rotation;
        ArObject.transform.rotation = Quaternion.LookRotation(trackedTargetTransform.forward, Vector3.up);
        //Debug.Log($"position of {ArObject.name}: {ArObject.transform.position}");
    }
}
