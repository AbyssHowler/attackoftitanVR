using UnityEngine;

public class TestCode : MonoBehaviour
{
  

    private void Start()
    {
        // targetRenderer�� ��������� �ڵ����� ������ Renderer ���
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�浹 ������: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Blade"))
        {
            Debug.Log("��Ҵ�!");
        }
    }

    // Ʈ���� ����� ��� �̰� ���:
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
