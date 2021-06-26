using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonDeselect : MonoBehaviour,IDeselectHandler
{
    public MousePlacesObjects mouseScript;

    public void OnDeselect(BaseEventData eventData)
    {
        mouseScript.UnsetPlacingObject();
        Debug.Log("Deselected Obj");
    }

}
