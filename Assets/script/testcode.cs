using UnityEngine;

public class TestCode : MonoBehaviour
{
  

    private void Start()
    {
        // targetRenderer가 비어있으면 자동으로 본인의 Renderer 사용
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌 감지됨: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Blade"))
        {
            Debug.Log("잡았다!");
        }
    }

    // 트리거 방식일 경우 이걸 사용:
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blade"))
        {
            targetRenderer.material.color = Color.red;
        }
    }
    */
}
