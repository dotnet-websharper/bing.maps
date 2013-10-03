# Using events

The AJAX Map control provides multiple events to enable various
interactive behavior in the map. The WebSharper extension offers a
low level interface to such events with almost no overhead.

The following code simply alerts with the mouse position in screen coordinates
and in map coordinates whenever the user clicks on the map.

Full Code:

```fsharp
[<JavaScript>]
let RespondToClicks() =
    Test(fun map ->
        let displayLatLong (e : MouseEventArgs) =
            let center = (e.Target :?> Map).GetCenter()
            let pinLocation = pin.GetLocation()
            let pinPoint = map.TryLocationToPixel(pinLocation)
            let mousePoint = Point(float(e.GetX()), float(e.GetY()))
            let mouseLocation = map.TryPixelToLocation(mousePoint)
            let message =
                "pushpin (lat/lon): " + string pinLocation.Latitude +
                ", " + string pinLocation.Longitude +
                "\npushpin (screen x/y): " + string pinPoint.X +
                "," + string pinPoint.Y +
                "\nmouse (lat/lon): " + string mouseLocation.Latitude +
                ", " + string mouseLocation.Longitude +
                "\nmouse (screen x/y): " + string mousePoint.X +
                "," + string mousePoint.Y
            JavaScript.Alert message
        Events.AddHandler(map, MouseEvent.Click, displayLatLong)
        |> ignore
)
```

The way you can attach events to the map is by using the `AddHandler` static method
of the `Events` class. This method receives the entity impacted by the event
(either the map, a shape, or an entity collection like `map.Entities`), the name of
the event you want to use and a callback function. The available events can be found
in the `{EventType}Event` class, where `EventType` is one of:

  * `Mouse`, for events related to clicking and mouse movement;
  * `Key`, for events related to keyboard strokes;
  * `EntityCollection`, for events related to an entity inside an entity collection.
  * `Unit`, for events with no extra parameter (like `Maptypechanged` or `Targetviewchanged`).

The callback signature is `{EventType}EventArgs -> unit`, apart from `UnitEvent`s
for which the callback signature is `unit -> unit`. Common properties for the
`MouseEventArgs` and `KeyEventArgs` objects are:

  * `event.eventName` which gives the exact event that was fired (like `MouseEvent.Click`).
  * `event.originalEvent` which return the native browser event.
  * `event.handled` which is a mutable boolean. Set it to false if you don't
    want the default behavior to be fired after running the callback.

This example also shows how to translate between screen coordinates and latitude / longitude,
using the `TryLocationToPixel` and `TryPixelToLocation` methods of a `Map` object.