using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public InputField nameInput;

    public void NewNameEnter(string name)
    {
        MainManager.Instance.name = name;
    }

    // Start is called before the first frame update
    void Start()
    {
        nameInput.onValueChanged.AddListener(NewNameEnter);
        nameInput.text = MainManager.Instance.name;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        MainManager.Instance.SaveInfo();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
