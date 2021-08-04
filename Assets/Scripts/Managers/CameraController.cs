using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController player;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.Instance;
        offset = new Vector3(transform.position.x, transform.position.y, transform.position.z - player.transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player.CurrentGameMode == GameMode.Playing)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
