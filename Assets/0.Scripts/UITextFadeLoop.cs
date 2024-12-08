using TMPro;
using UnityEngine;

public class UITextFadeLoop : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed = 2f;

    private TextMeshProUGUI target;

    private void Awake()
    {
        target = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(FadeEffect.FadeLoop(target, 0f, 1f, fadeSpeed));
    }
}
