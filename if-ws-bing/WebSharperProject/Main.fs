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

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
open IntelliFactory.WebSharper.Formlet

[<JavaScriptType>]
module Maps =
    
    [<Stub>]
    [<Name "VELatLong">]
    type VELatLong =
        new (lat: float, long: float) = {}

    [<Stub>]
    [<Name "VEMapStyle">]
    type VEMapStyle = class end

    [<Stub>]
    [<Name "VEMapMode">]
    type VEMapMode = class end
    
    [<Stub>]
    [<Name "VEMapOptions">]
    type VEMapOptions= class end
    
    [<Stub>]
    [<Name "VETileSourceSpecification">]
    type VETileSourceSpecification= class end

    [<Stub>]
    [<Name "VEDashboardSize">]
    type VEDashboardSize = class end

    [<Stub>]
    [<Name "VEFailedShapeRequest">]
    type VEFailedShapeRequest = class end

    [<Stub>]
    [<Name "VEPrintOptions">]
    type VEPrintOptions = class end

    [<Stub>]
    [<Name "VEFindType">]
    type VEFindType= class end

    [<Stub>]
    [<Name "VEFindResult">]
    type VEFindResult = class end

    [<Stub>]
    [<Name "VEDistanceUnit">]
    type VEDistanceUnit = class end

    [<Stub>]
    [<Name "VEShapeAccuracy">]
    type VEShapeAccuracy = class end

    [<Stub>]
    [<Name "VEMiniMapSize">]
    type VEMiniMapSize = class end
    
    [<Stub>]
    [<Name "VEShape">]
    type VEShape = class end

    [<Stub>]
    [<Name "VEShapeLayer">]
    type VEShapeLayer = class end

    [<Stub>]
    [<Name "VEPlace">]
    type VEPlace = class end

    [<Stub>]
    [<Name "VELatLongRectangle">]
    type VELatLongRectangle = class end

    [<Stub>]
    [<Name "VEBirdseyeScene">]
    type VEBirdseyeScene = class end

    [<JavaScriptType>]
    type Location = class end

    [<Stub>]
    [<Name "VERouteOptions">]
    type VERouteOptions = class end
    
    [<Stub>]
    [<Name "VEImageryMetadata">]
    type VEImageryMetadata = class end
    
    [<Stub>]
    [<Name "VEImageryMetadataOptions">]
    type VEImageryMetadataOptions = class end
 
    [<Stub>]
    [<Name "VEModelStatusCode">]
    type VEModelStatusCode = class end

    [<Stub>]
    [<Name "VEModelSourceSpecification">]
    type VEModelSourceSpecification = class end

    [<Stub>]
    [<Name "VEOrientation">]
    type VEOrientation = class end

    [<Stub>]
    [<Name "VEModelOrientation">]
    type VEModelOrientation = class end

    [<Stub>]
    [<Name "VEModelScale">]
    type VEModelScale = class end

    [<Stub>]
    [<Name "VEShapeSourceSpecification">]
    type VEShapeSourceSpecification = class end

    [<Stub>]
    [<Name "VEPixel">]
    type VEPixel = class end
    
    [<Stub>]
    [<Name "VEMapViewSpecification">]
    type VEMapViewSpecification = class end
    
    [<Stub>]
    [<Name "VEMap">]
    type VEMap = 
        new(id: string) = {}
        /// Loads the specified map. All parameters are optional.
        ///
        /// * latLong: VELatLong Class object that represents the center of
        ///   the map. Optional.
        /// 
        /// * zoom: The zoom level to display. Valid values range from 1
        ///   through 19. Optional. Default is 4.
        /// 
        /// * style: A VEMapStyle Enumeration value specifying the map
        ///   style. Optional. Default is VEMapStyle.Road.
        /// 
        /// * fixed: A Boolean value that specifies whether the map view is
        ///   displayed as a fixed map that the user cannot
        ///   change. Optional. Default is false.
        /// 
        /// * mode: A VEMapMode Enumeration value that specifies whether to
        ///   load the map in 2D or 3D mode. Optional. Default is
        ///   VEMapMode.Mode2D.
        /// 
        /// * showSwitch: A Boolean value that specifies whether to show the
        ///   map mode switch on the dashboard control. Optional. Default is
        ///   true (the switch is displayed).
        /// 
        /// * tileBuffer: How much tile buffer to use when loading
        ///   map. Default is 0 (do not load an extra boundary of
        ///   tiles). This parameter is ignored in 3D mode.
        /// 
        /// * mapOptions: A VEMapOptions Class that specifies other map
        ///   options to set.
        member this.LoadMap(latLong: VELatLong,
                            zoom: int,
                            style: VEMapStyle,
                            fixed': bool,
                            mode: VEMapMode,
                            showSwitch: bool,
                            tileBuffer: int,
                            mapOptions: VEMapOptions) = Undefined

        member this.LoadMap(latLong: VELatLong,
                            zoom: int,
                            style: VEMapStyle,
                            fixed': bool,
                            mode: VEMapMode,
                            showSwitch: bool,
                            tileBuffer: int) = Undefined

        member this.LoadMap(latLong: VELatLong,
                            zoom: int,
                            style: VEMapStyle,
                            fixed': bool,
                            mode: VEMapMode,
                            showSwitch: bool) = Undefined

        member this.LoadMap(latLong: VELatLong,
                            zoom: int,
                            style: VEMapStyle,
                            fixed': bool,
                            mode: VEMapMode) = Undefined

        member this.LoadMap(latLong: VELatLong,
                            zoom: int,
                            style: VEMapStyle,
                            fixed': bool) = Undefined

        member this.LoadMap(latLong: VELatLong,
                            zoom: int,
                            style: VEMapStyle) = Undefined

        member this.LoadMap(latLong: VELatLong,
                            zoom: int) = Undefined

        member this.LoadMap(latLong: VELatLong) = Undefined

        member this.LoadMap() = Undefined

        /// Adds a custom control to the map.
        /// VEMap.AddControl(element, zIndex);
        /// * element: An HTML element that contains the control to be added
        /// * zIndex: The z-order for the control. Optional.
        member this.AddControl(element: Element, zIndex: int) : unit = Undefined

        member this.AddControl(element: Element) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/dd581633(v=MSDN.10).aspx
        /// * object: The object to add as a layer to the map DIV container.
        member this.AddCustomLayer(object': obj) : unit = Undefined

        /// Adds a VEShape Class object or array of VEShape pushpin objects to the base layer.
        /// VEMap.AddShape(shape);
        /// * Shape: The VEShape object or array of VEShape pushpin objects to be added. Required.
        /// The shape parameter can be a single pushpin, polyline, or
        /// polygon, or an array of pushpins. If the map is in 3D mode, the
        /// shapes are added one-by-one. If the map has been redrawn, shapes
        /// are added one-by-one. If a shape with the same internal
        /// identifier already exists in the base layer, this method throws
        /// an exception.
        member this.AddShape(shape: VEShape) : unit = Undefined
        member this.AddShape(shape: VEShape []) : unit = Undefined

        /// Adds the specified VEShapeLayer Class object to the map.
        /// VEMap.AddShapeLayer(Layer);
        /// Layer: The VEShapeLayer Class object to add.
        /// If the layer reference already exists on the map, an exception is
        /// thrown, and no new layer is created.
        member this.AddShapeLayer(layer: VEShapeLayer): unit = Undefined
        
        /// Adds a tile layer to the map, and if the visibleOnLoad parameter
        /// is true, it also shows it on the map.
        /// VEMap.AddTileLayer(layerSource, visibleOnLoad);
        /// * layerSource: The VETileSourceSpecification Class object
        ///   representing the source of the tile layer. Required.
        /// * visibleOnLoad: If true, the layer is immediately shown when
        ///   added to the map. Optional.
        /// http://msdn.microsoft.com/en-us/library/bb429629(v=MSDN.10).aspx
        member this.AddTileLayer(layerSource: VETileSourceSpecification, visibleOnLoad: bool): unit = Undefined
        member this.AddTileLayer(layerSource: VETileSourceSpecification): unit = Undefined

        /// Attaches a Map Control event to a specified function.
        /// VEMap.AttachEvent(event, function);
        /// * event: The name of the Map Control event that generates the
        ///   call.
        /// * function: The function to run when the event fires. It can be
        ///   either the name of a function or the function itself.
        /// http://msdn.microsoft.com/en-us/library/bb412496(v=MSDN.10).aspx
        member this.AttachEvent(event: string, f: obj -> bool): unit = Undefined

        /// Removes all shapes, shape layers, and search results on the map. Also removes the
        /// route from the map, if one is displayed.
        /// http://msdn.microsoft.com/en-us/library/bb429611(v=MSDN.10).aspx
        member this.Clear() : unit = Undefined

        /// Clears out all of the default Bing Maps info box CSS styles.
        /// http://msdn.microsoft.com/en-us/library/bb412441(v=MSDN.10).aspx
        /// VEMap.ClearInfoBoxStyles();
        member this.ClearInfoBoxStyles() : unit = Undefined

        /// Clears the traffic map.
        /// http://msdn.microsoft.com/en-us/library/bb964371(v=MSDN.10).aspx
        member this.ClearTraffic() : unit = Undefined

        /// Deletes all VEShapeLayer Class objects on the map as well as any
        /// VEShape Class objects within the layers.
        /// VEMap.DeleteAllShapeLayers();
        member this.DeleteAllShapeLayers() : unit = Undefined

        /// Deletes all of the VEShape Class objects from all layers.
        /// VEMap.DeleteAllShapes();
        member this.DeleteAllShapes() : unit = Undefined

        /// Removes the specified control from the map.
        /// VEMap.DeleteControl(element);
        /// * element: An HTML element that contains the control to be deleted
        member this.DeleteControl(element: Element) : unit = Undefined

        /// Clears the current route (VERoute Class object) from the map.
        /// VEMap.DeleteRoute();
        member this.DeleteRoute() : unit = Undefined

        /// Deletes a VEShape Class object from any layer, including the base map layer.
        /// VEMap.DeleteShape(shape);
        /// * shape: The VEShape Class object to be deleted. Required.
        member this.DeleteShape(shape: VEShape) : unit = Undefined

        /// Deletes the specified VEShapeLayer Class object from the map.
        /// VEMap.DeleteShapeLayer(layer);
        /// * layer: A VEShapeLayer Class object to delete. Required.
        member this.DeleteShapeLayer(layer: VEShapeLayer) : unit = Undefined

        /// Completely removes a tile layer from the map.
        /// http://msdn.microsoft.com/en-us/library/bb964371(v=MSDN.10).aspx
        /// VEMap.DeleteTileLayer(layerID);
        /// * layerID: The ID of the layer to be deleted.
        member this.DeleteTileLayer(layerID: string) : unit = Undefined

        /// Detaches the specified map control event so that it no longer calls the specified function.
        /// http://msdn.microsoft.com/en-us/library/bb412537(v=MSDN.10).aspx
        /// VEMap.DetachEvent(event, function);
        /// * event: The name of the map control event that generates the call.
        /// * function: The function that was specified to run when the event fired.
        member this.DetachEvent(event: string, func: obj) : unit = Undefined

        /// Deletes the VEMap object and releases any associated resources.
        /// VEMap.Dispose();
        member this.Dispose() : unit = Undefined

        /// Specifies whether shapes are drawn below a threshold zoom level.
        /// http://msdn.microsoft.com/en-us/library/bb412537(v=MSDN.10).aspx
        /// VEMap.EnableShapeDisplayThreshold(enable);
        /// * enable: A Boolean value specifying whether to draw shapes below a threshold zoom
        ///           level. By default shapes are not drawn be
        ///           low a threshold zoom level.
        member this.EnableShapeDisplayThreshold(enable: bool) : unit = Undefined

        /// Stops the continuous map panning initiated by a call to the
        /// VEMap.StartContinuousPan Method.
        /// VEMap.EndContinuousPan();
        member this.EndContinuousPan() : unit = Undefined

        /// Performs a what (business) search or a where (location)
        /// search. At least one of these two parameters is required.
        /// 
        /// VEMap.Find(what,
        ///            where,
        ///            findType,
        ///            shapeLayer,
        ///            startIndex,
        ///            numberOfResults,
        ///            showResults,
        ///            createResults,
        ///            useDefaultDisambiguation,
        ///            setBestMapView,
        ///            callback);
        /// 
        /// * what: The business name, category, or other item for which the
        ///   search is conducted. This parameter must be supplied for a
        ///   pushpin to be included in the results.
        /// 
        /// * where: The address or place name of the area for which the search is
        ///   conducted. This parameter is overloaded; see the Remarks section for more
        ///   information.
        /// 
        /// * findType: A VEFindType Enumeration value that specifies the type of search
        ///   performed. The only currently supported value is VEFindType.Businesses.
        /// 
        /// * shapeLayer: A reference to the VEShapeLayer Class object that contain the pins that
        ///   result from this search if a what parameter is specified. Optional. If the shape
        ///   layer is not specified, the pins are added to the base map layer. If the reference
        ///   is not a valid VEShapeLayer reference, an exception is thrown.
        /// 
        /// * startIndex: The beginning index of the results returned. Optional. Default is 0.
        /// 
        /// * numberOfResults: The number of results to be returned, starting at startIndex. The
        ///   default is 10, the minimum is 1, and the maximum is 20.
        /// 
        /// * showResults: A Boolean value that specifies whether the resulting pushpins are
        ///   visible. Optional. Default is true.
        /// 
        /// * createResults: A Boolean value that specifies whether pushpins are created when a
        ///   what parameter is supplied. Optional. If true, pushpins are created. If false, the
        ///   arguments sent to the function specified by the callback parameter are set as
        ///   follows: The array of VEFindResult Class objects is still present, but no pushpin
        ///   layer is created. Each VEFindResult object's corresponding Shape property is null.
        /// 
        /// * useDefaultDisambiguation: A Boolean value that specifies whether the map control
        ///   displays a disambiguation box when multiple location matches are possible. If true,
        ///   the map control displays a disambiguation box. Optional. The default is true. To
        ///   ensure no messages appear when useDefaultDisambiguation is false, set the
        ///   VEMap.ShowMessageBox Property to false.
        /// 
        /// * setBestMapView: A Boolean value that specifies whether the map control moves the
        ///   view to the first location match. If true, the map control moves the
        ///   view. Optional. Default is true.
        /// 
        /// * callback: The name of the function that the server calls with the search
        ///   results. If this parameter is not null and useDefaultDisambiguation is true, this
        ///   function is not called until the user has made a disambiguation choice. Optional.
        ///   
        /// The function defined by the callback parameter receives the following arguments, in
        /// the order shown, from the server:
        /// 
        /// * A VEShapeLayer Class object. If shapeLayer was supplied, it should be the same object.
        /// 
        /// * An array of VEFindResult Class objects.
        /// 
        /// * An array of VEPlace Class objects.
        /// 
        /// * A Boolean value indicating whether there are more results after the current set.
        /// 
        /// * A String containing a possible error message.

        member this.Find(what: string,
                         where: string,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool,
                         createResults: bool,
                         useDefaultDisambiguation: bool,
                         setBestMapView: bool,
                         callback: (VEFindResult [] * VEPlace [] * bool * string -> unit)) : unit = Undefined

        member this.Find(what: string,
                         where: string,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool,
                         createResults: bool,
                         useDefaultDisambiguation: bool,
                         setBestMapView: bool) : unit = Undefined

        member this.Find(what: string,
                         where: string,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool,
                         createResults: bool,
                         useDefaultDisambiguation: bool) : unit = Undefined

        member this.Find(what: string,
                         where: string,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool,
                         createResults: bool) : unit = Undefined

        member this.Find(what: string,
                         where: string,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool) : unit = Undefined

        member this.Find(what: string,
                         where: string,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int) : unit = Undefined

        member this.Find(what: string,
                         where: string,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int) : unit = Undefined

        member this.Find(what: string,
                         where: string,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer) : unit = Undefined

        member this.Find(what: string,
                         where: string,
                         findType: VEFindType) : unit = Undefined

        member this.Find(what: string,
                         where: string) : unit = Undefined

        member this.Find(what: string) : unit = Undefined

        member this.Find(what: string,
                         where: VEPlace,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool,
                         createResults: bool,
                         useDefaultDisambiguation: bool,
                         setBestMapView: bool,
                         callback: (VEFindResult [] * VEPlace [] * bool * string -> unit)) : unit = Undefined

        member this.Find(what: string,
                         where: VEPlace,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool,
                         createResults: bool,
                         useDefaultDisambiguation: bool,
                         setBestMapView: bool) : unit = Undefined

        member this.Find(what: string,
                         where: VEPlace,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool,
                         createResults: bool,
                         useDefaultDisambiguation: bool) : unit = Undefined

        member this.Find(what: string,
                         where: VEPlace,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool,
                         createResults: bool) : unit = Undefined

        member this.Find(what: string,
                         where: VEPlace,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool) : unit = Undefined

        member this.Find(what: string,
                         where: VEPlace,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int) : unit = Undefined

        member this.Find(what: string,
                         where: VEPlace,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int) : unit = Undefined

        member this.Find(what: string,
                         where: VEPlace,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer) : unit = Undefined

        member this.Find(what: string,
                         where: VEPlace,
                         findType: VEFindType) : unit = Undefined

        member this.Find(what: string,
                         where: VEPlace) : unit = Undefined

        member this.Find(what: string,
                         where: VELatLongRectangle,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool,
                         createResults: bool,
                         useDefaultDisambiguation: bool,
                         setBestMapView: bool,
                         callback: (VEFindResult [] * VEPlace [] * bool * string -> unit)) : unit = Undefined

        member this.Find(what: string,
                         where: VELatLongRectangle,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool,
                         createResults: bool,
                         useDefaultDisambiguation: bool,
                         setBestMapView: bool) : unit = Undefined

        member this.Find(what: string,
                         where: VELatLongRectangle,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool,
                         createResults: bool,
                         useDefaultDisambiguation: bool) : unit = Undefined

        member this.Find(what: string,
                         where: VELatLongRectangle,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool,
                         createResults: bool) : unit = Undefined

        member this.Find(what: string,
                         where: VELatLongRectangle,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int,
                         showResults: bool) : unit = Undefined

        member this.Find(what: string,
                         where: VELatLongRectangle,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int,
                         numberOfResults: int) : unit = Undefined

        member this.Find(what: string,
                         where: VELatLongRectangle,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer,
                         startIndex: int) : unit = Undefined

        member this.Find(what: string,
                         where: VELatLongRectangle,
                         findType: VEFindType,
                         shapeLayer: VEShapeLayer) : unit = Undefined

        member this.Find(what: string,
                         where: VELatLongRectangle,
                         findType: VEFindType) : unit = Undefined

        member this.Find(what: string,
                         where: VELatLongRectangle) : unit = Undefined

        /// In 3D mode, returns a double that represents the altitude (in meters) above the geoid.
        /// http://msdn.microsoft.com/en-us/library/bb429624(v=MSDN.10).aspx
        member this.GetAltitude() : float = Undefined

        /// Performs a search for locations that match a VELatLong input.
        /// VEMap.FindLocations(veLatLong, callback);
        /// * veLatLong: A VELatLong Class object that specifies what map location to match.
        /// * callback: The name of the function that the server calls when it returns search
        ///   results.
        /// This method does not return a value. The function defined by the callback parameter
        /// receives one argument from the server:
        /// * An array of VEPlace Class objects. If the search is unsuccessful, this argument is
        ///   null. Otherwise, these objects re present the possible location matches.
        member this.FindLocations(veLatLong: VELatLong, callback: (VEPlace [] -> unit)) : unit = Undefined

        /// If the map view is already set to bird's eye, returns the current VEBirdseyeScene
        /// Class object.
        /// VEMap.GetBirdseyeScene();
        /// Returns null if the map mode is set to 3D. Otherwise returns a VEBirdseyeScene Class
        /// object that represents the current bird's eye image.
        member this.GetBirdseyeScene() : VEBirdseyeScene = Undefined

        /// Returns a VELatLong Class object that represents the location of the center of the
        /// current map view.
        /// VEMap.GetCenter();
        /// Returns a VELatLong Class object.
        member this.GetCenter() : VELatLong = Undefined

        /// Draws a multi-point route on the map and sends details about the route to a callback function.
        /// VEMap.GetDirections(locations, options);
        /// * locations: An array of objects specifying the points through which the route must
        ///   pass. The points can be either VELatLong Class objects or String objects. A maximum
        ///   of 25 locations can be specified.
        /// * options: A VERouteOptions Class object specifying the routing options.
        member this.GetDirections(locations: Location [], options: VERouteOptions) = Undefined

        /// In 3D mode, returns a double that represents the compass heading of the current map view.
        /// http://msdn.microsoft.com/en-us/library/bb412459(v=MSDN.10).aspx
        member this.GetHeading() : float = Undefined
        
        /// Returns information about the requested imagery, including imagery date stamps. This
        /// method requires that a valid token has been set using the VEMap.SetClientToken
        /// Method.
        /// VEMap.GetImageryMetadata(callback, options);
        /// * callback: The name of the function to call when results are returned. The function
        ///   must accept a VEImageryMetadata Class object. Required.
        /// * options: A VEImageryMetadataOptions Class object specifying the imagery for which
        /// information is returned. Optional.
        member this.GetImageryMetadata(callback: (VEImageryMetadata -> unit),
                                       options: VEImageryMetadataOptions) : unit = Undefined
        member this.GetImageryMetadata(callback: (VEImageryMetadata -> unit)) : unit = Undefined

        /// Returns the pixel value of the left edge of the map control.
        /// http://msdn.microsoft.com/en-us/library/bb412534(v=MSDN.10).aspx
        member this.GetLeft() : int = Undefined

        /// Returns the current map mode.
        /// http://msdn.microsoft.com/en-us/library/bb412510(v=MSDN.10).aspx
        member this.GetMapMode() : VEMapMode = Undefined

        /// Returns the current map style.
        /// http://msdn.microsoft.com/en-us/library/bb412451(v=MSDN.10).aspx
        member this.GetMapStyle() : unit = Undefined

        /// Returns the current map view object as a VELatLongRectangle Class object.
        /// VEMap.GetMapView();
        member this.GetMapView() : VELatLongRectangle = Undefined

        /// In 3D mode, returns a double that represents the pitch of the current map view.
        /// http://msdn.microsoft.com/en-us/library/bb429577(v=MSDN.10).aspx
        member this.GetPitch() : float = Undefined

        /// Gets the reference to a VEShape Class object based on its internal identifier.
        /// VEMap.GetShapeByID(ID);
        /// * ID: The identifier of the shape to retrieve. Required.
        member this.GetShapeByID(ID: string) : VEShape = Undefined

        /// GetRoute == Deprecated

        /// Gets the reference to a VEShapeLayer Class object based on its index.
        /// VEMap.GetShapeLayerByIndex(index);
        /// * index: The index of the layer that you wish to retrieve. Required.
        member this.GetShapeLayerByIndex(index: int) : VEShapeLayer = Undefined

        /// Gets the total number of shape layers on the map.
        /// http://msdn.microsoft.com/en-us/library/bb429636(v=MSDN.10).aspx
        member this.GetShapeLayerCount() : int = Undefined
        
        /// Gets the number of tile layers.
        /// http://msdn.microsoft.com/en-us/library/bb877861(v=MSDN.10).aspx
        member this.GetTileLayerCount() : int = Undefined
        
        /// Gets a tile layer based upon its identifier.
        /// VEMap.GetTileLayerByID(id);
        /// * id: The unique identifier of the tile layer
        member this.GetTileLayerByID(id: string) : VETileSourceSpecification = Undefined

        /// Gets a tile layer based upon an index value.
        /// VEMap.GetTileLayerByIndex(index);
        /// http://msdn.microsoft.com/en-us/library/bb877836(v=MSDN.10).aspx
        /// * index: The index into the list of tile layers. The value ranges from 0 to
        ///          GetTileLayerCount
        member this.GetTileLayerByIndex(index: int) : VETileSourceSpecification = Undefined

        /// Returns the pixel value of the top edge of the map control.
        /// http://msdn.microsoft.com/en-us/library/bb412550(v=MSDN.10).aspx
        member this.GetTop() : int = Undefined

        /// Returns the current version of the map control.
        /// VEMap.GetVersion();
        static member GetVersion() : string = Undefined

        /// Returns the current zoom level of the map.
        /// http://msdn.microsoft.com/en-us/library/bb412455(v=MSDN.10).aspx
        member this.GetZoomLevel() : int = Undefined

        /// In 3D mode, hides the default user interface for controlling the map in 3D mode. By
        /// default, this control is shown.
        /// http://msdn.microsoft.com/en-us/library/bb412557(v=MSDN.10).aspx
        member this.Hide3DNavigationControl() : unit = Undefined

        /// Hides all of the VEShapeLayer Class objects on the map.
        /// VEMap.HideAllShapeLayers();
        member this.HideAllShapeLayers() : unit = Undefined

        /// Hides the base tile layer of the map.
        member this.HideBaseTileLayer() : unit = Undefined

        /// Hides the specified control from view.
        /// http://msdn.microsoft.com/en-us/library/bb544967(v=MSDN.10).aspx
        /// VEMap.HideControl(element);
        /// * element: An HTML element that contains the control to be hidden.
        member this.HideControl(element: Element) : unit = Undefined

        /// Hides the default user interface for controlling the map (the compass and the
        /// zoom control).
        /// http://msdn.microsoft.com/en-us/library/bb412416(v=MSDN.10).aspx
        member this.HideDashboard() : unit = Undefined

        /// Hides a shape's custom or default info box.
        /// http://msdn.microsoft.com/en-us/library/bb412559(v=MSDN.10).aspx
        member this.HideInfoBox() : unit = Undefined

        /// Hides the mini map from view.
        /// http://msdn.microsoft.com/en-us/library/bb412432(v=MSDN.10).aspx
        member this.HideMiniMap() : unit = Undefined

        /// Hides the scale bar from the map.
        /// http://msdn.microsoft.com/en-us/library/cc980906(v=MSDN.10).aspx
        member this.HideScalebar() : unit = Undefined

        /// Hides a tile layer from view.
        /// VEMap.HideTileLayer(layerID);
        /// * layerID: The ID of the layer to be hidden.
        member this.HideTileLayer(layerID: string) : unit = Undefined

        /// Hides the traffic legend.
        /// http://msdn.microsoft.com/en-us/library/bb964372(v=MSDN.10).aspx
        member this.HideTrafficLegend() : unit = Undefined

        /// Imports a model data file and displays a 3D model on the map.
        /// VEMap.Import3DModel(modelShapeSource, callback, latLong, orientation, scale);
        /// * modelShapeSource: A VEModelSourceSpecification Class object specifying the model
        ///   data to import.
        /// * callback: The name of the function that is called after the data has been
        ///   imported. See below for the arguments received by the callback.
        /// * latLong: A VELatLong Class object that specifies the point at which to place the
        ///   origin of the model.
        /// * orientation: A VEModelOrientation Class object that specifies the orientation of
        ///   the model on the map.
        /// * scale: A VEModelScale Class object that specifies the scale of the model.
        member this.Import3DModel(modelShapeSource: VEModelSourceSpecification,
                                  callback: (VEShape * VEModelStatusCode -> unit),
                                  latLong: VELatLong,
                                  orientation: VEModelOrientation,
                                  scale: VEModelScale) : unit = Undefined


        /// Imports data from a GeoRSS feed, Bing Maps (http://maps.live.com) collection, or KML URL.
        /// VEMap.ImportShapeLayerData(shapeSource, callback, setBestView);
        /// * shapeSource: A VEShapeSourceSpecification Class object specifying the imported
        ///   shape data.
        /// * callback: The function that is called after the data has been imported.
        /// * setBestView: A Boolean value that specifies whether the map view is changed to the
        ///   best view for the layer.
        member this.ImportShapeLayerData(shapeSource: VEShapeSourceSpecification,
                                         callback: VEShapeLayer -> unit,
                                         setBestView: bool) : unit   = Undefined

        /// Changes the map view so that it includes both the specified VELatLong Class point
        /// and the center point of the current map.
        /// VEMap.IncludePointInView(latlong);
        /// * latlong: A VELatLong Class object that specifies the latitude and longitude of the
        ///   point to include
        member this.IncludePointInView(latlong) : unit = Undefined

        /// Determines whether the bird's eye map style is available in the current map view.
        /// http://msdn.microsoft.com/en-us/library/bb429589(v=MSDN.10).aspx
        member this.IsBirdseyeAvailable() : bool = Undefined

        /// Converts VELatLong Class objects (latitude/longitude pair) to VEPixel Class objects.
        /// VEMap.LatLongToPixel(latLongArray, zoomLevel, callback);
        ///
        /// * latLongArray: A VELatLong object or an array of VELatLong objects. Required. If an
        ///   array is passed, the callback parameter must be specified.
        ///
        /// * zoomLevel: The zoom level at which the VELatLong objects are converted to VEPixel
        ///   objects. Optional. If this parameter is not supplied, the current zoom level is
        ///   used.
        ///
        /// * callback: The name of the function called with the array of corresponding VEPixel
        ///   objects. Optional.
        member this.LatLongToPixel(latLongArray: VELatLong [],
                                   zoomLevel: int, callback: VEPixel [] -> unit) : unit = Undefined
        member this.LatLongToPixel(latLongArray: VELatLong) : VEPixel = Undefined
        member this.LatLongToPixel(latLongArray: VELatLong,
                                   zoomLevel: int) : VEPixel = Undefined
        member this.LatLongToPixel(latLongArray: VELatLong,
                                   zoomLevel: int, callback: VEPixel []) : unit = Undefined

        /// Loads the traffic map.
        /// VEMap.LoadTraffic(showFlow);
        /// http://msdn.microsoft.com/en-us/library/bb877877(v=MSDN.10).aspx
        /// * showFlow: Whether to show the traffic flow
        member this.LoadTraffic(showFlow: bool) : unit = Undefined

        /// When in 2D mode, moves the map the specified amount.
        /// VEMap.Pan(deltaX, deltaY);
        /// * deltaX: The amount to move the map horizontally, in pixels
        /// * deltaY: The amount to move the map vertically, in pixels
        /// http://msdn.microsoft.com/en-us/library/bb412449(v=MSDN.10).aspx
        member this.Pan(deltaX: int, deltaY: int) : unit = Undefined

        /// Pans the map to a specific latitude and longitude.
        /// VEMap.PanToLatLong(VELatLong);
        /// * VELatLong: An object that represents the latitude and longitude of the point on which
        ///              to center the map
        /// http://msdn.microsoft.com/en-us/library/bb412466(v=MSDN.10).aspx
        member this.PanToLatLong(latlong: VELatLong) : unit = Undefined

        /// Converts a pixel (a point on the map) to a VELatLong Class object
        /// (latitude/longitude pair).
        /// VEMap.PixelToLatLong(pixel);
        /// * pixel: A VEPixel Class object representing a pixel location on the map.
        member this.PixelToLatLong(pixel: VEPixel): VELatLong = Undefined
        
        /// Removes a custom layer from the map.
        /// VEMap.RemoveCustomLayer(object);
        /// * object: The object to remove from the map DIV container.
        /// http://msdn.microsoft.com/en-us/library/dd581632(v=MSDN.10).aspx
        member this.RemoveCustomLayer(object': obj) : unit = Undefined

        /// Resizes the map based on the specified width and height.
        /// VEMap.Resize(width, height);
        /// * width: The width, in pixels, of the map. Optional.
        /// * height: The height, in pixels, of the map. Optional.
        /// http://msdn.microsoft.com/en-us/library/bb429585(v=MSDN.10).aspx
        /// If this method is called with no parameters, the map is resized to fit 
        /// the entire DIV element.
        member this.Resize(width: int, height: int) : unit = Undefined
        member this.Resize(width: int) : unit = Undefined
        member this.Resize() : unit = Undefined

        /// In 3D mode, sets the altitude, in meters, above the WGS 84 ellipsoid in the map view.
        /// VEMap.SetAltitude(altitude);
        /// * altitude: The altitude, in meters
        member this.SetAltitude(altitude: float) : unit = Undefined

        /// Changes the orientation of the existing bird's eye
        /// image (VEBirdseyeScene Class object) to the specified
        /// orientation.
        /// VEMap.SetBirdseyeOrientation(orientation);
        /// * orientation: You can set this value by using either the
        ///   VEOrientation Enumeration or a string value. Valid string
        ///   values are North, South, East, and West.
        member this.SetBirdseyeOrientation(orientation: VEOrientation) : unit = Undefined

        /// VEMap.SetBirdseyeScene
        /// Displays the specified bird's eye image.
        /// VEMap.SetBirdseyeScene(id)
        /// VEMap.SetBirdseyeScene(veLatLong, orientation, zoomLevel, callback)
        /// http://msdn.microsoft.com/en-us/library/bb412464(v=MSDN.10).aspx
        member this.SetBirdseyeScene(id: string) : unit = Undefined
        member this.SetBirdseyeScene(veLatLong: VELatLong,
                                     orientation: VEOrientation,
                                     zoomLevel: int,
                                     callback:VEBirdseyeScene -> unit) : unit = Undefined
        member this.SetBirdseyeScene(veLatLong: VELatLong,
                                     orientation: VEOrientation,
                                     zoomLevel: int) : unit = Undefined
        member this.SetBirdseyeScene(veLatLong: VELatLong,
                                     orientation: VEOrientation) : unit = Undefined
        member this.SetBirdseyeScene(veLatLong: VELatLong) : unit = Undefined

        /// Centers the map to a specific latitude and longitude.
        /// VEMap.SetCenter(VELatLong);
        /// VELatLong Class: An object that contains the latitude and longitude of the point on
        ///                  which to center the map.
        /// http://msdn.microsoft.com/en-us/library/bb429618(v=MSDN.10).aspx
        member this.SetCenter(latlong: VELatLong) : unit = Undefined

        /// Centers the map to a specific latitude and longitude and sets the zoom level.
        /// VEMap.SetCenterAndZoom(VELatLong, zoomLevel);
        /// VELatLong Class: An object that contains the latitude and longitude of the point on
        ///                  which to center the map,
        /// * zoomLevel: The zoom level for the map.
        /// http://msdn.microsoft.com/en-us/library/bb412439(v=MSDN.10).aspx
        member this.SetCenterAndZoom(latlong: VELatLong, zoomLevel: int) : unit = Undefined

        /// Sets a Bing Maps token for the VEMap object.
        /// VEMap.SetClientToken(clientToken);
        /// * clientToken: A string representing the Bing Maps token.
        member this.SetClientToken(clientToken: string) : unit = Undefined

        /// Sets the credentials to use to authenticate map service requests.
        /// * credentials: A string that is the Bing Maps Key to set.
        member this.SetCredentials(credentials: string) : unit = Undefined

        /// Sets the map dashboard size and type.
        /// VEMap.SetDashboardSize(dashboard);
        /// * dashboardSize: A VEDashboardSize Enumeration representing the dashboard size.
        /// http://msdn.microsoft.com/en-us/library/bb412545(v=MSDN.10).aspx
        member this.SetDashboardSize(dashboard: VEDashboardSize) : unit = Undefined

        /// Sets the info box CSS styles back to their original classes.
        /// VEMap.SetDefaultInfoBoxStyles();
        /// http://msdn.microsoft.com/en-us/library/bb429537(v=MSDN.10).aspx
        member this.SetDefaultInfoBoxStyles() : unit = Undefined

        /// Specifies what the map control does when a request to the server to get the accurate
        /// position of a shape when the map style is changed to birdseye fails.
        /// VEMap.SetFailedShapeRequest(policy);
        /// * policy: A VEFailedShapeRequest Enumeration value that defines the policy.
        /// http://msdn.microsoft.com/en-us/library/bb877846(v=MSDN.10).aspx
        member this.SetFailedShapeRequest(policy: VEFailedShapeRequest) : unit = Undefined

        /// In 3D mode, sets the compass heading of the current map view.
        /// VEMap.SetHeading(heading);
        /// * heading: The compass direction, expressed as a double. A value
        ///   of 0 is true north, and a value of 180 is true south. Values
        ///   less than 0 and greater than 360 are valid and are calculated
        ///   as compass directions.
        member this.SetHeading(heading: float) : unit = Undefined

        /// Sets the mode of the map.
        /// VEMap.SetMapMode(mode);
        /// * mode: A VEMapMode Enumeration value that specifies whether to load the map in 2D or 3D mode
        /// http://msdn.microsoft.com/en-us/library/bb429545(v=MSDN.10).aspx
        member this.SetMapMode(mode: VEMapMode) : unit = Undefined

        /// Sets the style of the map.
        /// VEMap.SetMapStyle(mapStyle);
        /// * mapStyle: A VEMapStyle Enumeration value specifying the map style.
        /// http://msdn.microsoft.com/en-us/library/bb412454(v=MSDN.10).aspx
        member this.SetMapStyle(mapStyle: VEMapStyle) : unit = Undefined

        /// Sets the map view to include all of the points, lines, or
        /// polygons specified in the provided array, or to the view defined
        /// by a VEMapViewSpecification Class object.
        /// VEMap.SetMapView(object);
        /// * object: In 2D mode, an array of VELatLong Class points or a
        ///   VELatLongRectangle Class object. In 3D mode, can also be a
        ///   VEMapViewSpecification Class object. This object defines the
        ///   location, altitude, pitch, and heading to use for the map view.
        member this.SetMapView(o: VELatLong []) : unit = Undefined
        member this.SetMapView(o: VELatLongRectangle) : unit = Undefined
        member this.SetMapView(o: VEMapViewSpecification) : unit = Undefined

        /// Specifies whether to zoom to the center of the screen or to the cursor position on
        /// the screen.
        /// VEMap.SetMouseWheelZoomToCenter(zoomToCenter);
        /// 
        /// * zoomToCenter: A Boolean value specifying whether to zoom to the center of the screen
        ///               or to the cursor position. If true:, the map control zooms to the
        ///               center of the screen; if false: , the map control zooms to the cursor
        ///               position on the screen.
        /// 
        /// http://msdn.microsoft.com/en-us/library/bb877824(v=MSDN.10).aspx
        member this.SetMouseWheelZoomToCenter(zoomToCenter: bool) : unit = Undefined

        /// In 3D mode, sets the pitch of the current map view.
        /// VEMap.SetPitch(pitch);
        /// * pitch: The pitch direction, expressed as a double. A value of
        ///          0:  is level and a value of
        ///         -90:  is straight down. Values less than
        ///         -90:  or greater than
        ///         0:  are ignored, and the pitch is set to -90: .
        /// http://msdn.microsoft.com/en-us/library/bb412483(v=MSDN.10).aspx
        member this.SetPitch(pitch: float) : unit = Undefined

        /// This method controls how the map is printed.
        /// VEMap.SetPrintOptions(printOptions);
        /// * printOptions: A VEPrintOptions Class specifying the print options to set.
        /// http://msdn.microsoft.com/en-us/library/cc469977(v=MSDN.10).aspx
        member this.SetPrintOptions(printOptions: VEPrintOptions) : unit = Undefined

        /// Sets the distance unit (kilometers or miles) for the map scale.
        /// VEMap.SetScaleBarDistanceUnit(distanceUnit);
        /// * distanceUnit: A VEDistanceUnit Enumeration value that specifies either miles or
        ///                 kilometers
        /// http://msdn.microsoft.com/en-us/library/bb412528(v=MSDN.10).aspx
        member this.SetScaleBarDistanceUnit(distanceUnit: VEDistanceUnit) : unit = Undefined

        /// Specifies the accuracy in converting shapes when the map style is changed to birdseye.
        /// VEMap.SetShapesAccuracy(policy);
        /// * policy: The VEShapeAccuracy Enumeration value specifying the accuracy in converting shapes.
        /// http://msdn.microsoft.com/en-us/library/bb877873(v=MSDN.10).aspx
        member this.SetShapesAccuracy(policy: VEShapeAccuracy) : unit = Undefined

        /// Specifies the maximum number of shapes that are accurately converted at one time when
        /// the map style is changed to birdseye.
        /// VEMap.SetShapesAccuracyRequestLimit(max);
        /// * max: The maximum number of shapes that are accurately converted.
        /// http://msdn.microsoft.com/en-us/library/bb877840(v=MSDN.10).aspx
        member this.SetShapesAccuracyRequestLimit(max: int) : unit = Undefined

        /// Sets the number of "rings" of map tiles that should be loaded outside of the visible
        /// mapview area. This is also called tile overfetching.
        /// VEMap.SetTileBuffer(numRings);
        /// * numRings: An integer value greater than or equal to
        ///             0: that indicates the number of rings of additional tiles that should be
        ///                loaded. The default is
        ///             0: , and the maximum is
        /// http://msdn.microsoft.com/en-us/library/bb412434(v=MSDN.10).aspx
        member this.SetTileBuffer(numRings: int) : unit = Undefined

        /// Specifies the text shown with the traffic legend, if visible.
        /// VEMap.SetTrafficLegendText(text)
        /// * text: A string specifying the text shown with the traffic legend.
        /// http://msdn.microsoft.com/en-us/library/bb964366(v=MSDN.10).aspx
        member this.SetTrafficLegendText(text: string) : unit = Undefined

        /// Sets the view of the map to the specified zoom level.
        /// VEMap.SetZoomLevel(zoomLevel);
        /// * zoomLevel: The zoom level for the map. Valid values range from 1 through 19
        /// http://msdn.microsoft.com/en-us/library/bb429640(v=MSDN.10).aspx
        member this.SetZoomLevel(zoomLevel: int) : unit = Undefined

        /// Controls whether or not to show the Birdseye and BirdseyeHybrid
        /// map styles when the map mode is set to VEMapMode.Mode3D.
        /// VEMap.Show3DBirdseye(showBirdseye);
        /// * showBirdseye: A Boolean value that specifies whether or not to
        ///   show the Birdseye or BirdseyeHybrid map styles when the map
        ///   mode is set to VEMapMode.Mode3D. The default is false.
        member this.Show3DBirdseye(showBirdseye: bool): unit = Undefined

        /// VEMap.Show3DNavigationControl
        /// In 3D mode, shows the default user interface for controlling the map in 3D mode. By
        /// default, this control is shown.
        /// http://msdn.microsoft.com/en-us/library/bb412494(v=MSDN.10).aspx
        member this.Show3DNavigationControl() : unit = Undefined

        /// Shows all of the VEShapeLayer Class objects on the map.
        /// VEMap.ShowAllShapeLayers();
        member this.ShowAllShapeLayers() : unit = Undefined

        /// VEMap.ShowBaseTileLayer
        /// Shows the base tile layer of the map.
        member this.ShowBaseTileLayer() : unit = Undefined

        /// Makes the specified control visible. This method only affects
        /// control elements that have been hidden from view using the
        /// VEMap.ShowControl(element);
        /// * element: An HTML element that contains the control to be shown.
        member this.ShowControl(element: Element) : unit = Undefined

        /// Shows the default user interface for controlling the map (the compass-and-zoom
        /// control). By default, this control is shown.
        /// http://msdn.microsoft.com/en-us/library/bb412477(v=MSDN.10).aspx
        member this.ShowDashboard() : unit = Undefined

        /// Specifies whether the default disambiguation dialog is displayed
        /// when multiple results are returned from a location query using
        /// the VEMap.GetDirections Method.
        /// VEMap.ShowDisambiguationDialog(showDialog);
        /// * showDialog: A Boolean value. True enables the disambiguation dialog.
        member this.ShowDisambiguationDialog(showDialog: bool) : unit = Undefined

        /// Shows an information box for the shape.
        /// VEMap.ShowInfoBox(shape, anchor, offset);
        /// * shape: The reference to the shape for which an info box is to
        ///   be shown. Required.
        /// * anchor: The anchor point where the info box is docked when
        ///   displayed. This can either be a VELatLong Class object or a
        ///   VEPixel Class object. This value must be a valid point on the
        ///   current map. Optional.
        /// * offset: If the anchor point is a VELatLong object, this
        ///   parameter, a VEPixel object, specifies the anchor point's
        ///   offset from that latlong position. Optional.
        member this.ShowInfoBox(shape: VEShape,
                                anchor: VELatLong,
                                offset: VEPixel) : unit = Undefined
        member this.ShowInfoBox(shape: VEShape,
                                anchor: VELatLong) : unit = Undefined
        member this.ShowInfoBox(shape: VEShape) : unit = Undefined
        member this.ShowInfoBox(shape: VEShape,
                                anchor: VEPixel) : unit = Undefined

        /// Displays the specified message in a dialog box on the map.
        /// VEMap.ShowMessage(message);
        /// * message: The message you want to display on the map
        /// http://msdn.microsoft.com/en-us/library/bb429573(v=MSDN.10).aspx
        member this.ShowMessage(message) : unit = Undefined

        /// Displays the mini map at the specified offset from the top left corner of the screen.
        /// VEMap.ShowMiniMap(xoffset, yoffset, size);
        /// * xoffset: The x coordinate offset as a number of pixels from the top left corner
        ///            of the screen. Optional
        /// * yoffset: The y coordinate offset as a number of pixels from the top left corner
        ///            of the screen. Optional
        /// * size: A VEMiniMapSize Enumeration value that specifies the mini map
        ///         size. Optional.
        /// http://msdn.microsoft.com/en-us/library/bb412549(v=MSDN.10).aspx
        member this.ShowMiniMap(xoffset: int, yoffset: int, size: VEMiniMapSize) : unit = Undefined
        member this.ShowMiniMap(xoffset: int, yoffset: int) : unit = Undefined
        member this.ShowMiniMap(xoffset: int) : unit = Undefined
        member this.ShowMiniMap() : unit = Undefined

        /// Displays the scale bar on the map.
        /// http://msdn.microsoft.com/en-us/library/cc966838(v=MSDN.10).aspx
        member this.ShowScalebar() : unit = Undefined

        /// Shows a tile layer on the map.
        /// VEMap.ShowTileLayer(layerID);
        /// * layerID: The ID of the layer to be shown.
        /// http://msdn.microsoft.com/en-us/library/bb412462(v=MSDN.10).aspx
        member this.ShowTileLayer(layerID: string) : unit = Undefined

        /// Displays the traffic legend.
        /// VEMap.ShowTrafficLegend(x, y)
        /// * x: The x-coordinate of the top-left corner of the legend. Optional.
        /// * y: The y-coordinate of the top-left corner of the legend. Optional.
        /// http://msdn.microsoft.com/en-us/library/bb964370(v=MSDN.10) : unit.aspx
        member this.ShowTrafficLegend(x: int, y: int) = Undefined
        member this.ShowTrafficLegend(x: int) = Undefined
        member this.ShowTrafficLegend() = Undefined

        /// Moves the map in the specified direction until the
        /// VEMap.EndContinuousPan Method is called.
        /// VEMap.StartContinuousPan(x, y);
        /// * x: The speed, as a percentage of the fastest speed, to move the
        ///   map in the x direction. Positive numbers move the map to the
        ///   right, while negative numbers move the map to the left
        /// * y: The speed, as a percentage of the fastest speed, to move the
        ///   map in the y direction. Positive numbers move the map down,
        ///   while negative numbers move the map up
        member this.StartContinuousPan(x: float, y: float) : unit = Undefined

        /// Increases the map zoom level by 1.
        /// http://msdn.microsoft.com/en-us/library/bb412525(v=MSDN.10).aspx
        member this.ZoomIn() : unit = Undefined

        /// Decreases the map zoom level by 1.
        /// http://msdn.microsoft.com/en-us/library/bb429543(v=MSDN.10).aspx
        member this.ZoomOut() : unit = Undefined


