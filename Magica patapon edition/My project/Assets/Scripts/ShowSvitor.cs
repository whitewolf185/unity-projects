using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowSvitor : MonoBehaviour
{

    public void OnClickPlay(GameObject svitok)
    {
        svitok.SetActive(true);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickResume()
    {
        SceneManager.LoadScene(1);
    }
}
