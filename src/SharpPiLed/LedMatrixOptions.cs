namespace SharpPiLed
{
	public class LedMatrixOptions
	{
		/// <summary>
		/// Name of the hardware mapping used. Something like "regular" or "adafruit-hat".
		/// Default (if NULL): "regular"
		/// </summary>
		public string HardwareMapping { get; set; }

		/// <summary>
		/// The "rows" are the number of rows supported by the display, so 32 or 16.
		/// Default: 32.
		/// </summary>
		public int Rows { get; set; } = 32;

		/// <summary>
		/// The "cols" are the number of columns per panel. Typically something
		/// like 32, but also 64 is possible. Sometimes even 40.
		/// cols * chain_length is the total length of the display, so you can
		/// represent a 64 wide display as cols=32, chain=2 or cols=64, chain=1;
		/// same thing, but more convenient to think of.
		/// Default: 32.
		/// </summary>
		public int Columns { get; set; } = 32;

		/// <summary>
		/// The chain_length is the number of displays daisy-chained together
		/// (output of one connected to input of next).
		/// Default: 1
		/// </summary>
		public int ChainLength { get; set; } = 1;

		/// <summary>
		/// The number of parallel chains connected to the Pi; in old Pis with 26
		/// GPIO pins, that is 1, in newer Pis with 40 interfaces pins, that can also
		/// be 2 or 3. The effective number of pixels in vertical direction is then
		/// thus rows * parallel.
		/// Default: 1
		/// </summary>
		public int Parallel { get; set; } = 1;

		/// <summary>
		/// Set PWM bits used for output. Default is 11, but if you only deal with limited
		/// comic-colors, 1 might be sufficient. Lower require less CPU and increases refresh-rate.
		/// </summary>
		public int PwmBits { get; set; } = 11;

		/// <summary>
		/// Change the base time-unit for the on-time in the lowest significant bit in
		/// nanoseconds. Higher numbers provide better quality (more accurate color, less
		/// ghosting), but have a negative impact on the frame rate.
		/// </summary>
		public int PwmLsbNanoseconds { get; set; }

		/// <summary>
		/// The lower bits can be time-dithered for higher refresh rate.
		/// </summary>
		public int PwmDitherBits { get; set; }

		/// <summary>
		/// The initial brightness of the panel in percent. Valid range is 1..100
		/// Default: 100
		/// </summary>
		public int Brightness { get; set; } = 100;

		/// <summary>
		/// Scan mode: 0=progressive, 1=interlaced
		/// </summary>
		public ScanMode ScanMode { get; set; } = ScanMode.Progressive;

		/// <summary>
		/// Default row address type is 0, corresponding to direct setting of the
		/// row, while row address type 1 is used for panels that only have A/B,
		/// typically some 64x64 panels
		/// </summary>
		public RowAddressType RowAddressType { get; set; } = RowAddressType.Default;

		/// <summary>
		/// Type of multiplexing. 0 = direct, 1 = stripe, 2 = checker (typical 1:8)
		/// </summary>
		public Multiplexing Multiplexing { get; set; }

		/// <summary>
		/// In case the internal sequence of mapping is not "RGB", this contains the real mapping. Some panels mix up these colors.
		/// </summary>
		public string LedRgbSequence { get; set; }

		/// <summary>
		/// A string describing a sequence of pixel mappers that should be applied
		/// to this matrix. A semicolon-separated list of pixel-mappers with optional
		/// parameter.
		public string PixelMapperConfig { get; set; }

		/// <summary>
		/// Panel type. Typically just empty, but certain panels (FM6126)
		/// require an initialization sequence
		/// </summary>
		public string PanelType { get; set; }

		/// <summary>
		/// Allow to use the hardware subsystem to create pulses. This won't do anything if output enable is not connected to GPIO 18.
		/// </summary>
		public bool DisableHardwarePulsing { get; set; }

		public bool ShowRefreshRate { get; set; }

		public bool InverseColors { get; set; }

		/// <summary>
		/// Limit refresh rate of LED panel. This will help on a loaded system
		// to keep a constant refresh rate. <= 0 for no limit.
		/// </summary>
		public int LimitRefreshRateHz { get; set; }

		/// <summary>
		/// Slowdown GPIO. Needed for faster Pis/slower panels.
		/// </summary>
		public int GpioSlowdown { get; set; } = 1;
	}
}
