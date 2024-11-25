using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 可循环版本的列表
/// </summary>
public class ListGridLoop : UIWrapContent2
{
    public int initCount = 10;
    private List<ListGridItem> Items = new List<ListGridItem>();

    private UIPanel mPanel;

    public UIPanel Panel
    {
        get
        {
            if (!mPanel)
            {
                mPanel = NGUITools.FindInParents<UIPanel>(transform);
            }

            return mPanel;
        }
    }

    /// <summary>
    /// 给定数据集合
    /// 自动分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataList"></param>
    /// <param name="prefab"></param>
    /// <param name="countPerRow">每行最大几个</param>
    public void SetInfo<T>(List<T> dataList, ListGridItem prefab, int countPerRow)
    {
        List<List<T>> _list = new List<List<T>>();
        //分组 ，每组4个；
        for (int i = 0; i < dataList.Count; i += countPerRow)
        {
            _list.Add(dataList.Skip(i).Take(countPerRow).ToList());
        }

        SetInfo(_list, prefab);
    }

    /// <summary>
    /// 分完组的数据填充
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataList">分完组的数据,可用于实现交错展示</param>
    /// <param name="prefab"></param>
    public void SetInfo<T>(List<List<T>> dataList, ListGridItem prefab)
    {
        StopAllCoroutines();
        Items.ForEach(i => i.Hide());
        this.maxIndex = 0;
        this.minIndex = -(dataList.Count - 1);
        onInitializeItem = null;

        int count = (int)(Panel.height / itemSize) + 4;

        for (var i = 0; i < count; i++)
        {
            ListGridItem item;
            if (i >= Items.Count)
            {
                item = Instantiate(prefab, transform, false);
                Items.Add(item);
            }
            else
            {
                item = Items[i];
            }

            item.gameObject.SetActive(i < dataList.Count);
        }

        {
            onInitializeItem = (go, index, realIndex) =>
            {
                var item = go.GetComponent<ListGridItemGroup>();
                if (item)
                {
//                    EditorLog.Info($"realIndex:{realIndex}");
                    go.SetActive(true);
                    int dataIndex = Mathf.Clamp(Math.Abs(realIndex), 0, dataList.Count - 1);
                    item.SetInfo2(dataList[dataIndex]);
                    item.nDataIndex = dataIndex;
                }
            };
        }
        if (dataList.Count > 0)
        {
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(DelayRefresh());   
            }
        }
    }

    private IEnumerator DelayRefresh()
    {
        yield return null;
        SortBasedOnScrollMovement();
        if (mScroll) mScroll.ResetPosition();
    }

    public void DoClick(Func<ListGridItem, bool> func)
    {
        Items.FirstOrDefault(c => func(c))?.OnClick();
    }

    public void SetItemClick(Action<ListGridItem> onMountItemClick, Action<ListGridItem> onMountItemSet)
    {
        Items.ForEach(item =>
        {
            if (item is ListGridItemGroup group)
            {
                group.prefab.ForEach(p =>
                {
                    p.onClickEvent = onMountItemClick;
                    p.onSetInfoEvent = onMountItemSet;
                });
            }
            else if (item is ListGridItem gridItem)
            {
                gridItem.onClickEvent = onMountItemClick;
                gridItem.onSetInfoEvent = onMountItemSet;
            }
        });
    }
}