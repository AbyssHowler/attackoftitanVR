using UnityEngine;

public class CatchTrigger : MonoBehaviour
{
    public EnemyController enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("XR Origin") || other.CompareTag("Player"))
        {
            Debug.Log("[CatchTrigger] �÷��̾� ������, ĳġ ����");
            enemy.Catch(this.transform); // �ڽ��� ��ġ�� �ѱ�
        }
    }
}