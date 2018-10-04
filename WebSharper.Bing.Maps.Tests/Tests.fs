// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2018 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}
namespace WebSharper.Bing.Tests

open WebSharper

[<JavaScript>]
module Main =
    open WebSharper.JavaScript
    open WebSharper.Html.Client
    open WebSharper.Bing.Maps

    let credentials = "Ai6uQaKEyZbUvd33y5HU41hvoov_piUMn6t78Qzg7L1DWY4MFZqhjZdgEmCpQlbe"

    let IsUndefined x =
        JS.TypeOf x = JS.Kind.Undefined

    let MakeMapWrapper (f: Dom.Node -> MapApi -> unit) =
        Div [Attr.Style "height: 600px;"]
        |>! OnAfterRender (fun el -> MapsLoading.OnLoad (f el.Body))

    let MapElement () =
        MakeMapWrapper <| fun el api ->
            let options = MapViewOptions(Credentials = credentials)
            let map = api.Map(el, options)
            map.SetMapType(api.MapTypeId.Birdseye)

    let MapWithTileLayer () =
        MakeMapWrapper <| fun el api ->
            let options = MapViewOptions(Credentials = credentials,
                                         MapTypeId = api.MapTypeId.Aerial)
            let map = api.Map(el, options)
            let tileSource = api.TileSource(TileSourceOptions(UriConstructor="https://www.microsoft.com/maps/isdk/ajax/layers/lidar/{quadkey}.png"))
            let tileLayer = api.TileLayer(TileLayerOptions(Mercator=tileSource, Opacity=0.7))
            map.Entities.Push(tileLayer)

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

    let GeocodeCallback (api: MapApi) (map : Map) (resultElt : Element) (result : RestResponse) =
        match CheckJsonResponse result with
        | Some error ->
            resultElt.Text <- "Bad location request: " + error
        | None ->
            let resource = result.ResourceSets.[0].Resources.[0] :?> LocationResource
            if IsUndefined resource then
                resultElt.Text <- "Location not found or no site around"
            else
                let loc = api.Location(resource.Point.Coordinates.[0], resource.Point.Coordinates.[1])
                let pin = api.Pushpin(loc)
                map.Entities.Push(pin)
                map.SetView(ViewOptions(Center=loc))
                resultElt.Text <- resource.Name

    let LocationRequest () =
        let input = Input [Attr.Type "text"]
        let button = Input [Attr.Type "button"; Attr.Value "Search"]
        let responseDiv = Div []
        let mapContainer =
            MakeMapWrapper <| fun el api ->
                let opts = MapOptions(Credentials = credentials)
                let map = api.Map(el, opts)
                map.SetMapType(api.MapTypeId.Road)
                let request (_:Element) (_:Events.MouseEvent) =
                    Rest.RequestLocationByQuery(credentials, input.Value, GeocodeCallback api map responseDiv)
                button |>! OnClick request |> ignore
        Div [
            mapContainer
            Div [input; button]
            responseDiv
        ]

    let LatLonLocationRequest () =
        let inputLat = Input [Attr.Type "text"]
        let inputLon = Input [Attr.Type "text"]
        let button = Input [Attr.Type "button"; Attr.Value "Search"]
        let responseDiv = Div []
        let mapContainer =
            MakeMapWrapper <| fun el api ->
                let opts = MapOptions(Credentials = credentials)
                let map = api.Map(el, opts)
                map.SetMapType(api.MapTypeId.Road)
                let request (_:Element) (_:Events.MouseEvent) =
                    Rest.RequestLocationByPoint(credentials, float inputLat.Value, float inputLon.Value, [], GeocodeCallback api map responseDiv)
                button |>! OnClick request |> ignore
        Div [
            mapContainer
            Div [Span[Text "Latitude:"]; inputLat; Span[Text "Longitude"]; inputLon; button]
            responseDiv
        ]

    let MouseEvent () =
        MakeMapWrapper <| fun el api ->
            let opt = MapViewOptions(Credentials = credentials)
            let map = api.Map(el, opt)
            let pin = api.Pushpin(map.GetCenter(), PushpinOptions(Draggable=true))
            map.Entities.Push(pin)
            let displayLatLong (e:MouseEventArgs) =
                let center = (e.Target :?> Map).GetCenter()
                let pinLocation = pin.GetLocation()
                let pinPoint = map.TryLocationToPixel(pinLocation)
                let mousePoint = api.Point(float(e.GetX()), float(e.GetY()))
                let mouseLocation = map.TryPixelToLocation(mousePoint)
                let message = "pushpin (lat/lon): " + string pinLocation.Latitude + ", " + string pinLocation.Longitude +
                              "\npushpin (screen x/y): " + string pinPoint.X + "," + string pinPoint.Y +
                              "\nmouse (lat/lon): " + string mouseLocation.Latitude + ", " + string mouseLocation.Longitude +
                              "\nmouse (screen x/y): " + string mousePoint.X + "," + string mousePoint.Y
                JS.Alert message
            api.Events.AddHandler(map, MouseEvent.Click, displayLatLong) |> ignore

    open WebSharper.Bing.Maps.Directions

    let RouteRequest () =
        let origin = Input []
        let destination = Input []
        let button = Input [Attr.Type "button"; Attr.Value "Request route"]
        let highwayBox = Input [Attr.Type "checkbox"]
        let answer = Div [Id "answer"]
        let mapContainer =
            MakeMapWrapper <| fun el api ->
                let opts = MapOptions(Credentials = credentials)
                let map = api.Map(el, opts)
                map.SetMapType(api.MapTypeId.Road)
                Maps.LoadModule("Microsoft.Maps.Directions", LoadModuleArgs(fun () ->
                    let dirman = Directions.DirectionsManager(map)
                    button |> OnClick (fun _ _ ->
                        dirman.ResetDirections()
                        dirman.AddWaypoint <| Directions.Waypoint(WaypointOptions(Address = origin.Value))
                        dirman.AddWaypoint <| Directions.Waypoint(WaypointOptions(Address = destination.Value))
                        dirman.SetRenderOptions <| DirectionsRenderOptions(ItineraryContainer = answer.Dom)
                        dirman.SetRequestOptions <| DirectionsRequestOptions(DistanceUnit = DistanceUnit.Kilometers)
                        dirman.CalculateDirections()
                    )
                ))
        Div [mapContainer
             Span[Text "From:"]; origin
             Span[Text "To:"]; destination
             highwayBox; Span[Text "Accept highways"]; button
             answer]

    let StaticMap () =
        MakeMapWrapper <| fun el api ->
            let req1 = StaticMapRequest(CenterPoint=api.Point(47.2, 19.1),
                                        imagerySet=ImagerySet.Road,
                                        ZoomLevel=10,
                                        Pushpin=[|PushpinRequest(x=47.1, y=19.0, IconStyle=2, Label="P1")
                                                  PushpinRequest(x=47.13, y=19.17, IconStyle=10)|])
            Rest.StaticMap(credentials, req1) |> el.AppendChild |> ignore
            let req2 = StaticMapRequest(Query="Washington DC",
                                        imagerySet=ImagerySet.Aerial)
            Rest.StaticMap(credentials, req2) |> el.AppendChild |> ignore

    let ImageMetadata () =
        let callback (answer : Dom.Node) (result : RestResponse) =
            match CheckJsonResponse result with
            | Some error ->
                answer.TextContent <- "Bad metadata response: " + error
            | None ->
                let resource = result.ResourceSets.[0].Resources.[0] :?> ImageryMetadataResource
                let txt =
                    [
                        (P [Text("Road map tile size: " + string resource.ImageHeight + "x" + string resource.ImageWidth)]).Body
                        (P [Text("Road map tile URL: " + resource.ImageUrl)]).Body
                    ]
                List.iter (answer.AppendChild>>ignore) txt
        MakeMapWrapper <| fun el api ->
            let req = ImageryMetadataRequest(imagerySet=ImagerySet.Road,
                                             MapVersion=MapVersion.V1,
                                             CenterPoint=api.Point(47.2, 19.1))
            Rest.RequestImageryMetadata(credentials, req, callback el)



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
