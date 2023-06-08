using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class TouchToPosition : MonoBehaviour
{
    private Camera m_Camera;
    private Tile tuile;


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
        bool hasHit = Physics.Raycast(ray, out var hit, 300f);

        if (hasHit && hit.collider.GetComponent<Tile>())
        {
            // Si un UI de tuile est déjà affiché on le retire
            if (tuile != null)
            {
                tuile.globalUI.SetActive(false);
                tuile.UION = false;
            }

            // On viens afficher l'UI de la tuile
            tuile = hit.collider.GetComponent<Tile>();
            tuile.globalUI.SetActive(true);
            tuile.UION = true;


            //Vector3 myForward = IsVertical(hit.normal) ? Vector3.up : Vector3.ProjectOnPlane(m_Camera.transform.position - hit.point, hit.normal);
            //ObjectPrefab.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(myForward, Vector3.up));
        }
    }

    private bool IsVertical(Vector3 surfaceNormal)
    {
        float dotProd = Mathf.Abs(Vector3.Dot(Vector3.up, surfaceNormal));
        return dotProd < 0.15f;
    }

    public void TranslateUIDisplay()
    {
        if (tuile != null)
        {
            if (!tuile.UION)
            {
                tuile.UION = true;
                tuile.globalUI.SetActive(true);
            }

            tuile.rotationUI.SetActive(false);
            tuile.translationUI.SetActive(true);
        }
    }

    public void RotateUIDisplay()
    {
        if (tuile != null)
        {
            if (!tuile.UION)
            {
                tuile.UION = true;
                tuile.globalUI.SetActive(true);
            }

            tuile.translationUI.SetActive(false);
            tuile.rotationUI.SetActive(true);
        }
    }
}
