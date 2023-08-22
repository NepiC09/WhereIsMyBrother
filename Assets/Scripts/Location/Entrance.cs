using UnityEngine;

public class Entrance : MonoBehaviour
{

    [SerializeField] private LittleBoy littleBoy;
    [SerializeField] private GameObject dayBackground;
    [SerializeField] private GameObject eveningBackground;
    public void _SetDayOrEvening()
    {
        if (GreenSegment.GetValue("isSlept"))
        {
            dayBackground.SetActive(true);
            eveningBackground.SetActive(false);
            littleBoy.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            dayBackground.SetActive(false);
            eveningBackground.SetActive(true);
            littleBoy.transform.parent.gameObject.SetActive(true);
        }
    }
}
