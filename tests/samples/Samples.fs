namespace IntelliFactory.WebSharper.Bing.Samples

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.Bing
module Main =
    [<JavaScript>]
    let Main () =
        let foo = Bing.Location(1., 2., 3., Bing.AltitudeReference.Ellipsoid)
        let bar = Bing.Location.NormalizeLongitude 942.
        let container = Div []
        let map = Bing.Map(container.Body)
        Div [
            Text "Bing Samples"
            Br [] :> _
            Text (string bar + " " + foo.ToString())
            container :> _
            ]

[<JavaScriptType>]
type Samples() = 
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Main.Main() :> _

