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
        SceneManager.LoadScene("Game"); // �� �̸� ����
    }

    public void QuitGame()
    {
        Debug.Log("���� ���� �õ�");

#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // �����Ϳ����� �÷��� ��� ����
#else
        Application.Quit(); // ���忡���� ���� ����
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
