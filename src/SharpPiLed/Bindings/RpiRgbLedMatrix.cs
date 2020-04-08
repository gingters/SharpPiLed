namespace SharpPiLed.Bindings
{
	using System;
	using System.Runtime.InteropServices;

	internal static class RpiRgbLedMatrix
	{
		private const string LIBRARY_NAME = "librgbmatrix";

		#region Bindings for Canvas

		[DllImport(LIBRARY_NAME)]
		public static extern void led_canvas_get_size(IntPtr canvas, out int width, out int height);

		[DllImport(LIBRARY_NAME)]
		public static extern void led_canvas_set_pixel(IntPtr canvas, int x, int y, byte r, byte g, byte b);

		[DllImport(LIBRARY_NAME)]
		public static extern void led_canvas_clear(IntPtr canvas);

		[DllImport(LIBRARY_NAME)]
		public static extern void led_canvas_fill(IntPtr canvas, byte r, byte g, byte b);

		[DllImport(LIBRARY_NAME)]
		public static extern void draw_circle(IntPtr canvas, int xx, int y, int radius, byte r, byte g, byte b);

		[DllImport(LIBRARY_NAME)]
		public static extern void draw_line(IntPtr canvas, int x0, int y0, int x1, int y1, byte r, byte g, byte b);

		#endregion

		#region Bindings for Font

		[DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr load_font(string bdf_font_file);

		[DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int draw_text(IntPtr canvas, IntPtr font, int x, int y, byte r, byte g, byte b, string utf8_text, int extra_spacing);

		[DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int vertical_draw_text(IntPtr canvas, IntPtr font, int x, int y, byte r, byte g, byte b, string utf8_text, int kerning_offset);

		[DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void delete_font(IntPtr font);

		[DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int baseline_font(IntPtr font);

		[DllImport(LIBRARY_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int height_font(IntPtr font);

		#endregion

		#region Bindings for LedMatrix

		[DllImport(LIBRARY_NAME)]
		public static extern IntPtr led_matrix_create(int rows, int chained, int parallel);

		[DllImport(LIBRARY_NAME, CallingConvention= CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern IntPtr led_matrix_create_from_options_const_argv(
			ref NativeLedMatrixOptions options,
			int argc,
			string[] argv);

		[DllImport(LIBRARY_NAME)]
		public static extern void led_matrix_delete(IntPtr matrix);

		[DllImport(LIBRARY_NAME)]
		public static extern IntPtr led_matrix_create_offscreen_canvas(IntPtr matrix);

		[DllImport(LIBRARY_NAME)]
		public static extern IntPtr led_matrix_swap_on_vsync(IntPtr matrix, IntPtr canvas);

		[DllImport(LIBRARY_NAME)]
		public static extern IntPtr led_matrix_get_canvas(IntPtr matrix);

		[DllImport(LIBRARY_NAME)]
		public static extern byte led_matrix_get_brightness(IntPtr matrix);

		[DllImport(LIBRARY_NAME)]
		public static extern void led_matrix_set_brightness(IntPtr matrix, byte brightness);

		#endregion
	}
}
