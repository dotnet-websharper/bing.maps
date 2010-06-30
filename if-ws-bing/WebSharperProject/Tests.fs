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


open Microsoft.FSharp.Quotations
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html

module Maps = IntelliFactory.WebSharper.Bing.Maps

module Tests =
    [<JavaScript>]
    let Test n f =
        let id = "map" + string n
        Div [Attr.Id id ; Attr.Style "position:relative; margin-top:25px; width:400px; height:400px;"]
        |>! OnAfterRender (fun mapElement -> 
            let map = new Maps.VEMap(id)
            f map)

    [<JavaScript>]
    let TestElem n f =
        let id = "map" + string n
        Div [Attr.Id id ; Attr.Style "position:relative; margin-top:25px; width:400px; height:400px;"]
        |>! OnAfterRender (fun mapElement -> 
            let map = new Maps.VEMap(id)
            f map mapElement)


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
             map.LoadMap(new Maps.VELatLong(47.22, -122.44), 12, Maps.VEMapStyle.Road, false, Maps.VEMapMode.Mode3D, true))

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
                      let location = new Maps.VELatLong(47.64, -122.23)
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
                      let location = new Maps.VELatLong(47.64, -122.23)
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

    [<JavaScript>]
    let DraggablePushPins(n: int) =     
        let output = Div [Attr.Id "output"; Text "No event"]
        let onDrag (_: Maps.ShapeDragEventArgs) = output.Text <- "ondrag event"
        let onStartDrag (_: Maps.ShapeDragEventArgs) = output.Text <- "onstartdrag event"
        let onEndDrag (_: Maps.ShapeDragEventArgs) = output.Text <- "onenddrag event"
        
        let m = Test n (fun map ->
            
            map.LoadMap()
            let location = new Maps.VELatLong(0.,0.)
            location.Latitude <- 41.8756
            location.Longitude <- -87.9956
            map.SetMapStyle(Maps.VEMapStyle.Shaded)
            map.SetMouseWheelZoomToCenter(false)
            map.SetCenterAndZoom(location, 9)
            let pushpin = new Maps.VEShape(Maps.VEShapeType.Pushpin, [|location|])
            pushpin.Draggable <- true
            pushpin.ondrag <- onDrag
            pushpin.onstartdrag <- onStartDrag
            pushpin.onenddrag <- onEndDrag
            pushpin.SetTitle("Just click on the pin and drag away!!!")
            map.AddShape(pushpin))
        Div [m; output]

    // This is not documented and unsupported, so just for testing we do it with an inline.
    // polylineMask.Primitives[0].symbol.stroke_dashstyle = "Dash";
    [<Inline "$x.Primitives[0].symbol.stroke_dashstyle = \"Dash\"" >]
    let SetPolylineToDashed (x: Maps.VEShape) : unit = Undefined
    
    // Translation of the example found in: http://www.garzilla.net/vemaps/DrawPolyLine.aspx
    [<JavaScript>]
    let DynamicPolyLine(n: int) =     
        let output = Div [Attr.Id "output"; Text "Disabled"]
        let btn = Button [Text "Click to begin drawing"]
        let drawing = ref false 
        let points  = ref [||]
        let polyline = ref None
        let m = TestElem n (fun map elem ->
            map.LoadMap()
            let location = new Maps.VELatLong(41.8756,-87.9956)
            map.SetMapStyle(Maps.VEMapStyle.Shaded)
            map.SetMouseWheelZoomToCenter(false)
            map.SetCenterAndZoom(location, 9)
            let maskPoints = [| new Maps.VELatLong(0., 0.); new Maps.VELatLong(0., 0.) |]
            let polylineMask = new Maps.VEShape(Maps.VEShapeType.Polyline, maskPoints)
            polylineMask.HideIcon()
            polylineMask.Hide()
            polylineMask.SetLineColor(new Maps.VEColor(0, 0, 255, 0.5));
            SetPolylineToDashed polylineMask
            map.AddShape(polylineMask)
            let mouseDownHandler (e: Maps.OnMouseDownEventArgs) = 
                if !drawing then
                    let currentLatLon = map.PixelToLatLong(new Maps.VEPixel(e.mapX, e.mapY))
                    points := Array.append !points [|currentLatLon|]         
                    maskPoints.[0] <- currentLatLon
                    if (!points).Length > 1 then
                        match !polyline with
                        | None -> 
                            let shape = new Maps.VEShape(Maps.VEShapeType.Polyline, !points)
                            shape.HideIcon();
                            map.AddShape(shape)
                            polyline := Some <| shape
                            maskPoints.[1] <- currentLatLon
                        | Some p -> p.SetPoints(!points) 
                false
            
            let mouseMoveHandler (e: Maps.OnMouseMoveEventArgs) = 
                if !drawing then
                    let loc = map.PixelToLatLong(new Maps.VEPixel(e.mapX, e.mapY))
                    if (!points).Length > 0 then polylineMask.Show()
                    maskPoints.[1] <- loc
                    polylineMask.SetPoints(maskPoints)
                false

            let keyPressHandler (e: Maps.KeyboardEventArgs) = 
                if !drawing && e.keyCode = 27 then
                    drawing := false
                    output.Text <- "Enabled"
                    elem.JQuery.Css ("cursor", "") |> ignore
                    btn.Text <- "Click to begin drawing"
                    btn.JQuery.RemoveAttr "disabled" |> ignore
                    polylineMask.Hide()
                false
            
            let startDrawing(_: Element) (_: JQueryEvent) =
                match !polyline, not !drawing with
                | Some p, true -> 
                    map.DeleteShape p
                    polyline := None
                    points := [||]
                | _ -> ()
                drawing := true
                output.Text <- "Enabled"    
                elem.JQuery.Css ("cursor", "crosshair") |> ignore
                btn.Text <- "Press ESC to exit drawing mode"
                btn.JQuery.Attr("disabled", "true") |> ignore
            
            btn |>! OnClick startDrawing |> ignore
            
            // Attach the event handlers to the mouse 
            map.AttachEvent("onmousedown", mouseDownHandler)
            map.AttachEvent("onmousemove", mouseMoveHandler)
            map.AttachEvent("onkeypress", keyPressHandler))
            
        Div [m; output; btn]

            
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
                 Tests.LatLongProperties
                 Tests.DraggablePushPins
                 Tests.DynamicPolyLine]
        Div (Seq.append [title] maps)
        

