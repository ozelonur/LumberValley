using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TreeController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerController player;
    private GameManager gameManager;
    private CanvasManager canvasManager;

    private GameObject woodPrefab;

    private bool canCut = false;

    private int count = 0;

    private float distance = 0;

    private BoxCollider boxCollider;
    private Animator playerAnimator;

    private Vector3 startColliderCenter;
    private Vector3 startPosition;
    private Vector3 startScale;

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
        startScale = transform.localScale;
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
        Vector3 targetPosition = new Vector3(transform.position.x, 0, transform.position.z);
        player.transform.LookAt(targetPosition);
        playerMovement.PlayerRigidBody.velocity = Vector3.zero;
        playerMovement.CanRun = false;
        playerAnimator.SetBool(Constants.RUN_ANIM, false);
        playerAnimator.SetBool(Constants.CUT_ANIM, true);
        Invoke(Constants.STOP_CUT_ANIM, 1f);
    }
    private void StopCutAnim()
    {
        StartCoroutine(WoodenMovement());
        playerAnimator.SetBool(Constants.CUT_ANIM, false);
        canCut = false;
        count++;
        boxCollider.center = boxCollider.center + new Vector3(0, 0.018f, 0);
        if (count >= 4)
        {
            boxCollider.enabled = false;
            transform.position = new Vector3(100, 100, 100);
            transform.localScale = new Vector3(0, 0, 0);
            Invoke(Constants.SPAWN_AGAIN, 5);
        }
    }
   
    private IEnumerator WoodenMovement()
    {
        int rand = Random.Range(2,5);
        GameObject[] woods = new GameObject[rand];
        for (int i = 0; i < rand; i++)
        {
            woods[i] = Instantiate(woodPrefab, transform.position + new Vector3(0, Random.Range(.8f, 1.2f), 0), Quaternion.identity);
            woods[i].GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * 500, ForceMode.Force);

        }

        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < rand; i++)
        {
            if (gameManager.Counter > 3)
            {
                gameManager.Counter = 0;

            }

            woods[i].transform.parent = player.StackPositions[gameManager.Counter];

            woods[i].transform.localScale = new Vector3(.1f, .02f, .1f);
            woods[i].transform.DOLocalMove(Vector3.up * (player.StackPositions[gameManager.Counter].childCount * .021f), Random.Range(.1f, .3f)).SetEase(Ease.InOutSine);
            woods[i].transform.localEulerAngles = Random.insideUnitSphere * 90;
            woods[i].transform.localEulerAngles = new Vector3(0, 0, 0); 
            Destroy(woods[i].GetComponent<Rigidbody>());
            Destroy(woods[i].GetComponent<BoxCollider>());
            gameManager.Counter++;
            gameManager.WoodCount++;
            canvasManager.UpdateWoodCount();
        }
    }

    private void SpawnAgain()
    {
        count = 0;
        boxCollider.center = startColliderCenter;
        transform.position = startPosition;
        transform.DOScale(startScale, 1f);
      
        boxCollider.enabled = true;
    }

}
