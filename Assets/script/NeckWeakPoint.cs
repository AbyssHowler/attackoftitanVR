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
                Debug.LogError("[NeckWeakPoint] EnemyController 자동 연결 실패!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemy == null || enemy.isDie) return; //  직접 접근

        if (other.CompareTag("Blade"))
        {
            Debug.Log("[Enemy] 약점에 블레이드 적중!");

            Vector3 hitPosition = other.ClosestPointOnBounds(transform.position);
            Vector3 direction = (hitPosition - other.transform.position).normalized;
            Quaternion rot = Quaternion.LookRotation(direction);

            // 피 이펙트
            if (enemy.bloodEffectPrefab != null)
            {
                GameObject fx = Instantiate(enemy.bloodEffectPrefab, hitPosition, rot);
                fx.transform.SetParent(enemy.transform);
                Destroy(fx, 2.0f);
            }

            // 썰리는 사운드
            if (enemy.swordSlashSounds != null && enemy.swordSlashSounds.Length > 0)
            {
                var clip = enemy.swordSlashSounds[Random.Range(0, enemy.swordSlashSounds.Length)];
                if (clip != null)
                {
                    AudioSource.PlayClipAtPoint(clip, hitPosition);
                }
            }

            // 데미지
            enemy.TakeDamage(100);
        }
    }
}
