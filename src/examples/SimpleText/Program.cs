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
			if (args.Length < 1)
			{
				Console.WriteLine("SimpleText [font] [text]");
				Console.WriteLine("Example: SimpleText 6x9.bdf \"Hello World!\"");
				return -1;
			}

			var text = "Hello World!";
			if (args.Length > 1)
			{
				text = args[1];
			}

			var matrix = new LedMatrix(new LedMatrixOptions(), args);
			var canvas = matrix.CreateOffscreenCanvas();
			var font = new BdfFont(args[0]);

			canvas.DrawText(font, 1, canvas.Height - 2, new Color(0, 255, 0), text);

			matrix.SwapOnVsync(canvas);

			while (!Console.KeyAvailable)
			{
				Thread.Sleep(250);
			}

			return 0;
		}
	}
}
