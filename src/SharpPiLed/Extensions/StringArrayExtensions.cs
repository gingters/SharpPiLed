using System;

namespace SharpPiLed
{
	internal static class StringArrayExtensions
	{
		public static string[] ConvertToNativeArguments(this string[] args, LedMatrixOptions options)
		{
			args = args ?? Environment.GetCommandLineArgs();

			// Because gpio-slowdown is not provided in the options struct, we manually add it.
			// Let's add it first to the command-line we pass to the
			// matrix constructor, so that it can be overridden with the
			// users' commandline.
			// As always, as the _very_ first, we need to provide the
			// program name argv[0], so this is why our slowdown_arg
			// array will have these two elements.
			//
			// Given that we can't initialize the C# struct with a slowdown
			// that is not 0, we just override it here with 1 if we see 0
			// (zero only really is usable on super-slow vey old Rpi1,
			// but for everyone else, it would be a nuisance. So we use
			// 0 as our sentinel).
			string[] slowdown_arg = new string[] { args[0], $"--led-slowdown-gpio={((options.GpioSlowdown == 0) ? 1 : options.GpioSlowdown)}" };

			string[] argv = new string[ 2 + args.Length - 1 ];

			// Progname + slowdown arg first
			slowdown_arg.CopyTo(argv, 0);

			// Remaining args (excluding program name) then. This allows
			// the user to not only provide any of the other --led-*
			// options, but also override the --led-slowdown-gpio arg on
			// the commandline.
			Array.Copy(args, 1, argv, 2, args.Length-1);

			return argv;
		}
	}
}
