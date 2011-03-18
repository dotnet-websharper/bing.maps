namespace IntelliFactory.WebSharper.Bing.Samples

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.Bing
module Main =

    [<JavaScript>]
    let credentials = "Ai6uQaKEyZbUvd33y5HU41hvoov_piUMn6t78Qzg7L1DWY4MFZqhjZdgEmCpQlbe"

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
                Bing.MapViewOptinos(
                    Credentials = credentials,
                    Width = 400,
                    Height = 400,
                    MapTypeId = MapTypeId.Birdseye
                )
            let map = Bing.Map(el.Body, options)
            map.SetMapType(MapTypeId.Birdseye)
        )

    [<JavaScript>]
    let Main () =
        let foo = Bing.Location(1., 2., 3., Bing.AltitudeReference.Ellipsoid)
        let bar = Bing.Location.NormalizeLongitude 942.
        let container = Div []
        let opt = Bing.MapOptions()
        opt.Credentials <- credentials
        let map = Bing.Map(container.Body, opt)
        let displayLatLong (e:MouseEventArgs) =
            let center = (e.Target :?> Bing.Map).GetCenter()
            let message = "center: " + string center.Latitude + ", " + string center.Longitude +
                          "\nmouse: " + string (e.GetX()) + "," + string (e.GetY())
            JavaScript.Alert message
            ()
        let clickEvent = Bing.Events.AddHandler(map, Bing.MouseEvent.Click, displayLatLong)
        let pin = Bing.Pushpin(map.GetCenter())
        map.Entities.Push(pin)
        Div [
            Text "Bing Samples"
            Br [] :> _
            Text (string bar + " " + foo.ToString())
            container :> _
            ]

    [<JavaScript>]
    let Samples () =
        Div [
            MapElement ()
        ]


[<JavaScriptType>]
type Samples() = 
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Main.Main() :> _

