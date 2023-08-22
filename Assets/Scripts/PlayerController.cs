using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDataPersistance
{
    public static PlayerController Instance { get; private set; }

    public UnityEvent<float> onStartMove;
    public UnityEvent onEndMove;

    [SerializeField] private float moveSpeed = 1f;

    private List<InteractiveObject> interactiveObjectArray = new List<InteractiveObject>();
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool isPlayable = true;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HandledMove();
    }
    
    public void LoadData(GameData gameData)
    {
        this.transform.position = gameData.playerPosition;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.playerPosition = this.transform.position;
    }

    void HandledMove(){
        float moveDistance = moveSpeed * Time.deltaTime;
        rb.MovePosition(new Vector3(moveDistance * direction.x, 0, 0) + transform.position);

        if(direction.x != 0)
        {
            onStartMove?.Invoke(direction.x);
        }
        else
        {
            onEndMove?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!isPlayable)
            return;
        
        direction = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
            return;
        if (!isPlayable)
            return;

        if (interactiveObjectArray.Count > 0)
        {
            interactiveObjectArray[0]?.Interact();
        }
    }

    public void AddInteractiveObject(InteractiveObject interactiveObject)
    {
        interactiveObjectArray.Add(interactiveObject);
    }

    public void RemoveInteractiveObject(InteractiveObject interactiveObject)
    {
        if (interactiveObjectArray.Contains(interactiveObject))
        {
            interactiveObjectArray.Remove(interactiveObject);
        }
    }

    public bool isFirstInteractiveObject(InteractiveObject interactiveObject)
    {
        return interactiveObject == interactiveObjectArray[0];
    }

    public void setPlayable(bool playable)
    {
        direction = Vector2.zero;
        isPlayable = playable;
    }
}
