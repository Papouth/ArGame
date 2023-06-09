using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region Variables
    [Header("Global UI")]
    public GameObject globalUI;
    [HideInInspector] public bool UION;

    [Header("Translation UI")]
    public GameObject translationUI;

    [Header("Rotation UI")]
    public GameObject rotationUI;

    [Header("Settings Snap")]
    private Vector3 truePos;
    [SerializeField] private float offsetSnap = 0.1f;
    [SerializeField] private float offsetRot = 90f;

    #endregion


    private void Start()
    {
        globalUI.SetActive(false);
        rotationUI.SetActive(false);
    }

    public void GoRight()
    {
        truePos.x = transform.position.x + offsetSnap;
        truePos.z = transform.position.z;

        truePos.y = 0;
        transform.position = truePos;
    }

    public void GoLeft()
    {
        truePos.x = transform.position.x - offsetSnap;
        truePos.z = transform.position.z;

        truePos.y = 0;
        transform.position = truePos;
    }

    public void GoForward()
    {
        truePos.x = transform.position.x;
        truePos.z = transform.position.z + offsetSnap;

        truePos.y = 0;
        transform.position = truePos;
    }

    public void GoBackward()
    {
        truePos.x = transform.position.x;
        truePos.z = transform.position.z - offsetSnap;

        truePos.y = 0;
        transform.position = truePos;
    }

    public void RotateRight()
    {
        transform.eulerAngles = new Vector3(0f, transform.rotation.eulerAngles.y + offsetRot, 0f);
    }

    public void RotateLeft()
    {
        transform.eulerAngles = new Vector3(0f, transform.rotation.eulerAngles.y - offsetRot, 0f);
    }
}