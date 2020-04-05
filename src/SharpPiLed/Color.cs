namespace SharpPiLed
{
	using System;
	using System.Reflection;
	using System.Globalization;
	using System.ComponentModel;

	[TypeConverter(typeof(ColorConverter))]
	public struct Color
	{
		public readonly byte Red;
		public readonly byte Green;
		public readonly byte Blue;

		public Color(int red, int green, int blue)
			: this((byte) red, (byte) green, (byte) blue)
		{ }

		public Color(byte red, byte green, byte blue)
		{
			Red = red;
			Green = green;
			Blue = blue;
		}

		public bool IsBlack => Red == 0 && Green == 0 && Blue == 0;
		public bool IsWhite => Red == 255 && Green == 255 && Blue == 255;
	}

	public class ColorConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
			{
				return true;
			}

			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				var c = (Color)value;
				return $"{c.Red},{c.Green},{c.Blue}";
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, Object value)
		{
			if (value is string val)
			{
				var v = val.Split(new char[] { ',' });
				return new Color(int.Parse(v[0]), int.Parse(v[1]), int.Parse(v[2]));
			}

			return base.ConvertFrom(context, culture, value);
		}
	}
}
