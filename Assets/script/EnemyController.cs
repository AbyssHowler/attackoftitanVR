using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform catchHandTransform;
    public AudioClip[] swordSlashSounds;
    public GameObject player;
    public GameObject ctr1;
    public GameObject ctr2;
    public GameObject ctr3;
    public GameObject bloodEffectPrefab;
    public bool isDie = false;

    private NavMeshAgent navMesh;
    private Animator ani;
    private int HP;
    private bool isAttack = false;
    

    void Start()
    {
        HP = 100;
        player = GameObject.Find("XR Origin (XR Rig)");
        ctr1 = GameObject.Find("Left Controller");
        ctr2 = GameObject.Find("Right Controller");
        ctr3 = GameObject.Find("ODM geartest");

        navMesh = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        ani.applyRootMotion = false;
    }

    void Update()
    {
        if (isDie || player == null) return;

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (!isAttack && !isDie)
        {
            Vector3 localVelocity = transform.InverseTransformDirection(navMesh.velocity);
            ani.SetFloat("speedh", localVelocity.x);
            ani.SetFloat("speedv", localVelocity.z);
        }

        if (distance < 2.0f)
        {
            if (!isAttack)
            {
                navMesh.isStopped = true;
                StartCoroutine(Attack());
            }
        }
        else
        {
            if (!isAttack)
            {
                if (NavMesh.SamplePosition(player.transform.position, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                {
                    navMesh.isStopped = false;
                    navMesh.SetDestination(hit.position);
                }
            }
        }
    }

    IEnumerator Attack()
    {
        isAttack = true;
        navMesh.isStopped = true;

        ani.SetBool("Attack", true);
        yield return new WaitForSeconds(0.5f);

        Debug.Log("[Enemy] 공격 시도!");
        ani.SetBool("Attack", false);
        yield return new WaitForSeconds(0.9f);

        isAttack = false;
    }

    IEnumerator Die()
    {

        isDie = true;
        navMesh.isStopped = true;
        ani.SetBool("Fall", true);
        ani.applyRootMotion = false;

        yield return new WaitForSeconds(7.0f);

        GameManager.Instance?.OnEnemyKilled(); // 웨이브 다음 조건 체크
        Destroy(gameObject);

    }

    void OnTriggerEnter(Collider other)
    {
        if (isDie) return;

        if (other.CompareTag("Blade"))
        {
           

            Vector3 hitPoint = other.ClosestPoint(transform.position);
            Vector3 direction = (hitPoint - other.transform.position).normalized;
            Quaternion rot = Quaternion.LookRotation(direction);

            GameObject effect = Instantiate(bloodEffectPrefab, hitPoint, rot);
            Destroy(effect, 2f);

            if (swordSlashSounds.Length > 0)
            {
                AudioSource.PlayClipAtPoint(
                    swordSlashSounds[Random.Range(0, swordSlashSounds.Length)],
                    hitPoint
                );
            }

            Debug.Log("[Enemy] 몸통 맞음 (연출만)");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDie) return;

        HP -= damage;
        Debug.Log("[Enemy] 데미지: " + damage + " / 남은 HP: " + HP);

        if (HP <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void Catch(Transform handTransform)
    {
        if (isDie || player == null) return;

        Debug.Log("[Enemy] 플레이어를 잡았다!");

        // 손에 직접 붙이기
        player.transform.SetParent(handTransform);
        player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;

        ctr1.SetActive(false);
        ctr2.SetActive(false);
        ctr3.SetActive(false);

        // 후처리는 GameManager에 넘기기
        GameManager.Instance.OnPlayerCaught(player.transform);
    }
}
