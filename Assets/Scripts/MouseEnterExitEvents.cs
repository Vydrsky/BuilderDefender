using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MouseEnterExitEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    /************************ FIELDS ************************/

    public event EventHandler OnMouseEnter;
    public event EventHandler OnMouseExit;

    /************************ INITIALIZE ************************/
    private void Awake() {
        
    }

    private void Start() {
        
    }

    /************************ LOOPING ************************/
    private void Update() {
        
    }

    /************************ METHODS ************************/

    public void OnPointerEnter(PointerEventData eventData) {
        OnMouseEnter?.Invoke(this,EventArgs.Empty);
    }

    public void OnPointerExit(PointerEventData eventData) {
        OnMouseExit?.Invoke(this, EventArgs.Empty);
    }
}
