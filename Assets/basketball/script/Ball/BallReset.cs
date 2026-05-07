using UnityEngine;

public class BallReset : MonoBehaviour
{
    Vector3 startPos; //ボールの初期位置を保存する変数
    Rigidbody2D rb; 
     void Start()
    {
        startPos = new Vector3(-5, -3, 0); //初期位置を保存
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        // ボールが画面下に落ちたらリセット
        if (transform.position.y < -5f)
        {
            ResetBall();
        }
    }

    public void ResetBall() //ボールを初期位置にリセットするためのメソッド
    {
        rb.linearVelocity = Vector2.zero; //ボールの速度をリセット
        rb.angularVelocity = 0f; //ボールの回転をリセット

        transform.position = startPos; //ボールの位置を初期位置に設定

        rb.gravityScale = 0f; //重力を無効にする
        rb.bodyType = RigidbodyType2D.Kinematic; //Rigidbody2Dをキネマティックにして物理挙動を無効にする

        GetComponent<BallShoot2D>().ResetState(); //BallShoot2D クラスの ResetState メソッドを呼び出してシュート状態をリセット
    }
}
