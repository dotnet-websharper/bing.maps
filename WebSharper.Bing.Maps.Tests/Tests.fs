namespace WebSharper.Bing.Tests

open WebSharper

module Main =
    open WebSharper.JavaScript
    open WebSharper.Html.Client
    open WebSharper.Bing.Maps

    [<JavaScript>]
    let credentials = "Ai6uQaKEyZbUvd33y5HU41hvoov_piUMn6t78Qzg7L1DWY4MFZqhjZdgEmCpQlbe"

    [<JavaScript>]
    let IsUndefined x =
        JS.TypeOf x = JS.Kind.Undefined

    (*
      var map = new Microsoft.Maps.Map(document.getElementById("mapDiv"), 
                               {credentials: "Your Bing Maps Key",
                                center: new Microsoft.Maps.Location(45.5, -122.5),
                                mapTypeId: Microsoft.Maps.MapTypeId.road,
                                zoom: 7});
    *)

    open WebSharper.JavaScript

    [<JavaScript>]
    let MapElement () =
        Div []
        |>! OnAfterRender (fun el ->
            let options =
                MapViewOptions(
                    Credentials = credentials,
                    Width = 400,
                    Height = 400,
                    MapTypeId = MapTypeId.Birdseye
                )
            let map = Map(el.Body, options)
            map.SetMapType(MapTypeId.Birdseye)
        )

    [<JavaScript>]
    let MapWithTileLayer () =
        Div []
        |>! OnAfterRender (fun el ->
            let options = MapViewOptions(Credentials = credentials,
                                         Width = 400,
                                         Height = 400,
                                         MapTypeId = MapTypeId.Aerial)
            let map = Map(el.Body, options)
            let tileSource = TileSource(TileSourceOptions(UriConstructor="http://www.microsoft.com/maps/isdk/ajax/layers/lidar/{quadkey}.png"))
            let tileLayer = TileLayer(TileLayerOptions(Mercator=tileSource, Opacity=0.7))
            map.Entities.Push(tileLayer)
        )

    [<JavaScript>]
    let CheckJsonResponse (response : RestResponse) =
        if IsUndefined response ||
            IsUndefined response.ResourceSets ||
            response.ResourceSets.Length = 0 ||
            IsUndefined response.ResourceSets.[0].Resources ||
            response.ResourceSets.[0].Resources.Length = 0
        then
            Some(string response.StatusCode + ": " + String.concat " " response.ErrorDetails)
        else
            None

    [<JavaScript>]
    let GeocodeCallback (map : Map) (resultElt : Element) (result : RestResponse) =
        match CheckJsonResponse result with
        | Some error ->
            resultElt.Text <- "Bad location request: " + error
        | None ->
            let resource = result.ResourceSets.[0].Resources.[0] :?> LocationResource
            if IsUndefined resource then
                resultElt.Text <- "Location not found or no site around"
            else
                let loc = Location(resource.Point.Coordinates.[0], resource.Point.Coordinates.[1])
                let pin = Pushpin(loc)
                map.Entities.Push(pin)
                map.SetView(ViewOptions(Center=loc))
                resultElt.Text <- resource.Name

    [<JavaScript>]
    let LocationRequest () =
        let input = Input [Attr.Type "text"]
        let button = Input [Attr.Type "button"; Attr.Value "Search"]
        let responseDiv = Div []
        let mapContainer =
            Div []
            |>! OnAfterRender (fun el ->
                let opts = MapOptions(Credentials = credentials,
                                      Width = 600,
                                      Height = 500)
                let map = Map(el.Body, opts)
                map.SetMapType(MapTypeId.Road)
                let request (_:Element) (_:Events.MouseEvent) =
                    Rest.RequestLocationByQuery(credentials, input.Value, GeocodeCallback map responseDiv)
                button |>! OnClick request |> ignore
            )
        Div [
            mapContainer
            Div [input; button]
            responseDiv
        ]

    [<JavaScript>]
    let LatLonLocationRequest () =
        let inputLat = Input [Attr.Type "text"]
        let inputLon = Input [Attr.Type "text"]
        let button = Input [Attr.Type "button"; Attr.Value "Search"]
        let responseDiv = Div []
        let mapContainer =
            Div []
            |>! OnAfterRender (fun el ->
                let opts = MapOptions(Credentials = credentials,
                                      Width = 600,
                                      Height = 500)
                let map = Map(el.Body, opts)
                map.SetMapType(MapTypeId.Road)
                let request (_:Element) (_:Events.MouseEvent) =
                    Rest.RequestLocationByPoint(credentials, float inputLat.Value, float inputLon.Value, [], GeocodeCallback map responseDiv)
                button |>! OnClick request |> ignore
            )
        Div [
            mapContainer
            Div [Span[Text "Latitude:"]; inputLat; Span[Text "Longitude"]; inputLon; button]
            responseDiv
        ]

    [<JavaScript>]
    let MouseEvent () =
        let container = Div []
        let opt = MapViewOptions(Credentials = credentials,
                                 Width = 600,
                                 Height = 500)
        let map = Map(container.Body, opt)
        let pin = Pushpin(map.GetCenter(), PushpinOptions(Draggable=true))
        map.Entities.Push(pin)
        let displayLatLong (e:MouseEventArgs) =
            let center = (e.Target :?> Map).GetCenter()
            let pinLocation = pin.GetLocation()
            let pinPoint = map.TryLocationToPixel(pinLocation)
            let mousePoint = Point(float(e.GetX()), float(e.GetY()))
            let mouseLocation = map.TryPixelToLocation(mousePoint)
            let message = "pushpin (lat/lon): " + string pinLocation.Latitude + ", " + string pinLocation.Longitude +
                          "\npushpin (screen x/y): " + string pinPoint.X + "," + string pinPoint.Y +
                          "\nmouse (lat/lon): " + string mouseLocation.Latitude + ", " + string mouseLocation.Longitude +
                          "\nmouse (screen x/y): " + string mousePoint.X + "," + string mousePoint.Y
            JS.Alert message
        Events.AddHandler(map, MouseEvent.Click, displayLatLong) |> ignore
        container

    [<JavaScript>]
    let OldRouteRequest () =
        let origin = Input []
        let destination = Input []
        let button = Input [Attr.Type "button"; Attr.Value "Request route"]
        let highwayBox = Input [Attr.Type "checkbox"]
        let answer = Div []
        let RouteCallback (map : Map) (result : RestResponse) =
            match CheckJsonResponse result with
            | Some error ->
                answer.Text <- "Bad route: " + error
            | None ->
                let route = result.ResourceSets.[0].Resources.[0] :?> RouteResource
                // Draw the route on the map
                let corners = [|Location(route.Bbox.[0], route.Bbox.[1])
                                Location(route.Bbox.[2], route.Bbox.[3])|]
                let viewBoundaries = LocationRect.FromLocations(corners)
                map.SetView(ViewOptions(Bounds=viewBoundaries))
                let routeline = route.RoutePath.Line.Coordinates
                let routepoints = Array.init routeline.Length (fun i ->
                    Location(routeline.[i].[0], routeline.[i].[1]))
                let routeshape = Polyline(routepoints, PolylineOptions(StrokeColor=Color(200, 0, 0, 200)))
                map.Entities.Push(routeshape)
                // Write directions under the map
                let getItems (instructions : ItineraryItem[]) =
                    instructions
                    |> Array.map (fun inst ->
                        TR [TD [Text inst.Instruction.Text]
                            TD [Text (string inst.TravelDistance + " " + string route.DistanceUnit)]])
                let messages =
                    route.RouteLegs
                    |> Array.map (fun leg ->
                        getItems leg.ItineraryItems)
                    |> Array.concat
                    |> Table
                answer.Clear()
                answer.Append (messages)
        let mapContainer =
            Div []
            |>! OnAfterRender (fun el ->
            let opts = MapOptions(Credentials = credentials,
                                        Width = 600,
                                        Height = 500)
            let map = Map(el.Body, opts)
            map.SetMapType(MapTypeId.Road)
            let request (_:Element) (_:Events.MouseEvent) =
                let avoid =
                    if string ((?) highwayBox.Body "checked") = "true" then
                        [||]
                    else
                        [|RouteAvoid.Highways|]
                Rest.RequestRoute(credentials,
                                  RouteRequest(Waypoints=[|Waypoint origin.Value; Waypoint destination.Value|],
                                               Avoid=avoid,
                                               RoutePathOutput=RoutePathOutput.Points),
                                  RouteCallback map)
            button |>! OnClick request |> ignore
        )
        Div [mapContainer
             Span[Text "From:"]; origin
             Span[Text "To:"]; destination
             highwayBox; Span[Text "Accept highways"]; button
             answer]

    open WebSharper.Bing.Maps.Directions

    [<JavaScript>]
    let RouteRequest () =
        let origin = Input []
        let destination = Input []
        let button = Input [Attr.Type "button"; Attr.Value "Request route"]
        let highwayBox = Input [Attr.Type "checkbox"]
        let answer = Div [Id "answer"]
        let mapContainer =
            Div []
            |>! OnAfterRender (fun el ->
                let opts = MapOptions(Credentials = credentials,
                                      Width = 600,
                                      Height = 500)
                let map = Map(el.Body, opts)
                map.SetMapType(MapTypeId.Road)
                let onDirsLoaded() =
                    let dirman = DirectionsManager(map)
                    let request (_:Element) (_:Events.MouseEvent) =
                        dirman.ResetDirections()
                        dirman.AddWaypoint <| Waypoint(WaypointOptions(Address = origin.Value))
                        dirman.AddWaypoint <| Waypoint(WaypointOptions(Address = destination.Value))
                        dirman.SetRenderOptions <| DirectionsRenderOptions(ItineraryContainer = answer.Dom)
                        dirman.SetRequestOptions <| DirectionsRequestOptions(DistanceUnit = DistanceUnit.Kilometers)
                        dirman.CalculateDirections()
                    button |>! OnClick request |> ignore
                Maps.LoadModule("Microsoft.Maps.Directions", LoadModuleArgs(onDirsLoaded))
            )
        Div [mapContainer
             Span[Text "From:"]; origin
             Span[Text "To:"]; destination
             highwayBox; Span[Text "Accept highways"]; button
             answer]

    [<JavaScript>]
    let StaticMap () =
        let req1 = StaticMapRequest(CenterPoint=Point(47.2, 19.1),
                                    imagerySet=ImagerySet.Road,
                                    ZoomLevel=10,
                                    Pushpin=[|PushpinRequest(x=47.1, y=19.0, IconStyle=2, Label="P1")
                                              PushpinRequest(x=47.13, y=19.17, IconStyle=10)|])
        let req2 = StaticMapRequest(Query="Washington DC",
                                    imagerySet=ImagerySet.Aerial)
        Div []
        |>! OnAfterRender (fun div ->
            Rest.StaticMap(credentials, req1) |> div.Append
            Rest.StaticMap(credentials, req2) |> div.Append
        )

    [<JavaScript>]
    let ImageMetadata () =
        let req = ImageryMetadataRequest(imagerySet=ImagerySet.Road,
                                         MapVersion=MapVersion.V1,
                                         CenterPoint=Point(47.2, 19.1))
        let callback (answer : Element) (result : RestResponse) =
            match CheckJsonResponse result with
            | Some error ->
                answer.Text <- "Bad metadata response: " + error
            | None ->
                let resource = result.ResourceSets.[0].Resources.[0] :?> ImageryMetadataResource
                let txt : Pagelet list =
                    [
                        P [Text("Road map tile size: " + string resource.ImageHeight + "x" + string resource.ImageWidth)]
                        P [Text("Road map tile URL: " + resource.ImageUrl)]
                    ]
                List.iter (answer.Append:Pagelet->unit) txt
        Div []
        |>! OnAfterRender (fun el ->
            Rest.RequestImageryMetadata(credentials, req, callback el)
        )



    [<JavaScript>]
    let Samples () =
        Div [
            H2 [Text "Basic map"]
            MapElement ()
            H2 [Text "Tile layer"]
            MapWithTileLayer ()
            H2 [Text "Map with event management (click me!)"]
            MouseEvent ()
            H2 [Text "Search for a location"]
            LocationRequest ()
            H2 [Text "Pin a latitude/longitude point"]
            LatLonLocationRequest ()
            H2 [Text "Search for a route between two locations"]
            RouteRequest ()
            H2 [Text "Static maps"]
            StaticMap ()
            H2 [Text "Retrieve metadata about the images"]
            ImageMetadata ()
        ]

type Samples() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Main.Samples() :> _



open WebSharper.Sitelets

type Action = | Index

module Site =

    open WebSharper.Html.Server

    let HomePage ctx =
        Content.Page(
            Title = "WebSharper Bing Maps Tests",
            Body = [Div [new Samples()]]
        )

    let Main = Sitelet.Content "/" Index HomePage

[<Sealed>]
type Website() =
    interface IWebsite<Action> with
        member this.Sitelet = Site.Main
        member this.Actions = [Action.Index]

[<assembly: Website(typeof<Website>)>]
do ()
