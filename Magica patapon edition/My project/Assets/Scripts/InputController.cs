using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private double _inputWindow;
    [SerializeField] private GameObject _camera;
    
    private InputControllerNamespace.PlayerInput _input;
    private Stopwatch _timeElapsed;

    private void Awake()
    {
        _input = new InputControllerNamespace.PlayerInput();
        _timeElapsed = Stopwatch.StartNew();

        _input.Player.Dunka.performed += context => OnDunka();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void OnDunka()
    {
        
        double elapsedTime = _timeElapsed.ElapsedMilliseconds;
        UnityEngine.Debug.Log(elapsedTime.ToString());
        if (elapsedTime <= _inputWindow)
        {
            _camera.GetComponent<Transform>().localPosition += new Vector3(1,0,0);
        }
        
    }

    public void ResetInputWindow()
    {
        _timeElapsed.Restart();
    }

    
}
