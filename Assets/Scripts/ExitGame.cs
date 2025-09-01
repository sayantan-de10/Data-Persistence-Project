using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public void Exit()
    {
        MainHandler.Instance.SaveNameAndScore();
        SceneManager.LoadScene(0);
    }
}