using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Setting", menuName = "Turret Setting" , order = 1)]
public class TurretSettings : ScriptableObject
{
    [SerializeField] private float turretHealth;

    public float TurretHealth { get => turretHealth; }
}
