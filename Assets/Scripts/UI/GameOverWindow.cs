using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI leftStatValue;
    public TextMeshProUGUI rightStatValue;
    public TextMeshProUGUI scoreValue;
    private int score = 0;
    private Coroutine writeAllCo;

    public Button nextButton;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNext);
    }

    public override void Open()
    {
        ResetStats(); // Close로만 안꺼질 수 있어서 여기서도 Reset
        base.Open();
        writeAllCo = StartCoroutine(WriteAll());
    }

    private IEnumerator WriteAll()
    {
        yield return StartCoroutine(WriteStats(leftStatLabel, leftStatValue));
        yield return StartCoroutine(WriteStats(rightStatLabel, rightStatValue));
        yield return StartCoroutine(WriteScore());
        writeAllCo = null;
    }

    private IEnumerator WriteStats(TextMeshProUGUI label, TextMeshProUGUI value)
    {
        for (int i = 0; i < 3; i++)
        {
            label.text += $"Stat{i + 1}\n";
            int randomScore = Random.Range(1, 1000);
            score += randomScore;
            value.text += $"{randomScore:D4}\n";
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    private IEnumerator WriteScore()
    {
        float duration = score / 1000 + 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            int current = Mathf.FloorToInt(Mathf.Lerp(0f, score, elapsed / duration));
            scoreValue.text = current.ToString("D8");
            yield return null;
        }
    }

    public override void Close()
    {
        ResetStats();
        base.Close();
    }

    public void ResetStats()
    {
        leftStatLabel.text = "";
        leftStatValue.text = "";
        rightStatLabel.text = "";
        rightStatValue.text = "";
        scoreValue.text = "00000000";
        score = 0;

        if (writeAllCo != null)
        {
            StopCoroutine(writeAllCo);
        }
    }

    public void OnNext()
    {
        windowManager.Open(0);
    }
}
