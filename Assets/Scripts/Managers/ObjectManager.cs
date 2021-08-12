using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public  static ObjectManager Instance = null;

    [SerializeField] private Camera orthographicCamera;
    [SerializeField] private DynamicJoystick dynamicJoystick;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private GameObject canonTowerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private List<Transform> turrets;
    [SerializeField] private GameObject cannonBullet;

    public Camera OrthographicCamera { get => orthographicCamera; set => orthographicCamera = value; }
    public DynamicJoystick DynamicJoystick { get => dynamicJoystick; set => dynamicJoystick = value; }
    public GameObject WoodPrefab { get => woodPrefab; set => woodPrefab = value; }
    public GameObject CanonTowerPrefab { get => canonTowerPrefab; set => canonTowerPrefab = value; }
    public GameObject EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    public Transform[] EnemySpawnPoints { get => enemySpawnPoints; set => enemySpawnPoints = value; }
    public List<Transform> Turrets { get => turrets; set => turrets = value; }
    public GameObject CannonBullet { get => cannonBullet; set => cannonBullet = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
