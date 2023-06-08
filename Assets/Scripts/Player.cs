using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [Header("Player Movement")]
    [SerializeField] private float playerSpeed = 1f;

    [Header("Raycasts Detection")]
    [SerializeField] private Transform raycastLader;
    [SerializeField] private Transform raycastVoid;
    [SerializeField] private Transform raycastObstacle;
    private bool haveLader;
    private bool haveVoid;
    private bool haveObstacle;
    [SerializeField] private float distanceLader = 0.1f;
    [SerializeField] private float distanceVoid = 0.1f;
    [SerializeField] private float distanceObstacle = 0.1f;

    [Header("Player Components")]
    private Rigidbody rb;
    private Animator anim;
    #endregion


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Detection();
    }

    public void Avancer()
    {
        anim.SetBool("Walking", true);
        transform.Translate(0f, 0f, 1f * playerSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(transform.forward);

        StartCoroutine(StopAnim());
    }

    public void Reculer()
    {
        transform.rotation = Quaternion.LookRotation(-transform.forward);
    }

    public void Gauche()
    {
        transform.rotation = Quaternion.LookRotation(-transform.right);
    }

    public void Droit()
    {
        transform.rotation = Quaternion.LookRotation(transform.right);
    }

    private IEnumerator StopAnim()
    {
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("Walking", false);
    }

    private void Detection()
    {
        // Raycast qui Detecte s'il y a une échelle devant nous
        if (Physics.Raycast(raycastLader.position, transform.forward, distanceLader))
        {
            haveLader = true;
        }
        else
        {
            haveLader = false;
        }

        // Raycast qui Detecte s'il y a du vide devant nous
        if (Physics.Raycast(raycastVoid.position, -transform.up, distanceVoid))
        {
            haveVoid = true;
        }
        else
        {
            haveVoid = false;
        }

        // Raycast qui Detecte s'il y a un obstacle devant nous
        if (Physics.Raycast(raycastObstacle.position, transform.forward, distanceObstacle))
        {
            haveObstacle = true;
        }
        else
        {
            haveObstacle = false;
        }
    }


    private void OnDrawGizmos()
    {
        Debug.DrawRay(raycastLader.position, transform.forward * distanceLader, Color.yellow);
        Debug.DrawRay(raycastVoid.position, -transform.up * distanceVoid, Color.green);
        Debug.DrawRay(raycastObstacle.position, transform.forward * distanceObstacle, Color.blue);
    }
}