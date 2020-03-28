using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SharpPiLed;

namespace Matrix
{
	public class Program
	{
		const int MAX_HEIGHT = 16;
		const int COLOR_STEP = 15;
		const int FRAME_STEP = 1;

		public static int Main(string[] args)
		{
			var matrix = new LedMatrix(new LedMatrixOptions(), args);

			var canvas = matrix.CreateOffscreenCanvas();
			var rnd = new Random();

			var points = new List<Point>();
			var recycled = new Stack<Point>();
			int frame = 0;
			var stopwatch = new Stopwatch();

			while (!Console.KeyAvailable)
			{
				stopwatch.Restart();

				frame++;

				if (frame % FRAME_STEP == 0)
				{
					if (recycled.Count == 0)
					{
						points.Add(new Point(rnd.Next(0, canvas.Width), 0));
					}
					else
					{
						var point = recycled.Pop();
						point.X = rnd.Next(0, canvas.Width);
						point.Y = 0;
						point.Recycled = false;
					}
				}

				canvas.Clear();

				foreach (var point in points)
				{
					if (!point.Recycled)
					{
						point.Y++;

						if (point.Y - MAX_HEIGHT > canvas.Height)
						{
							point.Recycled = true;
							recycled.Push(point);
						}

						for (var i = 0; i< MAX_HEIGHT; i++)
						{
							canvas.SetPixel(point.X, point.Y - i, new Color(0, 255 - i * COLOR_STEP, 0));
						}
					}
				}

				canvas = matrix.SwapOnVsync(canvas);

				// force 30 FPS
				var elapsed= stopwatch.ElapsedMilliseconds;
				if (elapsed < 33)
				{
					Thread.Sleep(33 - (int)elapsed);
				}
			}

			return 0;
		}
	}
}
