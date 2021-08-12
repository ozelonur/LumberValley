using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IEnemyHit>()?.EnemyHit(enemy);
    }
}
