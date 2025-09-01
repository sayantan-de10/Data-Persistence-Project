using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public string inputName;
    public TMP_InputField inputField;

    public void ReadStringInput()
    {
        inputName = inputField.text;
        MainHandler.Instance.playerName = inputName;
    }

    // ----------- START & EXIT ----------
    public void StartNew() // Loads the next Scene
    {
        SceneManager.LoadScene(1);
    }

    public void Exit() // Quits the game
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

}