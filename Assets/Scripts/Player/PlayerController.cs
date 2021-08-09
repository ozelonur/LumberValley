using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Playing,
    Dead,
    Start
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance = null;

    [SerializeField] private PlayerSettings playerSettings;

    public PlayerSettings PlayerSettings { get => playerSettings; }
    public GameMode CurrentGameMode { get => currentGameMode; set => currentGameMode = value; }
    public Transform[] StackPositions { get => stackPositions; set => stackPositions = value; }

    private GameMode currentGameMode;

    [SerializeField] private Transform[]  stackPositions;

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
        currentGameMode = GameMode.Start;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IProperty>()?.Interact();
    }
}
