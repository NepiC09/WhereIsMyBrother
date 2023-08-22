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

    //Триггерится только на Игрока, иначе ошибка
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out PlayerController pc))
        {
            Debug.LogError("Triggered not Player!!!");
        }
        collision.GetComponent<PlayerController>().AddInteractiveObject(this);
        
        playerEnter?.Invoke();
    }

    //Триггерится только на Игрока, иначе ошибка
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
