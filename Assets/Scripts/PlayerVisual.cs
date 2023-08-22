using System.Collections;
using UnityEngine;
using Spine.Unity;
using Spine;

public class PlayerVisual : MonoBehaviour, IDataPersistance
{
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField] private AnimationReferenceAsset idleLeft;
    [SerializeField] private AnimationReferenceAsset idleRight;
    [SerializeField] private AnimationReferenceAsset idleToWalkLeft;
    [SerializeField] private AnimationReferenceAsset idleToWalkRight;
    [SerializeField] private AnimationReferenceAsset walkLeft;
    [SerializeField] private AnimationReferenceAsset walkRight;

    private enum State
    {
        L_IDLE, R_IDLE,
        L_IDLE_TO_WALK, R_IDLE_TO_WALK,
        L_WALK, R_WALK
    }

    public enum Emotion { NORMAL, JOY, ANGRY, SADNESS, AMAZEMENT }
    private Emotion currentEmotion = Emotion.NORMAL;

    private string[] stringEmotions = { "default", "Anya_joy_skin", "Anya_anger_skin", "Anya_sadness_skin", "Anya_amazement_skin" };

    private State currentState = State.R_IDLE;
    private float pastDirection = -1;

    private void Start()
    {
        SetCharacterState(State.L_IDLE);
        PlayerController.Instance.onStartMove.AddListener(Player_OnStartMove);
        PlayerController.Instance.onEndMove.AddListener(Player_OnEndMove);
        SetEmotion(currentEmotion);
        //Моргание
        //StartCoroutine(BlinkCoroutine());
    }

    public void LoadData(GameData gameData) {
        SetEmotion(gameData.playerEmotion);
    }
    public void SaveData(ref GameData gameData) { 
        gameData.playerEmotion = currentEmotion;
    }

    private void Player_OnStartMove(float direction)
    {
        if(direction != pastDirection)
        {
            transform.localScale =
                new Vector3(
                    transform.localScale.x*-1,
                    transform.localScale.y,
                    transform.localScale.z
                );
        }

        pastDirection = direction;

        if (direction < 0)
        {
            SetCharacterState(State.R_WALK);
        }
        else if (direction > 0)
        {
            SetCharacterState(State.L_WALK);
        }
        else
        {
            Debug.LogError("direction is 0 on start walking");
        }
    }
    private void Player_OnEndMove()
    {
        if (pastDirection <= 0)
        {
            SetCharacterState(State.R_IDLE);
        }
        else if (pastDirection > 0)
        {
            SetCharacterState(State.L_IDLE);
        }
    }

    private void SetAnimation(AnimationReferenceAsset animation, bool isLoop, float timeScale, State state)
    {
        if (currentState == state)
        {
            return;
        }
        currentState = state;
        TrackEntry trackEntry = skeletonAnimation.state.SetAnimation(0, animation, isLoop);
        trackEntry.TimeScale = timeScale;
        //пропуск моргания
        //if (state == State.L_IDLE || state == State.R_IDLE)
        //{
        //    trackEntry.AnimationStart = trackEntry.TrackEnd;
        //}
    }

    private void SetCharacterState(State state)
    {
        //IDLE
        if (state.Equals(State.L_IDLE))
        {
            SetAnimation(idleLeft, false, 1f, state);
        }
        if (state.Equals(State.R_IDLE))
        {
            SetAnimation(idleRight, false, 1f, state);
        }
        //WALK
        if (state.Equals(State.L_WALK))
        {
            SetAnimation(walkLeft, true, 1f, state);
        }
        if (state.Equals(State.R_WALK))
        {
            SetAnimation(walkRight, true, 1f, state);
        } 
    }

    private void SetEmotion(Emotion emo)
    {
        skeletonAnimation.skeleton.SetSkin(stringEmotions[(int) emo]);
        skeletonAnimation.skeleton.SetSlotsToSetupPose();
        currentEmotion = emo;
    }

    private IEnumerator BlinkCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(0.33f, 10f));
        if (currentState == State.L_IDLE)
        {
            TrackEntry trackEntry = skeletonAnimation.state.SetAnimation(0, idleLeft, false);
            trackEntry.TimeScale = 1f;
        }
        else if (currentState == State.R_IDLE)
        {
            TrackEntry trackEntry = skeletonAnimation.state.SetAnimation(0, idleRight, false);
            trackEntry.TimeScale = 1f;
        }
        StartCoroutine(BlinkCoroutine());
    }
}