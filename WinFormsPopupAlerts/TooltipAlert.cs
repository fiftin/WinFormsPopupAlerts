using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WinFormsPopupAlerts
{
    [System.ComponentModel.ToolboxItem(false)]
    public class TooltipAlert : PopupAlert
    {
        public TooltipAlert()
        {
            InitializeComponent();
            ResetRegion();
        }

        public TooltipAlert(object[] args)
            : base(args)
        {
            InitializeComponent();
            if (args.Length < 2 || args.Length > 3)
                throw new ArgumentException("2 or 3 arguments required", "args");
            if (args[0] == null)
                throw new ArgumentNullException("args[0]", "Popup alert title can not be null");
            if (args[1] == null)
                throw new ArgumentNullException("args[1]", "Popup alert text can not be null");
            if (args.Length == 3)
            {
                if (args[2] == null)
                    throw new ArgumentNullException("args[2]", "Popup alert icon can not be null");
                if (args[2].GetType() == typeof(ToolTipIcon))
                    Icon = (TooltipAlertIcon)args[2];
                else if (args[2] is Image)
                {
                    Icon = TooltipAlertIcon.Custom;
                    CustomIcon = (Image)args[2];
                }
                else
                    throw new ArgumentException("Popup alert icon can be ToolTipIcon or Image", "args[2]");
                
                
            }
            Title = args[0].ToString();
            Text = args[1].ToString();

            ResetRegion();
        }

        private void InitializeComponent()
        {
        }

        private string title;
        private string text;
        private TooltipAlertIcon icon;
        private Image customIcon;

        public Image CustomIcon
        {
            get
            {
                return customIcon;
            }
            set
            {
                customIcon = value;
            }
        }

        public new virtual TooltipAlertIcon Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public new virtual string Text
        {
            get { return text; }
            set
            {
                text = value;
                ResetRegion();
            }
        }

        public virtual string Title
        {
            get { return title; }
            set
            {
                title = value;
                ResetRegion();
            }
        }


        public override Padding Padding
        {
            get
            {
                return base.Padding;
            }
            set
            {
                if (base.Padding != value)
                {
                    base.Padding = value;
                    ResetRegion();
                }
            }
        }

        public override Size MaximumSize
        {
            get
            {
                return base.MaximumSize;
            }
            set
            {
                if (base.MaximumSize != value)
                {
                    base.MaximumSize = value;
                    ResetRegion();
                }
            }
        }

        public override Size MinimumSize
        {
            get
            {
                return base.MinimumSize;
            }
            set
            {
                if (base.MinimumSize != value)
                {
                    base.MinimumSize = value;
                    ResetRegion();
                }
            }
        }

        private TooltipAlertRenderer renderer = new SystemTooltipAlertRenderer();

        public TooltipAlertRenderer Renderer
        {
            get { return renderer; }
            set { renderer = value; }
        }

        Rectangle closeButtonRect;

        TooltipCloseButtonState closeButtonState;


        protected virtual void ResetRegion()
        {
            //return;

            Renderer.MaxSize = MaximumSize;
            Renderer.MinSize = MinimumSize;
            Renderer.Padding = Padding;
            Rectangle rect = new Rectangle(0, 0, MinimumSize.Width, MinimumSize.Height);
            using (Bitmap tempBitmap = new Bitmap(100, 100))
            {
                using (Graphics g = Graphics.FromImage(tempBitmap))
                {
                    rect = Renderer.GetRect(g, Title, Text, Icon, CustomIcon);
                    Region = Renderer.GetRegion(g, rect);
                    closeButtonRect = Renderer.GetCloseButtonRect(g, rect, closeButtonState);
                }
            }
            Size = rect.Size;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Draw(e.Graphics);
        }

        protected virtual void Draw(Graphics g)
        {
            Rectangle rect = Renderer.GetRect(g, Title, Text, Icon, CustomIcon);
            Renderer.Draw(g, Title, Text, Icon, CustomIcon);
            Renderer.DrawCloseButton(g, rect, closeButtonState);
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            UpdateCloseButtonState(e);
        }

        public override event EventHandler<MouseEventArgs> AlertMouseDown;
        protected override void OnAlertMouseDown(MouseEventArgs e)
        {
            if (AlertMouseDown != null)
            {
                if (!closeButtonRect.Contains(e.Location))
                {
                    AlertMouseDown(this, e);
                    ((Control)this).Hide();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            UpdateCloseButtonState(e);
            if (closeButtonRect.Contains(e.Location))
            {
                OnCloseBottonClick();
            }
        }

        protected virtual void OnCloseBottonClick()
        {
            ((Control)this).Hide();
        }

        private void UpdateCloseButtonState(MouseEventArgs e)
        {
            TooltipCloseButtonState newState;
            if (closeButtonRect.Contains(e.Location))
            {
                if (e.Button == System.Windows.Forms.MouseButtons.None)
                {
                    newState = TooltipCloseButtonState.Hot;
                }
                else
                {
                    newState = TooltipCloseButtonState.Pressed;
                }
            }
            else
            {
                newState = TooltipCloseButtonState.Normal;
            }
            if (closeButtonState != newState)
            {
                closeButtonState = newState;
                using (Graphics g = CreateGraphics())
                {
                    Draw(g);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            UpdateCloseButtonState(e);
        }


    }
}
