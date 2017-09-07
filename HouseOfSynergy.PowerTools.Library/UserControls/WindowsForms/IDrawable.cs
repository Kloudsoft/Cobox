using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HouseOfSynergy.PowerTools.Library.UserControls.WindowsForms
{
	public interface IDrawable
	{
		RectangleF Bounds { get; }

		void SetBounds (RectangleF bounds);
		void Render (Graphics graphics);
	}
}