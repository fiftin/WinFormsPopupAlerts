using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WinFormsPopupAlerts
{
    public class TooltipAlertArg
    {
        public TooltipAlertArg(string title, string text)
            : this(title, text, ToolTipIcon.None)
        {
        }
        public TooltipAlertArg(string title, string text, ToolTipIcon icon)
        {
            Title = title;
            Text = text;
            Icon = icon;
        }
        public TooltipAlertArg(string title, string text, Image image)
        {
            Title = title;
            Text = text;
            CustomImage = image;
        }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public Image CustomImage { get; private set; }
        public ToolTipIcon Icon { get; private set; }
    }

    [System.ComponentModel.ToolboxItem(false)]
    public class TooltipAlert : Alert
    {
        public TooltipAlert(AlertAlignment align)
            : base(align)
        {
            InitializeComponent();
            ResetRegion();
        }

        public TooltipAlert(TooltipAlertArg arg, AlertAlignment align)
            : base(align)
        {
            InitializeComponent();
            Title = arg.Title;
            Text = arg.Text;
            CustomImage = arg.CustomImage;
            Icon = arg.Icon;
            ResetRegion();
        }

        private void InitializeComponent()
        {
        }

        private string title;
        private string text;
        private ToolTipIcon icon;
        private Image customImage;

        public Image CustomImage
        {
            get
            {
                return customImage;
            }
            set
            {
                customImage = value;
            }
        }

        public new virtual ToolTipIcon Icon
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
            Renderer.MaxSize = MaximumSize;
            Renderer.MinSize = MinimumSize;
            Renderer.Padding = Padding;
            Rectangle rect = new Rectangle(0, 0, MinimumSize.Width, MinimumSize.Height);
            
            using (Bitmap tempBitmap = new Bitmap(100, 100))
            {
                using (Graphics g = Graphics.FromImage(tempBitmap))
                {
                    rect = Renderer.GetRect(g, Title, Text, Icon, CustomImage);
                    Region = Renderer.GetRegion(g, rect);
                    if (Renderer.TransparencyKey != Color.Transparent)
                        TransparencyKey = Renderer.TransparencyKey;
                    closeButtonRect = Renderer.GetCloseButtonRect(g, rect, closeButtonState);
                }
            }
            Size = rect.Size;
            if (Renderer.TransparencyKey != Color.Transparent)
                BackColor = Renderer.TransparencyKey;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Draw(e.Graphics);
        }

        protected virtual void Draw(Graphics g)
        {
            Rectangle rect = Renderer.GetRect(g, Title, Text, Icon, CustomImage);
            Renderer.Draw(g, Title, Text, Icon, CustomImage);
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
