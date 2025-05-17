using UnityEngine;

public class CatchTrigger : MonoBehaviour
{
    public EnemyController enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("XR Origin") || other.CompareTag("Player"))
        {
            Debug.Log("[CatchTrigger] 플레이어 감지됨, 캐치 실행");
            enemy.Catch(this.transform); // 자신의 위치를 넘김
        }
    }
}