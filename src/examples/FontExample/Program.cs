using System;
using System.IO;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;

namespace SharpPiLed.Examples.FontExample
{
	[Command(
		UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.CollectAndContinue,
		ExtendedHelpText =@"
Remarks:
	You can specify all other rpi-rgb-led-matrix options as well (i.e. --led-rows, --led-multiplexing etc.)."
	)]
	public class Program
	{
		[Required, Option(Description = "A font file to render the text in")]
		public string Font { get; set; }

		[Required, Option(Description = "The text to display")]
		public string Text { get; set; }

		public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

		private int OnExecute()
		{
			var matrix = new LedMatrix(new LedMatrixOptions());
			var canvas = matrix.CreateOffscreenCanvas();
			var font = new BdfFont(Font);

			canvas.DrawText(font, 1, canvas.Height - 2, new Color(0, 255, 0), Text);

			matrix.SwapOnVsync(canvas);

			while (!Console.KeyAvailable)
			{
				Thread.Sleep(250);
			}

			return 0;
		}
	}
}
