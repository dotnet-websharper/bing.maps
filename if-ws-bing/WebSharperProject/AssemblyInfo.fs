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
namespace IntelliFactory.WebSharper.Bing

#nowarn "191"

open System.Reflection
open IntelliFactory.WebSharper
[<assembly: WebSharper>]
[<assembly: Require(typeof<Dependencies.MapsAPI>)>]
do ()