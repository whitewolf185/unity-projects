using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _wonPanel;
    [SerializeField] private GameObject _losePanel;
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickTryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void OnWon()
    {
        this.gameObject.SetActive(true);
        _wonPanel.SetActive(true);
    }

    public void OnLose()
    {
        this.gameObject.SetActive(true);
        _losePanel.SetActive(true);
    }
}
