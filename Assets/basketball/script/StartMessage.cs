using UnityEngine;
using TMPro;

public class StartMessage : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public int limitTime = 100;   // 変更可能
    public int goalCount = 10;    // 変更可能
    public float displayDuration = 3f; // メッセージの表示時間

    void Start()
    {
        ShowStartMessage();
    }

    void ShowStartMessage()
    {
        messageText.text = $"Score {goalCount} points\n" +
                           $"in {limitTime} seconds\n" +
                           $"Game Start";
        GameManager.instance.isGameEnd = true; // ゲーム開始前にゲームオーバー状態にする
        StartCoroutine(HideMessage());
    }

    System.Collections.IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(displayDuration);
        messageText.gameObject.SetActive(false);
        GameManager.instance.isGameEnd = false; // メッセージが消えたらゲーム開始
    }
}
