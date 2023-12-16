using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    // event triggers
    [SerializeField] private double _inputWindow;

    [SerializeField] private UnityEvent _objectTriggers;

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
            _objectTriggers.Invoke();
        }
    }

    public void ResetInputWindow()
    {
        _timeElapsed.Restart();
    }

    
}
