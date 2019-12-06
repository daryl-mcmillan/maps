using System;
using System.Linq;

namespace maps {
    public class Drawing : IDrawing {

        void IDrawing.Start( double width, double height ) {
            Console.WriteLine(
                @"<svg width=""{0}"" height=""{1}"" xmlns=""http://www.w3.org/2000/svg"">",
                width,
                height
            );
        }

        void IDrawing.DrawLine( params Point[] points ) {
            Console.Write( "<polyline points=\"" );
            Console.Write(
                String.Join(
                    " ",
                    points.Select( p => p.x + "," + p.y )
                )
            );
            Console.WriteLine( "\" style=\"fill:none;stroke:black;stroke-width:1\" />" );
        }

        void IDrawing.End() {
            Console.WriteLine( "</svg>" );
        }

    }
}