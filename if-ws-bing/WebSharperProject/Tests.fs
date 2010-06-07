
namespace IntelliFactory.Tests

open IntelliFactory.WebSharper.Bing
open Microsoft.FSharp.Quotations
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html

module Tests =
    [<JavaScript>]
    let BasicMap () =     
        Div [Attr.Id "map1"; Attr.Style "padding-bottom:20px; width:500px; height:300px;"]
        |>! OnAfterRender (fun mapElement -> 
            Window.Alert("Test1")
            let map = new Maps.VEMap("map1")
            map.LoadMap(new Maps.VELatLong(12.,12.))
            Window.Alert("Test2"))
            
[<JavaScriptType>]
type Test() = 
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Tests.BasicMap()


