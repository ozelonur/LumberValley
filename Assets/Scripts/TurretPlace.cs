using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurretPlace : MonoBehaviour, IProperty
{
    private PlayerMovement player;
    private ObjectManager objectManager;
    private CanvasManager canvasManager;
    private GameManager gameManager;
    private Rigidbody playerRigidbody;

    private GameObject canonTower;

    private int counter = 0;

    private bool canThrow = false;
    private bool isCollided = false;
    private bool isBuilded = false;

    private float distance;

    private Transform stackTransform;
    private void Start()
    {
        player = PlayerMovement.Instance;
        objectManager = ObjectManager.Instance;
        canvasManager = CanvasManager.Instance;
        gameManager = GameManager.Instance;
        player = PlayerMovement.Instance;
        playerRigidbody = player.GetComponent<Rigidbody>();
        stackTransform = player.transform.GetChild(0).transform.GetChild(1);
    }
    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > 2)
        {
            canThrow = false;
        }
    }
    public void Interact()
    {
        if (!isCollided)
        {
            canonTower = Instantiate(objectManager.CanonTowerPrefab, transform);
            isCollided = true;
        }
        player.CanRun = false;
        playerRigidbody.velocity = Vector3.zero;
        canThrow = true;
        StartCoroutine(GetAllWoods());
    }

    int iterator = 0;

    private void Build()
    {
        counter++;
        print(iterator);
        if (counter % 8 == 0)
        {
            if (iterator < canonTower.transform.childCount)
            {
                canonTower.transform.GetChild(iterator).gameObject.SetActive(true);
                iterator++;
            }
            else
            {
                isBuilded = true;
                gameManager.TowerCount++;
                transform.GetChild(1).gameObject.SetActive(true);
                gameManager.SelectTarget();
            }
            if (counter > 56)
            {
                canThrow = false;
            }
        }
    }

    private IEnumerator GetAllWoods()
    {
        int i = 0;

        while (canThrow && !isBuilded)
        {
            yield return new WaitForSeconds(.1f);
            Transform stackGo = stackTransform.GetChild(i).transform;
            Transform wood;
            if (stackGo.childCount > 0)
            {
                wood = stackGo.GetChild(stackGo.childCount - 1);
                if (wood == null)
                    break;
                wood.parent = null;
                gameManager.WoodCount--;
                wood.DOMove(transform.position, .5f).OnComplete(Build);
                Destroy(wood.gameObject, 2f);
                canvasManager.UpdateWoodCount();
                i++;

                if (i > stackTransform.childCount - 1)
                    i = 0;
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IProperty>()?.Interact();
    }

}