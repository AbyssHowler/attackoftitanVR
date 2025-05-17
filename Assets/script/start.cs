using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartMenu : MonoBehaviour
{
    public GameObject question;
    public GameObject Sound;
    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // 씬 이름 주의
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료 시도");

#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // 에디터에서는 플레이 모드 종료
#else
        Application.Quit(); // 빌드에서는 실제 종료
#endif
    }
    public void questionmark()
    {
        question.SetActive(true);
    }
    public void questionmarkexit()
    {
        question.SetActive(false);
    }
    public void Soundmark()
    {
        Sound.SetActive(true);
    }
    public void Soundmarkexit()
    {
        Sound.SetActive(false);
    }
}
