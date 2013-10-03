# The REST API

Bing Maps provides a REST API which can be used for a number of requests:

  * Search for a location by name, address or coordinates;
  * Ask for directions between two locations, or into a location;
  * Request a static map, ie. a simple image (as opposed to interactive
    AJAX maps)
  * Ask for metadata about the maps, such as information about the tiles.

This WebSharper™ Extension provides functions to build a request for this
REST API, and classes to process the results. These functions can be found
in the `IntelliFactory.WebSharper.Bing.Rest` module.

The following code calls the REST API to search for a given location and
displays it on the map. It skips most error checking for the sake of clarity.

```fsharp
[<JavaScript>]
let SearchAndDisplay(query) =
    Test(fun map ->
        let callback (response : RestResponse) =
            let resources = response.ResourceSets.[0].Resources
            if resources.Length <> 0 then
                let resource = resources.[0] :?> LocationResource
                let location = Location(resource.Point.Coordinates.[0],
                                        resource.Point.Coordinates.[1])
                let pin = Pushpin(location)
                map.Entities.Push(pin)
                let view = ViewOptions(Center = location)
                map.SetView(view)
        Rest.RequestLocationByQuery(credentials, query, callback)
    )
```

Most functions in the `Rest` module are called in the same way. They take
three arguments:

  * A string containing your Bing Maps credentials;
  * An object describing your request;
  * A callback function of type `RestResponse -> unit` which will be called
    with the data returned by Bing.

The RestResponse object contains some metadata about the information it returns.
The actual data is in the `ResourceSets` array field. This will generally contain
a unique element, which itself contains an array of `Resources` and an
`EstimatedTotal` of their number. If the search was unsuccessful then this array
is empty; else it contains the result data.

You can find a reference about the [RestResponse][rest-response-ref] object,
as well as each type of resource that can be retrieved: [locations][loc-ref],
[routes][route-ref] and [image metadata][metadata-ref].

The following more complex example requests a route between two locations
and returns the driving instructions in an HTML table.

```fsharp
[<JavaScript>]
let GetRouteInfo(origin, destination) =
    let callback (result : RestResponse) =
        let route = result.ResourceSets.[0].Resource.[0] :?> RouteResource
        let getItems (instructions : ItineraryItem[]) =
            Array.map (fun inst ->
                    TR [TD [Text inst.Instruction.Text]
                        TD [Text (string inst.TravelDistance + " " +
                                  string route.DistanceUnit)]]
                )
                instructions
        let instructionTableRows =
            route.RouteLegs
            |> Array.map (fun leg -> getItems leg.ItineraryItems)
            |> Array.concat
        Table instructionTableRows
    let waypoints =
        [|
            Waypoint origin
            Waypoint destination
        |]
    let request =
        RouteRequest(Waypoints = waypoints,
                     Avoid = [| Bing.Avoid.Tolls |])
    RequestRoute(credentials, request, callback)
```

Exceptions to the described protocol are the `StaticMapUrl` and `StaticMap`
functions, which simply need your credentials and request, and directly
return an image url and an `Img` element, respectively.

```fsharp
[<JavaScript>]
let GetStaticMap() =
    let pushpins =
        [|
            PushpinRequest(x = 47.1, y = 19.0, Label = "P1")
            PushpinRequest(x = 47.13, y = 19.17, IconStyle = 2)
        |]
    let req = StaticMapRequest(imagerySet = Bing.ImagerySet.Road,
                               CenterPoint = Bing.Point(47.2, 19.1),
                               Pushpin = pushpins)
    Div [ StaticMap(credentials, req) ]
```

[rest-response-ref] http://msdn.microsoft.com/en-us/library/ff701707.aspx
[loc-ref] http://msdn.microsoft.com/en-us/library/ff701725.aspx
[route-ref] http://msdn.microsoft.com/en-us/library/ff701718.aspx
[metadata-ref] http://msdn.microsoft.com/en-us/library/ff701712.aspx
