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
    let RequestLocationByAddress(credentials, address : Bing.Address, callback : Bing.RestResponse -> unit) =
        JavaScript.Set JavaScript.Global RequestCallbackName callback
        let fields = seq {
            if not (IsUndefined address.AdminDistrict) then
                yield "adminDistrict=" + address.AdminDistrict
            if not (IsUndefined address.Locality) then
                yield "locality=" + address.Locality
            if not (IsUndefined address.AddressLine) then
                yield "addressLine=" + address.AddressLine
            if not (IsUndefined address.CountryRegion) then
                yield "countryRegion=" + address.CountryRegion
            if not (IsUndefined address.PostalCode) then
                yield "postalCode=" + address.PostalCode
        }
        let req = String.concat "&" fields
        let fullReq = restApiUri + "Locations?" + req + "&" + RequestStringBoilerplate credentials
        SendRequest fullReq

    [<JavaScript>]
    let RequestLocationByQuery(credentials, query : string, callback : Bing.RestResponse -> unit) =
        JavaScript.Set JavaScript.Global RequestCallbackName callback
        let req = restApiUri + "Locations?query=" + query + "&" + RequestStringBoilerplate credentials
        SendRequest req

    [<JavaScript>]
    let RequestLocationByPoint(credentials, x, y, entities, callback : Bing.RestResponse -> unit) =
        JavaScript.Set JavaScript.Global RequestCallbackName callback
        let retrieveEntities = function
        | [] -> ""
        | l -> "&includeEntityTypes=" + String.concat "," l
        let req = restApiUri + "Locations/" + string x + "," + string y +
                  "?" + RequestStringBoilerplate credentials +
                  retrieveEntities entities
        SendRequest req

    [<JavaScript>]
    let RequestRoute(credentials, request : Bing.RouteRequest, callback : Bing.RestResponse -> unit) =
        JavaScript.Set JavaScript.Global RequestCallbackName callback
        let fields =
            [| "avoid"; "heading"; "optimize"; "routePathOutput"; "distanceUnit"
               "dateTime"; "timeType"; "maxSolutions"; "travelMode" |]
            |> Array.choose (fun name ->
                                let value = JavaScript.Get name request
                                if IsUndefined value then
                                    None
                                else
                                    Some (name + "=" + string value))
            |> Array.append (Array.mapi (fun i w -> "wp." + string i + "=" + w) request.Waypoints)
        let req = String.concat "&" fields
        let fullReq = restApiUri + "/Routes?" + req + "&" + RequestStringBoilerplate credentials
        SendRequest fullReq
