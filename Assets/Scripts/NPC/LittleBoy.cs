using UnityEngine;

public class LittleBoy : MonoBehaviour
{
    public static LittleBoy Instance { get; private set; }

    [SerializeField] private LittleBoyVisual littleBoyVisual;

    [SerializeField] private TextAsset talkWithLittleBoy;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    bool isPlayerEnter = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        indicator.SetActive(false);
    }

    private void Update()
    {
        if(isPlayerEnter)
        {
            //using global pos
            float distanceToPlayer = PlayerController.Instance.transform.position.x - transform.position.x;
            //when little boy should look forward
            float middlePos = 0.15f;
            //player right
            if (distanceToPlayer >= middlePos)
            {
                littleBoyVisual.SetCharacterLook(LittleBoyVisual.LookTo.RIGHT);
            } 
            //player midle
            else if (distanceToPlayer > -middlePos && distanceToPlayer < middlePos)
            {
                littleBoyVisual.SetCharacterLook(LittleBoyVisual.LookTo.FORWARD);
            } 
            //player left
            else if(distanceToPlayer < -middlePos)
            {
                littleBoyVisual.SetCharacterLook(LittleBoyVisual.LookTo.LEFT);
            }
        }
    }

    public void _ShowIndicator()
    {
        indicator.SetActive(true);
        isPlayerEnter = true;
    }
    public void _HideIndicator()
    {
        indicator.SetActive(false);
        isPlayerEnter = false;
        littleBoyVisual.SetCharacterLook(LittleBoyVisual.LookTo.FORWARD);
    }

    public void _Interact()
    {
        DialogueManager.Instance._StartDialogue(talkWithLittleBoy);
    }

    public void LooseHelmet()
    {
        littleBoyVisual.SetHelmet(true);
        //littleBoyVisual.SetSkin(LittleBoyVisual.Skins.DEFAULT);
    }
}
