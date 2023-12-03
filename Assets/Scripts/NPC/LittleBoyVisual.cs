using System.Collections;
using UnityEngine;
using Spine.Unity;
using Spine;

public class LittleBoyVisual : MonoBehaviour, IDataPersistance
{
    [Header("PlayerAnimationSetting")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject helmet;

    public enum LookTo
    {
        LEFT, FORWARD, RIGHT
    }
    LookTo currentLook = LookTo.FORWARD;

    public enum Mood
    {
        NEUTRAL, JOY, SAD
    }
    Mood mood = Mood.NEUTRAL;

    private bool hasHelmet = true;

    private void Start()
    {
        SetCharacterLook(LookTo.FORWARD);
        SetMood(Mood.NEUTRAL);
    }

    public void LoadData(GameData gameData)
    {
        hasHelmet = gameData.hasHelmet;
        
        helmet.SetActive(hasHelmet);
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.hasHelmet = hasHelmet;
    }

    public void SetHelmet(bool isLoosing)
    {
        helmet.SetActive(!isLoosing);
    }

    public void SetCharacterLook(LookTo lookTo)
    {
        if (lookTo.Equals(LookTo.LEFT))
        {
            animator.SetInteger("Side", 0);
        }
        if (lookTo.Equals(LookTo.RIGHT))
        {
            animator.SetInteger("Side", 2);
        }
        if (lookTo.Equals(LookTo.FORWARD))
        {
            animator.SetInteger("Side", 1);
        }
    }

    public void SetMood(Mood newMood)
    {
        if (newMood.Equals(Mood.NEUTRAL))
        {
            animator.SetInteger("Mood", 0);
        } 
        else if (newMood.Equals(Mood.SAD))
        {
            animator.SetInteger("Mood", 2);
        } 
        else if (newMood.Equals(Mood.JOY))
        {
            animator.SetInteger("Mood", 1);
        }
    }
}