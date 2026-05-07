using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GoalAnimation goalAnim; //Animation を入れるための変数

    private void OnTriggerEnter2D(Collider2D other) //衝突したときに呼び出される関数
    {
        if(other.CompareTag("Ball")) //衝突したオブジェクトのタグが "Ball" であるかを確認
        {
            goalAnim.PlayAnimation(); //GoalAnimation クラスの PlayAnimation メソッドを呼び出す
        }
    }

}
