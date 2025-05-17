using UnityEngine;

public class FollowHeadPositionOnly : MonoBehaviour
{
    public Transform headTransform; // Main Camera

    // ���ϴ� �� �� ��ġ (Main Camera ���� ��� ��ġ)
    public Vector3 relativePosition = new Vector3(0f, -0.94f, -0.06f);

    // �� �ڷ� ȸ����Ű�� ���� ����
    private Quaternion rotationOffset = Quaternion.Euler(0, 180f, 0);

    void LateUpdate()
    {
      
        Quaternion yRotation = Quaternion.Euler(0, headTransform.eulerAngles.y, 0);

     
        Vector3 targetPosition = headTransform.position + yRotation * relativePosition;

       
        transform.position = targetPosition;

      
        transform.rotation = yRotation * rotationOffset;
    }
}
