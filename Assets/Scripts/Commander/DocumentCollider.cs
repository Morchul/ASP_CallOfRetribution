using UnityEngine;

public class DocumentCollider : MonoBehaviour
{
    private Document document;
    private void Awake()
    {
        document = GetComponentInParent<Document>();
    }

    private void OnMouseDown()
    {
        document.OnMouseDown();
    }

    private void OnMouseDrag()
    {
        document.OnMouseDrag();
    }

    private void OnMouseUp()
    {
        document.OnMouseUp();
    }
}
