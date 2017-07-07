using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Quad64.src.LevelInfo
{
    public class HexNumberTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            else
            {
                return base.CanConvertFrom(context, sourceType);
            }
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            else
            {
                return base.CanConvertTo(context, destinationType);
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && (value.GetType() == typeof(uint) || value.GetType() == typeof(int)))
            {
                if (Globals.useHexadecimal)
                {
                    if (!Globals.useSignedHex || value.GetType() == typeof(uint))
                        return string.Format("0x{0:X8}", value);
                    else
                    {
                        int v = (int)value;
                        if (v > -1)
                            return string.Format("0x{0:X8}", value);
                        else
                        {
                            return string.Format("-0x{0:X8}", -v);
                        }
                    }
                }
                else
                    return value.ToString();
            }
            else if (destinationType == typeof(string) && (value.GetType() == typeof(ushort) || value.GetType() == typeof(short)))
            {
                if (Globals.useHexadecimal)
                {
                    if (!Globals.useSignedHex || value.GetType() == typeof(ushort))
                        return string.Format("0x{0:X4}", value);
                    else
                    {
                        short v = (short)value;
                        if (v > -1)
                            return string.Format("0x{0:X4}", value);
                        else
                        {
                            return string.Format("-0x{0:X4}", -v);
                        }
                    }
                }
                else
                    return value.ToString();
            }
            else if (destinationType == typeof(string) && value.GetType() == typeof(byte))
            {
                if (Globals.useHexadecimal)
                    return string.Format("0x{0:X2}", value);
                else
                    return value.ToString();
            }
            else
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            //Console.WriteLine("Value = " + value + ", type = " + value.GetType().ToString());
            //Console.WriteLine("Context = " + context.PropertyDescriptor.PropertyType);
            Type propType = context.PropertyDescriptor.PropertyType;
            if (value.GetType() == typeof(string))
            {
                string input = (string)value;
                bool isHex = false;

                if (input.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    input = input.Substring(2);
                    isHex = true;
                }
                else if (input.StartsWith("$", StringComparison.OrdinalIgnoreCase))
                {
                    input = input.Substring(1);
                    isHex = true;
                }

                if (propType == typeof(uint))
                    if(isHex)
                        return uint.Parse(input, NumberStyles.HexNumber, culture);
                    else
                        return uint.Parse(input, NumberStyles.Integer, culture);
                else if (propType == typeof(int))
                    if (isHex)
                        return int.Parse(input, NumberStyles.HexNumber, culture);
                    else
                        return int.Parse(input, NumberStyles.Integer, culture);
                else if (propType == typeof(ushort))
                    if (isHex)
                        return ushort.Parse(input, NumberStyles.HexNumber, culture);
                    else
                        return ushort.Parse(input, NumberStyles.Integer, culture);
                else if (propType == typeof(short))
                    if (isHex)
                        return short.Parse(input, NumberStyles.HexNumber, culture);
                    else
                        return short.Parse(input, NumberStyles.Integer, culture);
                else if (propType == typeof(byte))
                    if (isHex)
                        return byte.Parse(input, NumberStyles.HexNumber, culture);
                    else
                        return byte.Parse(input, NumberStyles.Integer, culture);

                return input;
            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }
        }
    }
}
