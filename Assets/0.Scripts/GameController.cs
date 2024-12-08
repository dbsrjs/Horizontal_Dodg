using System.Collections;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject panelGameStart;

    [SerializeField]
    private TextMeshProUGUI textCurrentScore;

    [SerializeField]
    private GameObject panelGameOver;

    [SerializeField]
    private TextMeshProUGUI textBestScore;

    private int score = 0;

    public bool IsGameStart { get; private set; } = false;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            textCurrentScore.text = score.ToString();
        }
    }

    private IEnumerator Start()
    {
        while(true) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();

                yield break;
            }

            yield return null;
        }
    }

    public void GameStart()
    {
        IsGameStart = true;
        panelGameStart.SetActive(false);
        textCurrentScore.gameObject.SetActive(true);
    }
}
