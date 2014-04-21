using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace WinFormsPopupAlerts
{
    public class CornerRadiusConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value == null)
                return new CornerRadius();
            if (value is string)
            {
                string serializedData = (string)value;
                if (serializedData.Length == 0)
                    return new CornerRadius();
                string[] parts = serializedData.Split(culture.TextInfo.ListSeparator[0]);
                int[] radiuses;
                if (parts.Length == 1){
                    int radius = int.Parse(parts[0]);
                    radiuses = new int[] { radius, radius, radius, radius };
                }
                else if (parts.Length == 4)
                {
                    radiuses = Array.ConvertAll(parts, delegate(string s)
                    {
                        return int.Parse(s);
                    });
                }
                else
                {
                    throw new ArgumentException("Invalid CornerRadius object", "value");
                }
                return new CornerRadius(radiuses[0], radiuses[1], radiuses[2], radiuses[3]);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value != null && !(value is CornerRadius))
                throw new ArgumentException(
                    "Invalid CornerRadius object", "value");
            if (destinationType == typeof(string))
            {
                if (value == null)
                    return String.Empty;
                else
                {
                    CornerRadius cr = (CornerRadius)value;
                    string[] parts = new string[4];
                    parts[0] = cr.TopLeft.ToString();
                    parts[1] = cr.TopRight.ToString();
                    parts[2] = cr.BottomRight.ToString();
                    parts[3] = cr.BottomLeft.ToString();
                    return String.Join(culture.TextInfo.ListSeparator, parts);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
