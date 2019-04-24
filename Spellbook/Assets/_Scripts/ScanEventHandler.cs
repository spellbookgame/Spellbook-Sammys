using UnityEngine;
using UnityEngine.EventSystems;
using Vuforia;

public class ScanEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private void Start()
    {
        VuforiaBehaviour.Instance.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        VuforiaBehaviour.Instance.enabled = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        VuforiaBehaviour.Instance.enabled = false;
    }
}
