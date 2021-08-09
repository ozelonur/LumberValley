using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance = null;
    private ObjectManager objectManager;
    private PlayerSettings playerSettings;
    private PlayerController player;

    private Rigidbody playerRigidBody;
    private Animator playerAnimator;

    private Vector3 firstPosition;
    private Vector3 mousePosition;

    private bool canRun = false;

    private Quaternion targetRotation;

    public bool CanRun { get => canRun; set => canRun = value; }
    public Rigidbody PlayerRigidBody { get => playerRigidBody; set => playerRigidBody = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        objectManager = ObjectManager.Instance;
        playerSettings = PlayerController.Instance.PlayerSettings;
        player = PlayerController.Instance;

        PlayerRigidBody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        firstPosition = Vector3.Lerp(firstPosition, mousePosition, .1f);
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
    }
    private void FixedUpdate()
    {
        if (player.CurrentGameMode == GameMode.Playing)
        {
            if (CanRun)
            {
                PlayerRigidBody.velocity = transform.forward * playerSettings.Sensivity;
            }
            var input = new Vector3(objectManager.DynamicJoystick.Horizontal, 0, objectManager.DynamicJoystick.Vertical);
            if (input != Vector3.zero)
            {
                targetRotation = Quaternion.LookRotation(input);
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, playerSettings.RotationSpeed * Time.deltaTime);
        }
    }

    private void MouseDown(Vector3 inputPosition)
    {
        if (player.CurrentGameMode == GameMode.Playing)
        {
            playerAnimator.SetBool(Constants.RUN_ANIM, true);
            canRun = true;

        }
    }

    private void MouseUp()
    {
        if (player.CurrentGameMode == GameMode.Playing)
        {
            canRun = false;
            PlayerRigidBody.velocity = Vector3.zero;
            playerAnimator.SetBool(Constants.RUN_ANIM, false);
        }
    }


}
