using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class PlayerInputController : MonoBehaviour
{
    // event triggers
    [SerializeField] private double _inputWindow;

    // Showing player reaction time in milliseconds in Unity logs.
    [SerializeField] private bool showButtonTimeInLog;
    
    private InputController.PlayerInput _input;
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

    // pause game
    private bool isPaused = false;
    [SerializeField] private AudioSource _audioSource;


    private void Awake()
    {
        _input = new InputController.PlayerInput();
        _timeElapsed = Stopwatch.StartNew();

        _input.Player.Dunka.performed += context => OnCommand(dunkaAction);
        _input.Player.Unka.performed += context => OnCommand(unkaAction);
        _input.Player.Pause.performed += context => OnPause();
    }

    private void Start()
    {
        // in scane restart
        previousComboLen = 0;
        comboSystem = new List<int>();
        isPaused = false;
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
        if (isPaused)
        {
            UnityEngine.Debug.Log("Game is paused");
            return;
        }
        
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
                if (BattleMode.battleIsOn)
                {
                    UnityEngine.Debug.LogWarning("You cannot walk, while fighting");
                    return;
                }
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

    private void OnPause()
    {
        // if game NOT paused
        if (!isPaused)
        {
            //isPaused = true;
            //Time.timeScale = 0f;
            //_audioSource.Stop();
            //_timeElapsed.Stop();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
            _audioSource.Play();
            _timeElapsed.Start();
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
