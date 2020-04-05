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
		[Option(Description = "A font file to render the text in. Default: 6x9.bdf")]
		public string Font { get; set; } = "6x9.bdf."

		[Required, Option(Description = "The text to display")]
		public string Text { get; set; }

		[Option("-x|--x", Description = "X origin of displaying text")]
		public int XOrigin { get; set; }

		[Option("-y|--y", Description = "Y origin of displaying text")]
		public int YOrigin { get; set; }

		[Option(Description = "Spacing pixels between letters")]
		public int Spacing { get; set; }

		public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

		private int OnExecute()
		{
			var matrix = new LedMatrix(new LedMatrixOptions());
			var canvas = matrix.CreateOffscreenCanvas();
			var font = new BdfFont(Font);

			canvas.DrawText(font, XOrigin, YOrigin + font.Baseline, new Color(0, 255, 0), Text, Spacing);

			matrix.SwapOnVsync(canvas);

			while (!Console.KeyAvailable)
			{
				Thread.Sleep(250);
			}

			return 0;
		}
	}
}
