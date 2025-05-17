using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;

    [Header("BGM Clips (3��)")]
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;

    void Awake()
    {
        // �̱��� ����
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
    /// �ε����� ���� �ٸ� BGM ��� (0~2)
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
                Debug.LogWarning("�߸��� BGM �ε����Դϴ�. 0~2�� ����ϼ���.");
                return;
        }

        if (bgmSource.clip == selectedClip) return; // ���� ��� ���̸� ����
        bgmSource.clip = selectedClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }
}
