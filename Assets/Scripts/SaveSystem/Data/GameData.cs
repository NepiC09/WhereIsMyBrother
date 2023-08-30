using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public CompositeCollider2D locationCompositeCollider; //collider for camera
    //QUEST, DIALOGUE, BUBBLES
    public SerializableDictionary<string, bool> dialogueGlobalVariables; //variables for quest and dialogues
    public bool[] bubblesPlayed;
    public bool questManager_canTeleport;
    //Box
    public BoxGUI.State boxState;
    //Diary
    public int diariGUI_counter;
    public bool[] diariGUI_phrasedReaded;
    public int[] diariGUI_indexPhrase;
    //PHONE
    public bool phoneGUI_isInternetChecked;
    public bool phoneGUI_isGalleryChecked;
    public bool phoneGUI_isSMSChecked;
    public bool phoneGUI_isMessangerChecked;
    public bool phoneEntry_isFisrtOpenedMainScreen;
    public bool phoneGallery_isFirstImageChecked;
    public bool phoneGallery_isSecondImageChecked;
    //PLAYER VISUAL
    public PlayerVisual.Emotion playerEmotion;
    //LITTLE BOY VISUAL
    public LittleBoyVisual.Skins littleBoySkin;
    //GAME MANAGER
    public string gameManager_language;


    public GameData()
    {
        playerPosition = new Vector3(38.1f,0,0);
        locationCompositeCollider = Fader.Instance.startLocation;
        //QUEST, DIALOGUE, BUBBLES
        dialogueGlobalVariables = new SerializableDictionary<string, bool>();
        bubblesPlayed = new bool[3];
        questManager_canTeleport = false;
        //BOX
        boxState = BoxGUI.State.Front;
        //DIARY
        diariGUI_counter = 0;
        diariGUI_phrasedReaded = new bool[6];
        diariGUI_indexPhrase = new int[6];
        //PHONE
        phoneGUI_isInternetChecked = false;
        phoneGUI_isGalleryChecked = false;
        phoneGUI_isMessangerChecked = false;
        phoneGUI_isSMSChecked = false;
        phoneEntry_isFisrtOpenedMainScreen = false;
        phoneGallery_isFirstImageChecked = false;
        phoneGallery_isSecondImageChecked = false;
        //PLAYER VISUAL
        playerEmotion = PlayerVisual.Emotion.NORMAL;
        //LITTLE BOY VISUAL
        littleBoySkin = LittleBoyVisual.Skins.HELMET;
        //GAME MANAGER
        gameManager_language = GameManager.RU_LOCALIZATION;
    }
}