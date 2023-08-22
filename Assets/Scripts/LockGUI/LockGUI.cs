using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockGUI : MonoBehaviour
{
    [Header("Dialogues")]
    [SerializeField] private TextAsset startDialogue;
    [SerializeField] private TextAsset winDialogue;

    [Header("Visual")]
    [SerializeField] private RectTransform lockVisual;
    [SerializeField] private RectTransform leftStick;
    [SerializeField] private GameObject bar;

    [Header("Game steps")]
    [SerializeField] private GameObject[] stepList;
    [SerializeField] private GameObject[] areaList;

    [Header("Stick")]
    [SerializeField] private RightStick rightStick;

    [Header("Params")]
    [SerializeField] private float startSpeed;
    [SerializeField] private float speedStep;
    [SerializeField] private float speedMultiply;

    private int currentStep;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenLockGUI()
    {
        PlayerController.Instance.setPlayable(false);
        gameObject.SetActive(true);
        gameObject.transform.localScale = Vector3.zero;
        gameObject.LeanScale(Vector3.one, 0.25f);
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        DialogueManager.Instance._StartDialogue(startDialogue);
        yield return new WaitUntil(()=> { return !DialogueManager.Instance.isDialoguePlaying; });
        setStartStep();
        rightStick.onSuccessTap.AddListener(onSuccessTap);
        rightStick.onFailTap.AddListener(onFailTap);
        rightStick.SetSpeedOfRotate(startSpeed);
        rightStick.StartRotate(true);
    }

    private void setStartStep()
    {
        currentStep = 0;
        stepList[0].SetActive(true);
        for (int i = 1; i < stepList.Length; i++)
        {
            stepList[i].SetActive(false);
        }
        for (int i = 1; i < areaList.Length; i++)
        {
            areaList[i].SetActive(false);
        }
    }

    private void onSuccessTap()
    {
        if (currentStep != 5)
        {
            if (areaList[currentStep * 2].GetComponent<CanvasGroup>().alpha == 0 && areaList[currentStep*2+1].GetComponent<CanvasGroup>().alpha == 1)
            {
                areaList[currentStep * 2 + 1].SetActive(true);
                areaList[currentStep * 2 + 1].GetComponent<CanvasGroup>().LeanAlpha(1f, 0.25f);
            }

            if (!areaList[currentStep * 2].activeSelf && !areaList[currentStep * 2 + 1].activeSelf)
            {
                stepList[currentStep].SetActive(false);
                stepList[currentStep + 1].SetActive(true);

                lockVisual.LeanRotateZ(lockVisual.rotation.eulerAngles.z - 15f, 0.25f);
                rightStick.leftAngle += 5;
                rightStick.rightAngle -= 2.5f;
                leftStick.LeanRotateZ(leftStick.rotation.eulerAngles.z + 2.5f, 0.25f);

                currentStep++;

                LeanTween.scale(gameObject, Vector3.one, 0.1f).setOnComplete(() =>
                {
                    setActiveCurrentAreas();
                });

                if (speedStep != 0)
                {
                    rightStick.SetSpeedOfRotate(rightStick.speedOfRotate + speedStep);
                } else if (speedMultiply != 0)
                {
                    rightStick.SetSpeedOfRotate(rightStick.speedOfRotate* speedMultiply);
                }
            }
        }
        else
        {
            if (!areaList[currentStep * 2].activeSelf)
            {
                rightStick.leftAngle += 5;
                rightStick.rightAngle -= 2.5f;
                lockVisual.LeanRotateZ(lockVisual.rotation.eulerAngles.z - 15f, 0.25f);
                leftStick.LeanRotateZ(leftStick.rotation.eulerAngles.z + 2.5f, 0.25f);

                StartCoroutine(WinCoroutine());
            }
        }
    }

    private IEnumerator WinCoroutine()
    {
        rightStick.StopGame();
        bar.LeanAlpha(0, 0.25f);
        DialogueManager.Instance._StartDialogue(winDialogue);
        DialogueManager.Instance.ContinueStory();
        yield return new WaitUntil(() => { return !DialogueManager.Instance.isDialoguePlaying; });
        gameObject.LeanScale(Vector3.zero, 0.25f).setOnComplete(() => { gameObject.SetActive(false); });
        GreenSegment.SetValue("isMailBoxHacked", true);
        PlayerController.Instance.setPlayable(true);
    }

    private void onFailTap()
    {
        setActiveCurrentAreas();
    }

    private void setActiveCurrentAreas()
    {
        areaList[currentStep * 2].SetActive(true);
        areaList[currentStep * 2].GetComponent<CanvasGroup>().LeanAlpha(1f, 0.25f);
        if (currentStep != 5)
        {
            areaList[currentStep * 2 + 1].SetActive(false);
            areaList[currentStep * 2 + 1].GetComponent<CanvasGroup>().LeanAlpha(1f, 0.25f);
        }
    }
}
