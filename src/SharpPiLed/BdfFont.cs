namespace SharpPiLed
{
	using System;
	using System.IO;
	using Bindings;

	/// <summary>
	/// Represents a BDF font.
	/// BDF fonts are simple bitmap fonts that can be used for displaying on a LED matrix display.
	/// </summary>
	public class BdfFont : IDisposable
	{
		private IntPtr _font;

		/// <summary>
		/// Initializes a new instance of a <see cref="BdfFont" />.
		/// </summary>
		/// <param name="bdfFontFile">A path to a bdf font file.</param>
		public BdfFont(string bdfFontFile)
		{
			if (String.IsNullOrEmpty(bdfFontFile))
			{
				throw new ArgumentNullException(nameof(bdfFontFile));
			}

			if (!File.Exists(bdfFontFile))
			{
				throw new ArgumentException($"The provided file '{bdfFontFile}' needs to exist.");
			}

			_font = RpiRgbLedMatrix.load_font(bdfFontFile);
		}

		internal int DrawText(IntPtr canvas, int x, int y, Color color, string text, int spacing = 0, bool vertical = false)
		{
			return (vertical)
				? RpiRgbLedMatrix.vertical_draw_text(canvas, _font, x, y, color.Red, color.Green, color.Blue, text, spacing)
				: RpiRgbLedMatrix.draw_text(canvas, _font, x, y, color.Red, color.Green, color.Blue, text, spacing);
		}

		#region IDisposable Support
		private bool disposedValue = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
				}

				RpiRgbLedMatrix.delete_font(_font);
				_font = IntPtr.Zero;

				disposedValue = true;
			}
		}

		~BdfFont()
		{
			Dispose(false);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
