using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.AffinityDms.Entities;
using HouseOfSynergy.AffinityDms.Entities.Tenants;

namespace HouseOfSynergy.AffinityDms.OcrLibrary
{
	public class ComputeCoordinates
	{
		public int ElementId { get; set; }
		public double ComputedX { get; set; }
		public double ComputedY { get; set; }
		public double ComputedWidth { get; set; }
		public double ComputedHeight { get; set; }
		public string Text { get; set; }

		public void ComputeEleemnts (TemplateElement element, double _computeddifference)
		{
			this.ElementId = (int) element.Id;
			var x = (double) element.DivX; //element.DivX += (element.DivX * _computeddifference);
			var y = (double) element.DivY;
			var Width = double.Parse(element.DivWidth.Replace("px", ""));
			var Height = double.Parse(element.DivHeight.Replace("px", ""));
			this.ComputedX = (x * _computeddifference) + x;
			this.ComputedY = (y * _computeddifference) + y;
			this.ComputedWidth = (Width * _computeddifference) + Width;
			this.ComputedHeight = (Height * _computeddifference) + Height;
		}
	}
}