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

    // Showing player reaction time in milliseconds in Unity logs.
    [SerializeField] private bool showButtonTimeInLog;
    
    private InputControllerNamespace.PlayerInput _input;
    private Stopwatch _timeElapsed;

    // VFX
    [SerializeField] VisualEffect _unkaVFX;
    [SerializeField] VisualEffect _dunkaVFX;

    // combo events
    [Header("Move events")]
    [SerializeField] private bool _enableMove;
    [Tooltip(@"Set combination you want, to events downside was triggered:
1 - DunkaAction
2 - UnnkaAction
For example, if you want 'Unka Dunka Dunka' you must enter '2 1 1'")]
    [SerializeField] private string _comboToMove;
    [SerializeField] private UnityEvent _movementTriggers;

    [Header("Attack events")]
    [SerializeField] private bool _enableAttack;
    [Tooltip(@"Set combination you want, to events downside was triggered:
1 - DunkaAction
2 - UnnkaAction
For example, if you want 'Unka Dunka Dunka' you must enter '2 1 1'")]
    [SerializeField] private string _comboToAttack;
    [SerializeField] private UnityEvent _attackTriggers;

    private int previousComboLen = 0;
    private List<int> comboSystem = new List<int>();
    private const int unkaAction = 2;
    private const int dunkaAction = 1;


    private void Awake()
    {
        _input = new InputControllerNamespace.PlayerInput();
        _timeElapsed = Stopwatch.StartNew();

        _input.Player.Dunka.performed += context => OnCommand(dunkaAction);
        _input.Player.Unka.performed += context => OnCommand(unkaAction);
    }

    private void Start()
    {
        // in scane restart
        previousComboLen = 0;
        comboSystem = new List<int>();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void OnCommand(int buttonType)
    {
        double elapsedTime = _timeElapsed.ElapsedMilliseconds;
        if (showButtonTimeInLog) { UnityEngine.Debug.Log(elapsedTime.ToString()); }
        if (elapsedTime <= _inputWindow)
        {
            comboSystem.Add(buttonType);
            var comboStr = string.Join(" ", comboSystem);

            // which vfx to play
            if (buttonType == unkaAction)
            {
                _unkaVFX.Play();
            }
            if (buttonType == dunkaAction)
            {
                _dunkaVFX.Play();
            }

            // is combination successful
            if (_enableMove && _comboToMove == comboStr)
            {
                UnityEngine.Debug.Log("You have been triggered move events");
                _movementTriggers.Invoke();
                ResetComboKeys();
                return;
            }
            if (_enableAttack && _comboToAttack == comboStr)
            {
                UnityEngine.Debug.Log("You have been triggered attack events");
                _attackTriggers.Invoke();
                ResetComboKeys();
                return;
            }
        }
        else if (comboSystem.Count != 0)// stop spamming!
        {
            var comboStr = string.Join(" ", comboSystem);
            UnityEngine.Debug.LogWarning("Do not spam! Your combo is " + comboStr);
            comboSystem.Clear();
        }
    }
    private void ResetComboKeys()
    {
        comboSystem.Clear();
        previousComboLen = 0;
    }

    public void ResetInputWindow()
    {
        _timeElapsed.Restart();
        var comboStr = string.Join(" ", comboSystem);
        if (!IsButtonWasPressed() )
        {
            UnityEngine.Debug.LogWarning("You failed your combo of '" + comboStr + "'");
        }
    }

    private bool IsButtonWasPressed()
    {
        if (previousComboLen >= comboSystem.Count && comboSystem.Count != 0)
        {
            ResetComboKeys();
            return false;
        }

        previousComboLen = comboSystem.Count;
        return true;
    }
    
}
