//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Threading;
//using HouseOfSynergy.PowerTools.Library.Utility;

//namespace HouseOfSynergy.PowerTools.Library.Plotting
//{
//    public class Graph:
//        Disposable
//    {
//        public const int PaddingLeft = 10;
//        public const int PaddingRight = 10;
//        public const int PaddingTop = 20;
//        public const int PaddingBottom = 10;
//        public const int NumberOfValues = 100;

//        private Font Font { get; set; }
//        private object _SyncRoot { get; set; }
//        public RectangleF Bounds { get; private set; }
//        public RectangleF BoundsPadded { get; private set; }
//        private Dictionary<MessageType, Pen> DictionaryPens { get; set; }
//        private Dictionary<MessageType, List<float>> DictionaryValues { get; set; }

//        public Graph (Rectangle bounds)
//        {
//            this.SetBounds(bounds);
//            this._SyncRoot = new object();
//            this.DictionaryPens = new Dictionary<MessageType, Pen>();
//            this.DictionaryValues = new Dictionary<MessageType, List<float>>();
//            this.Font = this.AddDisposableObject(new Font(FontFamily.GenericMonospace, 12));

//            var types = EnumUtilities.GetValues<MessageType>();

//            types.Sort((x, y) => x.ToString().CompareTo(y.ToString()));

//            foreach (var type in types)
//            {
//                this.DictionaryValues.Add(type, new List<float>());

//                for (int i = 0; i < Graph.NumberOfValues; i++)
//                {
//                    this.DictionaryValues [type].Add(0);
//                }

//                do
//                {
//                    var color = Color.FromArgb(255, Global.Random.Next(0, 256), Global.Random.Next(0, 256), Global.Random.Next(0, 256));

//                    // Avoid colors that are too white.
//                    if ((color.R + color.G + color.B) < (Math.Pow(220, 3)))
//                    {
//                        this.DictionaryPens.Add(type, this.AddDisposableObject(new Pen(color)));

//                        break;
//                    }
//                }
//                while (true);
//            }

//            // TODO: Remove.
//            var action = new Action
//            (
//                () =>
//                {
//                    Thread.Sleep(TimeSpan.FromSeconds(2));

//                    do
//                    {
//                        lock (this._SyncRoot)
//                        {
//                            foreach (var type in types)
//                            {
//                                this.AddValue(type, (float) (Global.Random.NextDouble() * 10000));
//                            }
//                        }

//                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
//                    }
//                    while (true);
//                }
//            );
//            //Task.Factory.StartNew(action);
//        }

//        public object SyncRoot { get { return (this._SyncRoot); } }

//        public void AddValue (MessageType type, float value)
//        {
//            lock (this._SyncRoot)
//            {
//                if (this.DictionaryValues [type].Count >= Graph.NumberOfValues)
//                {
//                    this.DictionaryValues [type].RemoveAt(0);
//                }

//                this.DictionaryValues [type].Add(value);
//            }
//        }

//        public void Reset ()
//        {
//            lock (this._SyncRoot)
//            {
//                foreach (var pair in this.DictionaryValues)
//                {
//                    for (int i = 0; i < this.DictionaryValues [pair.Key].Count; i++)
//                    {
//                        this.DictionaryValues [pair.Key] [i] = 0;
//                    }
//                }
//            }
//        }

//        public void SetBounds (Rectangle bounds) { this.SetBounds((RectangleF) bounds); }

//        public void SetBounds (RectangleF bounds)
//        {
//            this.Bounds = bounds;
//            this.BoundsPadded = RectangleF.FromLTRB
//            (
//                bounds.X + Graph.PaddingLeft,
//                bounds.Y + Graph.PaddingTop,
//                bounds.Right - Graph.PaddingRight,
//                bounds.Bottom - Graph.PaddingBottom
//            );
//        }

//        public void Render (Graphics graphics)
//        {
//            var spacer = 4;
//            var top = this.Bounds.Top + spacer;
//            var left = this.Bounds.Left + spacer;
//            var types = this.DictionaryValues.Keys.ToList();
//            var midy = (this.Bounds.Height / 2F) + this.Bounds.Y;

//            var max = Math.Max(1, this.DictionaryValues.Max(pair => pair.Value.Max()));

//            for (int i = 0; i < types.Count; i++)
//            {
//                var type = types [i];
//                var text = type.ToString();
//                var x = this.BoundsPadded.X;
//                var y = this.BoundsPadded.Bottom;
//                var px = this.BoundsPadded.X;
//                var py = this.BoundsPadded.Bottom;
//                var size = graphics.MeasureString(text, this.Font);

//                graphics.DrawString(text, this.Font, this.DictionaryPens [type].Brush, left, top);
//                top += (size.Height + spacer);

//                px = this.BoundsPadded.X;
//                py = this.BoundsPadded.Bottom;

//                for (int j = 0; j < this.DictionaryValues [type].Count; j++)
//                {
//                    x = this.BoundsPadded.X + ((((float) j) / this.DictionaryValues [type].Count) * this.BoundsPadded.Width);
//                    y = this.BoundsPadded.Bottom - ((this.DictionaryValues [type] [j] / max) * this.BoundsPadded.Height);

//                    graphics.DrawLine(this.DictionaryPens [type], px, py, x, y);

//                    px = x;
//                    py = y;
//                }

//                graphics.DrawLine(this.DictionaryPens [type], px, py, this.BoundsPadded.Right, this.BoundsPadded.Bottom);
//            }

//            graphics.DrawRectangle(Pens.Black, this.Bounds);
//        }

//        private bool Disposed { get; set; }

//        protected override void Dispose (bool disposing)
//        {
//            if (!this.Disposed)
//            {
//                if (disposing)
//                {
//                    // Managed.
//                }

//                // Unmanaged.

//                this.Disposed = true;
//            }

//            base.Dispose(disposing);
//        }
//    }
//}