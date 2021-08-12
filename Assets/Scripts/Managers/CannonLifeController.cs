using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CannonLifeController : MonoBehaviour, IEnemyHit
{
    private GameManager gameManager;

    [SerializeField] TurretSettings turretSettings;

    private GameObject enemyObj;
    private float health;
    private TextMeshPro cannonHealthTextMesh;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        cannonHealthTextMesh = transform.parent.transform.parent.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshPro>();

        health = turretSettings.TurretHealth;
        cannonHealthTextMesh.text = health.ToString();
    }


    public void EnemyHit(GameObject enemy)
    {
        enemyObj = enemy;
        if (health > 0)
        {
            health -= 10;
            cannonHealthTextMesh.text = health.ToString();
        }
        if (health <= 0)
        {
            transform.parent.transform.parent = null;
            gameManager.TowerCount--;
            if (gameManager.TowerCount > 0)
            {
                gameManager.SelectTarget();
            }
            gameManager.SetDestination();
            Destroy(transform.parent.gameObject);
        }
    }
}
