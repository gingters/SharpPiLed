using System;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using SharpPiLed;

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
		private bool _useOutline = false;
		private Color _outlineColor = new Color(0, 0, 0);

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

		[Option("-c|--color", "Color for the font. Default: 255,255,0", CommandOptionType.SingleValue)]
		public Color Color { get; set; } = new Color(255, 255, 0);

		[Option("-bc|--background-color", "Color for the background. Default: 0,0,0", CommandOptionType.SingleValue)]
		public Color BackgroundColor { get; set; } = new Color(0, 0, 0);

		[Option("-oc|--outline-color", "Color for the outline font. Default: 0,0,0", CommandOptionType.SingleValue)]
		public Color OutlineColor {
			get
			{
				return _outlineColor;
			}
			set
			{
				_outlineColor = value;
				_useOutline = true;
			}
		}

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
				)
			{
				options.PwmBits = 1;
			}

			var matrix = new LedMatrix(options);

			var canvas = matrix.CreateOffscreenCanvas();
			var font = new BdfFont(Font);
			var outlineFont = (BdfFont) ((_useOutline) ? font.CreateOutlineFont() : null);

			while (!Console.KeyAvailable)
			{
				var text = DateTime.UtcNow.ToString(Format);

				canvas.Fill(BackgroundColor);

				if (_useOutline)
				{
					canvas.DrawText(outlineFont, XOrigin - 1, YOrigin + font.Baseline, OutlineColor, text, Spacing - 2);
				}
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
