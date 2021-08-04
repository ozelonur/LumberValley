using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public  static ObjectManager Instance = null;

    [SerializeField] private Camera orthographicCamera;
    [SerializeField] private DynamicJoystick dynamicJoystick;

    public Camera OrthographicCamera { get => orthographicCamera; set => orthographicCamera = value; }
    public DynamicJoystick DynamicJoystick { get => dynamicJoystick; set => dynamicJoystick = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
