using UnityEngine;

public class NeckWeakPoint : MonoBehaviour
{
    public EnemyController enemy;

    void Start()
    {
        if (enemy == null)
        {
            enemy = GetComponentInParent<EnemyController>();
            if (enemy == null)
            {
                Debug.LogError("[NeckWeakPoint] EnemyController �ڵ� ���� ����!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemy == null || enemy.isDie) return; //  ���� ����

        if (other.CompareTag("Blade"))
        {
            Debug.Log("[Enemy] ������ ���̵� ����!");

            Vector3 hitPosition = other.ClosestPointOnBounds(transform.position);
            Vector3 direction = (hitPosition - other.transform.position).normalized;
            Quaternion rot = Quaternion.LookRotation(direction);

            // �� ����Ʈ
            if (enemy.bloodEffectPrefab != null)
            {
                GameObject fx = Instantiate(enemy.bloodEffectPrefab, hitPosition, rot);
                fx.transform.SetParent(enemy.transform);
                Destroy(fx, 2.0f);
            }

            // �丮�� ����
            if (enemy.swordSlashSounds != null && enemy.swordSlashSounds.Length > 0)
            {
                var clip = enemy.swordSlashSounds[Random.Range(0, enemy.swordSlashSounds.Length)];
                if (clip != null)
                {
                    AudioSource.PlayClipAtPoint(clip, hitPosition);
                }
            }

            // ������
            enemy.TakeDamage(100);
        }
    }
}
