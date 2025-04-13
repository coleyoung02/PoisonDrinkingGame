using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class PoisonButtons : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Hi");
    }
}
