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
    
    [<JavaScriptType>]
    type VEAltitudeMode =

        | [<Inline "VEAltitudeMode.Default">]
          /// The altitude is meters above ground level
          Default

        | [<Inline "VEAltitudeMode.Absolute">]
          /// The altitude is meters above the WGS 84 ellipsoid
          Absolute

        | [<Inline "VEAltitudeMode.RelativeToGround">]
          /// The altitude is meters above ground level
          RelativeToGround

    [<Stub>]
    [<Name "VELatLong">]
    type VELatLong =
        
        /// Specifies the altitude of a single point on the globe.
        [<DefaultValue>]
        val mutable Altitude : float

        /// Specifies the mode in which an altitude is represented.
        [<DefaultValue>]
        val mutable AltitudeMode : VEAltitudeMode

        /// Specifies the latitude of a single point on the globe.
        [<DefaultValue>]
        val mutable Latitude : float

        /// Specifies the longitude of a single point on the globe.
        [<DefaultValue>]
        val mutable Longitude : float

        new (lat: float, long: float) = {}
        /// Specifies the altitude for a point on the globe.
        /// VELatLong.SetAltitude(altitude, mode)
        /// * altitude: The altitude, in meters
        /// * mode: The VEAltitudeMode Enumeration value that defines whether altitude is
        ///         relative to ground-level or absolute
        /// http://msdn.microsoft.com/en-us/library/bb877811(v=MSDN.10).aspx
        member this.SetAltitude(altitude: float, mode: VEAltitudeMode) : unit = Undefined
        
    [<JavaScriptType>]
    type VEMapStyle =
        | [<Inline "VEMapStyle.Road">] 
          /// The road map style
          Road

        | [<Inline "VEMapStyle.Shaded">]
          /// The shaded map style, which is a road map with shaded contours.
          Shaded

        | [<Inline "VEMapStyle.Aerial">]
          /// The aerial map style
          Aerial

        | [<Inline "VEMapStyle.Hybrid">]
          /// The hybrid map style, which is an aerial map with a label overlay
          Hybrid

        | [<Inline "VEMapStyle.Oblique">]
          /// The oblique map style, which is the same as Birdseye
          Oblique

        | [<Inline "VEMapStyle.Birdseye">]
          /// The bird's eye (oblique-angle) imagery map style
          Birdseye

        | [<Inline "VEMapStyle.BirdseyeHybrid">]
          /// The bird's eye hybrid map style, which is a bird's eye map with a label overlay
          BirdseyeHybrid

    [<JavaScriptType>]
    type VEMapMode =
        | [<Inline "VEMapMode.Mode2D" >]
            /// Displays the map in the traditional two dimensions
            Mode2D

        | [<Inline "VEMapMode.Mode3D" >]
            /// Loads the Bing Maps 3D control, displays the map in three dimensions, and displays the 3D navigation control
            Mode3D
    
    [<Stub>]
    [<Name "VEOrientation">]
    type VEOrientation = 
        | [<Inline "VEOrientation.North">]
          /// The image was taken looking toward the north.
          North

        | [<Inline "VEOrientation.South">]
          /// The image was taken looking toward the south.
          South

        | [<Inline "VEOrientation.East">]
          /// The image was taken looking toward the east.
          East

        | [<Inline "VEOrientation.West">]
          /// The image was taken looking toward the west.
          West

    [<Stub>]
    [<Name "VEMapOptions">]
    type VEMapOptions =
        new () = {} 
        /// A VEOrientation Enumeration value indicating the orientation of the bird's eye
        /// map. The default value is VEOrientation.North.
        [<DefaultValue>]
        val mutable BirdseyeOrientation : VEOrientation

        /// A Boolean value specifying whether or not to enable the Birdseye map style in the map
        /// control. The default value is true.
        [<DefaultValue>]
        val mutable EnableBirdseye : bool

        /// A Boolean value that specifies whether or not labels appear on the map when a user
        /// clicks the Aerial or Birdseye map style buttons on the map control dashboard. The
        /// default value is true.
        [<DefaultValue>]
        val mutable EnableDashboardLabels : bool

        /// A Boolean value indicating whether or not to load the base map tiles. The default
        /// value is true.
        [<DefaultValue>]
        val mutable LoadBaseTiles : bool

    [<Stub>]
    [<Name "VELatLongRectangle">]
    type VELatLongRectangle = 

        new (topLeftLatLong : VELatLong,
             bottomRightLatLong : VELatLong,
             topRightLatLong : VELatLong,
             bottomLeftLatLong : VELatLong) = {}

        /// A VELatLong Class object that specifies the latitude and longitude of the upper-left
        /// corner of the map view.
        [<DefaultValue>]
        val mutable TopLeftLatLong : VELatLong

        /// A VELatLong Class object that specifies the latitude and longitude of the lower-right
        /// corner of the map view.
        [<DefaultValue>]
        val mutable BottomRightLatLong : VELatLong

        /// If the map is in 3D mode, a VELatLong Class object that specifies the latitude and
        /// longitude of the upper-right corner of the map view.
        [<DefaultValue>]
        val mutable TopRightLatLong : VELatLong

        /// If the map is in 3D mode, a VELatLong Class object that specifies the latitude and
        /// longitude of the lower-left corner of the map
        [<DefaultValue>]
        val mutable BottomLeftLatLong : VELatLong

    [<Stub>]
    [<Name "VETileContext">]
    type VETileContext = 
        [<DefaultValue>]
        [<Name "x">]
        val mutable X : VELatLong
        
        [<DefaultValue>]
        [<Name "y">]
        val mutable Y : VELatLong

    [<Stub>]
    [<Name "VETileSourceSpecification">]
    type VETileSourceSpecification = 
        
        new (tileSourceId: string,
             tileSource: string,
             numServers: int,
             bounds: VELatLongRectangle [],
             minZoom: int,
             maxZoom: int,
             getTilePath: VETileContext -> string,
             opacity: float,
             zindex: int) = {}

        new (tileSourceId: string,
             tileSource: string,
             numServers: int,
             bounds: VELatLongRectangle [],
             minZoom: int,
             maxZoom: int,
             getTilePath: VETileContext -> string ,
             opacity: float) = {}

        new (tileSourceId: string,
             tileSource: string,
             numServers: int,
             bounds: VELatLongRectangle [],
             minZoom: int,
             maxZoom: int,
             getTilePath: VETileContext -> string) = {}

        new (tileSourceId: string,
             tileSource: string,
             numServers: int,
             bounds: VELatLongRectangle [],
             minZoom: int,
             maxZoom: int) = {}

        new (tileSourceId: string,
             tileSource: string,
             numServers: int,
             bounds: VELatLongRectangle [],
             minZoom: int) = {}

        new (tileSourceId: string,
             tileSource: string,
             numServers: int,
             bounds: VELatLongRectangle []) = {}

        new (tileSourceId: string,
             tileSource: string,
             numServers: int) = {}

        new (tileSourceId: string,
             tileSource: string) = {}

        /// An array of VELatLongRectangle Class objects that specifies the approximate coverage
        /// area of the layer.
        [<DefaultValue>]
        val mutable Bounds : VELatLongRectangle []

        /// The unique identifier for the layer. Each tile layer on a map must have a unique ID.
        [<DefaultValue>]
        val mutable ID : string

        /// The maximum zoom level at which to display the custom tile source.
        [<DefaultValue>]
        val mutable MaxZoomLevel : int

        /// The minimum zoom level at which to display the custom tile source.
        [<DefaultValue>]
        val mutable MinZoomLevel : int

        /// The number of servers on which the tiles are hosted.
        [<DefaultValue>]
        val mutable NumServers : int

        /// Specifies the opacity level of the tiles when displayed on the map.
        [<DefaultValue>]
        val mutable Opacity : float

        /// The location of the tiles.
        [<DefaultValue>]
        val mutable TileSource : string

        /// Specifies the z-index for the tiles.
        [<DefaultValue>]
        val mutable ZIndex : int

    [<JavaScriptType>]
    type VEDashboardSize = 
        | [<Inline "VEDashboardSize.Normal">]
          /// This is the dashboard that is used by default.
          Normal
        | [<Inline "VEDashboardSize.Small">]
          /// This is a dashboard smaller than the default: it only
          /// contains zoom-out (+) and zoom-in (-) buttons and road, aerial,
          /// and hybrid buttons for changing the map style.
          Small
        | [<Inline "VEDashboardSize.Tiny">]
          /// This is the smallest dashboard option available. This dashboard
          /// only contains zoom-out (+) and zoom-in (-) buttons.
          Tiny

    [<JavaScriptTypeAttribute>]
    type VEFailedShapeRequest = 
        | [<Inline "VEFailedShapeRequest.DoNotDraw">]
        /// Do not draw the shape
        DoNotDraw

        | [<Inline "VEFailedShapeRequest.DrawInaccurately">]
        /// Draw the shape inaccurately
        DrawInaccurately

        | [<Inline "VEFailedShapeRequest.QueueRequest">]
        /// Resubmit the drawing request
        QueueRequest

    [<Stub>]
    [<Name "VEPrintOptions">]
    type VEPrintOptions = 
        new () = {}
        /// A Boolean value specifying whether or not to make the map printable.
        [<DefaultValue>]
        val mutable EnablePrinting : bool

    [<Stub>]
    [<Name "VEFindType">]
    type VEFindType = 
        | /// Performs a business search.
          [<Inline "VEFindType.Businesses" >]
          Businesses
    
    [<Stub>]
    [<Name "VEShapeType">]
    type VEShapeType = 
        | [<Inline "VEShapeType.Pushpin">]
          /// This represents a Shape object that is a pushpin.
          Pushpin
        | [<Inline "VEShapeType.Polyline">]
          /// This represents a Shape object that is a polyline.
          Polyline
        | [<Inline "VEShapeType.Polygon">]
          /// This represents a Shape object that is a polygon.
          Polygon

    [<Stub>]
    [<Name "VEColor">]
    type VEColor = 
        new (r: int, g: int, b: int, a: int) = {}    

        /// Specifies the red component value. Valid values range from 0 through 255.
        [<DefaultValue>]
        val mutable R : int

        /// Specifies the green component value. Valid values range from 0 through 255.
        [<DefaultValue>]
        val mutable G : int

        /// Specifies the blue component value. Valid values range from 0 through 255.
        [<DefaultValue>]
        val mutable B : int

        /// Specifies the alpha (transparency) component value. Valid values range from 0.0 through 1.0.
        [<DefaultValue>]
        val mutable A : int
    
    [<Stub>]
    [<Name "VEPixel">]
    type VEPixel = 
        new (x: int, y: int) = {}
        [<DefaultValue>]
        val mutable x : int
        [<DefaultValue>]
        val mutable y : int

    [<Stub>]
    [<Name "VECustomIconSpecification">]
    type VECustomIconSpecification = 
        new () = {}
        /// A VEColor object representing the icon's background and transparency.
        [<DefaultValue>]
        val mutable BackColor : VEColor

        /// Custom HTML representing the pin's appearance. When specified, this HTML represents
        /// the pin's icon for 2D views only. String
        [<DefaultValue>]
        val mutable CustomHTML : string

        /// A VEColor object representing the icon's text color and transparency.
        [<DefaultValue>]
        val mutable ForeColor : VEColor

        /// A String representing the URL of an image file.
        [<DefaultValue>]
        val mutable Image : string

        /// A VEPixel object representing the image's offset from the icon's anchor.
        [<DefaultValue>]
        val mutable ImageOffset : VEPixel

        /// Specifies whether the text for the icon should be bold. Boolean.
        [<DefaultValue>]
        val mutable TextBold : bool

        /// The actual text to display for the icon. String.
        [<DefaultValue>]
        val mutable TextContent : string

        /// A String containing the name of the font to use for the icon text.
        [<DefaultValue>]
        val mutable TextFont : string

        /// Specifies whether the text for the icon should be italic. Boolean.
        [<DefaultValue>]
        val mutable TextItalics : bool

        /// A VEPixel object representing the amount to offset text from the top left corner.
        [<DefaultValue>]
        val mutable TextOffset : VEPixel

        /// Specifies the size at which to display text, in points. Integer.
        [<DefaultValue>]
        val mutable TextSize : int

        /// Specifies whether the text for the icon should be underlined. Boolean.
        [<DefaultValue>]
        val mutable TextUnderline : bool

    [<JavaScriptTypeAttribute>]
    type VEClusteringType = 
        | [<Inline "VEClusteringType.None">]
          /// No pushpin clustering
          None

        | [<Inline "VEClusteringType.Grid">]
          /// A simple clustering algorithm
          Grid
    
    [<Stub>]
    [<Name "VEClusteringOptions">]
    type VEClusteringOptions = 
        new () = {}
        /// A VECustomIconSpecification Class which describes the icon representing the pushpin cluster.
        [<DefaultValue>]        
        val mutable Icon : VECustomIconSpecification

        /// The name of the function called when clustering changes.
        [<DefaultValue>]        
        val mutable Callback : VEClusterSpecification [] -> unit

    and 
        [<Stub>]
        [<Name "VEClusterSpecification">]
        VEClusterSpecification = 
        /// Returns a VEShape Class that represents the pushpin cluster.
        member this.GetClusterShape() : VEShape = Undefined
        
        /// An array of VEShape Class items representing the pushpins in a pushpin cluster.
        val mutable Shapes : VEShape []

        /// A VELatLong Class object indicating the center of the pushpin cluster.
        val mutable LatLong : VELatLong
    and
        [<Stub>]
        [<Name "VEShapeLayer">]
        VEShapeLayer = 
        new () = {}
        /// http://msdn.microsoft.com/en-us/library/bb412548(v=MSDN.10).aspx
        /// Adds an object or array of Shape The VEShape object or array of VEShape objects to be
        /// added. Required.
        member this.AddShape(shape: VEShape) : unit = Undefined
        member this.AddShape(shape: VEShape []) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412473(v=MSDN.10).aspx
        /// Deletes all  objects (pushpins, polylines, and polygons) from the layer.
        member this.DeleteAllShapes() : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429601(v=MSDN.10).aspx
        /// Deletes a  object from the current layer.shape: A reference to the
        /// VEShape:  object to delete. Required.
        member this.DeleteShape(shape: VEShape) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429642(v=MSDN.10).aspx
        /// Returns a best-fit  object based on the shapes currently present in the layer.
        member this.GetBoundingRectangle() : VELatLongRectangle = Undefined

        /// http://msdn.microsoft.com/en-us/library/cc980821(v=MSDN.10).aspx
        /// Returns an array of  objects representing the calculated pushpin clusters.
        /// type: A VEClusteringType Enumeration: specifying the algorithm used to determine
        /// which pushpins to cluster.
        member this.GetClusteredShapes(ctype: VEClusteringType) : VEClusterSpecification [] = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412533(v=MSDN.10).aspx
        /// Gets the description of the  object.
        member this.GetDescription() : string = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412485(v=MSDN.10).aspx
        /// Retrieves a reference to a  object contained in this layer based on the specified ID.
        /// ID: The identifier of the VEShape object. Required.
        member this.GetShapeByID(id: string) : VEShape = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412540(v=MSDN.10).aspx
        /// Retrieves a reference to a  object contained in this layer based on the specified index.
        /// index: The index of the shape to retrieve. Required.
        member this.GetShapeByIndex(idx: int) : VEShape = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429622(v=MSDN.10).aspx
        /// VEShapeLayer.GetShapeCount
        /// Returns the total number of shapes in the current layer.
        member this.GetShapeCount() : int = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429635(v=MSDN.10).aspx
        /// Gets the title of the  object.
        member this.GetTitle() : string = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb877848(v=MSDN.10).aspx
        /// Returns whether the layer is visible.
        member this.IsVisible() : bool = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412443(v=MSDN.10).aspx
        /// Hides the layer from view on the map.
        member this.Hide() : unit = Undefined

        /// Sets the method for determining which pushpins are clustered as well as how the
        /// cluster is displayed.
        member this.SetClusteringConfiguration(type': VEClusteringType,
                                               options: VEClusteringOptions) : unit = Undefined
        member this.SetClusteringConfiguration(type': VEClusteringType) : unit = Undefined
        member this.SetClusteringConfiguration(algorithm: VEShapeLayer -> VEClusterSpecification [],
                                               options: VEClusteringOptions) : unit = Undefined
        member this.SetClusteringConfiguration(algorithm: VEShapeLayer -> VEClusterSpecification []) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429569(v=MSDN.10).aspx
        /// Sets the description of the  object.
        /// * details: A String: object containing either plain text or HTML that represents the
        ///            VEShapeLayer object's description field.
        member this.SetDescription(desc: string) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429542(v=MSDN.10).aspx
        /// Sets the title of the  object.
        /// title: A String object containing either plain text or HTML that represents the
        ///        VEShapeLayer object's title.
        member this.SetTitle(title: string) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412538(v=MSDN.10).aspx
        /// Shows the layer on the map.
        member this.Show() : unit = Undefined
    and 
        [<Stub>]
        [<Name "VEShape">]
        VEShape =
        /// * type: A VEShapeType Enumeration value of type that represents
        ///   the type of shape. Required.
        /// 
        /// * points: If the shape is a pushpin, this parameter can either be
        ///   a single VELatLong Class object or an array of VELatLong
        ///   objects. If it is an array of VELatLong objects, only the first
        ///   VELatLong object is used to define the pushpin's location. Any
        ///   additional data members are ignored. If the shape is a polyline
        ///   or polygon, it must be an array of VELatLong objects,
        ///   containing at least two points for a polyline and at least
        ///   three points for a polygon. Required.
        new (type': VEShapeType,
             points: VELatLong []) = {}
        /// http://msdn.microsoft.com/en-us/library/bb877821(v=MSDN.10).aspx
        /// Returns the altitude for the shape.
        member this.GetAltitude() : float = Undefined

        /// Gets the mode in which the shape's altitude is represented.
        member this.GetAltitudeMode() : VEAltitudeMode = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412517(v=MSDN.10).aspx
        /// Gets the objects custom icon.
        member this.GetCustomIcon() : VECustomIconSpecification = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412524(v=MSDN.10).aspx
        /// Gets the description of the object. This description will be displayed in the shape's
        /// info box.
        member this.GetDescription() : string = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412463(v=MSDN.10).aspx
        /// Gets the fill color and transparency for a polygon.
        member this.GetFillColor() : VEColor = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412408(v=MSDN.10).aspx
        /// Gets a  object representing the shape's custom icon anchor point.
        member this.GetIconAnchor() : VELatLong = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429553(v=MSDN.10).aspx
        /// Gets the internal identifier of the  object.
        member this.GetID () : string = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412422(v=MSDN.10).aspx
        /// Gets the line color or transparency for a polyline or polygon.
        member this.GetLineColor() : VEColor = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb877845(v=MSDN.10).aspx
        /// Gets whether a line is drawn from the shape to the ground.
        member this.GetLineToGround() : bool = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429641(v=MSDN.10).aspx
        /// Gets the line width of a polyline or polygon.
        member this.GetLineWidth() : int = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb877830(v=MSDN.10).aspx
        /// Gets the maximum zoom level at which the shape is visible.
        member this.GetMaxZoomLevel() : int = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb877822(v=MSDN.10).aspx
        /// Gets the minimum zoom level at which the shape is visible.
        member this.GetMinZoomLevel() : int = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429633(v=MSDN.10).aspx
        /// Gets the shape's "more info" URL.
        member this.GetMoreInfoURL() : string = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412536(v=MSDN.10).aspx
        /// Gets the shape's "photo" URL.
        member this.GetPhotoURL() : string = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412458(v=MSDN.10).aspx 
        /// Returns an array of objects representing the points that make up the pushpin,
        /// polyline, or polygon.  In the case of a pushpin, this will be a one-cell : VELatLong
        member this.GetPoints() : VELatLong [] = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429600(v=MSDN.10).aspx
        /// Gets the reference to the layer containing the specified  object.
        member this.GetShapeLayer() : VEShapeLayer = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412512(v=MSDN.10).aspx
        /// Gets the title of the  object.
        member this.GetTitle() : string = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412447(v=MSDN.10).aspx
        /// Gets the type of the object.
        member this.GetType : VEShapeType = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb877807(v=MSDN.10).aspx
        /// Gets the z-index of a pushpin shape or pushpin attached to a polyline or polygon.
        member this.GetZIndex() : int = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb877842(v=MSDN.10).aspx
        /// Gets the z-index for a polyline or polygon.
        member this.GetZIndexPolyShape() : int = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb544960(v=MSDN.10).aspx
        /// Hides the specified  object from view.
        member this.Hide() : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412468(v=MSDN.10).aspx
        /// Hides the icon associated with a polyline or polygon.
        member this.HideIcon() : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb877823(v=MSDN.10).aspx
        /// Returns whether the shape is a 3D model.
        member this.IsModel() : bool = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb877810(v=MSDN.10).aspx
        /// Specifies the altitude for the shape.
        /// * altitude: A floating-point value or array of floating-point values specifying
        ///   the altitude, in meters, of the shape.
        /// * altitudeMode: A VEAltitudeMode Enumeration value specifying the mode in which
        ///   the shape's altitude is represented.
        member this.SetAltitude(altitude: float [], altitudeMode: VEAltitudeMode) : unit = Undefined
        member this.SetAltitude(altitude: float, altitudeMode: VEAltitudeMode) : unit = Undefined

        /// Specifies the mode in which a shape's altitude is represented.
        /// * mode: A VEAltitudeMode Enumeration value specifying the altitude representation.
        member this.SetAltitudeMode(mode: VEAltitudeMode) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412425(v=MSDN.10).aspx
        /// Sets the  object's custom icon.
        /// customIcon: A String object containing either a URL to an image, custom HTML that
        ///             defines the custom icon, or a VECustomIconSpecification Class object.
        member this.SetCustomIcon(customIcon: string) : unit = Undefined
        member this.SetCustomIcon(customIcon: VECustomIconSpecification) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412553(v=MSDN.10).aspx
        /// Sets the description of the  object.
        /// details: A String object containing either plain text or HTML that represents the
        ///          VEShape object's description field.
        member this.SetDescription(details: string) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412486(v=MSDN.10).aspx
        /// Sets the fill color and transparency of a polygon.
        /// color: A VEColor:  object representing the fill color and transparency.
        member this.SetFillColor(color: VEColor) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429563(v=MSDN.10).aspx
        /// Sets the info box anchor of the  object.
        /// * point: A VELatLong Class object representing the shape's info box anchor point.
        member this.SetIconAnchor(latlong: VELatLong) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412522(v=MSDN.10).aspx
        /// Sets the line color or transparency for a polyline or polygon.
        /// color: A VEColor Class object representing the line color and transparency.
        member this.SetLineColor(color: VEColor) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb877859(v=MSDN.10).aspx
        /// Specifies whether a line is drawn from the shape to the ground.
        /// extrude: A Boolean value specifying whether a line is drawn from the shape to the ground.
        member this.SetLineToGround(extrude: bool) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412415(v=MSDN.10).aspx
        /// Sets the line width for a polyline or polygon.
        /// * width: An integer representing the line's width.
        member this.SetLineWidth(width) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb877839(v=MSDN.10).aspx
        /// Sets the maximum zoom level at which the shape is visible.
        /// level: An integer specifying the maximum zoom level.
        member this.SetMaxZoomLevel(level: int) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb877843(v=MSDN.10).aspx
        /// Sets the minimum zoom level at which the shape is visible.
        /// * level: An integer specifying the minimum zoom level.
        member this.SetMinZoomLevel(level) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429605(v=MSDN.10).aspx
        /// Sets the shape's "more info" URL.
        /// moreInfoURL: A String object containing the URL of the "more info" link that is displayed in the shape's info box.
        member this.SetMoreInfoURL(moreInfoURL: string) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412498(v=MSDN.10).aspx
        /// Sets the shape's photo URL.
        /// * photoURL: The string containing the URL of the image that is displayed in the shape's info box.
        member this.SetPhotoURL(photoURL: string) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429550(v=MSDN.10).aspx
        /// Sets the points of the  object.
        /// * points: An array of VELatLong Class objects.
        member this.SetPoints(points: VELatLong[]) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429575(v=MSDN.10).aspx
        /// Sets the title of the  object. This title will be displayed in the shape's info box.
        /// * title: A String object containing either plain text or HTML that represents the
        ///          VEShape object's title.
        member this.SetTitle(title: string) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb877869(v=MSDN.10).aspx
        /// Sets the z-index value for a shape.
        /// * icon: Optional. An integer specifying the z-index for the shape's icon (or for the
        ///   pushpin if the shape is a pushpin). If this value is null or undefined the z-index
        ///   is not changed.
        /// * polyshape: Optional. An integer specifying the z-index for the shape. This
        ///   parameter is ignored if the shape is a pushpin. If this value is null or undefined
        ///   the z-index is not changed.
        member this.SetZIndex(icon: int, polyshape: int) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb544965(v=MSDN.10).aspx
        /// Makes the specified object visible. This method only has an effect if the object was
        /// previously hidden.
        member this.Show () : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429571(v=MSDN.10).aspx
        /// Shows the icon associated with a polyline or polygon. This method is ignored for
        /// pushpins.
        member this.ShowIcon() : unit = Undefined
    
        
    [<Stub>]
    [<Name "VEFindResult">]
    type VEFindResult = 
        /// A reference to the VEShape Class object corresponding to this
        /// FindResult object. The VEShape object represents the result's
        /// pushpin displayed on the map.
        val mutable Shape : VEShape

        /// The name of the found result.
        val mutable Name : string

        /// The description of the found result.
        val mutable Description : string

        /// The VEFindType Enumeration enumeration that represents the type
        /// of Find that was performed. This matches the findType parameter
        /// specified in the Find method call from which this result was
        /// generated.
        val mutable FindType : VEFindType

        /// A Boolean value that indicates whether the found result is a paid
        /// advertisement.
        val mutable IsSponsored : bool

        /// A VELatLong Class object that represents the location of the
        /// found result.
        val mutable LatLong : VELatLong

        /// The telephone number of the found result.
        val mutable Phone : string

    [<JavaScriptTypeAttribute>]
    type VEDistanceUnit = 
        | [<Inline "VEDistanceUnit.Miles">]
          /// Generates route information in miles.
          Miles

        | [<Inline "VEDistanceUnit.Kilometers">]
          /// Generates route information in kilometers.
          Kilometers

    [<JavaScriptTypeAttribute>]
    type VEShapeAccuracy = 
        | [<Inline "VEShapeAccuracy.None">]
        /// No shapes are accurately converted
        None

        | [<Inline "VEShapeAccuracy.Pushpin">]
        /// Only pushpins are accurately converted
        Pushpin

    [<JavaScriptTypeAttribute>]
    type VEMiniMapSize =     
        | [<Inline "VEMiniMapSize.Small">]
        /// This represents a small mini map.
        Small

        | [<Inline "VEMiniMapSize.Large">]
        /// This represents a large mini map.
        Large
        
    [<JavaScriptTypeAttribute>]
    type VELocationPrecision = 
        | [<Inline "VELocationPrecision.Interpolated">]
          /// The precision is estimated from multiple geocoded sources.
          Interpolated

        | [<Inline "VELocationPrecision.Rooftop">]
          /// The precision is from a single match.
          Rooftop
       
    [<Stub>]
    [<Name "VEGeocodeLocation">]
    type VEGeocodeLocation =     
        /// A VELatLong Class object specifying the latitude and longitude of the location.
        val mutable LatLong : VELatLong

        /// A VELocationPrecision Enumeration value specifying the precision of the location.
        val mutable Precision : VELocationPrecision
     
    [<JavaScriptTypeAttribute>]
    type VEMatchConfidence = 
        | [<Inline "VEMatchConfidence.High">]
          /// The confidence of a match is high
          High

        | [<Inline "VEMatchConfidence.Medium">]
          /// The confidence of a match is medium
          Medium

        | [<Inline "VEMatchConfidence.Low">]
          /// The confidence of a match is low
          Low
    
    [<JavaScriptTypeAttribute>]
    type VEMatchCode = 
        | [<Inline "VEMatchCode.None">]
          /// No match was found
          None

        | [<Inline "VEMatchCode.Good">]
          /// The match was good
          Good

        | [<Inline "VEMatchCode.Ambiguous">]
          /// The match was ambiguous
          Ambiguous

        | [<Inline "VEMatchCode.UpHierarchy">]
          /// The match was found by a broader search
          UpHierarchy

        | [<Inline "VEMatchCode.Modified">]
          /// The match was found, but to a modified place
          Modified
        
    [<Stub>]
    [<Name "VEPlace">]
    type VEPlace = 
    
        /// Gets a VELatLong Class object that represents the best location of the found result.
        val mutable LatLong : VELatLong

        /// An array of VEGeocodeLocation Class objects specifying all of the possible match
        /// results returned by the geocoder for this place
        val mutable Locations : VEGeocodeLocation []

        /// Gets the String object that represents the unambiguous name for the Bing Maps location.
        val mutable Name : string

        /// A VEMatchCode Enumeration value specifying the match code from the geocoder. This
        /// property value is only valid for where-only searches.
        val mutable MatchCode : VEMatchCode

        /// A VEMatchConfidence Enumeration value specifying the match confidence from the
        /// geocoder. This property value is only valid for where-only searches.
        val mutable MatchConfidence : VEMatchConfidence

        /// A VELocationPrecision Enumeration value specifying the match precision from the
        /// geocoder for the best result, which is found in the VEPlace.LatLong property.
        val mutable Precision : VELocationPrecision

    [<Stub>]
    [<Name "VEBirdseyeScene">]
    type VEBirdseyeScene = 
        /// http://msdn.microsoft.com/en-us/library/bb429591(v=MSDN.10).aspx
        /// Determines whether the location specified by a VELatLong Class object is within
        /// the current
        /// * Latlong: A VELatLong Class object.
        member this.ContainsLatLong(latlong: VELatLong) : bool = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429632(v=MSDN.10).aspx
        /// Determines whether a specified pixel is within the current  object.
        /// * x: The X component of the pixel
        /// * y: The Y component of the pixel
        /// * zoomLevel: The current zoom level of the map
        member this.ContainsPixel(x: int, y: int, zoomLevel: int) : bool = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb877872(v=MSDN.10).aspx
        /// Returns an unencrypted and rounded off bounding rectangle for the  object.
        member this.GetBoundingRectangle() : VELatLongRectangle = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412523(v=MSDN.10).aspx
        /// Returns the height of the image in the current object, in pixels, at maximum
        /// resolution.
        member this.GetHeight() : int = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412423(v=MSDN.10).aspx
        /// Returns the ID of the current  object.
        member this.GetID() : string = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429595(v=MSDN.10).aspx
        /// Returns the orientation () of the current
        member this.GetOrientation() : VEOrientation = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412526(v=MSDN.10).aspx
        /// Returns the width of the image in the current object, in pixels, at maximum
        /// resolution.
        member this.GetWidth() : int = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb412470(v=MSDN.10).aspx
        /// Converts a  object (latitude/longitude pair) to the corresponding pixel on the map.
        /// * LatLong: A VELatLong Class object, which contains the latitude and longitude of a
        ///   point. This method also accepts an encrypted VELatLong: object, as supplied by the
        /// * zoomLevel: The zoom level of the current map view.
        member this.LatLongToPixel(latlong: VELatLong, zoomLevel: int) : unit = Undefined

        /// http://msdn.microsoft.com/en-us/library/bb429604(v=MSDN.10).aspx
        /// Converts a point in the bird's eye scene to an encrypted latitude/longitude value.
        /// * pixel: A VEPixel Class  object representing a pixel location on the map
        /// * zoomLevel: The zoom level of the current map view
        member this.PixelToLatLong(pixel: VEPixel, zoomLevel: int) : unit = Undefined
    
    [<JavaScriptTypeAttribute>]
    type VERouteDistanceUnit = 
        | [<Inline "VERouteDistanceUnit.Miles">]
          /// Generates route information in miles.
          Miles

        | [<Inline "VERouteDistanceUnit.Kilometers">]
          /// Generates route information in kilometers.
          Kilometers  
    
    [<Stub>]
    [<Name "VERouteHintType">]
    type VERouteHintType = 
        | [<Inline "VERouteHintType.PreviousIntersection">]
            /// The hint describes the intersection that comes before the itinerary item.
            PreviousIntersection

        | [<Inline "VERouteHintType.NextIntersection">]
            /// The hint describes the intersection that comes after the itinerary item.
            NextIntersection

        | [<Inline "VERouteHintType.Landmark">]
            /// The hint describes a landmark along the road or near the itinerary item.
            Landmark

    [<JavaScriptTypeAttribute>]
    type VERouteWarningSeverity = 
        | [<Inline "VERouteWarningSeverity.None">]
          /// A warning which has no impact on traffic
          None

        | [<Inline "VERouteWarningSeverity.LowImpact">]
          /// A warning which has low impact on traffic
          LowImpact

        | [<Inline "VERouteWarningSeverity.Minor">]
          /// A minor traffic incident
          Minor

        | [<Inline "VERouteWarningSeverity.Moderate">]
          /// A moderate traffic incident
          Moderate

        | [<Inline "VERouteWarningSeverity.Serious">]
          /// A serious traffic incident
          Serious

    [<Stub>]
    [<Name "VERouteWarning">]
    type VERouteWarning = 
        /// A VERouteWarningSeverity Enumeration value specifying the severity level of the warning.
        val mutable Severity : VERouteWarningSeverity

        /// A String that describes the route warning.
        val mutable Text : string
    
    [<Stub>]
    [<Name "VERouteHint">]
    type VERouteHint = 
        /// A VERouteHintType Enumeration value specifying the type of the hint.
        val mutable Type : VERouteHintType

        /// A string that describes the route itinerary item hint.
        val mutable Text : string
      
    [<Stub>]
    [<Name "VERouteItineraryItem">]
    type VERouteItineraryItem = 
    
        /// A floating-point number specifying the distance of the step
        val mutable Distance : float

        /// A VELatLong Class object specifying the position of the step
        val mutable LatLong : VELatLong

        /// A VEShape Class object specifying the shape of the step
        val mutable Shape : VEShape

        /// A String specifying the description of the step
        val mutable Text : string

        /// An integer specifying the total elapsed time, in seconds, to traverse the route itinerary step.
        val mutable Time : int

        /// An array of VERouteWarning Class items that correspond to the itinerary item.
        val mutable Warnings : VERouteWarning []

        /// An array of VERouteHint Class items that correspond to the itinerary item.
        val mutable Hints : VERouteHint []
    
    [<Stub>]
    [<Name "VERouteItinerary">]
    type VERouteItinerary = 
        /// An array of VERouteItineraryItem Class objects specifying the step-by-step
        /// directions for the route.
        val mutable Items : VERouteItineraryItem []
    
    [<Stub>]
    [<Name "VERouteLeg">]
    type VERouteLeg = 
        /// A floating-point number specifying the length of the route leg.
        val mutable Distance : float

        /// A VERouteItinerary Class object specifying the itinerary of the route leg.
        val mutable Itinerary : VERouteItinerary

        /// An integer specifying the total elapsed time, in seconds, to traverse the route leg.
        val mutable Time : int

    [<Stub>]
    [<Name "VERoute">]
    type VERoute = 

        /// A floating-point value that specifies the total length of the route.
        val mutable Distance : float

        /// An array of VERouteLeg Class objects that specify the intermediate legs of the
        /// route.
        val mutable RouteLegs : VERouteLeg []

        /// An array of VELatLong Class objects that identify the shape of the route.
        val mutable ShapePoints : VELatLong []

        /// An integer specifying the total elapsed time, in seconds, to traverse the route.
        val mutable Time : int
    
    [<JavaScriptTypeAttribute>]
    type VERouteMode = 
        | [<Inline "VERouteMode.Driving">]
          /// The returned route contains driving directions
          Driving

        | [<Inline "VERouteMode.Walking">]
          /// The returned route contains walking directions
          Walking

    [<JavaScriptTypeAttribute>]
    type VERouteOptimize = 
        | [<Inline "VERouteOptimize.MinimizeTime">]
          /// The route is calculated to minimize time.
          MinimizeTime

        | [<Inline "VERouteOptimize.MinimizeDistance">]
          /// The route is calculated to minimize distance.
          MinimizeDistance

    [<Stub>]
    [<Name "VERouteOptions">]
    type VERouteOptions = 
        new () = {}
        /// A VERouteDistanceUnit Enumeration value specifying the units used for the route. The
        /// default value is VERouteDistanceUnit.Mile.
        [<DefaultValue>]
        val mutable DistanceUnit : VERouteDistanceUnit

        /// A Boolean value specifying whether the route is drawn on the map. The default value
        /// is true, which means the route is drawn on the map.
        [<DefaultValue>]
        val mutable DrawRoute : bool

        /// The name of the function called when the method has generated the
        /// route. Optional. The default value is . The called function receives a VERoute Class
        /// object.
        [<DefaultValue>]
        val mutable RouteCallback : VERoute -> unit

        /// The VEColor Class object specifying the color of the route line. The default value is
        /// default is VEColor(0,169,235,0.7).
        [<DefaultValue>]
        val mutable RouteColor : VEColor

        /// A VERouteMode Enumeration value specifying the mode of route to return. The default
        /// value is VERouteMode.Driving.
        [<DefaultValue>]
        val mutable RouteMode : VERouteMode

        /// A VERouteOptimize Enumeration value specifying how the route is optimized. The
        /// default value is VERouteOptimize.MinimizeTime.
        [<DefaultValue>]
        val mutable RouteOptimize : VERouteOptimize

        /// The thickness, in pixels, of the route line. The default value is 6 pixels.
        [<DefaultValue>]
        val mutable RouteWeight : int

        /// The z-index of the route line. The default value is 4.
        [<DefaultValue>]
        val mutable RouteZIndex : int

        /// A Boolean value specifying whether the map view is set to the best view of the route
        /// after it is drawn. The default is true, which means that the map view is set.
        [<DefaultValue>]
        val mutable SetBestMapView : bool

        /// A Boolean value specifying whether a disambiguation dialog box is
        /// shown. Optional. The default value is true, which means the disambiguation dialog box
        /// is shown. If false, no disambiguation dialogs are displayed and the route uses the
        /// first geocoded response for each location.
        [<DefaultValue>]
        val mutable ShowDisambiguation : bool

        /// Whether to show any error messages. The default value is true.
        [<DefaultValue>]
        val mutable ShowErrorMessages : bool

        /// A Boolean value specifying whether to use the MapPoint Web Service to generate the
        /// route. The default value is false.
        [<DefaultValue>]
        val mutable UseMWS : bool

        /// A Boolean value specifying whether to calculate the route using traffic
        /// information. The default value is false.
        [<DefaultValue>]
        val mutable UseTraffic : bool

    
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
    [<Name "VEModelOrientation">]
    type VEModelOrientation = class end

    [<Stub>]
    [<Name "VEModelScale">]
    type VEModelScale = class end

    [<Stub>]
    [<Name "VEShapeSourceSpecification">]
    type VEShapeSourceSpecification = class end

    [<Stub>]
    [<Name "VEMapViewSpecification">]
    type VEMapViewSpecification = class end
    
    [<JavaScriptType>]
    type Location = class end

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


