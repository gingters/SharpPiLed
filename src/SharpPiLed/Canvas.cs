namespace SharpPiLed
{
	using System;
	using Bindings;

	public class Canvas
	{
		public int Width {get; private set; }
		public int Height { get; private set; }

		internal IntPtr _canvas;

		internal Canvas(IntPtr canvas)
		{
			if (canvas == null || canvas == IntPtr.Zero)
			{
				throw new ArgumentNullException(nameof(canvas));
			}

			_canvas = canvas;
			RpiRgbLedMatrix.led_canvas_get_size(_canvas, out int width, out int height);

			Width = width;
			Height = height;
		}

		public void SetPixel(int x, int y, Color color)
		{
			RpiRgbLedMatrix.led_canvas_set_pixel(_canvas, x, y, color.Red, color.Green, color.Blue);
		}

		public void Fill(Color color)
		{
			RpiRgbLedMatrix.led_canvas_fill(_canvas, color.Red, color.Green, color.Blue);
		}

		public void Clear()
		{
			RpiRgbLedMatrix.led_canvas_clear(_canvas);
		}

		public void DrawCircle(int x, int y, int radius, Color color)
		{
			RpiRgbLedMatrix.draw_circle(_canvas, x, y, radius, color.Red, color.Green, color.Blue);
		}

		public void DrawLine(int x0, int y0, int x1, int y1, Color color)
		{
			RpiRgbLedMatrix.draw_line(_canvas, x0, y0, x1, y1, color.Red, color.Green, color.Blue);
		}

		public int DrawText(BdfFont font, int x, int y, Color color, string text, int spacing = 0, bool vertical = false)
		{
			return font.DrawText(_canvas, x, y, color, text, spacing, vertical);
		}
	}
}
