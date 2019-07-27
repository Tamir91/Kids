/**
 * RealTimeInsertItemExample.cs
 *
 * @author mosframe / https://github.com/mosframe
 *
 */

namespace Mosframe {

    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    
    /// <summary>
    /// RealTimeInsertItemExample
    /// </summary>
    public class RealTimeInsertItemExample : BubbleElement {

        public static RealTimeInsertItemExample I;

        public int deleteIndex;
        public int editIndex;

        public class CustomData {

            public string   name;
            public string   value;
            public bool     on;
        }

        public List<CustomData>     data = new List<CustomData>();
        public List<Kid>            Kids;

        public DynamicScrollView    scrollView;

        public InputField           indexInput;
        public InputField           titleInput;
        public InputField           valueInput;
        public Button               insertButton;


        private void Awake () {
            I = this;
        }

        private void Start() {
            this.insertButton.onClick.AddListener( this.OnClick_InsertButton );
        }

        public void SetKidsInList(List<Kid> Kids)
        {
            this.Kids = new List<Kid>(Kids);
        }

        public void insertItem( int index, CustomData data ) {
            I.data.Insert( index, data );

            scrollView.totalItemCount = I.data.Count;
        }

        public void InsertKidToView(int index, string kidData)
        {
            insertItem(index, new CustomData { name = kidData, value = "", on = true });
        }

        public void RemoveItem(int index)
        {
            Debug.Log("RemoveItem" + index);
            deleteIndex = index;
            App.View.CabinetView.ShowDeleteDialog();
        }

        public void EditItem(int index)
        {
            Debug.Log("EditItem " + index);
            editIndex = index;

            App.Notify(BubbleNotification.EditKidByNumber, this);
        }

        public void DeleteAllItems()
        {
            data.Clear();
            Kids.Clear();
            scrollView.totalItemCount = I.data.Count;
        }

        public void OnDeleteClicked()
        {
            App.View.CabinetView.HideDeleteDialog();
            App.Notify(BubbleNotification.DeleteKidByNumber, this);
        }

        public void DeleteKidFromLists()
        {
            if(data.Count == 0 || Kids.Count == 0)
            {
                Debug.Log("---Error---");
                Debug.Log("DeleteKidFromLists::Can't delete kid!!");
                Debug.Log("DeleteKidFromLists::Can't delete kid!!");
                Debug.Log("RealTimeInsertItemExample::data length = " + data.Count);
                Debug.Log("RealTimeInsertItemExample:: Kids length = " + Kids.Count);
                Debug.Log("---Error---");
            }
            else
            {
                I.data.RemoveAt(deleteIndex);//Delete from dataList
                I.Kids.RemoveAt(deleteIndex);//Delete from List
                scrollView.totalItemCount = I.data.Count;
            }
            
        }

        public void OnDenyDeletingClicked()
        {
            App.View.CabinetView.HideDeleteDialog();
        }

        public void OnClick_InsertButton () {

#if UNITY_ANDROID
            this.insertItem( int.Parse(this.indexInput.text), new CustomData{ name=this.titleInput.text, value=this.valueInput.text, on=true } );
#endif
        }
    }

   //#if UNITY_EDITOR
   //
   //[UnityEditor.CustomEditor(typeof(RealTimeInsertItemExample))]
   //public class RealTimeAddItemExampleEditor : UnityEditor.Editor {
   //
   //    public override void OnInspectorGUI() {
   //        base.OnInspectorGUI();
   //
   //        if( Application.isPlaying ) {
   //
   //            if( GUILayout.Button("Insert") ) {
   //
   //                var example = (RealTimeInsertItemExample)this.target;
   //                example.insertItem(example.dataIndex, new RealTimeInsertItemExample.CustomData{ name=example.dataName, value=example.dataValue } );
   //            }
   //        }
   //    }
   //}
   //
   //#endif
}