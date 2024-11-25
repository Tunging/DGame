using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NGUI列表项
/// 用于同行需要显示多个ListGridItem的时候给ListGridLoop用
/// ListGrid也可以用,但是没有必要
/// </summary>
[RequireComponent(typeof(UITable))]
public class  ListGridItemGroup:ListGridItem
{
    public List<ListGridItem> prefab;
    private UITable table;
    private void Awake()
    {
        table = GetComponent<UITable>();
        table.keepWithinPanel = false;
        table.hideInactive = true;
    }

    public virtual void SetInfo2<T>(List<T> data)
    {
        prefab.ForEach(p => p.Reset());
        for (var i = 0; i < data.Count; i++)
        {
            var obj = prefab[i];
            obj.SetInfo(data[i]);
            obj.gameObject.name = i.ToString();
            obj.gameObject.SetActive(true);
        }
        table.repositionNow = true;
    }

    public override void Hide()
    {
        base.Hide();
        prefab.ForEach(p => p.gameObject.SetActive(false));
    }
}