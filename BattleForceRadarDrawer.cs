using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    //战力雷达组件
    public class DrawBattleForce : Graphic
    {
        [SerializeField]
        private int[] propertyArray;
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            var r = GetPixelAdjustedRect();
            var v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);

            center = r.center;
            this.r = r.width / 2;
            
            Color32 color32 = color;
            

            var p1 = DrawKeyPoint(30, propertyArray[0]);
            var p2 = DrawKeyPoint(90, propertyArray[1]);
            var p3 = DrawKeyPoint(150, propertyArray[2]);
            var p4 = DrawKeyPoint(210, propertyArray[3]);
            var p5 = DrawKeyPoint(270, propertyArray[4]);
            var p6 = DrawKeyPoint(330, propertyArray[5]);

            vh.Clear();
            vh.AddVert(new UIVertex() { position = p1, color = color32});
            vh.AddVert(new UIVertex() { position = p2, color = color32});
            vh.AddVert(new UIVertex() { position = p3, color = color32});
            vh.AddVert(new UIVertex() { position = p4, color = color32});
            vh.AddVert(new UIVertex() { position = p5, color = color32});
            vh.AddVert(new UIVertex() { position = p6, color = color32 });

            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(0,2,5);
            vh.AddTriangle(2,3,5);
            vh.AddTriangle(3,4, 5);
        }


        // private void OnGUI()
        // {
        //     if (GUILayout.Button("circle"))
        //     {
        //         DrawCircle();
        //     }
        //
        //     if (GUILayout.Button("Cube"))
        //     {
        //         
        //         
        //     }
        // }
        float r = 20;
        Vector2 center = new Vector2(0, 0);

        // void DrawCircle()
        // {
        //     StartCoroutine(AsynDrawCircle());
        // }
        //
        // IEnumerator AsynDrawCircle()
        // {
        //     float vertice = 0;
        //     while (vertice < 360)
        //     {
        //         vertice++;
        //         var rad = Mathf.Deg2Rad * vertice;
        //         var x = center.x + Mathf.Cos(rad) * r;
        //         var y = center.y + Mathf.Sin(rad) * r;
        //
        //         CreatePremitive( PrimitiveType.Sphere,new Vector3(x,y),vertice/360);
        //
        //         yield return new WaitForEndOfFrame();
        //     }
        // }
        //
        // private GameObject CreatePremitive(PrimitiveType type,Vector3 position, float colorR)
        // {
        //     var go = GameObject.CreatePrimitive(type);
        //     go.transform.position = position;
        //     
        //     go.GetComponent<Renderer>().material.color = new Color(colorR / 360, 1, 1);
        //     return go;
        // }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deg"></param>
        /// <param name="propertyValue">�ٷֱ�</param>
        Vector3 DrawKeyPoint(float deg, float propertyValue)
        {
            var rad = Mathf.Deg2Rad * deg;
            var x = center.x + Mathf.Cos(rad) * r * propertyValue/100;
            var y = center.y + Mathf.Sin(rad) * r * propertyValue/100;

            return new Vector3(x, y);// CreatePremitive(PrimitiveType.Cube, new Vector3(x, y), 1);

        }
    }
}
