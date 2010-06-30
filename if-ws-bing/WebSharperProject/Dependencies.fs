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

open System.Configuration
open IntelliFactory.WebSharper

/// Requires the Bing Maps API.
type MapsAPI() =
    interface IResource with
        member this.Render(resolver, writer) =
            writer.Write("<script type=\"text/javascript\" src=\"http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.3\"></script>")