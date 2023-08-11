using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Core.UI
{
    public abstract class BaseUIElement : IDisposable
    {
        private static LinkedList<BaseUIElement> Elements { set; get; } = new LinkedList<BaseUIElement>();

        public Control Control { set; get; }

        public bool Enabled
        {
            set => Control.Enabled = value;
            get => Control.Enabled;
        }
        public bool Focused { protected set; get; }
        public bool Visible
        {
            set => Control.Visible = value;
            get => Control.Visible;
        }

        public virtual Point Location
        {
            set => Control.Location = value;
            get => Control.Location;
        }
        public virtual Size Size
        {
            set => Control.Size = value;
            get => Control.Size;
        }
        public int Width
        {
            set => Size = new Size(value, Control.Height);
            get => Control.Width;
        }
        public int Height
        {
            set => Size = new Size(Control.Width, value);
            get => Control.Height;
        }
        public DockStyle Dock
        {
            set => Control.Dock = value;
            get => Control.Dock;
        }
        public bool TabStop
        {
            set => Control.TabStop = value;
            get => Control.TabStop;
        }

        public BaseUIElement() 
        {
            Elements.AddLast(this);
        }
        public BaseUIElement(Control control)
        {
            Control = control;
            Control.MouseDown += (obj, e) => { Focused = true; };
            Control.Leave += (obj, e) => { Focused = false; };
            Elements.AddLast(this);
        }

        public static BaseUIElement GetParent(Control control)
        {
            foreach (var element in Elements)
            {
                if (element.Control == control.Parent) return element;
            }
            return default;
        }

        public void AddControl(Control control) => Control?.Controls.Add(control);
        public void AddControl(BaseUIElement element) => Control?.Controls.Add(element.Control);

        public void BringToFront() => Control?.BringToFront();

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {               
                    Elements.Remove(this);
                    Control?.Dispose();
                }                
                disposed = true;
            }
        }
    }
}