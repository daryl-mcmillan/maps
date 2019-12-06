namespace maps {
    public class NullDrawing : IDrawing
    {
        void IDrawing.DrawLine(params Point[] points) { }

        void IDrawing.End() { }

        void IDrawing.Start(double width, double height) { }
    }
}