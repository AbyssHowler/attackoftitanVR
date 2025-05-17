using UnityEngine;

public class FollowHeadPositionOnly : MonoBehaviour
{
    public Transform headTransform; // Main Camera

    // 원하는 등 뒤 위치 (Main Camera 기준 상대 위치)
    public Vector3 relativePosition = new Vector3(0f, -0.94f, -0.06f);

    // 등 뒤로 회전시키기 위한 보정
    private Quaternion rotationOffset = Quaternion.Euler(0, 180f, 0);

    void LateUpdate()
    {
      
        Quaternion yRotation = Quaternion.Euler(0, headTransform.eulerAngles.y, 0);

     
        Vector3 targetPosition = headTransform.position + yRotation * relativePosition;

       
        transform.position = targetPosition;

      
        transform.rotation = yRotation * rotationOffset;
    }
}
