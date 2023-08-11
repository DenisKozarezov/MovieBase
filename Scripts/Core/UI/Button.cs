using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataBase.UI
{
    class Button : BaseUIElement, IInteractable
    {
        private System.Windows.Forms.Button _button { set; get; }
        private Panel Panel { set; get; }

        public FlatStyle FlatStyle
        {
            set => _button.FlatStyle = value;
            get => _button.FlatStyle;
        }
        public FlatButtonAppearance FlatAppearance
        {
            get => _button.FlatAppearance;
        }
        public Cursor Cursor
        {
            set => _button.Cursor = value;
            get => _button.Cursor;
        }
        public Image Image
        {
            set => _button.Image = value;
            get => _button.Image;
        }
        public Image BackgroundImage
        {
            set => _button.BackgroundImage = value;
            get => _button.BackgroundImage;
        }
        public ImageLayout BackgroundImageLayout
        {
            set => _button.BackgroundImageLayout = value;
            get => _button.BackgroundImageLayout;
        }
        public string Text
        {
            set => _button.Text = value;
            get => _button.Text;
        }
        public ContentAlignment TextAlign
        {
            set => _button.TextAlign = value;
            get => _button.TextAlign;
        }
        public Bitmap Icon { private set; get; }
        public Color BackColor
        {
            set => _button.BackColor = value;
            get => _button.BackColor;
        }

        public event EventHandler Click
        {
            add { _button.Click += value; }
            remove { _button.Click -= value; }
        }

        public Button()
        {      
            Control = new System.Windows.Forms.Button();
            Size = new Size(50, 50);          

            Panel = new Panel();
            Program.CurrentForm.Controls.Add(Control);
            _button = Control as System.Windows.Forms.Button;
        }
        public Button(ref System.Windows.Forms.Button button) : base(button)
        {
            _button = button;
            Panel = new Panel();
            _button.Controls.Add(Panel.Control);
        }

        public void SetIcon(Bitmap icon, HorizontalAlignment alignment)
        {
            Icon = icon;
            Panel.BackgroundImageLayout = ImageLayout.Stretch;
            Panel.BackColor = Control.BackColor;
            Panel.BackColor = Color.Transparent;
            Panel.Width = (int)(Control.Height * 0.4f);
            Panel.Height = Panel.Size.Width;
            Panel.BackgroundImage = icon;
            AddControl(Panel);
            Panel.BringToFront();
            switch (alignment)
            {
                case HorizontalAlignment.Center:
                    Panel.Dock = DockStyle.None;
                    int x = Control.Width / 2 - Panel.Size.Width / 2;
                    Panel.Location = new Point(x, Control.Location.Y);
                    break;
                case HorizontalAlignment.Left:
                    Panel.Dock = DockStyle.Left;
                    break;
                case HorizontalAlignment.Right:
                    Panel.Dock = DockStyle.Right;
                    break;
            }
        }

        public object GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
