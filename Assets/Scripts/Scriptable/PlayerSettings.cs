using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSetting", menuName = "PlayerSettings/PlayerSetting", order = 1)]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private float sensivity;
    [SerializeField] private float forwardSpeed;

    public float Sensivity { get => sensivity; }
    public float ForwardSpeed { get => forwardSpeed; }
}
