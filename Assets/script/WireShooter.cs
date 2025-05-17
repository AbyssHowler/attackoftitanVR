using UnityEngine;
using UnityEngine.InputSystem;

public class WireShooter : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference triggerButton;

    [Header("Settings")]
    public float maxDistance = 50f;
    public float pullSpeed = 10f;

    [Header("Line")]
    public LineRenderer lineRenderer;
    public Transform startPoint;

    [Header("Audio")]
    public AudioClip[] grappleSounds;
    private AudioSource audioSource;

    private CharacterController characterController;
    private Transform xrOrigin;
    private Vector3 grapplePoint;
    private bool isGrappling = false;

    // === 중력 제어 ===
    public static bool AnyGrappling = false; // 전역 상태 공유

    private float verticalSpeed = 0f;
    private float gravity = -9.81f;

    void Start()
    {
        characterController = GetComponentInParent<CharacterController>();
        xrOrigin = characterController.transform;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f;
    }

    void OnEnable()
    {
        triggerButton.action.performed += OnTriggerPressed;
        triggerButton.action.canceled += OnTriggerReleased;
        triggerButton.action.Enable();
    }

    void OnDisable()
    {
        triggerButton.action.performed -= OnTriggerPressed;
        triggerButton.action.canceled -= OnTriggerReleased;
        triggerButton.action.Disable();
    }

    void Update()
    {
        if (isGrappling)
        {
            Vector3 direction = (grapplePoint - xrOrigin.position).normalized;
            characterController.Move(direction * pullSpeed * Time.deltaTime);

            float distance = Vector3.Distance(xrOrigin.position, grapplePoint);
            if (distance < 1.5f)
            {
                StopGrapple();
            }

            if (lineRenderer != null && startPoint != null)
            {
                lineRenderer.SetPosition(0, startPoint.position);
                lineRenderer.SetPosition(1, grapplePoint);
            }
        }
        else
        {
            if (!AnyGrappling)
            {
                // 중력 적용
                verticalSpeed += gravity * Time.deltaTime;
                Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
                characterController.Move(gravityMove * Time.deltaTime);

                if (characterController.isGrounded && verticalSpeed < 0)
                {
                    verticalSpeed = 0f;
                }
            }
        }
    }

    void OnTriggerPressed(InputAction.CallbackContext context)
    {
        // 레이는 항상 컨트롤러 기준에서 앞으로 쏜다
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance))
        {
            grapplePoint = hit.point;
            isGrappling = true;
            AnyGrappling = true;

            if (lineRenderer != null)
                lineRenderer.enabled = true;

            PlayRandomGrappleSound();
            Debug.Log("[WireShooter] Grapple Started at " + grapplePoint);
        }
    }

    void OnTriggerReleased(InputAction.CallbackContext context)
    {
        StopGrapple();
    }

    void StopGrapple()
    {
        isGrappling = false;

        if (lineRenderer != null)
            lineRenderer.enabled = false;

        // 다른 손이 여전히 줄 타고 있는지 확인
        if (!IsAnotherGrappling())
        {
            AnyGrappling = false;
        }

        Debug.Log("[WireShooter] Grapple Ended.");
    }

    bool IsAnotherGrappling()
    {
        WireShooter[] shooters = FindObjectsOfType<WireShooter>();
        foreach (var shooter in shooters)
        {
            if (shooter != this && shooter.isGrappling)
                return true;
        }
        return false;
    }

    void PlayRandomGrappleSound()
    {
        if (grappleSounds != null && grappleSounds.Length > 0)
        {
            int index = Random.Range(0, grappleSounds.Length);
            AudioClip clip = grappleSounds[index];

            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
