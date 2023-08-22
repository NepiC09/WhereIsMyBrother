using UnityEngine;
using UnityEngine.UI;

public class GalleryScreen : MonoBehaviour, IDataPersistance
{
    [SerializeField] private GameObject photoPlace;
    [SerializeField] private GameObject spritePlace;

    private bool isImageOpened = false;

    private Image image;
    private bool isFirstImageChecked = false;
    private bool isSecondImageChecked = false;

    private void Start()
    {
        image = spritePlace.GetComponent<Image>();
    }
    public void LoadData(GameData gameData)
    {
        isFirstImageChecked = gameData.phoneGallery_isFirstImageChecked;
        isSecondImageChecked = gameData.phoneGallery_isSecondImageChecked;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.phoneGallery_isFirstImageChecked = isFirstImageChecked;
        gameData.phoneGallery_isSecondImageChecked = isSecondImageChecked;
    }

    public void OpenGalleryScreen()
    {
        gameObject.SetActive(true);
        photoPlace.SetActive(false);
        isImageOpened = false;
    }

    public void _LeftButtonPressed()
    {
        if (!isImageOpened)
        {
            MoveToMainScreen();
        } else
        {
            CloseImage();
        }
    }

    public void _RightButtonPressed()
    {
        MoveToMainScreen();
    }
    private void MoveToMainScreen()
    {
        PhoneGUI.Instance.OpenMainScreen();
        gameObject.SetActive(true);

        transform.LeanMoveLocalX(-500, 0.1f).setOnComplete(() => {
            gameObject.SetActive(false);
        });
    }

    public void _OpenImage(Sprite sprite)
    {
        isImageOpened = true;
        photoPlace.transform.localScale = Vector3.zero;
        photoPlace.SetActive(true);
        photoPlace.transform.LeanScale(Vector3.one, 0.2f);
        image.sprite = sprite;
    }

    private void CloseImage()
    {
        isImageOpened = false;
        photoPlace.transform.LeanScale(Vector3.zero, 0.2f).setOnComplete(() =>{
            photoPlace.SetActive(false);
        });
    }

    public void _OpenFirstPicture()
    {
        isFirstImageChecked = true;
        DiaryUpdate();
    }
    public void _OpenSecondPicture()
    {
        isSecondImageChecked = true;
        DiaryUpdate();
    }

    private void DiaryUpdate()
    {
        if (PhoneGUI.Instance.isGalleryChecked)
            return;

        if(isFirstImageChecked && isSecondImageChecked)
        {
            PhoneGUI.Instance.isGalleryChecked = true;
            if (PhoneGUI.Instance.isInternetChecked)
            {
                Diary.SetValue("BadEquip", true);
            }
        }
    }
}
