using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace WinFormsPopupAlerts
{
    /// <summary>
    /// Provide a user interface for representing and editing the value of objects of CornerRadius class.
    /// </summary>
    public class CornerRadiusEditor : UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            CornerRadius cr = (CornerRadius)value;
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                CornerRadiusEditorControl control = new CornerRadiusEditorControl(cr);
                edSvc.DropDownControl(control);
            }
            return new CornerRadius(cr.TopLeft, cr.TopRight, cr.BottomLeft, cr.BottomRight);
        }
    }
}
