using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace maps
{
    class Program
    {

        static XElement ReadElement( XmlReader reader ) {
            return (XElement)XElement.ReadFrom( reader );
        }

        static void Main(string[] args)
        {

            if( args.Length != 1 ) {
                Console.Error.WriteLine( "usage: maps <osm file>" );
                return;
            }

            Dictionary<long,Point> nodes = new Dictionary<long, Point>();
            IDrawing drawing = new NullDrawing();

            using( XmlReader reader = XmlReader.Create( args[0] ) ) {
                while( reader.Read() ) {
                    if( reader.NodeType != XmlNodeType.Element ) {
                        continue;
                    }
                    switch( reader.Name ) {
                        case "osm":
                            break;
                        case "bounds": {
                            var bounds = ReadElement( reader );
                            var minlat = bounds.GetAttributeDouble( "minlat" );
                            var minlon = bounds.GetAttributeDouble( "minlon" );
                            var maxlat = bounds.GetAttributeDouble( "maxlat" );
                            var maxlon = bounds.GetAttributeDouble( "maxlon" );
                            drawing = new BoundsMapping(
                                drawing: new Drawing(),
                                source: new Bounds(
                                    left: minlon,
                                    top: maxlat,
                                    right: maxlon,
                                    bottom: minlat
                                ),
                                dest: new Bounds(
                                    left: 0,
                                    top: 0,
                                    right: 800,
                                    bottom: 800
                                )
                            );
                            drawing.Start( 800, 800 );
                            break;
                        }
                        case "node": {
                            var node = ReadElement( reader );
                            nodes.Add(
                                key: node.GetAttributeLong( "id" ),
                                value: new Point {
                                    y = node.GetAttributeDouble( "lat" ),
                                    x = node.GetAttributeDouble( "lon" )
                                }
                            );
                            break;
                        }
                        case "way": {
                            var way = ReadElement( reader );
                            var path = way
                                .Elements( "nd" )
                                .Attributes( "ref" )
                                .Select( attr => nodes[ long.Parse( attr.Value ) ] );
                            var tags = way
                                .Elements( "tag" )
                                .ToDictionary(
                                    keySelector: e => e.GetAttribute( "k" ),
                                    elementSelector: e => e.GetAttribute( "v" )
                                );
                            if( tags.TryGetValue( "highway", out string val ) ) {
                                switch( val ) {
                                    case "path":
                                    case "footway":
                                    case "pedestrian":
                                    case "cycleway":
                                    case "track":
                                    case "platform":
                                    case "steps":
                                    case "construction":
                                    case "proposed":
                                    case "service":
                                    case "unclassified":
                                    case "raceway":
                                    case "corridor":
                                    case "bus_stop":
                                    case "living_street":
                                        break;
                                    case "road":
                                    case "residential":
                                    case "primary":
                                    case "primary_link":
                                    case "secondary":
                                    case "secondary_link":
                                    case "tertiary":
                                    case "tertiary_link":
                                    case "motorway":
                                    case "motorway_link":
                                        drawing.DrawLine( path.ToArray() );
                                        break;
                                    default:
                                        Console.Error.WriteLine( val );
                                        break;
                                }
                            }
                            break;
                        }
                        case "relation": {
                            var relation = ReadElement( reader );
                            break;
                        }
                        default:
                            Console.Error.WriteLine( "unknown element: {0}", reader.Name );
                            break;
                    }
                }
            }
            drawing.End();
        }
    }
}
