using UnityEngine;

public class GrandMa : MonoBehaviour
{
    [SerializeField] private TextAsset talkWithGrandma;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

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
        DialogueManager.Instance._StartDialogue(talkWithGrandma);
    }
}
