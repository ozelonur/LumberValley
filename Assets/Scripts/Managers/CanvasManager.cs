using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance = null;

    [SerializeField] private GameObject tapPanel;
    [SerializeField] private Text tapText;

    public Text TapText { get => tapText; set => tapText = value; }
    public GameObject TapPanel { get => tapPanel; set => tapPanel = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
