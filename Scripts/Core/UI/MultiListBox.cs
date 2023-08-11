using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Core.UI
{
    internal class MultiListBox
    {
        private readonly List<ListBox> _items = new List<ListBox>();
        private ListBox _selectedListBox;

        public string SelectedItem { private set; get; }
        public int SelectedIndex { private set; get; }
        public int ItemsCount
        {
            get
            {
                int count = 0;
                foreach (var listBox in _items) count += listBox.Items.Count;
                return count;
            }
        }

        public event Action SelectedIndexChanged
        {
            add
            {
                foreach (var listBox in _items)
                {
                    listBox.SelectedIndexChanged += (obj, e) =>  { value(); };
                }
            }
            remove
            {

            }
        }

        public MultiListBox()
        {
          
        }

        private int CalculateSelectedIndex(int relativeIndex)
        {
            int listBoxIndex = FindIndex((x) => { return x == _selectedListBox ? true : false; });
            int itemsCount = 0;
            for (int i = 0; i < listBoxIndex; i++) itemsCount += _items[i].Items.Count;
            return itemsCount + relativeIndex;
        }
        public void Select(string selectedItem)
        {
            int i = 0;
            foreach (var item in _items)
            {
                if (item.Items.Contains(selectedItem))
                {
                    item.SelectedItem = selectedItem;
                    SelectedItem = selectedItem;
                    SelectedIndex = i;
                    _selectedListBox = item;
                }
                i++;
            }
            Deselect();
        }
        public void Deselect()
        {
            foreach (var listBox in _items) if (listBox != _selectedListBox) listBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Соединяет мультибокс с новым списком.
        /// </summary>
        public void AddList(ListBox item)
        {
            item.MouseDown += (obj, e) => 
            { 
                _selectedListBox = (obj as ListBox);
                foreach (var listBox in _items) if (listBox != _selectedListBox) listBox.SelectedIndex = -1;
            };
            item.SelectedIndexChanged += (obj, e) =>
            {
                ListBox listBox = (obj as ListBox);
                if (listBox == _selectedListBox)
                {
                    SelectedIndex = CalculateSelectedIndex(listBox.SelectedIndex);
                    if (listBox.SelectedItem != null) SelectedItem = listBox.SelectedItem.ToString();
                }
            };
            _items.Add(item);
        }

        /// <summary>
        /// Разъединяет мультибокс с указанным списком.
        /// </summary>
        public void RemoveList(ListBox item) => _items.Remove(item);

        /// <summary>
        /// Разъединяет мультибокс со списком с указанным индексом.
        /// </summary>
        public void RemoveListAt(int index) => _items.RemoveAt(index);

        /// <summary>
        /// Возвращает список по указанному критерию.
        /// </summary>
        /// <returns></returns>
        public ListBox Find(Predicate<ListBox> predicate) => _items.Find(predicate);        

        /// <summary>
        /// Возвращает индекс списка по указанному критерию.
        /// </summary>
        /// <returns></returns>
        public int FindIndex(Predicate<ListBox> predicate) => _items.FindIndex(predicate);        

        public ListBox this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }
    }
}