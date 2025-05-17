using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;

    [Header("BGM Clips (3개)")]
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;

    void Awake()
    {
        // 싱글톤 패턴
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 인덱스에 따라 다른 BGM 재생 (0~2)
    /// </summary>
    public void PlayBGMByIndex(int index)
    {
        AudioClip selectedClip = null;

        switch (index)
        {
            case 0:
                selectedClip = clip1;
                break;
            case 1:
                selectedClip = clip2;
                break;
            case 2:
                selectedClip = clip3;
                break;
            default:
                Debug.LogWarning("잘못된 BGM 인덱스입니다. 0~2만 사용하세요.");
                return;
        }

        if (bgmSource.clip == selectedClip) return; // 현재 재생 중이면 무시
        bgmSource.clip = selectedClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }
}
