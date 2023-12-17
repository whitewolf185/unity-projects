using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class InputController : MonoBehaviour
{
    // event triggers
    [SerializeField] private double _inputWindow;
    [SerializeField] private UnityEvent _objectTriggers;
    private InputControllerNamespace.PlayerInput _input;
    private Stopwatch _timeElapsed;

    // VFX
    [SerializeField] VisualEffect _unkaVFX;
    [SerializeField] VisualEffect _dunkaVFX;


    private void Awake()
    {
        _input = new InputControllerNamespace.PlayerInput();
        _timeElapsed = Stopwatch.StartNew();

        _input.Player.Dunka.performed += context => OnDunka();
        _input.Player.Unka.performed += context => OnUnka();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void OnUnka()
    {
        double elapsedTime = _timeElapsed.ElapsedMilliseconds;
        UnityEngine.Debug.Log(elapsedTime.ToString());
        if (elapsedTime <= _inputWindow)
        {
            _unkaVFX.Play();
            _objectTriggers.Invoke();
        }
    }

    private void OnDunka()
    {
        double elapsedTime = _timeElapsed.ElapsedMilliseconds;
        UnityEngine.Debug.Log(elapsedTime.ToString());
        if (elapsedTime <= _inputWindow)
        {
            _dunkaVFX.Play();
            _objectTriggers.Invoke();
        }
    }

    public void ResetInputWindow()
    {
        _timeElapsed.Restart();
    }

    
}
