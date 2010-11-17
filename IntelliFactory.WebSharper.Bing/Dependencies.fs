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

namespace IntelliFactory.WebSharper.Bing.Dependencies

open IntelliFactory.WebSharper

/// Requires the Bing Maps API.
type MapsAPI() =
    interface Resources.IResource with
        member this.Id              = "MS.Bing.Maps"
        member this.Dependencies    = Seq.empty
        member this.Render resolver =
            "http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.3"
            |> Resources.RenderJavaScript

[<assembly: Require(typeof<MapsAPI>)>]
do ()
