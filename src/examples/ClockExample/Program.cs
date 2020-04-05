using System;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;

namespace SharpPiLed.Examples.ClockExample
{
		[Command(
		UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.CollectAndContinue,
		ExtendedHelpText =@"
Remarks:
	You can specify all other rpi-rgb-led-matrix options as well (i.e. --led-rows, --led-multiplexing etc.)."
	)]
	public class Program
	{
		[Option(Description = "A font file to render the clock in. Default: 9x13.bdf")]
		public string Font { get; set; } = "8x13.bdf";

		[Range(0, 100), Option(Description = "Brightness percent. Default: 100")]
		public int Brightness { get; set; } = 100;

		[Option(Description = "X origin of displaying text")]
		public int XOrigin { get; set; }

		[Option(Description = "Y origin of displaying text")]
		public int YOrigin { get; set; }

		[Option(Description = "Spacing pixels between letters")]
		public int Spacing { get; set; }

		[Option("-fmt|--format", Description = "The date time format string. Default: hh:mm:ss")]
		public string Format { get; set; }= "hh:mm:ss";

		public Color BackgroundColor = new Color(0, 0, 0);
		public Color Color = new Color(255, 255, 0);
		public Color OutlineColor = new Color(0, 0, 0);

		public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

		private int OnExecute()
		{
			var options = new LedMatrixOptions()
			{
				Brightness = (byte)Brightness,
			};

			// Check if we have only extreme colors
			if (Brightness == 100
				&& FullSaturation(Color)
				&& FullSaturation(BackgroundColor)
				&& FullSaturation(OutlineColor)
				)
			{
				options.PwmBits = 1;
			}

			var matrix = new LedMatrix(options);

			var canvas = matrix.CreateOffscreenCanvas();
			var font = new BdfFont(Font);

			while (!Console.KeyAvailable)
			{
				var text = DateTime.UtcNow.ToString(Format);

				canvas.Fill(BackgroundColor);
				canvas.DrawText(font, XOrigin, YOrigin + font.Baseline, Color, text, Spacing);

				matrix.SwapOnVsync(canvas);

				Thread.Sleep(TimeSpan.FromMilliseconds(250));
			}

			return 0;
		}

		private bool FullSaturation(Color color)
		{
			return (color.Red == 0 || color.Red == 255)
				&& (color.Green == 0 || color.Green == 255)
				&& (color.Blue == 0 || color.Blue == 255);
		}
	}
}
