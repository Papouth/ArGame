using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public GameObject asset;
    public GameObject target;
    Vector3 truePos;
    void LateUpdate()
    {
        truePos.x = Mathf.Round(target.transform.position.x);
        truePos.y = 0;
        truePos.z = Mathf.Round(target.transform.position.z);

        asset.transform.position = truePos;
    }
}
