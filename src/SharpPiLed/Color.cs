namespace SharpPiLed
{
	public struct Color
	{
		public readonly byte Red;
		public readonly byte Green;
		public readonly byte Blue;

		public Color (int red, int green, int blue)
			: this((byte) red, (byte) green, (byte) blue)
		{ }

		public Color(byte red, byte green, byte blue)
		{
			Red = red;
			Green = green;
			Blue = blue;
		}
	}
}
