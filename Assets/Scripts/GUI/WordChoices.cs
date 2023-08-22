using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordChoices : MonoBehaviour
{
    [SerializeField] private Word[] wordChoicesArray;
    [SerializeField] private TextMeshProUGUI choiceLabel;
    [SerializeField] private TextMeshProUGUI dialogueLabel;

    private Story currentStory;
    private List<string> wordArray;

    private List<string> choiceLetterArray = new List<string>();

    public void AddToChoiceLetterArray(Word wordButton)
    {
        wordButton.setSelected(!wordButton.isSelected);
        //change isSelected to different 

        if (wordButton.isSelected)
        {
            choiceLetterArray.Add(wordButton.tmpUGUI.text);
        }
        else
        {
            choiceLetterArray.Remove(wordButton.tmpUGUI.text);
        }

        UpdateChoiceLabel();
    }

    private void UpdateChoiceLabel()
    {
        choiceLabel.text = "";
        int i = 0;
        foreach (string word in choiceLetterArray)
        {
            if (i != 0)
                choiceLabel.text += " ";
            choiceLabel.text += word;
            i++;
        }
    }

    public void SetWordChoices(Story story)
    {
        gameObject.SetActive(true);
        currentStory = story;
        dialogueLabel.text = currentStory.currentText;
        AddToWordArray();
        UpdateVisibleOfWords();
        StartCoroutine(SelectFirstWord());
    }

    private void AddToWordArray()
    {
        wordArray = new List<string>();
        string wordStringTemp = "";
        foreach (Choice choice in currentStory.currentChoices)
        {
            wordStringTemp += choice.text + " ";
        }
        string wordTemp = "";
        for (int i = 0; i < wordStringTemp.Length; i++)
        {
            if (wordStringTemp[i] != ' ')
            {
                wordTemp += wordStringTemp[i];
            }
            else
            {
                bool isExists = false;
                foreach(string word in wordArray)
                {
                    if (word == wordTemp)
                    {
                        isExists = true;
                        break;
                    }
                }
                if (!isExists)
                {
                    wordArray.Add(wordTemp);
                }
                wordTemp = "";
            }
        }
    }

    private void UpdateVisibleOfWords()
    {
        if (wordArray.Count > wordChoicesArray.Length)
            Debug.LogError("There's more word then can be choosed");
        
        //shuffle words
        for(int i = wordArray.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i);
            string temp = wordArray[i];
            wordArray[i] = wordArray[rand];
            wordArray[rand] = temp;
        }

        for (int i = 0; i < wordChoicesArray.Length; i++)
        {
            wordChoicesArray[i].GetTmpUGUI();
        }
        for (int i = 0; i < wordChoicesArray.Length; i++)
        {
            if (i < wordArray.Count)
            {
                wordChoicesArray[i].tmpUGUI.text = wordArray[i];
                wordChoicesArray[i].gameObject.SetActive(true);
            }
            else
            {
                wordChoicesArray[i].gameObject.SetActive(false);
            }
        }
    }

    public void ResetChoice()
    {
        for (int i = 0; i < wordChoicesArray.Length; i++)
        {
            wordChoicesArray[i].setSelected(false);
        }
        choiceLetterArray.Clear();
        UpdateChoiceLabel();
    }

    public void AcceptChoice()
    {
        foreach(Choice choice in currentStory.currentChoices)
        {
            if(choiceLabel.text.Equals(choice.text))
            {
                DialogueManager.Instance.MakeChoice(choice.index);
                gameObject.SetActive(false);
                ResetChoice();
                return;
            }
        }
    }

    private IEnumerator SelectFirstWord()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(wordChoicesArray[0].gameObject);
    }
}
