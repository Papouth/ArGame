using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class TouchToPosition : MonoBehaviour
{
    [SerializeField] private GameObject ObjectPrefab;

    private Camera m_Camera;

    private void Awake()
    {
        m_Camera = Camera.main;
    }

    private void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += OnScreenTouched;
    }

    private void OnDisable()
    {
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= OnScreenTouched;
    }

    private void OnScreenTouched(Finger fingerTouch)
    {
        if (fingerTouch.index != 0) return;

        var ray = m_Camera.ScreenPointToRay(fingerTouch.currentTouch.screenPosition);
        bool hasHit = Physics.Raycast(ray, out var hit, 30f);
        if (hasHit)
        {
            var obj = Instantiate(ObjectPrefab);
            Vector3 myForward = IsVertical(hit.normal) ? Vector3.up : Vector3.ProjectOnPlane(m_Camera.transform.position - hit.point, hit.normal);
            obj.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(myForward, Vector3.up));
        }
    }

    private bool IsVertical(Vector3 surfaceNormal)
    {
        float dotProd = Mathf.Abs(Vector3.Dot(Vector3.up, surfaceNormal));
        return dotProd < 0.15f;
    }
}
