using System;

namespace SharpPiLed.Examples.MinimalExample
{
	public class Program
	{
		public static int Main(string[] args)
		{
			var matrix= new LedMatrix(new LedMatrixOptions(), args);
			var canvas = matrix.CreateOffscreenCanvas();

			for (var i = 0; i < 1000; ++i)
			{
				for (var y = 0; y < canvas.Height; ++y)
				{
					for (var x = 0; x < canvas.Width; ++x)
					{
						canvas.SetPixel(x, y, new Color(i & 0xff, x, y));
					}
				}

				canvas.DrawCircle(canvas.Width / 2, canvas.Height / 2, 6, new Color(0, 0, 255));
				canvas.DrawLine(canvas.Width / 2 - 3, canvas.Height / 2 - 3, canvas.Width / 2 + 3, canvas.Height / 2 + 3, new Color(0, 0, 255));
				canvas.DrawLine(canvas.Width / 2 - 3, canvas.Height / 2 + 3, canvas.Width / 2 + 3, canvas.Height / 2 - 3, new Color(0, 0, 255));

				canvas = matrix.SwapOnVsync(canvas);
			}

			return 0;
		}
	}
}
