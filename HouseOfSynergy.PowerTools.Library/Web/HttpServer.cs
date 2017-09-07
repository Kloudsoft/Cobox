using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using HouseOfSynergy.PowerTools.Library.EventArguments;
using HouseOfSynergy.PowerTools.Library.Threading;

namespace HouseOfSynergy.PowerTools.Library.Web
{
	public class HttpServer:
		ThreadBase
	{
		protected static readonly Encoding EncodingDefault = Encoding.UTF8;
		public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);

		private Encoding Encoding { get; set; } = HttpServer.EncodingDefault;

		/// <summary>
		/// Example: [http://localhost:55487/].
		/// </summary>
		private IEnumerable<Uri> Prefixes { get; set; }
		private HttpListener HttpListener { get; set; }

		public HttpServer (Uri uri) : this(new Uri [] { uri }) { }
		public HttpServer (IEnumerable<Uri> uris) : this(uris, HttpServer.EncodingDefault) { }
		public HttpServer (Uri uri, Encoding encoding) : this(new Uri [] { uri }, encoding) { }

		public HttpServer (IEnumerable<Uri> uris, Encoding encoding)
		{
			if (uris == null) { throw (new ArgumentNullException("uris")); }
			if (encoding == null) { throw (new ArgumentNullException("encoding")); }
			if (!uris.Any()) { throw (new ArgumentException("The argument [uris] must contain at least one element.", "uris")); }

			this.Encoding = encoding;
			this.HttpListener = new HttpListener();

			foreach (var uri in uris)
			{
				this.HttpListener.Prefixes.Add(uri.ToString());
			}
		}

		public virtual TimeSpan Timeout { get { return (HttpServer.DefaultTimeout); } }

		protected sealed override void OnStarting (CancelEventArgs e)
		{
			this.HttpListener.Start();
		}

		protected sealed override void OnStopping (CancelEventArgs e)
		{
			this.HttpListener.Stop();
		}

		protected sealed override void OnStarted () { }
		protected sealed override void OnStopped () { }

		protected sealed override void OnProcess (CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				if (!this.HttpListener.IsListening) { break; }
				if (cancellationToken.IsCancellationRequested) { break; }

				var context = this.HttpListener.GetContext();

				if (!this.HttpListener.IsListening) { break; }
				if (cancellationToken.IsCancellationRequested) { break; }

				this.OnContextCreated(context, cancellationToken);

				context.Response.Close();

				if (!this.HttpListener.IsListening) { break; }
				if (cancellationToken.IsCancellationRequested) { break; }

				Thread.Sleep(TimeSpan.FromMilliseconds(100));
			}
		}

		protected virtual void OnContextCreated (HttpListenerContext context, CancellationToken cancellationToken)
		{
			var count = 0;
			var timedout = false;
			var data = new byte [] { };
			var watch = Stopwatch.StartNew();
			var buffer = new byte [context.Request.ContentLength64];
			var encoding = ((this.Encoding) ?? ((context.Request.ContentEncoding) ?? (HttpServer.EncodingDefault)));

			do
			{
				if (watch.Elapsed >= this.Timeout) { timedout = true; break; }
				count += context.Request.InputStream.Read(buffer, 0, buffer.Length - count);
			}
			while (count < context.Request.ContentLength64);

			if (timedout)
			{
				data = this.OnTimeout(buffer, encoding, cancellationToken);
			}
			else
			{
				data = this.OnDataReceived(buffer, encoding, cancellationToken);
			}

			context.Response.ContentEncoding = encoding;
			context.Response.ContentLength64 = data.Length;
			context.Response.OutputStream.Write(data, 0, data.Length);
		}

		protected virtual byte [] OnDataReceived (byte [] buffer, Encoding encoding, CancellationToken cancellationToken)
		{
			var html = encoding.GetString(buffer);
			var data = encoding.GetBytes(html);

			return (data);
		}

		protected virtual byte [] OnTimeout (byte [] buffer, Encoding encoding, CancellationToken cancellationToken)
		{
			return (buffer);
		}

		private bool Disposed { get; set; }

		protected override void Dispose (bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					// Managed.

					try { this.HttpListener.Stop(); }
					catch { }

					try { this.HttpListener.Close(); }
					catch { }

					try { (this.HttpListener as IDisposable)?.Dispose(); }
					finally { this.HttpListener = null; }
				}

				// Unmanaged.

				this.Disposed = true;
			}

			base.Dispose(disposing);
		}
	}
}