using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private CanvasManager canvasManager;
    private PlayerController player;


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
        canvasManager = CanvasManager.Instance;
        player = PlayerController.Instance;

        canvasManager.TapText.text = Constants.TAP_TO_PLAY;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (player.CurrentGameMode == GameMode.Start)
        {
            canvasManager.TapPanel.SetActive(false);
            player.CurrentGameMode = GameMode.Playing;
        }
    }
}
