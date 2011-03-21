namespace IntelliFactory.WebSharper.Bing.Samples

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.Bing
module Main =

    [<JavaScript>]
    let credentials = "Ai6uQaKEyZbUvd33y5HU41hvoov_piUMn6t78Qzg7L1DWY4MFZqhjZdgEmCpQlbe"

    [<JavaScript>]
    let IsUndefined x =
        JavaScript.TypeOf x = JavaScript.Kind.Undefined

    (*
      var map = new Microsoft.Maps.Map(document.getElementById("mapDiv"), 
                               {credentials: "Your Bing Maps Key",
                                center: new Microsoft.Maps.Location(45.5, -122.5),
                                mapTypeId: Microsoft.Maps.MapTypeId.road,
                                zoom: 7});
    *)

    [<JavaScript>]
    let MapElement () =
        Div []
        |>! OnAfterRender (fun el ->
            let options =
                Bing.MapViewOptions(
                    Credentials = credentials,
                    Width = 400,
                    Height = 400,
                    MapTypeId = MapTypeId.Birdseye
                )
            let map = Bing.Map(el.Body, options)
            map.SetMapType(MapTypeId.Birdseye)
        )

    [<JavaScript>]
    let GeocodeCallback (map : Bing.Map) (resultElt : Element) (result : Bing.RestResponse) =
        let resource = result.ResourceSets.[0].Resources.[0] :?> LocationResource
        if IsUndefined resource then
            resultElt.Text <- "Location not found or no site around"
        else
            let loc = Bing.Location(resource.Point.Coordinates.[0], resource.Point.Coordinates.[1])
            let pin = Bing.Pushpin(loc, Bing.PushpinOptions())
            map.Entities.Push(pin)
            map.SetView(Bing.ViewOptions(Center=loc))
            let foo = Bing.ViewOptions()
            resultElt.Text <- resource.Name

    [<JavaScript>]
    let LocationRequest () =
        let input = Input [Attr.Type "text"]
        let button = Input [Attr.Type "button"; Attr.Value "Search"]
        let responseDiv = Div []
        let mapContainer =
            Div []
            |>! OnAfterRender (fun el ->
                let opts = Bing.MapOptions(Credentials = credentials,
                                           Width = 600,
                                           Height = 500)
                let map = Bing.Map(el.Body, opts)
                map.SetMapType(MapTypeId.Road)
                let request (_:Element) (_:Events.MouseEvent) =
                    Bing.Rest.RequestLocationByQuery(credentials, input.Value, GeocodeCallback map responseDiv)
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
                let opts = Bing.MapOptions(Credentials = credentials,
                                           Width = 600,
                                           Height = 500)
                let map = Bing.Map(el.Body, opts)
                map.SetMapType(MapTypeId.Road)
                let request (_:Element) (_:Events.MouseEvent) =
                    Bing.Rest.RequestLocationByPoint(credentials, float inputLat.Value, float inputLon.Value, [], GeocodeCallback map responseDiv)
                button |>! OnClick request |> ignore
            )
        Div [
            mapContainer
            Div [inputLat; inputLon; button]
            responseDiv
        ]

    [<JavaScript>]
    let MouseEvent () =
        let container = Div []
        let opt = Bing.MapViewOptions(Credentials = credentials,
                                      Width = 600,
                                      Height = 500)
        let map = Bing.Map(container.Body, opt)
        let displayLatLong (e:MouseEventArgs) =
            let center = (e.Target :?> Bing.Map).GetCenter()
            let message = "center: " + string center.Latitude + ", " + string center.Longitude +
                          "\nmouse: " + string (e.GetX()) + "," + string (e.GetY())
            JavaScript.Alert message
            ()
        Bing.Events.AddHandler(map, Bing.MouseEvent.Click, displayLatLong) |> ignore
        let pin = Bing.Pushpin(map.GetCenter())
        map.Entities.Push(pin)
        container

    [<JavaScript>]
    let RouteRequest () =
        let origin = Input []
        let destination = Input []
        let button = Input [Attr.Type "button"; Attr.Value "Request route"]
        let answer = Div []
        let RouteCallback (map : Bing.Map) (result : Bing.RestResponse) =
            if IsUndefined result ||
               IsUndefined result.ResourceSets ||
               result.ResourceSets.Length = 0 ||
               IsUndefined result.ResourceSets.[0].Resources ||
               result.ResourceSets.[0].Resources.Length = 0
            then
                answer.Text <- "Bad route"
            else
                let route = result.ResourceSets.[0].Resources.[0] :?> RouteResource
                // Draw the route on the map
                let corners = [|Bing.Location(route.Bbox.[0], route.Bbox.[1])
                                Bing.Location(route.Bbox.[2], route.Bbox.[3])|]
                let viewBoundaries = Bing.LocationRect.FromLocations(corners)
                map.SetView(Bing.ViewOptions(Bounds=viewBoundaries))
                let routeline = route.RoutePath.Line.Coordinates
                let routepoints = Array.init routeline.Length (fun i ->
                    Bing.Location(routeline.[i].[0], routeline.[i].[1]))
                let routeshape = Bing.Polyline(routepoints, Bing.PolylineOptions(StrokeColor=Bing.Color(200, 0, 0, 200)))
                map.Entities.Push(routeshape)
                // Write directions under the map
                let getItems (instructions : Bing.ItineraryItem[]) =
                    instructions
                    |> Array.map (fun inst ->
                        TR [TD [Text inst.Instruction.Text]
                            TD [Text (string inst.TravelDistance + " " + string route.DistanceUnit)]])
                let message =
                    route.RouteLegs
                    |> Array.map (fun leg ->
                        getItems leg.ItineraryItems)
                    |> Array.concat
                answer.Clear()
                answer.Append (Table message)
        let mapContainer =
            Div []
            |>! OnAfterRender (fun el ->
            let opts = Bing.MapOptions(Credentials = credentials,
                                        Width = 600,
                                        Height = 500)
            let map = Bing.Map(el.Body, opts)
            map.SetMapType(MapTypeId.Road)
            let request (_:Element) (_:Events.MouseEvent) =
                Bing.Rest.RequestRoute(credentials,
                                       RouteRequest(Waypoints=[|origin.Value; destination.Value|],
                                                    Avoid=[|Bing.RouteAvoid.Highways;|],
                                                    RoutePathOutput=RoutePathOutput.Points),
                                       RouteCallback map)
            button |>! OnClick request |> ignore
        )
        Div [origin; destination; button; mapContainer; answer]

    

    [<JavaScript>]
    let Samples () =
        Div [
            MapElement ()
            Br []
            MouseEvent ()
            Br []
            LocationRequest ()
            Br []
            LatLonLocationRequest ()
            Br []
            RouteRequest ()
        ]


[<JavaScriptType>]
type Samples() = 
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Main.Samples() :> _

