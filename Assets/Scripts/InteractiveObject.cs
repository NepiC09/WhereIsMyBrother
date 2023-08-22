using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    public UnityEvent OnInteract;
    public UnityEvent playerEnter;
    public UnityEvent playerExit;

    public void Interact()
    {
        OnInteract?.Invoke();
    }

    //����������� ������ �� ������, ����� ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out PlayerController pc))
        {
            Debug.LogError("Triggered not Player!!!");
        }
        collision.GetComponent<PlayerController>().AddInteractiveObject(this);
        
        playerEnter?.Invoke();
    }

    //����������� ������ �� ������, ����� ������
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out PlayerController pc))
        {
            Debug.LogError("Triggered not Player!!!");
        }
        collision.GetComponent<PlayerController>().RemoveInteractiveObject(this);

        playerExit?.Invoke();
    }

    public void RemoveFromInteractiveArray()
    {
        PlayerController.Instance.GetComponent<PlayerController>().RemoveInteractiveObject(this);
    }
}
