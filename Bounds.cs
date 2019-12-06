namespace maps {
    public class Bounds {
        public readonly double minx;
        public readonly double miny;
        public readonly double maxx;
        public readonly double maxy;
        public readonly double width;
        public readonly double height;

        public Bounds( double left, double top, double right, double bottom ) {
            this.minx = left;
            this.miny = top;
            this.maxx = right;
            this.maxy = bottom;
            this.width = maxx-minx;
            this.height = maxy-miny;
        }

    }
}