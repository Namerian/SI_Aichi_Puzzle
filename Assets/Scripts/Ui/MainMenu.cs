using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _mainMenuCanvasGroup;

    [SerializeField]
    private CanvasGroup _levelSelectionCanvasGroup;

    [SerializeField]
    private EventSystem _eventSystem;

    [SerializeField]
    private List<string> _levelNames;

    [SerializeField]
    private GameObject _mainMenuFirstSelected;

    [SerializeField]
    private GameObject _levelSelectionFirstSelected;

    private void Start()
    {
        _mainMenuCanvasGroup.gameObject.SetActive(true);
        _levelSelectionCanvasGroup.gameObject.SetActive(true);

        _mainMenuCanvasGroup.alpha = 1;
        _levelSelectionCanvasGroup.alpha = 0;

        _eventSystem.firstSelectedGameObject = _mainMenuFirstSelected;
    }

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

    public void OnGoToLevelSelectionButtonPressed()
    {
        _mainMenuCanvasGroup.alpha = 0;
        _levelSelectionCanvasGroup.alpha = 1;

        _eventSystem.firstSelectedGameObject = _levelSelectionFirstSelected;
    }

    public void OnBackToMainMenuButtonPressed()
    {
        _mainMenuCanvasGroup.alpha = 1;
        _levelSelectionCanvasGroup.alpha = 0;

        _eventSystem.firstSelectedGameObject = _mainMenuFirstSelected;
    }
}
