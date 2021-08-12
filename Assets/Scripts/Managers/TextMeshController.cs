using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshController : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
