using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Core.UI
{
    class Dropdown : BaseUIElement
    {       
        private LinkedList<Button> Items { set; get; } = new LinkedList<Button>();

        public event Action Expanded
        {
            add 
            { 
                Control.MouseClick += (obj, e) => 
                { 
                    if (Enabled) value(); 
                }; 
            }
            remove { }
        }
        public event Action<Button> ItemSelected;

        public bool IsExpanded { private set; get; } = false;

        public Button SelectedItem { private set; get; }
        public int ItemsCount => Items.Count;
        public int ItemWidth { set; get; }
        public int ItemHeight { set; get; } = 20;
        
        public Dropdown(Control dropdown) : base(dropdown)
        {
            Control.MouseClick += (obj, e) =>
            {
                if (!Enabled) return;
                if (!IsExpanded)
                {
                    Expand();
                    IsExpanded = true;
                }
                else
                {
                    Shrink();
                    IsExpanded = false;
                }
            };
        }
        public Dropdown(Button button) : base(button)
        {
            Control.Click += (obj, e) =>
            {
                if (!Enabled) return;
                if (!IsExpanded)
                {
                    Expand();
                    IsExpanded = true;
                }
                else
                {
                    Shrink();
                    IsExpanded = false;
                }
            };
        }

        private void Expand()
        {
            foreach (var item in Items) item.Visible = true;
            IsExpanded = true;
        }
        private void Shrink()
        {
            foreach (var item in Items) item.Visible = false;
            IsExpanded = false;
        }

        public void AddItem(Button item)
        {
            item.Size = new Size(ItemWidth, ItemHeight);
            Point screen_location = Program.CurrentForm.PointToScreen(Control.Location);
            Point client_location = Program.CurrentForm.Location;
            int x = screen_location.X - client_location.X + ItemWidth / 2;
            int y = screen_location.Y - client_location.Y + ItemHeight * (ItemsCount + 1);
            item.Location = new Point(x, y);
            item.Visible = false;
            item.TabStop = false;
            Program.CurrentForm.Controls.Add(item);
            item.BringToFront();

            item.Click += (obj, e) => 
            {
                SelectedItem = obj as Button;
                ItemSelected?.Invoke(obj as Button); 
            };
            Items.AddLast(item);
        }
        public void RemoveItem(Button item)
        {
            Items.Remove(item);
            item = default;
        }

        public Button this[int index]
        {
            get
            {
                int i = 0;
                foreach (var item in Items) if (i == index) return item; else i++;
                throw new IndexOutOfRangeException();
            }
        }
    }
}