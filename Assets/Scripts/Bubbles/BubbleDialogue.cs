using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using UnityEngine;

public class BubbleDialogue : MonoBehaviour, IDataPersistance
{
    [Header("Story JSON")]
    [SerializeField] private TextAsset bubbleStory;

    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;
    [SerializeField] private float timeBetweenBubbles = 3f;
    [SerializeField] private Transform startTransform;
    private Vector2 startPosition;

    [Header("Points")]
    [SerializeField] private Transform[] pointsToBubbles;

    [Header("Bubbles")]
    [SerializeField] private SingleBubble bubblePrefab;
    [SerializeField] private Transform bubbleParent;

    private CanvasGroup canvasGroup;
    private int id = 0;
    private static int static_id = 0;

    private Story currentStory;

    private List<SingleBubble> bubbleList;

    public bool isStoryPlayed;

    public static bool isBubblePlaying = false;

    private void Awake()
    {
        isStoryPlayed = false;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        startPosition = new Vector2(startTransform.position.x, startTransform.position.y);
        bubbleList = new List<SingleBubble>();
        HideAll();
        id = static_id;
        static_id++;
    }

    public void LoadData(GameData gameData)
    {
        isStoryPlayed = gameData.bubblesPlayed[id];
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.bubblesPlayed[id] = isStoryPlayed;
    }

    private void Update()
    {
        float xViewport = Camera.main.WorldToViewportPoint(startPosition).x;
        if (xViewport >= 0 && xViewport <= 1) {
            transform.position = Vector3.Lerp(transform.position, startPosition, 0.05f);
        }
        else if (xViewport < 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Camera.main.ViewportToWorldPoint(new Vector2(0.15f, 0)).x, transform.position.y, transform.position.z), 0.05f);
        } else if(xViewport > 1)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Camera.main.ViewportToWorldPoint(new Vector2(0.85f, 0)).x, transform.position.y, transform.position.z), 0.05f);
        }
    }

    public void _StartDialogue()//TextAsset inkJSON)
    {
        if (isStoryPlayed)
            return;
        if (isBubblePlaying)
            return;

        isBubblePlaying = true;
        canvasGroup.alpha = 1;
;       currentStory = new Story(bubbleStory.text);
        currentStory.ChoosePathString(GameManager.Instance.GetLanguage());
        StartCoroutine(StoryProcess());
    }

    private IEnumerator StopBubbleStory()
    {
        yield return new WaitForSeconds(0.1f);
        foreach(SingleBubble bubble in bubbleList)
        {
            bubble.SelfDestroy();
        }
        bubbleList.Clear();
        isStoryPlayed = true;
        isBubblePlaying = false;
        HideAll();
    }

    private void HideAll()
    {
        canvasGroup.alpha = 0;
    }

    private IEnumerator StoryProcess()
    {
        yield return null;
        while (currentStory.canContinue)
        {
            //next line
            currentStory.Continue();
            
            if (currentStory.currentText.Length == 0)
                break;

            //Moving up bubbles and delte first
            if (bubbleList.Count() == pointsToBubbles.Count())
            {
                int countOfBubbles = bubbleList.Count();
                for(int i = 0; i < countOfBubbles; i++)
                {
                    if (i == 0)
                    {
                        SingleBubble bubble = bubbleList[i]; 
                        bubbleList.RemoveAt(i);
                        bubble.SelfDestroy();
                    }
                    else
                    {
                        bubbleList[i-1].SetTargetPosition(pointsToBubbles[i-1]);
                    }
                }
            }

            bubbleList.Add(Instantiate(bubblePrefab, bubbleParent));
            HandleTags(currentStory.currentTags, bubbleList.Last());
            bubbleList.Last().SetBubbleText(currentStory.currentText, typingSpeed);
            bubbleList.Last().SetSpawnDialogueTransform(startPosition, pointsToBubbles[bubbleList.Count() - 1].position);
            bubbleList.Last().SetTargetPosition(pointsToBubbles[bubbleList.Count() - 1]);
            yield return new WaitUntil(() => { return bubbleList.Last().isAllTextShowed; });
            yield return new WaitForSeconds(timeBetweenBubbles);
        }
        StartCoroutine(StopBubbleStory());
    }

    private void HandleTags(List<string> currentTags, SingleBubble bubble)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be approriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            //Methods with tags
            switch (tagKey)
            {
                case "form":
                    bubble.SetBubbleForm(tagValue);
                    break;
                case "color":
                    bubble.SetFormColor(tagValue);
                    break;
                default:
                    Debug.LogWarning("This tag is not currently handled: " + tag);
                    break;
            }
        }
    }
}
