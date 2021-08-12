using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySetting", menuName = "EnemySetting", order = 1)]
public class EnemySettings : ScriptableObject
{
    [SerializeField] private float enemyHealth;

    public float EnemyHealth { get => enemyHealth; }
}
