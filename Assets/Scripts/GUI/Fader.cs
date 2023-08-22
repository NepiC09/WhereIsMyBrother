using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fader : MonoBehaviour, IDataPersistance
{
    public static Fader Instance {  get; private set; }

    [SerializeField] private Cinemachine.CinemachineConfiner cinemachineConfiner;
    [SerializeField] public CompositeCollider2D startLocation;

    [SerializeField] private Color fadedColor;// = new Color(31 / 255f, 31 / 255f, 31 / 255f, 1f);
    [SerializeField] private Color unFadedColor;// = new Color(31 / 255f, 31 / 255f, 31 / 255f, 0f);

    private CanvasGroup canvasGroup;
    private CompositeCollider2D currentCollider;

    private Image image;

    private void Awake()
    {
        Instance = this;
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        cinemachineConfiner.m_BoundingShape2D = startLocation;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        FadeOut(1f);
    }

    public void LoadData(GameData gameData)
    {
        currentCollider = gameData.locationCompositeCollider;
        cinemachineConfiner.m_BoundingShape2D = currentCollider;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.locationCompositeCollider = currentCollider;
    }

    public void FadeIn(float timeToFadeIn)
    {
        canvasGroup.LeanAlpha(1, timeToFadeIn);
    }

    public void FadeOut(float timeToFadeOut)
    {
        gameObject.SetActive(true);
        canvasGroup.LeanAlpha(0, timeToFadeOut);
    }

    public void FadeInOut(float timeToFadeIn, float timeToWait, float timeToFadeOut, UnityAction action = null)
    {
        PlayerController.Instance.setPlayable(false);

        canvasGroup.LeanAlpha(1f, timeToFadeIn).setOnComplete(() =>
        {
            if (action != null) action();

            canvasGroup.LeanAlpha(1f, timeToWait).setOnComplete(() =>
            {
                PlayerController.Instance.setPlayable(true);
                canvasGroup.LeanAlpha(0f, timeToFadeOut).setOnComplete(() =>
                {
                    //nothing yet
                });
            });
        });
    }

    public void Teleport(Transform placeToTeleport, float timeToFadeIn, float timeToWait, float timeToFadeOut, CompositeCollider2D compositeCollider2D)
    {
        PlayerController.Instance.setPlayable(false);

        //FADE IN
        canvasGroup.LeanAlpha(1f, timeToFadeIn).setOnComplete(() =>
        {
            //FADE IN COMPLETED

            //confinding camera
            currentCollider = compositeCollider2D;
            cinemachineConfiner.m_BoundingShape2D = compositeCollider2D;
            //teleport
            Vector3 teleportPosition = new Vector3(placeToTeleport.position.x, placeToTeleport.position.y, 0);
            PlayerController.Instance.transform.position = teleportPosition;
            
            //WAIT TIME
            canvasGroup.LeanAlpha(1f, timeToWait).setOnComplete(() =>
            {
                //WAIT TIME COMPLETED

                //Giving a control to player
                if (DialogueManager.Instance.isDialoguePlaying != true)
                {
                    PlayerController.Instance.setPlayable(true);
                }

                //FADE OUT
                canvasGroup.LeanAlpha(0f, timeToFadeOut).setOnComplete(() =>
                {
                    //FADE OUT COMPLETED
                });
            });
        });
    }

    

    public bool isUnFaded()
    {
        return canvasGroup.alpha == 0;
        //return image.color == unFadedColor;
    }
    public bool isFaded()
    {
        return canvasGroup.alpha == 1;
        //return image.color == unFadedColor;
    }
}
