using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RightStick : MonoBehaviour
{
    [Header("Angle Clamp")]
    [SerializeField] public float leftAngle;
    [SerializeField] public float rightAngle;

    [Header("Input")]
    [SerializeField] private PlayerInput playerInput;

    private RectTransform rectTransform;
    private bool isInArea;
    public float speedOfRotate { get; private set; }
    private GameObject currentArea;

    [HideInInspector] public UnityEvent onSuccessTap;
    [HideInInspector] public UnityEvent onFailTap;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        isInArea = false;
        rectTransform.localEulerAngles = new Vector3(0, 0, leftAngle);
    }

    public void SetSpeedOfRotate(float speed)
    {
        speedOfRotate = speed;
    }

    public void StartRotate(bool isToRight)
    {
        playerInput.currentActionMap.FindAction("LockTap").performed += LockTapPerformed;
        StartCoroutine(RotateCoroutine(isToRight));
    }

    private IEnumerator RotateCoroutine(bool RightTarget)
    {
        yield return null;

        if (RightTarget)
        {
            while (rectTransform.rotation.eulerAngles.z > rightAngle)
            {
                rectTransform.Rotate(new Vector3(0, 0, -1 * speedOfRotate * Time.deltaTime));
                yield return null;
            }
            StartCoroutine(RotateCoroutine(false));
        }
        else
        {
            while (rectTransform.rotation.eulerAngles.z < leftAngle)
            {
                rectTransform.Rotate(new Vector3(0, 0, 1 * speedOfRotate * Time.deltaTime));
                yield return null;
            }
            StartCoroutine(RotateCoroutine(true));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInArea = true;
        currentArea = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInArea = false;
        currentArea = null;
    }

    private void LockTapPerformed(InputAction.CallbackContext o)
    {
        if (isInArea)
        {
            GameObject tempArea = currentArea;

            tempArea?.GetComponent<CanvasGroup>().LeanAlpha(0f, 0.1f).setOnComplete(() =>
            {
                tempArea?.SetActive(false);
                tempArea = null;
                onSuccessTap?.Invoke();
            });
        } else
        {
            StopAllCoroutines();
            rectTransform.LeanRotateZ(leftAngle, 0.1f).setOnComplete(() =>
            {
                StartCoroutine(RotateCoroutine(true));
            });
            onFailTap?.Invoke();
        }
    }

    public void StopGame()
    {
        StopAllCoroutines();
        playerInput.currentActionMap.FindAction("LockTap").performed -= LockTapPerformed;
    }
}
