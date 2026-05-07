using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance; //GameManager クラスのインスタンスを保持する静的変数
   
    public int score = 0; //現在のスコアを保持する変数
    public float timeLimit = 100f; //ゲームの制限時間を設定する変数

    public TextMeshProUGUI ScoreText; //スコアを表示するための Text コンポーネント
    public TextMeshProUGUI TimeText; //時間を表示するための Text コンポーネント
    public GameObject clearUI; //ゲームクリア時に表示する UI オブジェクト
    public GameObject gameOverUI; //ゲームオーバー時に表示する UI オブジェクト

    public bool isGameEnd = false; //ゲームが終了したかどうかを示すフラグ

    void Awake()
    {
        instance = this; //Awake メソッドで GameManager クラスのインスタンスを設定
    }

    void Update()
    {
        if (isGameEnd) return; //ゲームが終了している場合は Update メソッドを終了

        timeLimit -= Time.deltaTime; //制限時間を減少させる
        TimeText.text = "Time: " + Mathf.CeilToInt(timeLimit).ToString(); //残り時間をテキストに表示

        if (timeLimit <= 0) //制限時間が0以下になった場合
        {
            GameOver(); //ゲームオーバー処理を呼び出す
        }
    }

    public void AddScore() //スコアを加算するためのメソッド
    {
       if (isGameEnd) return; //ゲームが終了している場合はスコアを加算しない

       score += 1; //スコアを1加算
       ScoreText.text = "Score: " + score.ToString(); //現在のスコアをテキストに表示
       if (score >= 10) //スコアが10以上になった場合
       {
           GameClear(); //ゲームクリア処理を呼び出す
       }
    }

    void GameClear() //ゲームクリア処理を行うメソッド
    {
        isGameEnd = true; //ゲームが終了したことを示すフラグを設定
        clearUI.SetActive(true); //ゲームクリア UI を表示
        StartCoroutine(ReturnToSelect());
    }

    void GameOver() //ゲームオーバー処理を行うメソッド
    {
        isGameEnd = true; //ゲームが終了したことを示すフラグを設定
        gameOverUI.SetActive(true); //ゲームオーバー UI を表示
        StartCoroutine(ReturnToSelect());
    }

    System.Collections.IEnumerator ReturnToSelect() //セレクトシーンに戻るためのコルーチン
    {
        yield return new WaitForSeconds(3f); //3秒待機
        SceneManager.LoadScene("MiniGame_Select"); //セレクトシーンをロード
    }
}
