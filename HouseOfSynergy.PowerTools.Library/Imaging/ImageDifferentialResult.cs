using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Imaging
{
	public sealed class ImageDifferentialResult
	{
		private List<Rectangle> _RegionsAll { get; set; }
		private List<Rectangle> _RegionsChanged { get; set; }
		private List<Rectangle> _RegionsUnchanged { get; set; }

		public ReadOnlyCollection<Rectangle> RegionsAll { get; private set; }
		public ReadOnlyCollection<Rectangle> RegionsChanged { get; private set; }
		public ReadOnlyCollection<Rectangle> RegionsUnchanged { get; private set; }

		public ImageDifferentialResult (IEnumerable<Rectangle> regionsAll, IEnumerable<Rectangle> regionsChanged, IEnumerable<Rectangle> regionsUnchanged)
		{
			this._RegionsAll = new List<Rectangle>();
			this._RegionsChanged = new List<Rectangle>();
			this._RegionsUnchanged = new List<Rectangle>();

			this.RegionsAll = new ReadOnlyCollection<Rectangle>(this._RegionsAll);
			this.RegionsChanged = new ReadOnlyCollection<Rectangle>(this._RegionsChanged);
			this.RegionsUnchanged = new ReadOnlyCollection<Rectangle>(this._RegionsUnchanged);
		}
	}
}