using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameEnd : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Start"); // 
    }
}
