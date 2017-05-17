using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private List<string> _levelNames;

    public void OnLevelButtonPressed(int id)
    {
        if(_levelNames.Count >= id)
        {
            SceneManager.LoadScene(_levelNames[id]);
        }
    }

    public void OnQuitButtonPressed()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
		Application.Quit ();
        #endif
    }
}
