using UnityEngine;
using UnityEngine.UI;

public class BGMButtonLinker : MonoBehaviour
{
    public int bgmIndex; // 0, 1, 2 Áß ÇÏ³ª
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => SoundManager.Instance.PlayBGMByIndex(bgmIndex));
        }
    }
}
