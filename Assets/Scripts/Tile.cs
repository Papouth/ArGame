using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Global UI")]
    public GameObject globalUI;
    public bool UION;

    [Header("Translation UI")]
    public GameObject translationUI;

    [Header("Rotation UI")]
    public GameObject rotationUI;


    private void Start()
    {
        globalUI.SetActive(false);

        // On les actives en interargissant avec l'UI global quand on appuie sur la tuile
        //translationUI.SetActive(false);
        //rotationUI.SetActive(false);
    }
}