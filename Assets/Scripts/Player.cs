using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    #region Variables
    [Header("Player Movement")]
    [SerializeField] private float playerSpeed = 1f;
    private bool isClimbing;

    [Header("Raycasts Detection")]
    [SerializeField] private Transform raycastLader;
    [SerializeField] private Transform raycastVoid;
    [SerializeField] private Transform raycastObstacle;
    [SerializeField] private LayerMask laderMask;
    private bool haveLader;
    private bool haveVoid;
    private bool haveObstacle;
    [SerializeField] private float distanceLader = 0.1f;
    [SerializeField] private float distanceVoid = 0.1f;
    [SerializeField] private float distanceObstacle = 0.1f;

    [Header("UI Victoire")]
    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private LayerMask layerVictoire;

    [Header("Player Components")]
    private Animator anim;
    #endregion


    private void Start()
    {
        Time.timeScale = 1;
        anim = GetComponent<Animator>();
        victoryText.enabled = false;
    }

    private void Update()
    {
        Detection();

        if (!haveLader && isClimbing)
        {
            anim.SetBool("Climbing", false);
            transform.Translate(0f, 0f, 1f * playerSpeed * Time.deltaTime);
            isClimbing = false;
            StartCoroutine(WalkLaderEnd());
            return;
        }

        if (haveLader && isClimbing)
        {
            transform.Translate(0f, 1f * playerSpeed * Time.deltaTime, 0f);
        }
    }

    #region Déplacements
    public void Avancer()
    {
        if (!haveLader && !haveVoid && !haveObstacle)
        {
            anim.SetBool("Walking", true);
            transform.Translate(0f, 0f, 1f * playerSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(transform.forward);

            StartCoroutine(StopAnim());
        }
        else if (haveLader)
        {
            anim.SetBool("Climbing", true);
            isClimbing = true;
        }
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
    #endregion

    private void Detection()
    {
        // Raycast qui Detecte s'il y a une échelle devant nous
        if (Physics.Raycast(raycastLader.position, transform.forward, distanceLader, laderMask))
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
            haveVoid = false;
        }
        else
        {
            haveVoid = true;
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

    private IEnumerator WalkLaderEnd()
    {
        yield return new WaitForSeconds(0.2f);
        transform.Translate(0f, 1f * playerSpeed * Time.deltaTime, 0f);
        yield return new WaitForSeconds(0.1f);
        transform.Translate(0f, 0f, 1.5f * playerSpeed * Time.deltaTime);
    }

    public void VictoryUI()
    {
        if (Physics.Raycast(raycastObstacle.position, transform.forward, distanceObstacle, layerVictoire))
        {
            // Affichage de la victoire et arrêt du jeu
            victoryText.enabled = true;
            Time.timeScale = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(raycastLader.position, transform.forward * distanceLader, Color.yellow);
        Debug.DrawRay(raycastVoid.position, -transform.up * distanceVoid, Color.green);
        Debug.DrawRay(raycastObstacle.position, transform.forward * distanceObstacle, Color.blue);
    }
}