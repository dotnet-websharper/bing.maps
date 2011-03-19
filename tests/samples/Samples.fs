namespace IntelliFactory.WebSharper.Bing.Samples

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.Bing
module Main =

    [<JavaScript>]
    let credentials = "Ai6uQaKEyZbUvd33y5HU41hvoov_piUMn6t78Qzg7L1DWY4MFZqhjZdgEmCpQlbe"

    [<JavaScript>]
    let restApiUri = "http://dev.virtualearth.net/REST/v1/"

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
        if resource = JavaScript.Undefined then
            resultElt.Text <- "Location not found or no site around"
        else
            let loc = Bing.Location(resource.Point.Coordinates.[0], resource.Point.Coordinates.[1])
            let pin = Bing.Pushpin(loc)
            map.Entities.Push(pin)
            map.SetView(Bing.ViewOptions(Center=loc))
            resultElt.Text <- resource.Name

    [<JavaScript>]
    let SendRequest req =
        let script = Script [Attr.Type "text/javascript"; Attr.Src req]
        Dom.Document.Current.DocumentElement.AppendChild script.Dom |> ignore

    [<JavaScript>]
    let RequestStringBoilerplate = "output=json&jsonp=BingOnReceive&key=" + credentials

    [<JavaScript>]
    let RequestLocationByAddress (address : Bing.Address) (callback : Bing.RestResponse -> unit) =
        JavaScript.Set JavaScript.Global "BingOnReceive" callback
        let fields = seq {
            if address.AdminDistrict <> JavaScript.Undefined then
                yield "adminDistrict=" + address.AdminDistrict
            if address.Locality <> JavaScript.Undefined then
                yield "locality=" + address.Locality
            if address.AddressLine <> JavaScript.Undefined then
                yield "addressLine=" + address.AddressLine
            if address.CountryRegion <> JavaScript.Undefined then
                yield "countryRegion=" + address.CountryRegion
            if address.PostalCode <> JavaScript.Undefined then
                yield "postalCode=" + address.PostalCode
        }
        let req = String.concat "&" fields
        let fullReq = restApiUri + "Locations?" + req + "&" + RequestStringBoilerplate
        SendRequest fullReq

    [<JavaScript>]
    let RequestLocationByQuery (query : string) (callback : Bing.RestResponse -> unit) =
        JavaScript.Set JavaScript.Global "BingOnReceive" callback
        let req = restApiUri + "Locations?query=" + query + "&" + RequestStringBoilerplate
        SendRequest req

    [<JavaScript>]
    let RequestLocationByPoint x y entities (callback : Bing.RestResponse -> unit) =
        let retrieveEntities = function
        | [] -> ""
        | l -> "&includeEntityTypes=" + String.concat "," l
        JavaScript.Set JavaScript.Global "BingOnReceive" callback
        let req = restApiUri + "Locations/" + string x + "," + string y +
                  "?" + RequestStringBoilerplate +
                  retrieveEntities entities
        SendRequest req

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
                    RequestLocationByQuery input.Value (GeocodeCallback map responseDiv)
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
                    RequestLocationByPoint (float inputLat.Value) (float inputLon.Value) [] (GeocodeCallback map responseDiv)
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
    let Samples () =
        Div [
            MapElement ()
            Br []
            MouseEvent ()
            Br []
            LocationRequest ()
            Br []
            LatLonLocationRequest ()
        ]


[<JavaScriptType>]
type Samples() = 
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Main.Samples() :> _

