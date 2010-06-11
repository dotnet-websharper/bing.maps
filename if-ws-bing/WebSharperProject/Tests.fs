// $begin{copyright}
//
// This file is confidential and proprietary.
//
// Copyright (c) IntelliFactory, 2004-2010.
//
// All rights reserved.  Reproduction or use in whole or in part is
// prohibited without the written consent of the copyright holder.
//-----------------------------------------------------------------
// $end{copyright}

namespace IntelliFactory.Tests

open IntelliFactory.WebSharper.Bing
open Microsoft.FSharp.Quotations
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html

module Tests =
    open IntelliFactory.WebSharper.Bing.Maps
    [<JavaScript>]
    let Test n f =
        let id = "map" + string n
        Div [Attr.Id id ; Attr.Style "position:relative; margin-top:25px; width:400px; height:400px;"]
        |>! OnAfterRender (fun mapElement -> 
            let map = new Maps.VEMap(id)
            f map)

    [<JavaScript>]
    let BasicMap (n: int) =     
        Test n (fun map -> 
            map.LoadMap())
    
    [<JavaScript>]
    let GetMapMode (n: int) =     
        Test n (fun map -> 
            map.LoadMap(new Maps.VELatLong(0.,45.))
            Window.Alert(map.GetMapMode().ToString()))
    
    [<JavaScript>]
    let SpecificMap(n: int) =     
        Test n (fun map -> 
            map.LoadMap(new Maps.VELatLong(47.6, -122.33), 10, Maps.VEMapStyle.Hybrid, false))
    
    [<JavaScript>]
    let Map3D(n: int) =     
        Test n (fun map ->
             map.LoadMap(new VELatLong(47.22, -122.44), 12, VEMapStyle.Road, false, VEMapMode.Mode3D, true))

    [<JavaScript>]
    let LatLongProperties(n: int) =     
        Test n (fun map ->
            let location = new Maps.VELatLong(0.,0.)
            location.Latitude <- 47.6
            location.Longitude <- -122.33 
            map.LoadMap(location, 10, Maps.VEMapStyle.Hybrid, false))
    

    [<JavaScript>]
    let Traffic n =
        let (btn1,btn2) =
             Button [Text "Add Traffic"],
             Button [Text "Remove Traffic"]  
        let div = Test n (fun map -> 
                      let location = new VELatLong(47.64, -122.23)
                      let ShowTraffic (_:Element) (_:JQueryEvent) =
                          map.LoadTraffic(true)
                          map.ShowTrafficLegend(50,50)
                          map.SetTrafficLegendText("The traffic dude")
                      let ClearTraffic (_:Element) (_:JQueryEvent) =
                          map.ClearTraffic();
                      btn1 |>! OnClick ShowTraffic  |> ignore
                      btn2 |>! OnClick ClearTraffic |> ignore
                      map.LoadMap(location, 9))
        Div [div 
             btn1 
             btn2]
        
    [<JavaScript>]
    let ZoomInZoomOut n =
        let (btn1,btn2) =
             Button [Text "+"],
             Button [Text "-"]  
        let div = Test n (fun map -> 
                      let location = new VELatLong(47.64, -122.23)
                      let ZoomIn(_: Element) (_: JQueryEvent) =
                          map.ZoomIn()
                      let ZoomOut(_: Element) (_: JQueryEvent) =
                          map.ZoomOut()
                      btn1 |>! OnClick ZoomIn  |> ignore
                      btn2 |>! OnClick ZoomOut |> ignore
                      map.LoadMap(location, 9))
        Div [div 
             btn1 
             btn2]
            
[<JavaScriptType>]
type Test() = 
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = 
        let numbers = Seq.initInfinite id
        let title = H1 [Text "Examples:"]
        let maps =
            Seq.map2 (|>) numbers
                [Tests.BasicMap
                 Tests.GetMapMode
                 Tests.SpecificMap
                 Tests.Map3D
                 Tests.Traffic
                 Tests.ZoomInZoomOut
                 Tests.LatLongProperties]
        Div (Seq.append [title] maps)
        

