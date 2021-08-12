using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IBullet
{
    [SerializeField] private EnemySettings enemySetting;

    private GameManager gameManager;

    private float enemyHealth;
    private Animator enemyAnimator;

    private bool isDead = false;

    public bool IsDead { get => isDead; set => isDead = value; }

    private void Start()
    {
        gameManager = GameManager.Instance;
        enemyHealth = enemySetting.EnemyHealth;
        enemyAnimator = GetComponent<Animator>();
    }

    public void BulletHit(GameObject bullet)
    {
        Destroy(bullet);
        print("Hitted");
        if (enemyHealth < 0)
        {
            isDead = true;
            GetComponent<Collider>().enabled = false;
            enemyHealth = 0;
            enemyAnimator.SetTrigger(Constants.DYING_ANIM);
            if (gameManager.EnemyList.Contains(gameObject))
            {
                gameManager.EnemyList.Remove(gameObject);
                print("Element Removed");
            }
            Destroy(gameObject, 5f);

        }
        else
        {
            enemyHealth -= 10;
        }

    }
}
