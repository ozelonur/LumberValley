using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private ObjectManager objectManager;
    private PlayerSettings playerSettings;
    private PlayerController player;

    private Rigidbody playerRigidBody;
    private Animator playerAnimator;

    private Vector3 firstPosition;
    private Vector3 mousePosition;
    private Vector3 difference;

    private void Start()
    {
        objectManager = ObjectManager.Instance;
        playerSettings = PlayerController.Instance.PlayerSettings;
        player = PlayerController.Instance;

        playerRigidBody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        firstPosition = Vector3.Lerp(firstPosition, mousePosition, .1f);
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            MouseHold(Input.mousePosition);
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
            playerRigidBody.velocity = transform.forward * playerSettings.Sensivity;
            transform.Rotate(0, objectManager.DynamicJoystick.Horizontal * 2, 0);

        }
    }

    private void MouseDown(Vector3 inputPosition)
    {
        playerAnimator.SetBool(Constants.RUN_ANIM, true);
        //mousePosition = objectManager.OrthographicCamera.ScreenToWorldPoint(inputPosition);
        //firstPosition = mousePosition;
    }

    private void MouseUp()
    {
        playerAnimator.SetBool(Constants.RUN_ANIM, false);
        //difference = Vector3.zero;
    }

    private void MouseHold(Vector3 inputPosition)
    {
        //mousePosition = objectManager.OrthographicCamera.ScreenToWorldPoint(inputPosition);
        //difference = mousePosition - firstPosition;
        //difference *= playerSettings.Sensivity;
    }

}
