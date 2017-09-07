using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace HouseOfSynergy.PowerTools.Library.Extensions
{
	public static partial class Extensions
	{
		public static void SetQualityHighest (this Graphics graphics)
		{
			graphics.CompositingMode = CompositingMode.SourceOver;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		}

		public static void SetQualityLowest (this Graphics graphics)
		{
			graphics.CompositingMode = CompositingMode.SourceOver;
			graphics.CompositingQuality = CompositingQuality.HighSpeed;
			graphics.InterpolationMode = InterpolationMode.Low;
			graphics.SmoothingMode = SmoothingMode.HighSpeed;
			graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
		}

		public static Point ToPoint (this PointF point) { return (new Point((int) point.X, (int) point.Y)); }
		public static PointF ToPointF (this Point point) { return (new PointF(point.X, point.Y)); }
		public static SizeF ToSizeF (this Size size) { return (new SizeF(size.Width, size.Height)); }
		public static int Area (this Rectangle rectangle) { return (rectangle.Width * rectangle.Height); }
		public static float Area (this RectangleF rectangle) { return (rectangle.Width * rectangle.Height); }

		public static Rectangle Clone (this Rectangle rectangle) { return (new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)); }
		public static RectangleF Clone (this RectangleF rectangle) { return (new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)); }
		public static Rectangle ToRectangle (this RectangleF self) { return (new Rectangle((int) self.X, (int) self.Y, (int) self.Width, (int) self.Height)); }
		public static RectangleF ToRectangleF (this Rectangle self) { return (new RectangleF(self.X, self.Y, self.Width, self.Height)); }

		public static PointF GetCenterPointF (this Rectangle rectangle) { return (new PointF(rectangle.X + (rectangle.Width / 2F), rectangle.Y + (rectangle.Height / 2F))); }
		public static Point GetCenterPoint (this Rectangle rectangle) { return (new Point((int) (rectangle.X + (rectangle.Width / 2F)), (int) (rectangle.Y + (rectangle.Height / 2F)))); }
		public static PointF GetCenterPointF (this RectangleF rectangle) { return (new PointF(rectangle.X + (rectangle.Width / 2F), rectangle.Y + (rectangle.Height / 2F))); }

		public static XmlDocument ToXml (this GraphicsPath path)
		{
			XmlElement element = null;
			XmlDocument document = null;

			document = new XmlDocument();

			document.LoadXml("<GraphicsPath></GraphicsPath>");

			document.DocumentElement.AppendChild(document.CreateElement("Points"));

			for (int i = 0; i < path.PathData.Points.Length; i++)
			{
				element = document.CreateElement("Point");

				element.Attributes.Append(document, "Id", (i + 1).ToString());
				element.Attributes.Append(document, "Type", path.PathData.Types [i].ToString());
				element.Attributes.Append(document, "X", path.PathData.Points [i].X.ToString());
				element.Attributes.Append(document, "Y", path.PathData.Points [i].Y.ToString());

				document.DocumentElement.ChildNodes [0].AppendChild(element);
			}

			return (document);
		}

		public static GraphicsPath FromXml (this GraphicsPath path, string xml)
		{
			global::System.Xml.XmlDocument document = null;

			try
			{
				document = new XmlDocument();

				if (xml.Trim().Length > 0)
				{
					document.LoadXml(xml);

					path.FromXml(document);
				}
			}
			catch
			{
			}

			return (path);
		}

		public static void FromXml (this GraphicsPath path, XmlDocument document)
		{
			byte [] types = null;
			PointF [] points = null;
			XmlElement element = null;

			element = (XmlElement) document.DocumentElement.ChildNodes [0];

			points = new PointF [element.ChildNodes.Count];
			types = new byte [element.ChildNodes.Count];

			for (int i = 0; i < element.ChildNodes.Count; i++)
			{
				points [i] = new PointF
				(
					float.Parse(element.ChildNodes [i].Attributes ["X"].Value),
					float.Parse(element.ChildNodes [i].Attributes ["Y"].Value)
				);

				types [i] = byte.Parse(element.ChildNodes [i].Attributes ["Type"].Value);
			}

			path.Reset();

			if (points.Length > 1)
			{
				for (int i = 0; i < (points.Length - 1); i++)
				{
					path.AddLine(points [i], points [i + 1]);
					//path.AddLines(points);
				}
			}
		}

		public static XmlElement ToXmlElement (this GraphicsPath path, XmlDocument document)
		{
			XmlElement element = null;

			element = document.CreateElement("GraphicsPath");

			element.AppendChild(path.PathData.Points.ToXmlElement(document));
			element.AppendChild(path.PathData.Types.ToXmlElement(document));

			return (element);
		}

		public static GraphicsPath FromXmlElement (this GraphicsPath path, XmlElement element)
		{
			byte [] bytes = null;
			PointF [] points = null;
			GraphicsPath temp = null;

			points = path.PathData.Points.FromXmlElement((XmlElement) element.ChildNodes [0]);
			bytes = path.PathData.Types.FromXmlElement((XmlElement) element.ChildNodes [1]);

			temp = new GraphicsPath(points, bytes);

			return (temp);
		}

		public static XmlElement ToXmlElement (this PointF [] points, XmlDocument document)
		{
			XmlElement element = null;

			element = document.CreateElement("Points");

			points.ToList().ForEach
			(
				point =>
				{
					XmlElement pe = null;

					pe = document.CreateElement("Point");
					pe.Attributes.Append(document, "X", point.X.ToString());
					pe.Attributes.Append(document, "Y", point.Y.ToString());

					element.AppendChild(pe);
				}
			);

			return (element);
		}

		public static PointF [] FromXmlElement (this PointF [] points, XmlElement element)
		{
			PointF [] temp = null;

			temp = new PointF [element.ChildNodes.Count];

			for (int i = 0; i < element.ChildNodes.Count; i++)
			{
				temp [i] = new PointF
				(
					float.Parse(element.ChildNodes [i].Attributes ["X"].Value),
					float.Parse(element.ChildNodes [i].Attributes ["Y"].Value)
				);
			}

			return (temp);
		}

		public static XElement ToXElement (this Rectangle rectangle)
		{
			XElement element = null;

			element = new XElement
			(
				"RectangleF",
				new XObject []
				{
					new XAttribute("X", rectangle.X),
					new XAttribute("Y", rectangle.Y),
					new XAttribute("W", rectangle.Width),
					new XAttribute("H", rectangle.Height),
				}
			);

			return (element);
		}

		public static Rectangle FromXElement (this Rectangle rectangle, XElement element)
		{
			rectangle.X = int.Parse(element.Attribute("X").Value);
			rectangle.Y = int.Parse(element.Attribute("Y").Value);
			rectangle.Width = int.Parse(element.Attribute("W").Value);
			rectangle.Height = int.Parse(element.Attribute("H").Value);

			return (rectangle);
		}

		public static XElement ToXElement (this RectangleF rectangle)
		{
			XElement element = null;

			element = new XElement
			(
				"Rectangle",
				new XObject []
				{
					new XAttribute("X", rectangle.X),
					new XAttribute("Y", rectangle.Y),
					new XAttribute("W", rectangle.Width),
					new XAttribute("H", rectangle.Height),
				}
			);

			return (element);
		}

		public static RectangleF FromXElement (this RectangleF rectangle, XElement element)
		{
			rectangle.X = int.Parse(element.Attribute("X").Value);
			rectangle.Y = int.Parse(element.Attribute("Y").Value);
			rectangle.Width = int.Parse(element.Attribute("W").Value);
			rectangle.Height = int.Parse(element.Attribute("H").Value);

			return (rectangle);
		}

		public static XElement ToXElement (this Size size)
		{
			XElement element = null;

			element = new XElement
			(
				"Size",
				new XObject []
				{
					new XAttribute("W", size.Width),
					new XAttribute("H", size.Height),
				}
			);

			return (element);
		}

		public static Size FromXElement (this Size size, XElement element)
		{
			size.Width = int.Parse(element.Attribute("X").Value);
			size.Height = int.Parse(element.Attribute("Y").Value);

			return (size);
		}

		public static XElement ToXElement (this SizeF size)
		{
			XElement element = null;

			element = new XElement
			(
				"SizeF",
				new XObject []
				{
					new XAttribute("W", size.Width),
					new XAttribute("H", size.Height),
				}
			);

			return (element);
		}

		public static SizeF FromXElement (this SizeF size, XElement element)
		{
			size.Width = int.Parse(element.Attribute("X").Value);
			size.Height = int.Parse(element.Attribute("Y").Value);

			return (size);
		}
	}
}