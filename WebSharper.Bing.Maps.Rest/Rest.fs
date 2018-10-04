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
namespace WebSharper.Bing.Maps

open WebSharper
open WebSharper.JavaScript

[<JavaScript>]
module MapsLoading =
    let private cbs = ResizeArray<MapApi -> unit>()

    type Resource() =
        inherit Resources.BaseResource("https://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=7.0&callback=WebSharperBingMapsLoaded")

    [<Require(typeof<Resource>)>]
    let Loaded() =
        for cb in cbs do
            cb JS.Global?Microsoft?Maps
        cbs.Clear()

    let OnLoad (f: MapApi -> unit) =
        cbs.Add(f)
        if JS.Global?Microsoft?Maps?MapTypeId then
            Loaded()
        else
            JS.Global?WebSharperBingMapsLoaded <- Loaded

[<JavaScript>]
module Rest =

    let private restApiUri = "https://dev.virtualearth.net/REST/v1/"

    let private IsUndefined x =
        JS.TypeOf x = JS.Kind.Undefined

    let private SendRequest req =
        let script = JS.Document.CreateElement("script")
        script.SetAttribute("type", "text/javascript")
        script.SetAttribute("src", req)
        JS.Document.DocumentElement.AppendChild script |> ignore

    let private RequestCallbackName = "BingOnReceive"

    let private RequestStringBoilerplate credentials = "output=json&jsonp=" + RequestCallbackName + "&key=" + credentials

    let private OptionalFields request arr =
        arr
        |> Array.choose (fun name ->
            let value = (?) request name
            if IsUndefined value then None else
                Some (name + "=" + string value))

    let RequestLocationByAddress(credentials, address : Address, callback : RestResponse -> unit) =
        (?<-) JS.Global RequestCallbackName callback
        let fields =
            OptionalFields address
                [|"adminDistrict"; "locality"; "addressLine"; "countryRegion"; "postalCode"|]
        let req = String.concat "&" fields
        let fullReq = restApiUri + "Locations?" + req + "&" + RequestStringBoilerplate credentials
        SendRequest fullReq

    let RequestLocationByQuery(credentials, query : string, callback : RestResponse -> unit) =
        (?<-) JS.Global RequestCallbackName callback
        let req = restApiUri + "Locations?query=" + query + "&" + RequestStringBoilerplate credentials
        SendRequest req

    let RequestLocationByPoint(credentials, x:float, y:float, entities, callback : RestResponse -> unit) =
        (?<-) JS.Global RequestCallbackName callback
        let retrieveEntities = function
        | [] -> ""
        | l -> "&includeEntityTypes=" + String.concat "," l
        let req =
            restApiUri + "Locations/" + string x + "," + string y +
                "?" + RequestStringBoilerplate credentials +
                retrieveEntities entities
        SendRequest req

    let private StringifyWaypoints waypoints =
        Array.mapi (fun i (w:Waypoint) -> "wp." + string i + "=" + string w) waypoints

    let RequestRoute(credentials, request : RouteRequest, callback : RestResponse -> unit) =
        (?<-) JS.Global RequestCallbackName callback
        let fields =
            OptionalFields request
                [| "avoid"; "heading"; "optimize"; "routePathOutput"; "distanceUnit"
                   "dateTime"; "timeType"; "maxSolutions"; "travelMode" |]
            |> Array.append (StringifyWaypoints request.Waypoints)
        let req = String.concat "&" fields
        let fullReq = restApiUri + "/Routes?" + req + "&" + RequestStringBoilerplate credentials
        SendRequest fullReq

    let RequestRouteFromMajorRoads(credentials, request : RouteFromMajorRoadsRequest, callback : RestResponse -> unit) =
        (?<-) JS.Global RequestCallbackName callback
        let fields =
            OptionalFields request
                [| "destination"; "exclude"; "routePathOutput"; "distanceUnit" |]
        let req = String.concat "&" fields
        let fullReq = restApiUri + "/Routes/FromMajorRoads?" + req + "&" + RequestStringBoilerplate credentials
        SendRequest fullReq

    let RequestImageryMetadata(credentials, request : ImageryMetadataRequest,
                               callback : RestResponse -> unit) =
        (?<-) JS.Global RequestCallbackName callback
        let fields =
            OptionalFields request
                [| "include"; "mapVersion"; "orientation"; "zoomLevel" |]
        let req = String.concat "&" fields
        let fullReq =
            restApiUri + "Imagery/Metadata/" + string request.ImagerySet +
            (if not(IsUndefined request.CenterPoint) then "/" + request.CenterPoint.ToUrlString() else "") + "?" +
            req + "&" + RequestStringBoilerplate credentials
        SendRequest fullReq

    let StaticMapUrl(credentials, request : StaticMapRequest) =
        let fields =
            [
                yield! OptionalFields request
                    [| "avoid"; "dateTime"; "mapLayer"; "mapVersion"
                       "maxSolutions"; "optimize"; "timeType"; "travelMode"; "zoomLevel" |]
                if not (IsUndefined request.MapArea) then
                    yield (fst request.MapArea).ToUrlString() + "," + (snd request.MapArea).ToUrlString()
                if not (IsUndefined request.MapSize) then
                    yield string (fst request.MapSize) + "," + string (snd request.MapSize)
                if not (IsUndefined request.Pushpin) then
                    let pushpinToUrlString (pin : PushpinRequest) =
                        let coords = string pin.X + "," + string pin.Y
                        let icstyle = if IsUndefined pin.IconStyle then "" else string pin.IconStyle
                        let label = if IsUndefined pin.Label then "" else pin.Label
                        coords + ";" + icstyle + ";" + label
                    yield! request.Pushpin |> Array.map (fun pin -> "pp=" + pushpinToUrlString pin)
                if not (IsUndefined request.Waypoints) then
                    yield! StringifyWaypoints request.Waypoints
                if not (IsUndefined request.DeclutterPins) then
                    yield "dcl=" + if request.DeclutterPins then "1" else "0"
                if not (IsUndefined request.DistanceBeforeFirstTurn) then
                    yield "dbft=" + string request.DistanceBeforeFirstTurn
            ]
        let query =
            if not (IsUndefined request.Query) then
                request.Query
            elif not (IsUndefined request.CenterPoint) then
                request.CenterPoint.ToUrlString() + "/" + string request.ZoomLevel
            else
                ""
        let hasRoute = not (IsUndefined request.Waypoints)
        let req = String.concat "&" fields
        let fullReq =
            restApiUri + "Imagery/Map/" +
            string request.ImagerySet + "/" +
            (if hasRoute then "Route/" else "") + query + "?" +
            req + "&key=" + credentials
        fullReq

    let StaticMap(credentials, request) =
        let img = JS.Document.CreateElement("img")
        img.SetAttribute("src", StaticMapUrl(credentials, request))
        img
