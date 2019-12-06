namespace maps {
    public interface IDrawing {

        void Start( double width, double height );
        void DrawLine( params Point[] points );
        void End();

    }
}