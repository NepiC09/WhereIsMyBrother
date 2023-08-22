using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxGUI : MonoBehaviour, IDataPersistance
{
    [Header("Box Visuals")]
    [SerializeField] private GameObject boxOpened;
    [SerializeField] private GameObject boxForward;
    [SerializeField] private GameObject boxUp;
    [SerializeField] private GameObject sewingKitVisual;

    [Header("Moving Blocks")]
    [SerializeField] private Block[] blocks;
    [SerializeField] private RectTransform[] rightBlocks;
    [SerializeField] private RectTransform keyBlock;

    [Header("Inputs")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Dialogues")]
    [SerializeField] private TextAsset wrongKey;
    [SerializeField] private TextAsset rightKey;
    [SerializeField] private TextAsset openedKey;

    private bool isWin = false;
    public enum State
    {
        Front, Up, Opened
    }
    State state = State.Front;

    private void Start()
    {
        foreach(Block block in blocks)
        {
            block.onDragEnded.AddListener(CheckBlocks);
        }
        gameObject.SetActive(false);
        
        SetState(state);
    }

    public void LoadData(GameData gameData)
    {
        state = gameData.boxState;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.boxState = state;
    }

    public void OpenBoxGUI()
    {
        PlayerController.Instance.setPlayable(false);
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.LeanScale(Vector3.one, 0.25f);
        playerInput.currentActionMap.FindAction("RotateBox").performed += RotateBoxPerfomed;
        playerInput.currentActionMap.FindAction("Cancel").performed += CancelPerfomed;
        
        SetState(state);
    }

    private void SetState(State new_state)
    {
        state = new_state;
        switch (state)
        {
            case State.Front:
                boxForward.SetActive(true);
                boxOpened.SetActive(false);
                boxUp.SetActive(false);
                break;
            case State.Up:
                boxForward.SetActive(false);
                boxOpened.SetActive(false);
                boxUp.SetActive(true);
                break;
            case State.Opened:
                boxForward.SetActive(false);
                boxOpened.SetActive(true);
                boxUp.SetActive(false);
                break;
        }
    }

    public void CloseBoxGUI()
    {
        PlayerController.Instance.setPlayable(true);
        playerInput.currentActionMap.FindAction("RotateBox").performed -= RotateBoxPerfomed;
        playerInput.currentActionMap.FindAction("Cancel").performed -= CancelPerfomed;
        transform.LeanScale(Vector3.zero, 0.25f).setOnComplete(() => { gameObject.SetActive(false); });
    }

    private void CancelPerfomed(InputAction.CallbackContext o)
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;
        CloseBoxGUI();
    }

    private void CheckBlocks()
    {
        int counter = 0;
        for (int i = 0; i < blocks.Length; i++)
        {
            float yPosBlock = blocks[i].GetComponent<RectTransform>().anchoredPosition.y;
            float yPosRightBlocks = rightBlocks[i].anchoredPosition.y;

            float Exp = 15;

            if (yPosBlock - yPosRightBlocks < Exp && yPosBlock - yPosRightBlocks > -Exp)
            {
                counter++;
            }
        }
        if (counter == blocks.Length)
        {
            isWin = true;
            DialogueManager.Instance._StartDialogue(rightKey);
            DialogueManager.Instance.ContinueStory();
        } else
        {
            isWin = false;
        }
    }

    public void OnKeyPressed()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        if (isWin)
        {
            if (!LeanTween.isTweening(keyBlock))
            {
                keyBlock.LeanRotateZ(90, 0.5f).setOnComplete(() =>
                {
                    //StartDialogue And wait end
                    DialogueManager.Instance._StartDialogue(openedKey);
                    DialogueManager.Instance.ContinueStory();
                    //wait end dialogue
                    StartCoroutine(openBoxAfterDialogue());
                });
            }
        }
        else
        {
            DialogueManager.Instance._StartDialogue(wrongKey);
            DialogueManager.Instance.ContinueStory();
        }
    }

    private IEnumerator openBoxAfterDialogue()
    {
        yield return null;
        yield return new WaitWhile(() => { return DialogueManager.Instance.isDialoguePlaying; });

        yield return new WaitForSeconds(0.5f);
        SetState(State.Opened);
    }

    private void  RotateBoxPerfomed(InputAction.CallbackContext o)
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        if(boxUp.activeSelf && !boxForward.activeSelf)
        {
            SetState(State.Front);
        } 
        else if (!boxUp.activeSelf && boxForward.activeSelf)
        {
            SetState(State.Up);
        }
    }

    public void OnSewingKeyButtonPressed()
    {
        sewingKitVisual.transform.LeanScale(Vector3.zero, 0.25f).setOnComplete(() => { sewingKitVisual.gameObject.SetActive(false); });
        YellowSegment.SetValue("isSewingKitGotten", true);
        Inventory.SetValue("SewingKit", true);
    }
}
