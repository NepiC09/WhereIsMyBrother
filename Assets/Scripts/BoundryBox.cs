using UnityEngine;

public class BoundryBox : MonoBehaviour
{
    EdgeCollider2D edge;
    private Camera cam;
    private float width;
    private float height;

    private void Awake()
    {
        cam = Camera.main;
        edge = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        FoundBoundries();
        SetBoundPoints();
    }

    private void SetBoundPoints()
    {
        Vector2[] pointArray = {
            new Vector2(width/2,height/2),
            new Vector2(width/2,-height/2),
            new Vector2(-width/2,-height/2),
            new Vector2(-width/2,height/2),
            new Vector2(width/2,height/2),
        };

        edge.points = pointArray;
    }

    private void FoundBoundries()
    {
        Vector3 upRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight));
        Vector3 downLeft = cam.ScreenToWorldPoint(Vector3.zero);
        width = upRight.x - downLeft.x;
        height = upRight.y - downLeft.y;
    }
}
