using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VRBladeTrail : MonoBehaviour
{
    public Transform bladeTip; // VR Į �Ǵ� �ճ�
    public float trailDuration = 0.2f;
    public float pointSpacing = 0.01f;

    private LineRenderer lineRenderer;
    private List<Vector3> positions = new List<Vector3>();
    private float lastPointTime;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        // ���� �ð� ���ݸ��� ���ο� ��ġ ����
        if (Time.time - lastPointTime >= pointSpacing)
        {
            positions.Add(bladeTip.position);
            lastPointTime = Time.time;
        }

        // trailDuration �� ���� ����Ʈ ����
        while (positions.Count > 0 && Time.time - lastPointTime >= trailDuration)
        {
            positions.RemoveAt(0);
        }

        // ���ο� ������ ����
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }
}
