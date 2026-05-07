using UnityEngine;
using UnityEngine.InputSystem;//kye入力を受け取るために導入

public class BallShoot2D : MonoBehaviour
{
    public LineRenderer lineRenderer;// 軌道表示用のLineRenderer
    public Transform shootDirection;// シュート方向を示すオブジェクト

    Rigidbody2D rb;// ボールのRigidbody2D

    float power = 5f;// シュートの初期パワー
    float angle = 45f;// シュートの初期角度
    float timer = 0f;// タイマー

    enum ShootState { PowerSelect, AngleSelect, Fired }// シュートの状態
    ShootState state = ShootState.PowerSelect;// 初期状態はパワー選択

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();// Rigidbody2Dを取得
        rb.gravityScale = 0f;// 重力を無効にする
        rb.bodyType = RigidbodyType2D.Kinematic;// 最初は動かないようにする

        lineRenderer.positionCount = 30;// 軌道表示の点の数

        // 初期位置（必要なら変更）
        transform.position = new Vector3(-5, -3, 0);// 画面左下からスタート
    }

    void Update()
    {
        if (GameManager.instance.isGameEnd) return; // ゲームオーバー時は操作を受け付けない

        timer += Time.deltaTime * 5f; // タイマーを進める（速度調整）

        // --- ステップ1：パワー選択 ---
        if (state == ShootState.PowerSelect)
        {
            if (timer >= 1f)
            {
                timer = 0f;

                power += 2f;          // パワーを増やす
                if (power > 20f) power = 5f; // ループ
            }

            angle = 45f; // 角度は固定
            shootDirection.rotation = Quaternion.Euler(0, 0, angle);//Quaternion.Eulerで回転を設定

            ShowTrajectory(power);// 軌道を表示

            if (Mouse.current.leftButton.wasPressedThisFrame)// マウスの左クリックで次のステップへ
            {
                state = ShootState.AngleSelect;// 角度選択に移行
                timer = 0f;
            }
        }

        // --- ステップ2：角度選択 ---
        else if (state == ShootState.AngleSelect)
        {
            if (timer >= 1f)
            {
                timer = 0f;

                angle += 10f; // 角度を変える
                if (angle > 75f) angle = 15f;
            }

            shootDirection.rotation = Quaternion.Euler(0, 0, angle);

            ShowTrajectory(power); // 軌道を表示

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                state = ShootState.Fired; // 発射状態に移行
                lineRenderer.enabled = false; // 軌道表示を消す
                Shoot(power); // ボールをシュート
            }
        }
    }

    void ShowTrajectory(float p) // シュートの軌道を表示する関数
    {
        lineRenderer.enabled = true;

        Vector2 startPos = transform.position; // ボールの現在位置をスタート位置とする
        Vector2 startVel = shootDirection.right * p; // シュート方向の右ベクトルにパワーを掛けて初速を計算

        for (int i = 0; i < lineRenderer.positionCount; i++) // 軌道の点を計算してLineRendererにセット
        {
            float t = i * 0.1f;
            Vector2 pos = startPos + startVel * t + 0.5f * Physics2D.gravity * t * t; // 位置 = 初速 * 時間 + 0.5 * 重力 * 時間^2
            lineRenderer.SetPosition(i, pos); // LineRendererに位置をセット
        }
    }

    void Shoot(float p) // ボールをシュートする関数
    {
        rb.bodyType = RigidbodyType2D.Dynamic; // Rigidbody2Dを動的にして物理挙動を有効にする
        rb.gravityScale = 1f;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.AddForce(shootDirection.right * p, ForceMode2D.Impulse);// シュート方向の右ベクトルにパワーを掛けてインパルスで力を加える
    }

    public void ResetState() // シュート状態をリセットする関数
    {
        state = ShootState.PowerSelect; // 状態をパワー選択に戻す
        timer = 0f; // タイマーをリセット
        power = 5f; // パワーを初期値に戻す
        angle = 45f; // 角度を初期値に戻す

        lineRenderer.enabled = true; // 軌道表示を戻す
    }
}