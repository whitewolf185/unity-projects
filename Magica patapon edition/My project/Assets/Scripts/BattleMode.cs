using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMode : MonoBehaviour
{
    [SerializeField] private Condition _player;
    [SerializeField] private Condition _enemy;
    [SerializeField] private EndGameUI _endGameUI;
    [SerializeField] private AudioSource _audioSource;

    public static bool battleIsOn = false;
    private bool _gameEnd = false;

    private void Start()
    {
        _gameEnd = false;
        battleIsOn = false;
    }

    private void Update()
    {
        if (_gameEnd) { return; }
        if (_player.GetHealth() <= 0)
        {
            _gameEnd = true;
            Lose();
            return;
        }
        if (_enemy.GetHealth() <= 0)
        {
            _gameEnd = true;
            Win();
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("GameController")) { return; }

        battleIsOn = true;
    }

    private void Win()
    {
        StopGame();
        _endGameUI.OnWon();
    }

    private void Lose()
    {
        StopGame();
        _endGameUI.OnLose();
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
        _audioSource.Stop();
    }
}
