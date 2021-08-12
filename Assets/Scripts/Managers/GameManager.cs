using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private CanvasManager canvasManager;
    private PlayerController player;
    private ObjectManager objectManager;

    public Action victoryAction;

    private List<GameObject> enemyList;


    private int counter = 0;
    private int woodCount = 0;
    private int targetIndex = 0;
    private int towerCount = 0;

    private bool canBattle = false;
    public int Counter { get => counter; set => counter = value; }
    public int WoodCount { get => woodCount; set => woodCount = value; }
    public bool CanBattle { get => canBattle; set => canBattle = value; }
    public List<GameObject> EnemyList { get => enemyList; set => enemyList = value; }
    public int TowerCount { get => towerCount; set => towerCount = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasManager = CanvasManager.Instance;
        player = PlayerController.Instance;
        objectManager = ObjectManager.Instance;

        enemyList = new List<GameObject>();
        canvasManager.TapText.text = Constants.TAP_TO_PLAY;
    }

    public void OnClick()
    {
        if (player.CurrentGameMode == GameMode.Start)
        {
            canvasManager.TapText.gameObject.SetActive(false);
            objectManager.DynamicJoystick.transform.SetAsLastSibling();
            player.CurrentGameMode = GameMode.Playing;
        }
    }
    public void SetDestination()
    {
        print(enemyList.Count);
        for (int i = 0; i < enemyList.Count; i++)
        {
            print("Set Destination Work");
            enemyList[i].GetComponent<EnemyMovement>().SetDestination();
        }
    }
    public void SelectTarget()
    {
        do
        {
            targetIndex = UnityEngine.Random.Range(0, objectManager.Turrets.Count);
        } while (objectManager.Turrets[targetIndex].childCount < 3);
        print("Target Index : " + targetIndex);
        print(objectManager.Turrets[targetIndex].childCount);
    }

    public Transform GetTarget()
    {
        return objectManager.Turrets[targetIndex];
    }
}
