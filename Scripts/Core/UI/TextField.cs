using System;
using System.Drawing;
using System.Windows.Forms;

namespace Core.UI
{
    class TextField : BaseUIElement, IInteractable
    {
        private Button _button;

        public bool IsEmpty => string.IsNullOrEmpty(Text);
        public bool IconEnabled
        {
            set
            {
                if (value) _button.Cursor = Cursors.Hand;
                else _button.Cursor = Cursors.Default;
                _button.Enabled = value;
            }
            get => _button.Enabled;
        }
        public bool IconHiddenWhileFocused { set; get; } = true;

        public string Text { private set; get; } = string.Empty;
        public string DefaultText { set; get; }
        public Bitmap Icon { private set; get; }

        /// <summary>
        /// Событие, вызываемое при нажатии на иконку.
        /// </summary>
        public event EventHandler Click
        {
            add { _button.Click += value; }
            remove { _button.Click -= value; }
        }

        public TextField() : base() 
        {
            _button = new Button();
            _button.Enabled = false;

            Control = new TextBox();
            Control.Location = new Point(0, 0);
            Control.Size = new Size(100, 50);

            Control.MouseDown += (obj, e) => { Focused = true; };
            Control.Leave += (obj, e) => { Focused = false; };

            Control.MouseDown += (obj, e) =>
            {
                if (IsEmpty)
                {
                    ResetText(string.Empty);
                    if (IconHiddenWhileFocused) _button.Visible = false;
                }
            };
            Control.Leave += (obj, e) =>
            {
                if (IsEmpty)
                {
                    ResetText();
                    _button.Visible = true;
                }
            };
            Control.TextChanged += (obj, e) =>
            {
                SetText(Control.Text);
            };

            Program.CurrentForm.Controls.Add(Control);
            AddControl(_button);
            Control.BringToFront();
        }
        public TextField(TextBox control) : base(control)
        {
            _button = new Button();
            _button.Enabled = false;

            Control.MouseDown += (obj, e) => 
            {
                if (IsEmpty)
                {
                    ResetText(string.Empty);
                    if (IconHiddenWhileFocused) _button.Visible = false;
                }
            };
            Control.Leave += (obj, e) =>
            {
                if (IsEmpty)
                {
                    ResetText();
                    _button.Visible = true;
                }
            };
            Control.TextChanged += (obj, e) => 
            {
                SetText(Control.Text); 
            };
        }

        /// <summary>
        /// Редактирует содержимое текстового поля.
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text)
        {
            Text = text;
            Control.Text = text;
            Control.ForeColor = Color.Black;
        }

        /// <summary>
        /// Сбрасывает содержимое текстового поля и вписывает стандартный текст.
        /// </summary>
        public void ResetText()
        {
            ResetText(DefaultText);
        }

        /// <summary>
        /// Сбрасывает содержимое текстового поля и вписывает указанный текст.
        /// </summary>
        /// <param name="defaultText"></param>
        public void ResetText(string defaultText)
        {          
            Control.Text = defaultText;
            Text = string.Empty;
            Control.ForeColor = Color.Gray;
        }

        /// <summary>
        /// Устанавливает текстовому полю иконку.
        /// </summary>
        public void SetIcon(Bitmap icon, HorizontalAlignment alignment)
        {
            Icon = icon;
            _button.BackgroundImageLayout = ImageLayout.Stretch;
            _button.FlatStyle = FlatStyle.Flat;
            _button.FlatAppearance.BorderSize = 0;
            _button.Width = (int)(Control.Height * 0.8f);
            _button.Height = _button.Size.Width;
            _button.BackgroundImage = icon;
            AddControl(_button);
            _button.BringToFront();
            switch (alignment)
            {
                case HorizontalAlignment.Center:
                    _button.Dock = DockStyle.None;
                    int x = Control.Width / 2 - _button.Size.Width / 2;
                    _button.Location = new Point(x, Control.Location.Y);
                    break;
                case HorizontalAlignment.Left:
                    _button.Dock = DockStyle.Left;
                    break;
                case HorizontalAlignment.Right:
                    _button.Dock = DockStyle.Right;
                    break;
            }
        }

        public object GetValue() => Text;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _button?.Dispose();
                //Icon?.Dispose();
                Text = null;
                DefaultText = null;
            }
            base.Dispose(disposing);
        }
    }
}