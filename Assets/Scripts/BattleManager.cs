using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleManager : MonoBehaviour, IProperty
{
    public static BattleManager Instance = null;
    private ObjectManager objectManager;
    private GameManager gameManager;
    private CanvasManager canvasManager;

    private bool isCollided = false;

    private Vector3 startPosition;

    private int battleCount = 1;

    public int BattleCount { get => battleCount; set => battleCount = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        objectManager = ObjectManager.Instance;
        gameManager = GameManager.Instance;
        canvasManager = CanvasManager.Instance;
        gameManager.victoryAction += BattleComplete;
        startPosition = transform.position;
    }
    public void Interact()
    {
        GetComponent<Collider>().enabled = false;
        transform.DOMoveY(transform.position.y - .1f, .3f);
        print("Worked");

        if (!isCollided)
        {
            SpawnEnemy(battleCount);
            isCollided = true;
        }
    }

    private void SpawnEnemy(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(objectManager.EnemyPrefab, objectManager.EnemySpawnPoints[Random.Range(0, 9)].position, Quaternion.identity);
        }
    }
    private void BattleComplete()
    {
        StartCoroutine(BattleCompleteCoroutine());
    }
    private IEnumerator BattleCompleteCoroutine()
    {
        battleCount++;
        yield return new WaitForSeconds(3f);
        isCollided = false;
        canvasManager.VictoryText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        canvasManager.VictoryText.gameObject.SetActive(false);
        transform.position = startPosition;
        GetComponent<Collider>().enabled = true;

    }
}
