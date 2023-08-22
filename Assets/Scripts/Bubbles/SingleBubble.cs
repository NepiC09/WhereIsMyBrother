using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleBubble : MonoBehaviour
{
    [Header("Bubble params")]
    [SerializeField] private float animationTime = 0.5f;

    [Header("Bubble visual")]
    [SerializeField] private TextMeshProUGUI bubbleText;

    [SerializeField] private GameObject backForm1;
    [SerializeField] private GameObject backForm2;

    [SerializeField] private Color redColor;
    [SerializeField] private Color blueColor;

    [SerializeField] private RectTransform leftArrow;
    [SerializeField] private RectTransform rightArrow;

    private CanvasGroup canvasGroup;

    private GameObject currentForm = null;
    public bool isAllTextShowed { get; private set; }

    private Transform targetPosition;
    private Vector2 startDialogueTransform;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        transform.localScale = Vector3.zero;
        transform.LeanScale(Vector3.one, animationTime).setEaseOutCubic(); 
        leftArrow.sizeDelta = new Vector2(0, leftArrow.sizeDelta.y);
        rightArrow.sizeDelta = new Vector2(0, leftArrow.sizeDelta.y);
    }

    private void Update()
    {
        if (startDialogueTransform == null)
            return;

        float distance = transform.position.x - startDialogueTransform.x;

        if(transform.position.x > startDialogueTransform.x)
        {
            leftArrow.sizeDelta = new Vector2(distance, leftArrow.sizeDelta.y);
            rightArrow.sizeDelta = new Vector2(0, leftArrow.sizeDelta.y);
        }
        else if (transform.position.x <= startDialogueTransform.x)
        {
            leftArrow.sizeDelta = new Vector2(0, leftArrow.sizeDelta.y);
            rightArrow.sizeDelta = new Vector2(-distance, leftArrow.sizeDelta.y);
        }
    }

    public void SetBubbleText(string text, float typingSpeed)
    {
        StartCoroutine(TextAnimations(text, typingSpeed));
    }

    public void SetBubbleForm(string form)
    {
        switch (form)
        {
            case "1":
                backForm1.SetActive(true); 
                backForm2.SetActive(false);
                currentForm = backForm1;
                break;
            case "2":
                backForm1.SetActive(false); 
                backForm2.SetActive(true);
                currentForm = backForm2;
                break;
            default:
                Debug.LogWarning("There's no form: " + form);
                break;
        }
    }

    public void SetFormColor(string color)
    {
        if (currentForm == null)
        {
            Debug.LogWarning("Form isn't choosed. Can't set color");
            return;
        }
        switch (color)
        {
            case "red":
                currentForm.GetComponent<Image>().color = redColor;
                leftArrow.GetComponent<Image>().color = redColor;
                rightArrow.GetComponent<Image>().color = redColor;
                break;
            case "blue":
                currentForm.GetComponent<Image>().color = blueColor;
                leftArrow.GetComponent<Image>().color = blueColor;
                rightArrow.GetComponent<Image>().color = blueColor;
                break;
            default:
                Debug.LogWarning("There's no color: " + color);
                break;
        }
    }

    private IEnumerator TextAnimations(string text, float typingSpeed) 
    {
        isAllTextShowed = false;

        bubbleText.text = "";
        
        foreach (char letter in text.ToCharArray())
        {
            bubbleText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isAllTextShowed = true;
    }

    public void SetTargetPosition(Transform target)
    {
        LeanTween.move(this.gameObject, target, animationTime).setEaseOutCubic();
    }

    public void SetSpawnDialogueTransform(Vector2 target, Vector2 spawnPosition)
    {
        startDialogueTransform = target;
        transform.position = spawnPosition;
    }


    public void SelfDestroy()
    {
        /*transform.LeanScale(Vector3.zero, animationTime)*/
        //LeanTween.moveLocalY(gameObject, transform.localPosition.y + 1.5f, animationTime);
        transform.LeanMoveLocalY(transform.localPosition.y + 1.5f, animationTime);
        canvasGroup.LeanAlpha(0, animationTime).setEaseOutCubic().setOnComplete(() =>
        {
            gameObject.SetActive(false); 
            Destroy(gameObject);
        });
    }
}
