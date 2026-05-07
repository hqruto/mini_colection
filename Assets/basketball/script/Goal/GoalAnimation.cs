using UnityEditor.Build.Content;
using UnityEngine;

public class GoalAnimation : MonoBehaviour
{
    public Sprite[] goalSprites; //ゴールのアニメーションに使用するスプライトを格納する配列
    public Sprite normalNetSprite; // 最後に表示する通常ネット画像
    public SpriteRenderer sr; //スプライトを表示するための SpriteRenderer コンポーネント

    public float frameTime = 0.23f; //アニメーションのフレーム間の時間

    public Transform ball; //ボールの位置を取得するための Transform コンポーネント
    public float endY = -2.6f; //アニメーションの終了位置の Y 座標

    public SpriteRenderer rim_be; //リムの前面のプライトレンダラー
    public SpriteRenderer rim_af; //リムの背側のスプライトレンダラー
    public SpriteRenderer net; //ネットのスプライトレンダラー

    public void PlayAnimation() //アニメーションを再生するためのメソッド
    {
        StartCoroutine(Play()); //コルーチンを開始してアニメーションを再生
    }

    System.Collections.IEnumerator Play() //アニメーションを再生するためのコルーチン
    {
        if (rim_be != null) rim_be.enabled = false; //リムの前面のスプライトレンダラーを無効にする
        if (rim_af != null) rim_af.enabled = false; //リムの背面のスプライトレンダラーを無効にする
        if (net != null) net.enabled = false; //ネットのスプライトレンダラーを無効にする
        while (ball.position.y > endY) //ボールの Y 座標が終了位置より高い間
        {
            for (int i = 0; i < goalSprites.Length; i++) //アニメーションのスプライトを順番に表示するループ
            {
                sr.sprite = goalSprites[i]; //現在のスプライトを SpriteRenderer に設定
                yield return new WaitForSeconds(frameTime); //フレーム間の時間だけ待機

                if (ball.position.y <= endY) //ボールの Y 座標が終了位置以下になった場合
                {
                    break; //ループを終了
                }
            }
        }
        // ★ 最後に通常ネット画像を表示
        sr.sprite = normalNetSprite;

        // ★ Rim と Net を再表示
        if (rim_be != null) rim_be.enabled = true;
        if (rim_af != null) rim_af.enabled = true;
        if (net != null) net.enabled = true;

        GameManager.instance.AddScore(); //スコアを加算するために GameManager の AddScore メソッドを呼び出す
    }
}
