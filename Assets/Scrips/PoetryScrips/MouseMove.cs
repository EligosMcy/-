using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseMove: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 pos;                            //控件初始位置
    private Vector2 mousePos;                       //鼠标初始位置（画布空间）
    //private Vector3 mouseWorldPos;                  //鼠标初始位置（世界空间）
    private RectTransform canvasRec;                //控件所在画布
    private void Start()
    {
        canvasRec = this.GetComponentInParent<Canvas>().transform as RectTransform;
    }
    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        //控件所在画布空间的初始位置
        pos = this.GetComponent<RectTransform>().anchoredPosition;
        Camera camera = eventData.pressEventCamera;
        //将屏幕空间鼠标位置eventData.position转换为鼠标在画布空间的鼠标位置
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRec, eventData.position, camera, out mousePos);
    }
    //拖拽过程中
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newVec = new Vector2();
        Camera camera = eventData.pressEventCamera;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRec, eventData.position, camera, out newVec);
        //鼠标移动在画布空间的位置增量
        Vector3 offset = new Vector3(newVec.x - mousePos.x, newVec.y - mousePos.y, 0);
        //原始位置增加位置增量即为现在位置
        (this.transform as RectTransform).anchoredPosition = pos + offset;

    }
    //结束拖拽
    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
