namespace SharpPiLed
{
	using System;
	using System.IO;
	using System.Linq;
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
				// try local folder as an alternative
				var alternateFontPath = Path.Combine(
					Path.GetDirectoryName(GetType().Assembly.Location),
					Path.GetFileName(bdfFontFile)
				);

				if (!File.Exists(alternateFontPath))
				{
					throw new ArgumentException($"The provided file '{bdfFontFile}' needs to exist.");
				}

				bdfFontFile = alternateFontPath;
			}

			_font = RpiRgbLedMatrix.load_font(bdfFontFile);
		}

		///<summary>
		/// This assembly contains some bdf font files as embedded resources. This method will
		/// write the files into a specified folder, so that they can be loaded from there.
		/// </summary>
		/// <param name="location">A path to extract the font files into.</param>
		public static bool ExtractFontFiles(string location)
		{
			if (String.IsNullOrWhiteSpace(location))
			{
				throw new ArgumentNullException(nameof(location));
			}

			try
			{
				// make sure directory is  available
				if (!Directory.Exists(location))
				{
					Directory.CreateDirectory(location);
				}

				// try to extract all fonts
				var assembly = typeof(BdfFont).Assembly;
				foreach (var font in assembly.GetManifestResourceNames()
					.Where(r => r.EndsWith(".bdf")))
				{
					var fileName = Path.Combine(location, font.Replace("SharpPiLed.fonts.", String.Empty));

					if (!File.Exists(fileName))
					{
						using (var fileStream = File.Create(fileName))
						using (var assemblyStream = assembly.GetManifestResourceStream(font))
						{
							assemblyStream.CopyTo(fileStream);
						}
					}
				}
			}
			catch
			{
				return false;
			}

			return true;
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
