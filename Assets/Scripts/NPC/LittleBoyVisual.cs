using System.Collections;
using UnityEngine;
using Spine.Unity;
using Spine;

public class LittleBoyVisual : MonoBehaviour, IDataPersistance
{
    [Header("Skeleton animation reference")]
    [SerializeField] private SkeletonAnimation skeletonAnimation;

    [Header("Animation References")]
    [SerializeField] private AnimationReferenceAsset lookLeftAnim;
    [SerializeField] private AnimationReferenceAsset lookForwardAnim;
    [SerializeField] private AnimationReferenceAsset lookRightAnim;
    [SerializeField] private AnimationReferenceAsset inhaleAnim;
    [SerializeField] private AnimationReferenceAsset shoulderAnim;

    public enum Skins
    {
        DEFAULT, ANGRY, HELMET, SMILE
    }
    Skins currentSkin = Skins.HELMET;
    string[] stringSkins = { "default", "angry", "helmet", "smile"};

    public enum LookTo
    {
        LEFT, FORWARD, RIGHT
    }
    LookTo currentLook = LookTo.LEFT;

    private void Start()
    {
        SetCharacterLook(LookTo.FORWARD);
        SetSkin(currentSkin);
        StartCoroutine(BlinkCoroutine(3, 9));
        StartCoroutine(Inhale_Shoulder_Coroutine(5, 10));
    }

    public void LoadData(GameData gameData)
    {
        SetSkin(gameData.littleBoySkin);
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.littleBoySkin = currentSkin;
    }

    private void SetAnimation(AnimationReferenceAsset animation, bool isLoop, float timeScale, LookTo lookTo)
    {
        if (currentLook == lookTo)
        {
            return;
        }
        currentLook = lookTo;
        TrackEntry trackEntry = skeletonAnimation.state.SetAnimation(0, animation, isLoop);
        trackEntry.TimeScale = timeScale;
    }

    public void SetCharacterLook(LookTo lookTo)
    {
        //IDLE
        if (lookTo.Equals(LookTo.LEFT))
        {
            SetAnimation(lookLeftAnim, false, 1f, lookTo);
        }
        if (lookTo.Equals(LookTo.RIGHT))
        {
            SetAnimation(lookRightAnim, false, 1f, lookTo);
        }
        if (lookTo.Equals(LookTo.FORWARD))
        {
            SetAnimation(lookForwardAnim, false, 1f, lookTo);
        }
    }

    public void SetSkin(Skins skin)
    {
        skeletonAnimation.skeleton.SetSkin(stringSkins[(int)skin]);
        skeletonAnimation.skeleton.SetSlotsToSetupPose();
        currentSkin = skin;
    }

    private IEnumerator Inhale_Shoulder_Coroutine(float timeToWaitLeft, float timeToWaitRight) 
    {
        bool switchAnim = true;
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(timeToWaitLeft, timeToWaitRight));
            TrackEntry trackEntry;
            if (switchAnim)
            {
                trackEntry = skeletonAnimation.state.AddAnimation(0, shoulderAnim, false, 0);
                trackEntry.TimeScale = 0.5f;
                switchAnim = false;
            }
            else
            {
                trackEntry = skeletonAnimation.state.AddAnimation(0, inhaleAnim, false, 0);
                trackEntry.TimeScale = 0.8f;
                switchAnim = true;
            }
            trackEntry.Complete += inhale_shoulder_Complete;
        }
    }

    private void inhale_shoulder_Complete(TrackEntry trackEntry)
    {
        Blink(0.05f);
    }

    private IEnumerator BlinkCoroutine(float timeToWaitLeft, float timeToWaitRight)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(timeToWaitLeft, timeToWaitRight));
            Blink(0.05f);
        }
    }

    private void Blink(float mixDuration)
    {
        TrackEntry trackEntry;
        if (currentLook == LookTo.LEFT)
        {
            trackEntry = skeletonAnimation.state.AddAnimation(0, lookLeftAnim, false, 0);
        }
        else if (currentLook == LookTo.FORWARD)
        {
            trackEntry = skeletonAnimation.state.AddAnimation(0, lookForwardAnim, false, 0);
        }
        else
        {
            trackEntry = skeletonAnimation.state.AddAnimation(0, lookRightAnim, false, 0);
        }

        trackEntry.TimeScale = 0.8f;
        trackEntry.MixDuration = mixDuration;
    }
}