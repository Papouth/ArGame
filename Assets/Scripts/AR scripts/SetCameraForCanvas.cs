using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class SetCameraForCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
