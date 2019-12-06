using System.Linq;

namespace maps {
    public class BoundsMapping : IDrawing
    {
        private readonly IDrawing drawing;
        private readonly Bounds source;
        private readonly Bounds dest;
 
        public BoundsMapping( IDrawing drawing, Bounds source, Bounds dest ) {
            this.drawing = drawing;
            this.source = source;
            this.dest = dest;
        }

        private Point MapPoint( Point pt ) {
            return new Point {
                x = (pt.x - source.minx)/source.width * dest.width + dest.minx,
                y = (pt.y - source.miny)/source.height * dest.height + dest.miny
            };
        }

        void IDrawing.DrawLine(params Point[] points)
        {
            var mapped = points.Select( p => MapPoint( p ) ).ToArray();
            drawing.DrawLine( mapped );
        }

        void IDrawing.End()
        {
            drawing.End();
        }

        void IDrawing.Start(double width, double height)
        {
            drawing.Start( width, height );
        }
    }
}