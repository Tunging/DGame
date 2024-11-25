using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NGUI 列表 非循环版本
/// </summary>
public class ListGrid:UIGrid
{
    private List<ListGridItem> Items = new List<ListGridItem>();
        
    public void SetInfo<T>(List<T> dataList,ListGridItem prefab)
    {
        Items.ForEach(i=>i.gameObject.SetActive(false));
            
        for (var i = 0; i < dataList.Count; i++)
        {
            ListGridItem item;
            if (i>=Items.Count)
            {
                item = Instantiate(prefab,transform,false);
                Items.Add(item);
            }
            else
            {
                item = Items[i];
            }
                
            item.gameObject.SetActive(true);
            item.SetInfo<T>(dataList[i]);
        }

        repositionNow = true;
    }
}