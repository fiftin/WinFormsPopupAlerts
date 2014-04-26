using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace WinFormsPopupAlerts
{
    /// <summary>
    /// Represents an always top window.
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    public abstract class TopFormBase : Form
    {

        public virtual Rectangle RealBounds
        {
            get
            {
                return Rectangle.Empty;
            }
            set
            {
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 128;
                return createParams;
            }
        }

        protected virtual bool IsTopMost
        {
            get
            {
                return true;
            }
        }

        protected virtual IntPtr InsertAfterWindow
        {
            get
            {
                return (IntPtr)(this.IsTopMost ? -1 : 0);
            }
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        protected virtual bool AllowMouseActivate
        {
            get
            {
                return false;
            }
        }

        protected TopFormBase()
        {
            this.Parent = (Control)null;
            this.TopLevel = true;
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Visible = false;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        protected virtual void UpdateZOrder(IntPtr after)
        {
            if (!this.Visible)
                return;
            if (after == IntPtr.Zero)
                after = this.InsertAfterWindow;
            uint num = 16U;
            Rectangle realBounds = this.RealBounds;
            uint uFlags = num | 3U;
            TopFormBase.SetWindowPos(this.Handle, after, realBounds.X, realBounds.Y, realBounds.Width, realBounds.Height, uFlags);
        }

        protected void SetVisibleCoreBase(bool newVisible)
        {
            base.SetVisibleCore(newVisible);
        }

        protected override void SetVisibleCore(bool newVisible)
        {
            this.SetVisibleCoreBase(newVisible);
            if (!newVisible || !this.IsHandleCreated)
                return;
            TopFormBase.SetWindowPos(this.Handle, this.InsertAfterWindow, 0, 0, 0, 0, 83U);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 515:
                    this.OnDoubleClick(EventArgs.Empty);
                    return;
                case 533:
                    if (m.LParam == this.Handle)
                    {
                        this.OnGotCapture();
                        break;
                    }
                    else
                    {
                        this.OnLostCapture();
                        break;
                    }
                case 33:
                    if (!this.AllowMouseActivate)
                    {
                        m.Result = (IntPtr)3;
                        return;
                    }
                    else
                        break;
                case 36:
                    this.WMGetMinMazInfo(ref m);
                    return;
            }
            base.WndProc(ref m);
        }

        private void WMGetMinMazInfo(ref Message m)
        {
            base.WndProc(ref m);
        }

        protected virtual void OnLostCapture()
        {
        }

        protected virtual void OnGotCapture()
        {
        }

        [DllImport("USER32.dll")]
        internal extern static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    }
}
