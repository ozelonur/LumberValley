using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private ObjectManager objectManager;
    private BattleManager battleManager;
    private GameManager gameManager;

    [SerializeField] private GameObject cannon;

    private bool isCollided = false;

    private int enemyCount;

    private void Start()
    {
        objectManager = ObjectManager.Instance;
        battleManager = BattleManager.Instance;
        gameManager = GameManager.Instance;


        enemyCount = battleManager.BattleCount;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Constants.ENEMY))
        {
            if (!gameManager.EnemyList.Contains(other.gameObject))
            {
                gameManager.EnemyList.Add(other.gameObject);
                print("Enemy Count : " + gameManager.EnemyList.Count);
            }
            if (!isCollided)
            {
                print("Triggered");
                StartCoroutine(Attack(other));
                isCollided = true;
            }
            
        }
    }

    private IEnumerator Attack(Collider other)
    {
        int iterator = 0;
        while (iterator < 10)
        {
            if (!other.GetComponent<EnemyController>().IsDead)
            {
                print("Attack Starting...");
                cannon.transform.LookAt(other.transform.GetChild(0).transform);
                GameObject bullet = Instantiate(objectManager.CannonBullet, cannon.transform);
                bullet.GetComponent<Rigidbody>().AddForce(cannon.transform.forward * 1000);
                yield return new WaitForSeconds(.5f);

            }
            else
            {
                enemyCount--;
                if (enemyCount <= 0)
                {
                    print("Battle Count : " + battleManager.BattleCount);
                    gameManager.victoryAction();
                    enemyCount = battleManager.BattleCount;
                }
                isCollided = false;
                break;
            }
        }
    }

    

}
