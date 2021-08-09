using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private CanvasManager canvasManager;
    private PlayerController player;
    private ObjectManager objectManager;


    private int counter = 0;
    private int woodCount = 0;
    public int Counter { get => counter; set => counter = value; }
    public int WoodCount { get => woodCount; set => woodCount = value; }

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
        objectManager = ObjectManager.Instance;

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
            canvasManager.TapText.gameObject.SetActive(false);
            objectManager.DynamicJoystick.transform.SetAsLastSibling();
            player.CurrentGameMode = GameMode.Playing;
        }
    }
}
