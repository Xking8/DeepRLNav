using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GUIEvent : MonoBehaviour, IBeginDragHandler,
IDragHandler, IEndDragHandler,
IPointerEnterHandler, IPointerExitHandler {
	public UnityEvent eventBeginDrag;
	public UnityEvent eventDrag;
	public UnityEvent eventEndDrag;
	public UnityEvent eventPointerEnter;
	public UnityEvent eventPointerExit;
	
	public void OnBeginDrag(PointerEventData ped) {
		eventBeginDrag.Invoke();
	}
	public void OnDrag(PointerEventData ped) {
		eventDrag.Invoke();
	}
	public void OnEndDrag(PointerEventData ped) {
		eventEndDrag.Invoke();
	}
	public void OnPointerEnter(PointerEventData ped) {
		eventPointerEnter.Invoke();
	}
	public void OnPointerExit(PointerEventData ped) {
		eventPointerExit.Invoke();
	}
	
}