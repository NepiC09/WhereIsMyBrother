using UnityEngine;

public class Dad : MonoBehaviour
{
    public static Dad Instance { get; private set; }

    [SerializeField] private TextAsset talkWithDad;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        indicator.SetActive(false);
    }
    public void _ShowIndicator()
    {
        indicator.SetActive(true);
    }
    public void _HideIndicator()
    {
        indicator.SetActive(false);
    }

    public void _Interact()
    {
        DialogueManager.Instance._StartDialogue(talkWithDad);
    }
}
