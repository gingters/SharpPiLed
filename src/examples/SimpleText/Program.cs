using System;
using System.IO;
using System.Threading;
using SharpPiLed;

namespace SimpleText
{
	public class Program
	{
		public static int Main(string[] args)
		{
			var matrix = new LedMatrix(new LedMatrixOptions(), args);
			var canvas = matrix.CreateOffscreenCanvas();
			canvas.DrawLine(0, 0, 16, 16, new Color(255, 0, 0));

			var font = new BdfFont("6x13.bdf");
			canvas.DrawText(font, 1, 20, new Color(0, 255, 0), "Thank you!");

			matrix.SwapOnVsync(canvas);

			while (!Console.KeyAvailable)
			{
				Thread.Sleep(250);
			}

			return 0;
		}
	}
}
