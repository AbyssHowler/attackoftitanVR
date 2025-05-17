using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VRBladeTrail : MonoBehaviour
{
    public Transform bladeTip; // VR 칼 또는 손끝
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
        // 일정 시간 간격마다 새로운 위치 저장
        if (Time.time - lastPointTime >= pointSpacing)
        {
            positions.Add(bladeTip.position);
            lastPointTime = Time.time;
        }

        // trailDuration 이 지난 포인트 삭제
        while (positions.Count > 0 && Time.time - lastPointTime >= trailDuration)
        {
            positions.RemoveAt(0);
        }

        // 라인에 포지션 적용
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }
}
