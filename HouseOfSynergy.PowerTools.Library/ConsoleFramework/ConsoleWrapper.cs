using System;
using System.IO;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.ConsoleFramework
{
	public sealed class ConsoleWrapper
	{
		#region Constructors.

		//====================================================================================================
		// Constructors.
		//====================================================================================================

		internal ConsoleWrapper ()
		{
		}

		#endregion Constructors.

		#region Wrapped Members.

		//====================================================================================================
		// Wrapped Members.
		//====================================================================================================

		#region Wrapped Properties.

		//====================================================================================================
		// Wrapped Properties.
		//====================================================================================================

		public ConsoleColor BackgroundColor { get { return (Console.BackgroundColor); } set { Console.BackgroundColor = value; } }
		public Int32 BufferHeight { get { return (Console.BufferHeight); } set { Console.BufferHeight = value; } }
		public Int32 BufferWidth { get { return (Console.BufferWidth); } set { Console.BufferWidth = value; } }
		public Boolean CapsLock { get { return (Console.CapsLock); } }
		public Int32 CursorLeft { get { return (Console.CursorLeft); } set { Console.CursorLeft = value; } }
		public Int32 CursorSize { get { return (Console.CursorSize); } set { Console.CursorSize = value; } }
		public Int32 CursorTop { get { return (Console.CursorTop); } set { Console.CursorTop = value; } }
		public Boolean CursorVisible { get { return (Console.CursorVisible); } set { Console.CursorVisible = value; } }
		public TextWriter Error { get { return (Console.Error); } }
		public ConsoleColor ForegroundColor { get { return (Console.ForegroundColor); } set { Console.ForegroundColor = value; } }
		public TextReader In { get { return (Console.In); } }
		public Encoding InputEncoding { get { return (Console.InputEncoding); } set { Console.InputEncoding = value; } }
		//public Boolean IsErrorRedirected { get { return (Console.IsErrorRedirected); } }
		//public Boolean IsInputRedirected { get { return (Console.IsInputRedirected); } }
		//public Boolean IsOutputRedirected { get { return (Console.IsOutputRedirected); } }
		public Boolean KeyAvailable { get { return (Console.KeyAvailable); } }
		public Int32 LargestWindowHeight { get { return (Console.LargestWindowHeight); } }
		public Int32 LargestWindowWidth { get { return (Console.LargestWindowWidth); } }
		public Boolean NumberLock { get { return (Console.NumberLock); } }
		public TextWriter Out { get { return (Console.Out); } }
		public Encoding OutputEncoding { get { return (Console.OutputEncoding); } set { Console.OutputEncoding = value; } }
		public String Title { get { return (Console.Title); } set { Console.Title = value; } }
		public Boolean TreatControlCAsInput { get { return (Console.TreatControlCAsInput); } set { Console.TreatControlCAsInput = value; } }
		public Int32 WindowHeight { get { return (Console.WindowHeight); } set { Console.WindowHeight = value; } }
		public Int32 WindowLeft { get { return (Console.WindowLeft); } set { Console.WindowLeft = value; } }
		public Int32 WindowTop { get { return (Console.WindowTop); } set { Console.WindowTop = value; } }
		public Int32 WindowWidth { get { return (Console.WindowWidth); } set { Console.WindowWidth = value; } }

		#endregion Wrapped Properties.

		#region Wrapped Methods.

		//====================================================================================================
		// Wrapped Methods.
		//====================================================================================================

		public void Beep (Int32 frequency, Int32 duration) { Console.Beep(frequency, duration); }
		public void Beep () { Console.Beep(); }
		public void Clear () { Console.Clear(); }
		public void MoveBufferArea (Int32 sourceLeft, Int32 sourceTop, Int32 sourceWidth, Int32 sourceHeight, Int32 targetLeft, Int32 targetTop, Char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor) { Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor); }
		public void MoveBufferArea (Int32 sourceLeft, Int32 sourceTop, Int32 sourceWidth, Int32 sourceHeight, Int32 targetLeft, Int32 targetTop) { Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop); }
		public Stream OpenStandardError (Int32 bufferSize) { return (Console.OpenStandardError(bufferSize)); }
		public Stream OpenStandardError () { return (Console.OpenStandardError()); }
		public Stream OpenStandardInput (Int32 bufferSize) { return (Console.OpenStandardInput(bufferSize)); }
		public Stream OpenStandardInput () { return (Console.OpenStandardInput()); }
		public Stream OpenStandardOutput (Int32 bufferSize) { return (Console.OpenStandardOutput(bufferSize)); }
		public Stream OpenStandardOutput () { return (Console.OpenStandardOutput()); }
		public Int32 Read () { return (Console.Read()); }
		public ConsoleKeyInfo ReadKey () { return (Console.ReadKey()); }
		public ConsoleKeyInfo ReadKey (Boolean intercept) { return (Console.ReadKey(intercept)); }
		public String ReadLine () { return (Console.ReadLine()); }
		public void ResetColor () { Console.ResetColor(); }
		public void SetBufferSize (Int32 width, Int32 height) { Console.SetBufferSize(width, height); }
		public void SetCursorPosition (Int32 left, Int32 top) { Console.SetCursorPosition(left, top); }
		public void SetError (TextWriter newError) { Console.SetError(newError); }
		public void SetIn (TextReader newIn) { Console.SetIn(newIn); }
		public void SetOut (TextWriter newOut) { Console.SetOut(newOut); }
		public void SetWindowPosition (Int32 left, Int32 top) { Console.SetWindowPosition(left, top); }
		public void SetWindowSize (Int32 width, Int32 height) { Console.SetWindowSize(width, height); }
		public void Write (Boolean value) { Console.Write(value); }
		public void Write (String format, Object [] arg) { Console.Write(format, arg); }
		public void Write (Char [] buffer) { Console.Write(buffer); }
		public void Write (Char value) { Console.Write(value); }
		public void Write (String format, Object arg0, Object arg1) { Console.Write(format, arg0, arg1); }
		public void Write (String format, Object arg0) { Console.Write(format, arg0); }
		public void Write (String format, Object arg0, Object arg1, Object arg2, Object arg3) { Console.Write(format, arg0, arg1, arg2, arg3); }
		public void Write (String format, Object arg0, Object arg1, Object arg2) { Console.Write(format, arg0, arg1, arg2); }
		public void Write (Char [] buffer, Int32 index, Int32 count) { Console.Write(buffer, index, count); }
		public void Write (Int64 value) { Console.Write(value); }
		public void Write (UInt32 value) { Console.Write(value); }
		public void Write (Object value) { Console.Write(value); }
		public void Write (UInt64 value) { Console.Write(value); }
		public void Write (Decimal value) { Console.Write(value); }
		public void Write (Double value) { Console.Write(value); }
		public void Write (Int32 value) { Console.Write(value); }
		public void Write (Single value) { Console.Write(value); }
		public void Write (String value) { Console.Write(value); }
		public void WriteLine (Double value) { Console.WriteLine(value); }
		public void WriteLine (Decimal value) { Console.WriteLine(value); }
		public void WriteLine (Int32 value) { Console.WriteLine(value); }
		public void WriteLine (Single value) { Console.WriteLine(value); }
		public void WriteLine (Char [] buffer, Int32 index, Int32 count) { Console.WriteLine(buffer, index, count); }
		public void WriteLine (Boolean value) { Console.WriteLine(value); }
		public void WriteLine () { Console.WriteLine(); }
		public void WriteLine (Char [] buffer) { Console.WriteLine(buffer); }
		public void WriteLine (Char value) { Console.WriteLine(value); }
		public void WriteLine (String format, Object arg0, Object arg1) { Console.WriteLine(format, arg0, arg1); }
		public void WriteLine (String format, Object arg0) { Console.WriteLine(format, arg0); }
		public void WriteLine (String format, Object arg0, Object arg1, Object arg2) { Console.WriteLine(format, arg0, arg1, arg2); }
		public void WriteLine (String format, Object arg0, Object arg1, Object arg2, Object arg3) { Console.WriteLine(format, arg0, arg1, arg2, arg3); }
		public void WriteLine (String format, Object [] arg) { Console.WriteLine(format, arg); }
		public void WriteLine (Int64 value) { Console.WriteLine(value); }
		public void WriteLine (UInt32 value) { Console.WriteLine(value); }
		public void WriteLine (UInt64 value) { Console.WriteLine(value); }
		public void WriteLine (String value) { Console.WriteLine(value); }
		public void WriteLine (Object value) { Console.WriteLine(value); }

		#endregion Wrapped Methods.

		#endregion Wrapped Members.

		#region Custom Members.

		//====================================================================================================
		// Custom Members.
		//====================================================================================================

		#region Custom Properties.

		//====================================================================================================
		// Custom Properties.
		//====================================================================================================

		public ConsolePosition CursorPosition { get { return (this.GetCursorPosition()); } }

		#endregion Custom Properties.

		#region Custom Methods.

		//====================================================================================================
		// Custom Methods.
		//====================================================================================================

		public ConsolePosition GetCursorPosition () { return (new ConsolePosition(Console.CursorLeft, Console.CursorTop)); }

		public Int32 ReadAt (int x, int y) { this.SetCursorPosition(x, y); return (this.Read()); }
		public ConsoleKeyInfo ReadKeyAt (int x, int y) { this.SetCursorPosition(x, y); return (this.ReadKey()); }
		public ConsoleKeyInfo ReadKeyAt (int x, int y, Boolean intercept) { this.SetCursorPosition(x, y); return (this.ReadKey(intercept)); }
		public String ReadLineAt (int x, int y) { this.SetCursorPosition(x, y); return (this.ReadLine()); }

		public Int32 ReadAt (ConsolePosition point) { this.SetCursorPosition(point); return (this.Read()); }
		public ConsoleKeyInfo ReadKeyAt (ConsolePosition point) { this.SetCursorPosition(point); return (this.ReadKey()); }
		public ConsoleKeyInfo ReadKeyAt (ConsolePosition point, Boolean intercept) { this.SetCursorPosition(point); return (this.ReadKey(intercept)); }
		public String ReadLineAt (ConsolePosition point) { this.SetCursorPosition(point); return (this.ReadLine()); }

		public void SetCursorPosition (ConsolePosition position) { this.SetCursorPosition(position.Left, position.Top); }
		public void SetWindowPosition (ConsolePosition position) { this.SetWindowPosition(position.Left, position.Top); }
		public void SetWindowSize (ConsoleDimension size) { this.SetWindowSize(size.Width, size.Height); }

		public void WriteAt (int x, int y, Boolean value) { this.SetCursorPosition(x, y); this.Write(value); }
		public void WriteAt (int x, int y, String format, Object [] arg) { this.SetCursorPosition(x, y); this.Write(format, arg); }
		public void WriteAt (int x, int y, Char [] buffer) { this.SetCursorPosition(x, y); this.Write(buffer); }
		public void WriteAt (int x, int y, Char value) { this.SetCursorPosition(x, y); this.Write(value); }
		public void WriteAt (int x, int y, String format, Object arg0, Object arg1) { this.SetCursorPosition(x, y); this.Write(format, arg0, arg1); }
		public void WriteAt (int x, int y, String format, Object arg0) { this.SetCursorPosition(x, y); this.Write(format, arg0); }
		public void WriteAt (int x, int y, String format, Object arg0, Object arg1, Object arg2, Object arg3) { this.SetCursorPosition(x, y); this.Write(format, arg0, arg1, arg2, arg3); }
		public void WriteAt (int x, int y, String format, Object arg0, Object arg1, Object arg2) { this.SetCursorPosition(x, y); this.Write(format, arg0, arg1, arg2); }
		public void WriteAt (int x, int y, Char [] buffer, Int32 index, Int32 count) { this.SetCursorPosition(x, y); this.Write(buffer, index, count); }
		public void WriteAt (int x, int y, Int64 value) { this.SetCursorPosition(x, y); this.Write(value); }
		public void WriteAt (int x, int y, UInt32 value) { this.SetCursorPosition(x, y); this.Write(value); }
		public void WriteAt (int x, int y, Object value) { this.SetCursorPosition(x, y); this.Write(value); }
		public void WriteAt (int x, int y, UInt64 value) { this.SetCursorPosition(x, y); this.Write(value); }
		public void WriteAt (int x, int y, Decimal value) { this.SetCursorPosition(x, y); this.Write(value); }
		public void WriteAt (int x, int y, Double value) { this.SetCursorPosition(x, y); this.Write(value); }
		public void WriteAt (int x, int y, Int32 value) { this.SetCursorPosition(x, y); this.Write(value); }
		public void WriteAt (int x, int y, Single value) { this.SetCursorPosition(x, y); this.Write(value); }
		public void WriteAt (int x, int y, String value) { this.SetCursorPosition(x, y); this.Write(value); }
		public void WriteAt (ConsolePosition position, Boolean value) { this.SetCursorPosition(position); this.Write(value); }
		public void WriteAt (ConsolePosition position, String format, Object [] arg) { this.SetCursorPosition(position); this.Write(format, arg); }
		public void WriteAt (ConsolePosition position, Char [] buffer) { this.SetCursorPosition(position); this.Write(buffer); }
		public void WriteAt (ConsolePosition position, Char value) { this.SetCursorPosition(position); this.Write(value); }
		public void WriteAt (ConsolePosition position, String format, Object arg0, Object arg1) { this.SetCursorPosition(position); this.Write(format, arg0, arg1); }
		public void WriteAt (ConsolePosition position, String format, Object arg0) { this.SetCursorPosition(position); this.Write(format, arg0); }
		public void WriteAt (ConsolePosition position, String format, Object arg0, Object arg1, Object arg2, Object arg3) { this.SetCursorPosition(position); this.Write(format, arg0, arg1, arg2, arg3); }
		public void WriteAt (ConsolePosition position, String format, Object arg0, Object arg1, Object arg2) { this.SetCursorPosition(position); this.Write(format, arg0, arg1, arg2); }
		public void WriteAt (ConsolePosition position, Char [] buffer, Int32 index, Int32 count) { this.SetCursorPosition(position); this.Write(buffer, index, count); }
		public void WriteAt (ConsolePosition position, Int64 value) { this.SetCursorPosition(position); this.Write(value); }
		public void WriteAt (ConsolePosition position, UInt32 value) { this.SetCursorPosition(position); this.Write(value); }
		public void WriteAt (ConsolePosition position, Object value) { this.SetCursorPosition(position); this.Write(value); }
		public void WriteAt (ConsolePosition position, UInt64 value) { this.SetCursorPosition(position); this.Write(value); }
		public void WriteAt (ConsolePosition position, Decimal value) { this.SetCursorPosition(position); this.Write(value); }
		public void WriteAt (ConsolePosition position, Double value) { this.SetCursorPosition(position); this.Write(value); }
		public void WriteAt (ConsolePosition position, Int32 value) { this.SetCursorPosition(position); this.Write(value); }
		public void WriteAt (ConsolePosition position, Single value) { this.SetCursorPosition(position); this.Write(value); }
		public void WriteAt (ConsolePosition position, String value) { this.SetCursorPosition(position); this.Write(value); }

		public void WriteAtRestoreAfter (int x, int y, Boolean value) { var p = this.GetCursorPosition(); this.WriteAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, String format, Object [] arg) { var p = this.GetCursorPosition(); this.WriteAt(x, y, format, arg); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, Char [] buffer) { var p = this.GetCursorPosition(); this.WriteAt(x, y, buffer); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, Char value) { var p = this.GetCursorPosition(); this.WriteAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, String format, Object arg0, Object arg1) { var p = this.GetCursorPosition(); this.WriteAt(x, y, format, arg0, arg1); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, String format, Object arg0) { var p = this.GetCursorPosition(); this.WriteAt(x, y, format, arg0); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, String format, Object arg0, Object arg1, Object arg2, Object arg3) { var p = this.GetCursorPosition(); this.WriteAt(x, y, format, arg0, arg1, arg2, arg3); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, String format, Object arg0, Object arg1, Object arg2) { var p = this.GetCursorPosition(); this.WriteAt(x, y, format, arg0, arg1, arg2); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, Char [] buffer, Int32 index, Int32 count) { var p = this.GetCursorPosition(); this.WriteAt(x, y, buffer, index, count); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, Int64 value) { var p = this.GetCursorPosition(); this.WriteAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, UInt32 value) { var p = this.GetCursorPosition(); this.WriteAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, Object value) { var p = this.GetCursorPosition(); this.WriteAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, UInt64 value) { var p = this.GetCursorPosition(); this.WriteAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, Decimal value) { var p = this.GetCursorPosition(); this.WriteAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, Double value) { var p = this.GetCursorPosition(); this.WriteAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, Int32 value) { var p = this.GetCursorPosition(); this.WriteAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, Single value) { var p = this.GetCursorPosition(); this.WriteAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (int x, int y, String value) { var p = this.GetCursorPosition(); this.WriteAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, Boolean value) { var p = this.GetCursorPosition(); this.WriteAt(position, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, String format, Object [] arg) { var p = this.GetCursorPosition(); this.WriteAt(position, format, arg); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, Char [] buffer) { var p = this.GetCursorPosition(); this.WriteAt(position, buffer); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, Char value) { var p = this.GetCursorPosition(); this.WriteAt(position, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, String format, Object arg0, Object arg1) { var p = this.GetCursorPosition(); this.WriteAt(position, format, arg0, arg1); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, String format, Object arg0) { var p = this.GetCursorPosition(); this.WriteAt(position, format, arg0); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, String format, Object arg0, Object arg1, Object arg2, Object arg3) { var p = this.GetCursorPosition(); this.WriteAt(position, format, arg0, arg1, arg2, arg3); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, String format, Object arg0, Object arg1, Object arg2) { var p = this.GetCursorPosition(); this.WriteAt(position, format, arg0, arg1, arg2); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, Char [] buffer, Int32 index, Int32 count) { var p = this.GetCursorPosition(); this.WriteAt(position, buffer, index, count); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, Int64 value) { var p = this.GetCursorPosition(); this.WriteAt(position, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, UInt32 value) { var p = this.GetCursorPosition(); this.WriteAt(position, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, Object value) { var p = this.GetCursorPosition(); this.WriteAt(position, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, UInt64 value) { var p = this.GetCursorPosition(); this.WriteAt(position, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, Decimal value) { var p = this.GetCursorPosition(); this.WriteAt(position, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, Double value) { var p = this.GetCursorPosition(); this.WriteAt(position, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, Int32 value) { var p = this.GetCursorPosition(); this.WriteAt(position, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, Single value) { var p = this.GetCursorPosition(); this.WriteAt(position, value); this.SetCursorPosition(p); }
		public void WriteAtRestoreAfter (ConsolePosition position, String value) { var p = this.GetCursorPosition(); this.WriteAt(position, value); this.SetCursorPosition(p); }

		public void WriteLineAt (int x, int y, Double value) { this.SetCursorPosition(x, y); this.WriteLine(value); }
		public void WriteLineAt (int x, int y, Decimal value) { this.SetCursorPosition(x, y); this.WriteLine(value); }
		public void WriteLineAt (int x, int y, Int32 value) { this.SetCursorPosition(x, y); this.WriteLine(value); }
		public void WriteLineAt (int x, int y, Single value) { this.SetCursorPosition(x, y); this.WriteLine(value); }
		public void WriteLineAt (int x, int y, Char [] buffer, Int32 index, Int32 count) { this.SetCursorPosition(x, y); this.WriteLine(buffer, index, count); }
		public void WriteLineAt (int x, int y, Boolean value) { this.SetCursorPosition(x, y); this.WriteLine(value); }
		public void WriteLineAt (int x, int y) { this.SetCursorPosition(x, y); this.WriteLine(); }
		public void WriteLineAt (int x, int y, Char [] buffer) { this.SetCursorPosition(x, y); this.WriteLine(buffer); }
		public void WriteLineAt (int x, int y, Char value) { this.SetCursorPosition(x, y); this.WriteLine(value); }
		public void WriteLineAt (int x, int y, String format, Object arg0, Object arg1) { this.SetCursorPosition(x, y); this.WriteLine(format, arg0, arg1); }
		public void WriteLineAt (int x, int y, String format, Object arg0) { this.SetCursorPosition(x, y); this.WriteLine(format, arg0); }
		public void WriteLineAt (int x, int y, String format, Object arg0, Object arg1, Object arg2) { this.SetCursorPosition(x, y); this.WriteLine(format, arg0, arg1, arg2); }
		public void WriteLineAt (int x, int y, String format, Object arg0, Object arg1, Object arg2, Object arg3) { this.SetCursorPosition(x, y); this.WriteLine(format, arg0, arg1, arg2, arg3); }
		public void WriteLineAt (int x, int y, String format, Object [] arg) { this.SetCursorPosition(x, y); this.WriteLine(format, arg); }
		public void WriteLineAt (int x, int y, Int64 value) { this.SetCursorPosition(x, y); this.WriteLine(value); }
		public void WriteLineAt (int x, int y, UInt32 value) { this.SetCursorPosition(x, y); this.WriteLine(value); }
		public void WriteLineAt (int x, int y, UInt64 value) { this.SetCursorPosition(x, y); this.WriteLine(value); }
		public void WriteLineAt (int x, int y, String value) { this.SetCursorPosition(x, y); this.WriteLine(value); }
		public void WriteLineAt (int x, int y, Object value) { this.SetCursorPosition(x, y); this.WriteLine(value); }
		public void WriteLineAt (ConsolePosition position, Double value) { this.SetCursorPosition(position); this.WriteLine(value); }
		public void WriteLineAt (ConsolePosition position, Decimal value) { this.SetCursorPosition(position); this.WriteLine(value); }
		public void WriteLineAt (ConsolePosition position, Int32 value) { this.SetCursorPosition(position); this.WriteLine(value); }
		public void WriteLineAt (ConsolePosition position, Single value) { this.SetCursorPosition(position); this.WriteLine(value); }
		public void WriteLineAt (ConsolePosition position, Char [] buffer, Int32 index, Int32 count) { this.SetCursorPosition(position); this.WriteLine(buffer, index, count); }
		public void WriteLineAt (ConsolePosition position, Boolean value) { this.SetCursorPosition(position); this.WriteLine(value); }
		public void WriteLineAt (ConsolePosition position) { this.SetCursorPosition(position); this.WriteLine(); }
		public void WriteLineAt (ConsolePosition position, Char [] buffer) { this.SetCursorPosition(position); this.WriteLine(buffer); }
		public void WriteLineAt (ConsolePosition position, Char value) { this.SetCursorPosition(position); this.WriteLine(value); }
		public void WriteLineAt (ConsolePosition position, String format, Object arg0, Object arg1) { this.SetCursorPosition(position); this.WriteLine(format, arg0, arg1); }
		public void WriteLineAt (ConsolePosition position, String format, Object arg0) { this.SetCursorPosition(position); this.WriteLine(format, arg0); }
		public void WriteLineAt (ConsolePosition position, String format, Object arg0, Object arg1, Object arg2) { this.SetCursorPosition(position); this.WriteLine(format, arg0, arg1, arg2); }
		public void WriteLineAt (ConsolePosition position, String format, Object arg0, Object arg1, Object arg2, Object arg3) { this.SetCursorPosition(position); this.WriteLine(format, arg0, arg1, arg2, arg3); }
		public void WriteLineAt (ConsolePosition position, String format, Object [] arg) { this.SetCursorPosition(position); this.WriteLine(format, arg); }
		public void WriteLineAt (ConsolePosition position, Int64 value) { this.SetCursorPosition(position); this.WriteLine(value); }
		public void WriteLineAt (ConsolePosition position, UInt32 value) { this.SetCursorPosition(position); this.WriteLine(value); }
		public void WriteLineAt (ConsolePosition position, UInt64 value) { this.SetCursorPosition(position); this.WriteLine(value); }
		public void WriteLineAt (ConsolePosition position, String value) { this.SetCursorPosition(position); this.WriteLine(value); }
		public void WriteLineAt (ConsolePosition position, Object value) { this.SetCursorPosition(position); this.WriteLine(value); }

		public void WriteLineAtRestoreAfter (int x, int y, Boolean value) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, String format, Object [] arg) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, format, arg); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, Char [] buffer) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, buffer); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, Char value) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, String format, Object arg0, Object arg1) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, format, arg0, arg1); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, String format, Object arg0) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, format, arg0); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, String format, Object arg0, Object arg1, Object arg2, Object arg3) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, format, arg0, arg1, arg2, arg3); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, String format, Object arg0, Object arg1, Object arg2) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, format, arg0, arg1, arg2); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, Char [] buffer, Int32 index, Int32 count) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, buffer, index, count); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, Int64 value) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, UInt32 value) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, Object value) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, UInt64 value) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, Decimal value) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, Double value) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, Int32 value) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, Single value) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (int x, int y, String value) { var p = this.GetCursorPosition(); this.WriteLineAt(x, y, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, Boolean value) { var p = this.GetCursorPosition(); this.WriteLineAt(position, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, String format, Object [] arg) { var p = this.GetCursorPosition(); this.WriteLineAt(position, format, arg); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, Char [] buffer) { var p = this.GetCursorPosition(); this.WriteLineAt(position, buffer); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, Char value) { var p = this.GetCursorPosition(); this.WriteLineAt(position, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, String format, Object arg0, Object arg1) { var p = this.GetCursorPosition(); this.WriteLineAt(position, format, arg0, arg1); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, String format, Object arg0) { var p = this.GetCursorPosition(); this.WriteLineAt(position, format, arg0); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, String format, Object arg0, Object arg1, Object arg2, Object arg3) { var p = this.GetCursorPosition(); this.WriteLineAt(position, format, arg0, arg1, arg2, arg3); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, String format, Object arg0, Object arg1, Object arg2) { var p = this.GetCursorPosition(); this.WriteLineAt(position, format, arg0, arg1, arg2); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, Char [] buffer, Int32 index, Int32 count) { var p = this.GetCursorPosition(); this.WriteLineAt(position, buffer, index, count); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, Int64 value) { var p = this.GetCursorPosition(); this.WriteLineAt(position, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, UInt32 value) { var p = this.GetCursorPosition(); this.WriteLineAt(position, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, Object value) { var p = this.GetCursorPosition(); this.WriteLineAt(position, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, UInt64 value) { var p = this.GetCursorPosition(); this.WriteLineAt(position, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, Decimal value) { var p = this.GetCursorPosition(); this.WriteLineAt(position, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, Double value) { var p = this.GetCursorPosition(); this.WriteLineAt(position, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, Int32 value) { var p = this.GetCursorPosition(); this.WriteLineAt(position, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, Single value) { var p = this.GetCursorPosition(); this.WriteLineAt(position, value); this.SetCursorPosition(p); }
		public void WriteLineAtRestoreAfter (ConsolePosition position, String value) { var p = this.GetCursorPosition(); this.WriteLineAt(position, value); this.SetCursorPosition(p); }

		#endregion Custom Methods.

		#endregion Custom Members.
	}
}