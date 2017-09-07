using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace HouseOfSynergy.PowerTools.Library.Imaging
{
	public static class ImagingUtilities
	{
		public static void GenerateRandomNoise (Bitmap bitmap, byte opacity = 255, RandomNumberGenerator randomNumberGenerator = null)
		{
			var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);

			var data = new byte [bitmapData.Stride * bitmapData.Height];

			if (randomNumberGenerator == null)
			{
				using (var generator = RandomNumberGenerator.Create())
				{
					generator.GetBytes(data);
				}
			}
			else
			{
				randomNumberGenerator.GetBytes(data);
			}

			if (bitmapData.PixelFormat == PixelFormat.Format32bppArgb)
			{
				for (int i = 3; i < data.Length; i += 4)
				{
					data [i] = opacity;
				}
			}

			Marshal.Copy(data, 0, bitmapData.Scan0, data.Length);

			bitmap.UnlockBits(bitmapData);
		}

		/// <summary>
		/// Represents a dictionary of image codecs. Use ImageFormat.Guid as the key for access.
		/// </summary>
		public static Dictionary<Guid, List<ImageCodecInfo>> ImageEncoders { get; private set; }

		static ImagingUtilities ()
		{
			ImagingUtilities.ImageEncoders = new Dictionary<Guid, List<ImageCodecInfo>>();

			var properties = typeof(ImageFormat)
				.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty)
				.Where(property => ((property.CanRead) && (!property.CanWrite) && (property.PropertyType == typeof(ImageFormat))))
				.ToList();

			foreach (var property in properties)
			{
				var format = (ImageFormat) property.GetValue(null, null);
				var encoders = ImagingUtilities.GetImageCodecInfos(format);

				if (encoders.Count == 0)
				{
					ImagingUtilities.ImageEncoders.Add(format.Guid, new List<ImageCodecInfo>());
				}
				else
				{
					ImagingUtilities.ImageEncoders.Add(format.Guid, new List<ImageCodecInfo>(encoders));
				}
			}
		}

		public static ImageCodecInfo GetImageCodecInfo (ImageFormat format)
		{
			return (ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => (codec.FormatID == format.Guid)));
		}

		public static List<ImageCodecInfo> GetImageCodecInfos (ImageFormat format)
		{
			return (ImageCodecInfo.GetImageDecoders().Where(codec => (codec.FormatID == format.Guid)).ToList());
		}

		/// <summary>
		/// Determines whether the provided image contains any pixels that have an alpha channel value of less than 255.
		/// </summary>
		/// <param name="image">the image to process.</param>
		/// <returns>Returns [True] if at least one pixel has an alpha channel value of less than 255. [False] otherwise.</returns>
		public static bool IsImageTransparent (Image image)
		{
			var result = false;

			if (image == null) { throw (new ArgumentNullException("image")); }
			if (!(image is Bitmap)) { throw (new ArgumentException("The argument [image] is not of the expected type [Bitmap].", "image")); }
			if (image.PixelFormat != PixelFormat.Format32bppArgb) { throw (new ArgumentException("The argument [image] does not have the required pixel format of [PixelFormat.Format32bppArgb].", "image")); }

			var bitmap = image as Bitmap;
			var data = bitmap.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

			try
			{
				var buffer = new byte [Math.Abs(data.Stride) * data.Height];

				Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

				for (int i = 0; i < (Math.Abs(data.Stride) * data.Height); i += 4)
				{
					// BGRA.
					if (buffer [i + 3] < 255) { result = true; break; }
				}
			}
			finally
			{
				bitmap.UnlockBits(data);
			}

			return (result);
		}

		/// <summary>
		/// Determines whether the provided image contains any pixels that have an alpha channel value of less than 255.
		/// </summary>
		/// <param name="image">the image to process.</param>
		/// <returns>Returns [True] if at least one pixel has an alpha channel value of less than 255. [False] otherwise.</returns>
		public static unsafe bool IsImageTransparentUnmanaged (Image image)
		{
			var result = false;

			if (image == null) { throw (new ArgumentNullException("image")); }
			if (!(image is Bitmap)) { throw (new ArgumentException("The argument [image] is not of the expected type [Bitmap].", "image")); }
			if (image.PixelFormat != PixelFormat.Format32bppArgb) { throw (new ArgumentException("The argument [image] does not have the required pixel format of [PixelFormat.Format32bppArgb].", "image")); }

			var bitmap = image as Bitmap;
			var data = bitmap.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

			try
			{
				byte* p = (byte*) data.Scan0.ToPointer();

				{
					for (int i = 0; i < (Math.Abs(data.Stride) * data.Height); i += 4)
					{
						// BGRA.
						if (p [i + 3] < 255) { result = true; break; }
					}
				}
			}
			finally
			{
				bitmap.UnlockBits(data);
			}

			return (result);
		}

		public static void SetImageOpacity (Bitmap bitmap, byte opacity)
		{
			if (bitmap == null) { throw (new ArgumentNullException("bitmap")); }
			if (bitmap.PixelFormat != PixelFormat.Format32bppArgb) { throw (new ArgumentException("The argument [bitmap] does not have a [PixelFormat] of [Format32bppArgb].", "bitmap")); }

			var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

			try
			{
				var buffer = new byte [Math.Abs(data.Stride) * data.Height];

				Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

				for (int i = 0; i < buffer.Length; i += 4)
				{
					buffer [i + 3] = opacity;
				}

				Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);
			}
			finally
			{
				bitmap.UnlockBits(data);
			}
		}

		public static void SetImageOpacityUnmanaged (Bitmap bitmap, byte opacity)
		{
			if (bitmap == null) { throw (new ArgumentNullException("bitmap")); }
			if (bitmap.PixelFormat != PixelFormat.Format32bppArgb) { throw (new ArgumentException("The argument [bitmap] does not have a [PixelFormat] of [Format32bppArgb].", "bitmap")); }

			ImagingUtilities.SetImageOpacityUnmanagedInternal(bitmap, opacity);
		}

		private static unsafe void SetImageOpacityUnmanagedInternal (Bitmap bitmap, byte opacity)
		{
			if (bitmap == null) { throw (new ArgumentNullException("bitmap")); }
			if (bitmap.PixelFormat != PixelFormat.Format32bppArgb) { throw (new ArgumentException("The argument [bitmap] does not have a [PixelFormat] of [Format32bppArgb].", "bitmap")); }

			var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

			try
			{
				byte* p = (byte*) data.Scan0.ToPointer();

				for (int i = 0; i < (Math.Abs(data.Stride) * data.Height); i += 4)
				{
					p [i + 3] = opacity;
				}
			}
			finally
			{
				bitmap.UnlockBits(data);
			}
		}

		public static bool GetDifferencialRegions (Bitmap imageOld, Bitmap imageNew, out ImageDifferentialResult imageDifferentialResult, out Exception exception)
		{
			bool result = false;
			BitmapData bitmapDataOld = null;
			BitmapData bitmapDataNew = null;

			exception = null;
			imageDifferentialResult = null;

			try
			{
				if (imageOld.Width != imageNew.Width) { throw (new Exception("The width and height of both images must be identical.")); }
				if (imageOld.Height != imageNew.Height) { throw (new Exception("The width and height of both images must be identical.")); }
				if (imageOld.PixelFormat != imageNew.PixelFormat) { throw (new Exception("The pixel format of both images must be identical.")); }

				bitmapDataOld = imageOld.LockBits(new Rectangle(0, 0, imageOld.Width, imageOld.Height), ImageLockMode.ReadOnly, imageOld.PixelFormat);
				bitmapDataNew = imageNew.LockBits(new Rectangle(0, 0, imageNew.Width, imageNew.Height), ImageLockMode.ReadOnly, imageNew.PixelFormat);

				result = ImagingUtilities.GetDifferencialRegions(bitmapDataOld, bitmapDataNew, out imageDifferentialResult, out exception);
			}
			catch (Exception e)
			{
				exception = e;
			}
			finally
			{
				if (bitmapDataOld != null) { try { imageOld.UnlockBits(bitmapDataOld); } catch { } }
				if (bitmapDataNew != null) { try { imageNew.UnlockBits(bitmapDataNew); } catch { } }
			}

			return (result);
		}

		/// <summary>
		/// Gets a list of rectangular regions that differ in pixel color.
		/// The function assumes an image of size 1024x768 and breaks it up into a 16x16 grid or rectangles.
		/// </summary>
		/// <param name="bitmapDataOld">The image to use as a base line.</param>
		/// <param name="bitmapDataNew">The new image to compare against the base line.</param>
		/// <param name="rectangles">The list of rectanlges to return.</param>
		/// <param name="exception">The exceptions to report (if any).</param>
		/// <returns>Returns if the operation succeeded. False in case of errors.</returns>
		public static unsafe bool GetDifferencialRegions (BitmapData bitmapDataOld, BitmapData bitmapDataNew, out ImageDifferentialResult imageDifferentialResult, out Exception exception)
		{
			var result = false;
			var rectangle = Rectangle.Empty;
			var all = new List<Rectangle>();
			var changed = new List<Rectangle>();
			var unchanged = new List<Rectangle>();

			exception = null;
			imageDifferentialResult = null;

			try
			{
				if (bitmapDataOld.Width != bitmapDataNew.Width) { throw (new Exception("The width and height of both images must be identical.")); }
				if (bitmapDataOld.Height != bitmapDataNew.Height) { throw (new Exception("The width and height of both images must be identical.")); }
				if (bitmapDataOld.PixelFormat != bitmapDataNew.PixelFormat) { throw (new Exception("The pixel format of both images must be identical.")); }

				var found = false;
				var blockCountH = 16;
				var blockCountV = 16;
				var sizeH = bitmapDataOld.Width / blockCountH;
				var sizeV = bitmapDataOld.Height / blockCountV;
				var grid = new Rectangle [blockCountH, blockCountV];
				var bpp = bitmapDataOld.Stride / bitmapDataOld.Width;

				byte* bytesOld = (byte*) bitmapDataOld.Scan0.ToPointer();
				byte* bytesNew = (byte*) bitmapDataNew.Scan0.ToPointer();

				for (int gy = 0; gy < blockCountV; gy++)
				{
					for (int gx = 0; gx < blockCountH; gx++)
					{
						found = false;
						rectangle = new Rectangle(gx * sizeH, gy * sizeV, sizeH, sizeV);

						all.Add(rectangle);
						unchanged.Add(rectangle);

						for (int y = rectangle.Y; y < rectangle.Bottom; y++)
						{
							for (int x = rectangle.X; x < rectangle.Right; x++)
							{
								var index = (y * bitmapDataOld.Stride) + (x * bpp);

								if
								(
									(bytesOld [index + 0] != bytesNew [index + 0])
									|| (bytesOld [index + 1] != bytesNew [index + 1])
									|| (bytesOld [index + 2] != bytesNew [index + 2])
								)
								{
									changed.Add(rectangle);
									unchanged.Remove(rectangle);

									found = true;

									break;
								}
							}

							if (found) { break; }
						}
					}
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}
			finally
			{
			}

			return (result);
		}

		public static bool CompareImages (Image image1, Image image2, out Exception exception, byte threshold = 10)
		{
			bool result = false;
			byte [] buffer1 = null;
			byte [] buffer2 = null;
			Bitmap bitmap1 = null;
			Bitmap bitmap2 = null;
			BitmapData data1 = null;
			BitmapData data2 = null;
			Graphics graphics1 = null;
			Graphics graphics2 = null;

			exception = null;

			if (image1 == null)
			{
				exception = new ArgumentNullException("image1", "The argument [image1] cannot be null.");
			}
			else
			{
				if (image2 == null)
				{
					exception = new ArgumentNullException("image2", "The argument [image2] cannot be null.");
				}
				else
				{
					try
					{
						if ((image1.Width == image2.Width) && (image1.Height == image2.Height))
						{
							bitmap1 = new Bitmap(image1);
							graphics1 = Graphics.FromImage(bitmap1);
							bitmap2 = new Bitmap(image2);
							graphics2 = Graphics.FromImage(bitmap2);
						}
						else
						{
							bitmap1 = new System.Drawing.Bitmap(200, 200);
							graphics1 = Graphics.FromImage(bitmap1);
							graphics1.DrawImage(image1, new Rectangle(0, 0, bitmap1.Width, bitmap1.Height));
							bitmap2 = new Bitmap(200, 200);
							graphics2 = Graphics.FromImage(bitmap2);
							graphics2.DrawImage(image2, new Rectangle(0, 0, bitmap2.Width, bitmap2.Height));
						}

						try
						{
							data1 = bitmap1.LockBits(new Rectangle(0, 0, bitmap1.Width, bitmap1.Height), ImageLockMode.ReadOnly, bitmap1.PixelFormat);
							data2 = bitmap2.LockBits(new Rectangle(0, 0, bitmap2.Width, bitmap2.Height), ImageLockMode.ReadOnly, bitmap2.PixelFormat);

							buffer1 = new byte [data1.Stride * data1.Height];
							Marshal.Copy(data1.Scan0, buffer1, 0, buffer1.Length);
							buffer2 = new byte [data2.Stride * data2.Height];
							Marshal.Copy(data2.Scan0, buffer2, 0, buffer2.Length);

							bitmap1.UnlockBits(data1);
							bitmap2.UnlockBits(data2);

							graphics1.Dispose();
							bitmap1.Dispose();
							graphics2.Dispose();
							bitmap2.Dispose();

							try
							{
								result = true;
								for (int i = 0; i < buffer1.Length; i++)
								{
									if (Math.Abs(buffer1 [i] - buffer2 [i]) > threshold)
									{
										result = false;

										break;
									}
								}
							}
							catch (Exception e)
							{
								exception = e;
								result = false;
							}
						}
						catch (Exception e)
						{
							exception = e;
							result = false;
						}
						finally
						{
							try { bitmap1.UnlockBits(data1); }
							catch { }
							try { bitmap2.UnlockBits(data2); }
							catch { }
						}
					}
					catch (Exception e)
					{
						exception = e;
						result = false;
					}
				}
			}

			return (result);
		}

		public static bool ConvertToPng (Image source, out Stream target, out Exception exception)
		{
			var result = false;

			target = null;
			exception = null;

			if (source == null) { throw (new ArgumentNullException("source")); }

			try
			{
				var stream = new MemoryStream();
				source.Save(stream, ImageFormat.Png);
				target = stream;

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static bool ConvertToPng (Image source, FileInfo file, out Exception exception)
		{
			var result = false;

			file = null;
			exception = null;

			if (source == null) { throw (new ArgumentNullException("source")); }
			if (file == null) { throw (new ArgumentNullException("target")); }
			if (file.Extension.ToLower() != "bmp") { throw (new ArgumentException("The argument [target] does not have a [bmp] extension.", "target")); }

			try
			{
				using (var stream = File.Open(file.FullName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
				{
					source.Save(stream, ImageFormat.Png);
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		public static bool ConvertToPng (Image source, out Bitmap target, out Exception exception)
		{
			var result = false;

			target = null;
			exception = null;

			if (source == null) { throw (new ArgumentNullException("source")); }

			try
			{
				using (var stream = new MemoryStream())
				{
					source.Save(stream, ImageFormat.Png);

					target = (Bitmap) Image.FromStream(stream);
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}


		public static bool ConvertToPng (Stream source, out Stream target, out Exception exception)
		{
			exception = null;
			target = new MemoryStream();

			var result = ImagingUtilities.ConvertToPng(source, target, out exception);
			if (!result) { if (target != null) { target.Dispose(); target = null; } }

			return (result);
		}

		public static bool ConvertToPng (Stream source, Stream target, out Exception exception)
		{
			var result = false;

			target = null;
			exception = null;

			if (source == null) { throw (new ArgumentNullException("source")); }
			if (target == null) { throw (new ArgumentNullException("target")); }

			try
			{
				using (var image = Image.FromStream(source))
				{
					image.Save(target, ImageFormat.Png);
				}

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		//public static bool ConvertToPng (Stream source, FileInfo target, out Exception exception)
		//{
		//	var result = false;

		//	target = null;
		//	exception = null;

		//	if (source == null) { throw (new ArgumentNullException("source")); }

		//	try
		//	{
		//		using (var stream = new MemoryStream())
		//		{
		//			source.Save(stream, ImageFormat.Png);

		//			target = (Bitmap) Image.FromStream(stream);
		//		}

		//		result = true;
		//	}
		//	catch (Exception e)
		//	{
		//		exception = e;
		//	}

		//	return (result);
		//}

		//public static bool ConvertToPng (Stream source, out Bitmap target, out Exception exception)
		//{
		//	var result = false;

		//	target = null;
		//	exception = null;

		//	if (source == null) { throw (new ArgumentNullException("source")); }

		//	try
		//	{
		//		using (var stream = new MemoryStream())
		//		{
		//			source.Save(stream, ImageFormat.Png);

		//			target = (Bitmap) Image.FromStream(stream);
		//		}

		//		result = true;
		//	}
		//	catch (Exception e)
		//	{
		//		exception = e;
		//	}

		//	return (result);
		//}


		//public static bool ConvertToPng (FileInfo source, out Stream target, out Exception exception)
		//{
		//	var result = false;

		//	target = null;
		//	exception = null;

		//	if (source == null) { throw (new ArgumentNullException("source")); }

		//	try
		//	{
		//		using (var stream = new MemoryStream())
		//		{
		//			source.Save(stream, ImageFormat.Png);

		//			target = (Bitmap) Image.FromStream(stream);
		//		}

		//		result = true;
		//	}
		//	catch (Exception e)
		//	{
		//		exception = e;
		//	}

		//	return (result);
		//}

		//public static bool ConvertToPng (FileInfo source, FileInfo target, out Exception exception)
		//{
		//	var result = false;

		//	target = null;
		//	exception = null;

		//	if (source == null) { throw (new ArgumentNullException("source")); }

		//	try
		//	{
		//		using (var stream = new MemoryStream())
		//		{
		//			source.Save(stream, ImageFormat.Png);

		//			target = (Bitmap) Image.FromStream(stream);
		//		}

		//		result = true;
		//	}
		//	catch (Exception e)
		//	{
		//		exception = e;
		//	}

		//	return (result);
		//}

		//public static bool ConvertToPng (FileInfo source, out Bitmap target, out Exception exception)
		//{
		//	var result = false;

		//	target = null;
		//	exception = null;

		//	if (source == null) { throw (new ArgumentNullException("source")); }

		//	try
		//	{
		//		using (var stream = new MemoryStream())
		//		{
		//			source.Save(stream, ImageFormat.Png);

		//			target = (Bitmap) Image.FromStream(stream);
		//		}

		//		result = true;
		//	}
		//	catch (Exception e)
		//	{
		//		exception = e;
		//	}

		//	return (result);
		//}
	}
}