namespace SharpPiLed.Bindings
{
	using System;
	using System.Runtime.InteropServices;

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct NativeLedMatrixOptions
	{
		public IntPtr hardware_mapping;
		public int rows;
		public int cols;
		public int chain_length;
		public int parallel;
		public int pwm_bits;
		public int pwm_lsb_nanoseconds;
		public int pwm_dither_bits;
		public int brightness;
		public int scan_mode;
		public int row_address_type;
		public int multiplexing;
		public IntPtr led_rgb_sequence;
		public IntPtr pixel_mapper_config;
		public IntPtr panel_type;
		public byte disable_hardware_pulsing;
		public byte show_refresh_rate;
		public byte inverse_colors;
		public int limit_refresh_rate_hz;

		public NativeLedMatrixOptions(LedMatrixOptions options)
		{
			chain_length = options.ChainLength;
			rows = options.Rows;
			cols = options.Columns;
			hardware_mapping = options.HardwareMapping != null ? Marshal.StringToHGlobalAnsi(options.HardwareMapping) : IntPtr.Zero;
			inverse_colors = (byte) (options.InverseColors ? 1 : 0);
			led_rgb_sequence = options.LedRgbSequence != null ? Marshal.StringToHGlobalAnsi(options.LedRgbSequence) : IntPtr.Zero;
			pixel_mapper_config = options.PixelMapperConfig != null ? Marshal.StringToHGlobalAnsi(options.PixelMapperConfig) : IntPtr.Zero;
			panel_type = options.PanelType != null ? Marshal.StringToHGlobalAnsi(options.PanelType) : IntPtr.Zero;
			parallel = options.Parallel;
			multiplexing = (int) options.Multiplexing;
			pwm_bits = options.PwmBits;
			pwm_lsb_nanoseconds = options.PwmLsbNanoseconds;
			pwm_dither_bits = options.PwmDitherBits;
			scan_mode = (int) options.ScanMode;
			show_refresh_rate = (byte) (options.ShowRefreshRate ? 1 : 0);
			limit_refresh_rate_hz = options.LimitRefreshRateHz;
			brightness = options.Brightness;
			disable_hardware_pulsing = (byte) (options.DisableHardwarePulsing ? 1 : 0);
			row_address_type = (int) options.RowAddressType;
		}

		public void Free(ref IntPtr pointer)
		{
			Marshal.FreeHGlobal(pointer);
			pointer = IntPtr.Zero;
		}
	}
}
