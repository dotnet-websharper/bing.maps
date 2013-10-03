# Shapes

The AJAX Map Control supports some drawing primitives like pushpins, lines and
polygons. The following example demonstrates how to add shapes to the map.

```fsharp
[<JavaScript>]
let Shapes() =     
    Test (fun map ->
        let location = Location(47.6,-122.33)
        let view = ViewOptions(Location = location,
                               Zoom = 9,
                               MapTypeId = MapTypeId.Aerial)

        // Pushpin
        let pushpin = Pushpin(location)
        map.Entities.Push pushpin

        let fromPoints (x,y) = Location(x + location.Latitude,
                                        y + location.Longitude)

        // Line
        let linePoints = 
            [|(0.2,0.2); (0.2,-0.2); (-0.2,-0.2); (-0.2,0.2); (0.2,0.2)|]
            |> Array.map fromPoints
        let line = PolyLine(linePoints)
        map.Entities.Push(line)

        // Polygon
        let polyPoints = 
            [|(0.1,0.1); (0.1,-0.1); (-0.1,-0.1); (-0.1,0.1)|]
            |> Array.map fromPoints
        let polygon = Polygon(polyPoints)
        map.Entities.Push(polygon)

        let pushpin2 = Pushpin(Bing.Location(47.3, -122.17)
        let pinOptions = PushpinOptions(
                             Draggable = true,
                             Icon = "my_pushpin_icon.png",
                             Width = 50,
                             Height = 50)
        map.Entities.Push(pushpin2, pinOptions)

        let polyOptions = PolygonOptions(StrokeColor = Color(200, 250, 190, 204),
                                         StrokeThickness = 12.)
        polygon.SetOptions(polyOptions)
    )
```

First, we use the `Test` function from the previous example. We create a Location
for selecting the center coordinate.

Having the map loaded in line 8, we start adding the different possible shapes to the map.
Adding a shape to the map is done in 3 steps:

 * Generating the `Location` object(s) on which the shape will be placed.
 * Instantiating the shape.
 * Adding the shape to the map.

In the case of the pushpin (Lines 10-12), we use the `Pushpin` to create it. We simply
pass it the location at which it will be placed.

The next 2 cases (polyline and polygon) work similarly, but take an array of Locations
instead of a single Location. The order in which the shape is drawn is based on the
order that is used in the array.

The last pushpin example also shows how to modify the appearence of a shape when creating it.
You just need to create a {Shapename}Options object with the desired options, and pass
it as an extra argument to the shape constructor.

You can also pass a {Shapename}Options object to the SetOptions method of a shape to modify
it after it has been created; its display will be modified automatically.

The end result is the following:

![Shapes](Shapes.png)