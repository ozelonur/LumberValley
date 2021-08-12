using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour, IProperty
{
    private GameManager gameManager;
    private PlayerController player;
    private Transform target;
    private NavMeshAgent navMeshAgent;

    private Animator enemyAnimator;

    private int iterator = 0;

    private bool isCollided = false;


    private void Start()
    {
        player = PlayerController.Instance;
        gameManager = GameManager.Instance;
        enemyAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetDestination();
        enemyAnimator.SetBool(Constants.RUN_ANIM, true);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, 0), transform.position.z);
    }
    public void Interact()
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.enabled = false;
        enemyAnimator.SetBool(Constants.RUN_ANIM,false);
        if (!isCollided)
        {
            isCollided = true;
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        while (iterator < 10)
        {
            enemyAnimator.SetBool(Constants.CUT_ANIM, true);
            yield return new WaitForSeconds(1f);
            enemyAnimator.SetBool(Constants.CUT_ANIM, false);
            yield return new WaitForSeconds(.5f);
            iterator++;
        }
    }

    public void SetDestination()
    {
        if ((Vector3.Distance(player.transform.position, transform.position) < Vector3.Distance(gameManager.GetTarget().position, transform.position)) || gameManager.TowerCount <= 0)
        {
            print("Destination set to player");
            target = player.transform;
        }
        else
        {
            target = gameManager.GetTarget();
        }

        if (!navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.enabled = true;
        }
        navMeshAgent.destination = target.position;
    }

}
