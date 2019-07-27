/**
 * Item2.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

namespace Mosframe {

    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class Item2 : UIBehaviour, IDynamicScrollViewItem 
    {
	    private readonly Color[] colors = new Color[] {
		    Color.grey,
		    Color.white,
	    };

	    //public Image    icon;
	    public Text     title;
        public Button   DeleteItembutton;
        public Button   EditItembutton;
	    public Image    background;

        private int     dataIndex = -1;

        protected override void OnEnable () {
            base.OnEnable();
            DeleteItembutton.onClick.AddListener( OnDeleteClick );
            EditItembutton.onClick.AddListener(OnEditClick);
        }

        protected override void OnDisable () {
            base.OnDisable();
            DeleteItembutton.onClick.RemoveListener( OnDeleteClick );
            EditItembutton.onClick.RemoveListener( OnDeleteClick );
        }

        public void OnUpdateItem( int index ) {

            if( RealTimeInsertItemExample.I.data.Count != index ) {

                this.dataIndex = index;
                this.updateItem();
            }
        }

        public void OnDeleteClick () {
            Debug.Log("OnDeleteClick");

            if( dataIndex == -1 ) return;

            var realTimeInsertItemExample = FindObjectOfType<RealTimeInsertItemExample>();
            realTimeInsertItemExample.RemoveItem(dataIndex);
        }

        public void OnEditClick() {
            Debug.Log("OnEditClick");

            if (dataIndex == -1) return;
            var realTimeInsertItemExample = FindObjectOfType<RealTimeInsertItemExample>();
            realTimeInsertItemExample.EditItem(dataIndex);
        }
        
        private void updateItem () {

            if( this.dataIndex == -1 ) return;

            var data = RealTimeInsertItemExample.I.data[ dataIndex ];

            //this.background.color   = this.colors[Mathf.Abs(this.dataIndex) % this.colors.Length];
            //this.icon.sprite        = Resources.Load<Sprite>( (Mathf.Abs(this.dataIndex) % 20 + 1).ToString("icon_00") );

            this.title.text = data.name;  
        }
    }
}