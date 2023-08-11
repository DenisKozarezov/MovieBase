using System;
using System.Drawing;
using System.Windows.Forms;

namespace Core.UI
{
    class TextLink : BaseUIElement, IInteractable
    {
        public string Text
        {
            set => Control.Text = value;
            get => Control.Text;
        }
        public Color DefaultColor { set; get; } = Color.Black;
        public Color SelectedColor { set; get; } = Color.Blue;            

        public event EventHandler Click
        {
            add => Control.Click += value;
            remove => Control.Click -= value;
        }

        public TextLink()
        {
            Control = new Label();
            Control.Cursor = Cursors.Hand;
            Control.Enter += (obj, e) =>
            {
                Control.ForeColor = SelectedColor;
            };
            Control.Leave += (obj, e) =>
            {
                Control.ForeColor = DefaultColor;
            };
        }
        public TextLink(Label label) : base (label)
        {
            Control.Cursor = Cursors.Hand;
            Control.Enter += (obj, e) =>
            {
                Control.ForeColor = SelectedColor;
            };
            Control.Leave += (obj, e) =>
            {
                Control.ForeColor = DefaultColor;
            };
        }

        public object GetValue() => Text;
    }
}