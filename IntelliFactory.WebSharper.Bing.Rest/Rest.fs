namespace IntelliFactory.WebSharper.Bing

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.Bing

module Rest =

    [<JavaScript>]
    let private credentials = "Ai6uQaKEyZbUvd33y5HU41hvoov_piUMn6t78Qzg7L1DWY4MFZqhjZdgEmCpQlbe"

    [<JavaScript>]
    let private restApiUri = "http://dev.virtualearth.net/REST/v1/"

    [<JavaScript>]
    let private IsUndefined x =
        JavaScript.TypeOf x = JavaScript.Kind.Undefined

    [<JavaScript>]
    let private SendRequest req =
        let script = Script [Attr.Type "text/javascript"; Attr.Src req]
        Dom.Document.Current.DocumentElement.AppendChild script.Dom |> ignore

    [<JavaScript>]
    let private RequestCallbackName = "BingOnReceive"

    [<JavaScript>]
    let private RequestStringBoilerplate credentials = "output=json&jsonp=" + RequestCallbackName + "&key=" + credentials

    [<JavaScript>]
    let private OptionalFields request arr =
        arr
        |> Array.choose (fun name ->
                            let value = JavaScript.Get name request
                            if IsUndefined value then
                                None
                            else
                                Some (name + "=" + string value))

    [<JavaScript>]
    let RequestLocationByAddress(credentials, address : Bing.Address, callback : Bing.RestResponse -> unit) =
        JavaScript.Set JavaScript.Global RequestCallbackName callback
        let fields =
            OptionalFields address
                [|"adminDistrict"; "locality"; "addressLine"; "countryRegion"; "postalCode"|]
        let req = String.concat "&" fields
        let fullReq = restApiUri + "Locations?" + req + "&" + RequestStringBoilerplate credentials
        SendRequest fullReq

    [<JavaScript>]
    let RequestLocationByQuery(credentials, query : string, callback : Bing.RestResponse -> unit) =
        JavaScript.Set JavaScript.Global RequestCallbackName callback
        let req = restApiUri + "Locations?query=" + query + "&" + RequestStringBoilerplate credentials
        SendRequest req

    [<JavaScript>]
    let RequestLocationByPoint(credentials, x:float, y:float, entities, callback : Bing.RestResponse -> unit) =
        JavaScript.Set JavaScript.Global RequestCallbackName callback
        let retrieveEntities = function
        | [] -> ""
        | l -> "&includeEntityTypes=" + String.concat "," l
        let req = restApiUri + "Locations/" + string x + "," + string y +
                  "?" + RequestStringBoilerplate credentials +
                  retrieveEntities entities
        SendRequest req

    [<JavaScript>]
    let private StringifyWaypoints waypoints =
        Array.mapi (fun i (w:Waypoint) -> "wp." + string i + "=" + string w) waypoints

    [<JavaScript>]
    let RequestRoute(credentials, request : Bing.RouteRequest, callback : Bing.RestResponse -> unit) =
        JavaScript.Set JavaScript.Global RequestCallbackName callback
        let fields =
            OptionalFields request
                [| "avoid"; "heading"; "optimize"; "routePathOutput"; "distanceUnit"
                   "dateTime"; "timeType"; "maxSolutions"; "travelMode" |]
            |> Array.append (StringifyWaypoints request.Waypoints)
        let req = String.concat "&" fields
        let fullReq = restApiUri + "/Routes?" + req + "&" + RequestStringBoilerplate credentials
        SendRequest fullReq

    [<JavaScript>]
    let RequestImageryMetadata(credentials, request : Bing.ImageryMetadataRequest,
                               callback : Bing.RestResponse -> unit) =
        JavaScript.Set JavaScript.Global RequestCallbackName callback
        let fields =
            OptionalFields request
                [| "include"; "mapVersion"; "orientation"; "zoomLevel" |]
        let req = String.concat "&" fields
        let fullReq =
            restApiUri + "Imagery/Metadata/" + string request.ImagerySet +
            (if not(IsUndefined request.CenterPoint) then "/" + request.CenterPoint.ToUrlString() else "") + "?" +
            req + "&" + RequestStringBoilerplate credentials
        SendRequest fullReq

    [<JavaScript>]
    let StaticMap(credentials, request : Bing.StaticMapRequest) =
        let fields =
            [
                OptionalFields request
                    [| "avoid"; "dateTime"; "mapLayer"; "mapVersion"
                       "maxSolutions"; "optimize"; "timeType"; "travelMode"; "zoomLevel" |]
                (if IsUndefined request.MapArea then
                     [||]
                 else
                    [|(fst request.MapArea).ToUrlString() + "," + (snd request.MapArea).ToUrlString()|])
                (if IsUndefined request.MapSize then
                     [||]
                 else
                    [|string (fst request.MapSize) + "," + string (snd request.MapSize)|])
                (if IsUndefined request.Pushpin then
                     [||]
                 else
                    let pushpinToUrlString (pin : PushpinRequest) =
                        let coords = string pin.X + "," + string pin.Y
                        let icstyle = if IsUndefined pin.IconStyle then "" else string pin.IconStyle
                        let label = if IsUndefined pin.Label then "" else pin.Label
                        coords + ";" + icstyle + ";" + label
                    request.Pushpin |> Array.map (fun pin -> "pp=" + pushpinToUrlString pin))
                (if IsUndefined request.Waypoints then
                     [||]
                 else
                    StringifyWaypoints request.Waypoints)
            ]
            |> Array.concat
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
        Img [Attr.Src fullReq]
