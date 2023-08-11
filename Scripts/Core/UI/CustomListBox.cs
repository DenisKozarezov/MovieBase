using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace Core.UI
{
    class CustomListBox<T> : BaseUIElement, IEnumerable<T> where T : IInteractable, new()
    {
        private LinkedList<T> Items { set; get; } = new LinkedList<T>();

        public int ItemsCount => Items.Count;
        public int ItemHeight { set; get; } = 20;
        public byte ItemsMaxCount { private set; get; }
        public T FirstItem => Items.First.Value;
        public T SecondLastItem
        {
            get
            {
                if (ItemsCount > 1) return Items.Last.Previous.Value;
                else return Items.First.Value;
            }
        }
        public T LastItem => Items.Last.Value;

        public event EventHandler ItemAdded;
        public event EventHandler ItemRemoved;

        public CustomListBox(System.Windows.Forms.Panel panel, byte itemsMaxCount) : base(panel) 
        { 
            ItemsMaxCount = itemsMaxCount;
        }

        public T Instantiate()
        {
            if (ItemsCount + 1 > ItemsMaxCount) return default;

            T item = new T();
            (item as BaseUIElement).Size = new Size(Control.Width, ItemHeight);
            (item as BaseUIElement).Control.TabStop = false;
            return item;
        }

        public void AddItem(T item)
        {
            if (ItemsCount + 1 > ItemsMaxCount) return;

            BaseUIElement element = item as BaseUIElement;

            Control.Controls.Add(element.Control);
            element.Location = new Point(0, 0);
            element.Size = new Size(Control.Width, ItemHeight);
            element.Control.Dock = DockStyle.Top;
            element.Control.BringToFront();
            Items.AddLast(item);

            ItemAdded?.Invoke(item, null);
        }
        public void RemoveItem(T item)
        {      
            Items.Remove(item);
            Control.Controls.Remove((item as BaseUIElement).Control);    
            ItemRemoved?.Invoke(item, null);
            (item as BaseUIElement)?.Dispose();
        }
        public void RemoveItemAt(int index)
        {
            T item = this[index];             
            Items.Remove(this[index]);
            Control.Controls.Remove((item as BaseUIElement).Control);
            ItemRemoved?.Invoke(item, null);
        }
        public T Find(T item) => Items.Find(item).Value;
        public int FindIndex(T item)
        {
            int i = 0;
            foreach (var obj in Items)
            {
                if (obj.Equals(item)) return i;
                else i++;
            }
            return -1;
        }
        public bool Contains(T item) => Items.Contains(item);
        public void Clear() => Items.Clear();

        public string[] GetValues()
        {
            List<string> values = new List<string>();
            foreach (var item in Items)
            {
                string value = item.GetValue().ToString();
                if (!string.IsNullOrEmpty(value)) values.Add(value);
            }
            return values.ToArray();
        }

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)Items).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Items).GetEnumerator();

        public T this[int index]
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