using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance = null;

    private GameManager gameManager;
    [SerializeField] private GameObject tapPanel;
    [SerializeField] private Text tapText;
    [SerializeField] private Text woodCountText;
    [SerializeField] private Text victoryText;

    public Text TapText { get => tapText; set => tapText = value; }
    public GameObject TapPanel { get => tapPanel; set => tapPanel = value; }
    public Text WoodCountText { get => woodCountText; set => woodCountText = value; }
    public Text VictoryText { get => victoryText; set => victoryText = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        victoryText.gameObject.SetActive(false);
        UpdateWoodCount();
    }
    public void UpdateWoodCount()
    {
        woodCountText.text = gameManager.WoodCount.ToString();
    }


}
