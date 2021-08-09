using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TreeController : MonoBehaviour
{
    private Animator playerAnimator;

    private PlayerMovement playerMovement;
    private PlayerController player;
    private GameManager gameManager;
    private CanvasManager canvasManager;

    private GameObject woodPrefab;

    private bool canCut = false;

    private int count = 0;

    private float distance = 0;

    private BoxCollider boxCollider;

    private Vector3 startColliderCenter;
    private Vector3 startPosition;

    private void Start()
    {
        playerMovement = PlayerMovement.Instance;
        playerAnimator = playerMovement.GetComponent<Animator>();
        gameManager = GameManager.Instance;
        canvasManager = CanvasManager.Instance;
        woodPrefab = ObjectManager.Instance.WoodPrefab;
        player = PlayerController.Instance;
        boxCollider = GetComponent<BoxCollider>();
        startColliderCenter = boxCollider.center;
        startPosition = transform.position;
    }

    private void LateUpdate()
    {
        distance = Vector3.Distance(playerMovement.transform.position, transform.position);
        if (distance <= 1.5f && !canCut)
        {
            canCut = true;
            Cut();
        }
    }

    private void Cut()
    {
        Vector3 targetPosition = new Vector3(0, transform.localRotation.y, 0);
        player.transform.LookAt(targetPosition);
        playerMovement.PlayerRigidBody.velocity = Vector3.zero;
        playerMovement.CanRun = false;
        playerAnimator.SetBool(Constants.RUN_ANIM, false);
        playerAnimator.SetBool(Constants.CUT_ANIM, true);
        Invoke(Constants.STOP_CUT_ANIM, 1f);
    }

    private void StopCutAnim()
    {
        print(count);
        gameManager.WoodCount++;
        canvasManager.UpdateWoodCount();
        playerAnimator.SetBool(Constants.CUT_ANIM, false);
        transform.GetChild(count).GetComponent<MeshRenderer>().enabled = false;
        canCut = false;
        count++;
        boxCollider.center = boxCollider.center + new Vector3(0, 0.018f, 0);
        if (count >= 4)
        {
            boxCollider.enabled = false; 
            Invoke(Constants.SPAWN_AGAIN, 5);
        }

        if (gameManager.Counter <= 3)
        {
            GameObject wood = Instantiate(woodPrefab, player.StackPositions[gameManager.Counter]);
            wood.transform.localPosition += Vector3.up * (player.StackPositions[gameManager.Counter].childCount * .012f);
            gameManager.Counter++;
        }
        else
        {
            gameManager.Counter = 0;
        }

        
    }

    private void SpawnAgain()
    {
        print("Spawning...");
        boxCollider.center = startColliderCenter;
        transform.position = startPosition;
        boxCollider.enabled = true;
        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
        }
    }

}
