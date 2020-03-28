
namespace SharpPiLed
{
	using System;
	using Bindings;

	public class LedMatrix : IDisposable
	{
		private IntPtr _matrix;

		public LedMatrix(LedMatrixOptions options = null, string[] arguments = null)
		{
			options = options ?? new LedMatrixOptions();
			var opt = new NativeLedMatrixOptions(options);

			try
			{
				var nativeArguments = arguments.ConvertToNativeArguments(options);

				_matrix = RpiRgbLedMatrix.led_matrix_create_from_options_const_argv(ref opt, nativeArguments.Length, nativeArguments);
			}
			finally
			{
				if (options.HardwareMapping != null) opt.Free(ref opt.hardware_mapping);
				if (options.LedRgbSequence != null) opt.Free(ref opt.led_rgb_sequence);
				if (options.PixelMapperConfig != null) opt.Free(ref opt.pixel_mapper_config);
				if (options.PanelType != null) opt.Free(ref opt.panel_type);
			}
		}

		public Canvas CreateOffscreenCanvas()
		{
			var canvas = RpiRgbLedMatrix.led_matrix_create_offscreen_canvas(_matrix);
			return new Canvas(canvas);
		}

		public Canvas GetCanvas()
		{
			var canvas = RpiRgbLedMatrix.led_matrix_get_canvas(_matrix);
			return new Canvas(canvas);
		}

		public Canvas SwapOnVsync(Canvas canvas)
		{

			canvas._canvas = RpiRgbLedMatrix.led_matrix_swap_on_vsync(_matrix, canvas._canvas);
			return canvas;
		}

		public byte Brightness
		{
			get { return RpiRgbLedMatrix.led_matrix_get_brightness(_matrix); }
			set { RpiRgbLedMatrix.led_matrix_set_brightness(_matrix, value); }
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

				RpiRgbLedMatrix.led_matrix_delete(_matrix);
				_matrix = IntPtr.Zero;

				disposedValue = true;
			}
		}

		~LedMatrix()
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
