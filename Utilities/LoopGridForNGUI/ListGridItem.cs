using System;
using System.Collections.Generic;

/// <summary>
/// NGUI 列表项
/// </summary>
public class ListGridItem:UIUnit
{
    public Action<ListGridItem> onClickEvent;
    public Action<ListGridItem> onSetInfoEvent;
    public int nDataIndex;

    public void SetInfo<T>(T data)
    {
        OnSetInfo(data);
        onSetInfoEvent?.Invoke(this);
    }

    public virtual void OnSetInfo<T>(T data)
    {
        
    }

    public virtual void Reset()
    {
        
    }

    /// <summary>
    /// 隐藏物体用
    /// </summary>
    public virtual void Hide()
    {
        
    }

    public virtual void OnClick()
    {
        onClickEvent?.Invoke(this);       
    }
}