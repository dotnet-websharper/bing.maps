namespace IntelliFactory.WebSharper.BingExtension

open IntelliFactory.WebSharper.Dom

module Bing =
    open IntelliFactory.WebSharper.InterfaceGenerator

    let AltitudeReference = Type.New()

    let AltitudeReferenceClass =
        Class "Microsoft.Maps.AltitudeReference"
        |=> AltitudeReference
        |+> [
                "ground" =? AltitudeReference
                |> WithComment "The altitude is measured from the ground level."

                "ellipsoid" =? AltitudeReference
                |> WithComment "The altitude is measured from the WGS 84 ellipsoid of the Earth."

                "isValid" => AltitudeReference ^-> T<bool>
                |> WithComment "Determines if the specified reference is a supported AltitudeReference."
            ]

    let Location = Type.New()
    
    let LocationClass =
        Class "Microsoft.Maps.Location"
        |=> Location
        |+> [
                Constructor (T<float> * T<float> * T<float> * AltitudeReference)
                Constructor (T<float * float * float>)
                Constructor (T<float * float>)

                "areEqual" => Location * Location ^-> T<bool>
                |> WithComment "Determines if the specified Location objects are equal."

                "normalizeLongitude" => T<float -> float>
                |> WithComment "Normalizes the specified longitude so that it is between -180 and 180."
            ]
        |+> Protocol
            [
                "altitude" =? T<float>
                |> WithComment "The altitude of the location."

                "altitudeMode" =? AltitudeReference
                |> WithComment "The reference from which the altitude is measured."

                "latitude" =? T<float>
                |> WithComment "The latitude of the location."

                "longitude" =? T<float>
                |> WithComment "The longitude of the location."

                "clone" => T<unit> ^-> Location
                |> WithComment "Returns a copy of the Location object."

                "toString" => T<unit -> string>
                |> WithComment "Converts the Location object to a string."
            ]

    let LocationRect = Type.New()

    let LocationRectClass =
        Class "Microsoft.Maps.LocationRect"
        |=> LocationRect
        |+> [
                Constructor (Location * T<float> * T<float>)

                "fromCorners" => Location * Location ^-> LocationRect
                |> WithComment "Returns a LocationRect using the specified locations for the northwest and southeast corners."

                "fromEdges" => T<float> * T<float> * T<float> * T<float> * T<float> * AltitudeReference ^-> LocationRect
                |> WithComment "Returns a LocationRect using the specified northern and southern latitudes and western and eastern longitudes for the rectangle boundaries."

                "fromLocation" => Type.ArrayOf Location ^-> LocationRect
                |> WithComment "Returns a LocationRect using an array of locations."

                "fromString" => T<string> ^-> LocationRect
                |> WithComment "Creates a LocationRect from a string with the following format: \"north,west,south,east\". North, west, south and east specify the coordinate number values."
            ]
        |+> Protocol [
                "center" =? Location
                |> WithComment "The location that defines the center of the rectangle."

                "height" =? T<float>
                |> WithComment "The height, in degrees, of the rectangle."

                "width" =? T<float>
                |> WithComment "The width, in degrees, of the rectangle."

                "clone" => T<unit> ^-> LocationRect
                |> WithComment "Returns a copy of the LocationRect object."

                "contains" => Location ^-> T<bool>
                |> WithComment "Returns whether the specified Location is within the LocationRect."

                "getEast" => T<unit -> float>
                |> WithComment "Returns the longitude that defines the eastern edge of the LocationRect."
                "getWest" => T<unit -> float>
                |> WithComment "Returns the latitude that defines the western edge of the LocationRect."
                "getNorth" => T<unit -> float>
                |> WithComment "Returns the latitude that defines the northern edge of the LocationRect."
                "getSouth" => T<unit -> float>
                |> WithComment "Returns the latitude that defines the southern edge of the LocationRect."

                "getSouthEast" => T<unit> ^-> Location
                |> WithComment "Returns the Location that defines the southeast corner of the LocationRect."
                "getNorthWest" => T<unit> ^-> Location
                |> WithComment "Returns the Location that defines the northwest corner of the LocationRect."

                "intersects" => LocationRect ^-> T<bool>
                |> WithComment "Returns whether the specified LocationRect intersects with this LocationRect."

                "toString" => T<unit -> string>
                |> WithComment "Converts the LocationRect object to a string."
            ]

    let Point = Type.New()

    let PointClass =
        Class "Microsoft.Maps.Point"
        |=> Point
        |+> [
                Constructor (T<float> * T<float>)

                "areEqual" => Point * Point ^-> T<bool>
                |> WithComment "Determines if the specified points are equal."

                "clonePoint" => Point ^-> Point
                |> WithSourceName "clone"
                |> WithComment "Returns a copy of the Point object."
            ]
        |+> Protocol
            [
                "clone" => T<unit> ^-> Point
                |> WithComment "Returns a copy of the Point object."

                "toString" => T<unit -> string>
                |> WithComment "Converts the Point object into a string."
            ]

    let EventHandler = Type.New()

    let EventHandlerClass =
        Class "Microsoft.Maps.EventHandler"
        |=> EventHandler

    let Events = Type.New()

    let EventsClass =
        Class "Microsoft.Maps.Events"
        |=> Events
        |+> [
                "addHandler" => T<obj * string * (obj -> obj)> ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target."

                "addThrottledHandler" => T<obj * string * (obj -> obj) * float> ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target, where the minimum interval between events (in milliseconds) is specified in the ThrottleInterval parameter. The last occurrence of the event is called after the specified ThrottleInterval."

                "hasHandler" => T<obj * string -> bool>
                |> WithComment "Checks if the target has any attached event handler."

                "invoke" => T<obj * string -> unit>
                |> WithComment "Invokes an event on the target. This causes all handlers for the specified eventName to be called."

                "removeHandler" => EventHandler ^-> T<unit>
                |> WithComment "Detaches the specified handler from the event."
            ]

    let KeyEventArgs = Type.New()

    let KeyEventArgsClass =
        Class "Microsoft.Maps.KeyEventArgs"
        |=> KeyEventArgs
        |+> Protocol
            [
                "altKey" =? T<bool>
                |> WithComment "A boolean indicating if the ALT key was pressed."

                "ctrlKey" =? T<bool>
                |> WithComment "A boolean indicating if the CTRL key was pressed."

                "eventName" =? T<string>
                |> WithComment "The event that occurred."

                "handled" =? T<bool>
                |> WithComment "A boolean indicating whether the event is handled. If this property is set to true, the default map control behavior for the event is cancelled."

                "keyCode" =? T<string>
                |> WithComment "The code that identifies the keyboard key that was pressed."

                "originalEvent" =? T<obj>
                |> WithComment "The original browser event."

                "shiftKey" =? T<bool>
                |> WithComment "A boolean indicating if the SHIFT key was pressed."
            ]

    let LabelOverlay = Type.New()

    let LabelOverlayClass =
        Class "Microsoft.Maps.LabelOverlay"
        |=> LabelOverlay
        |+> [
                "hidden" =? LabelOverlay
                |> WithComment "Map labels are not shown on top of imagery."

                "visible" =? LabelOverlay
                |> WithComment "Map labels are shown on top of imagery."

                "isValid" => LabelOverlay ^-> T<bool>
                |> WithComment "Determines whether the specified labelOverlay is a supported LabelOverlay."
            ]

    let MapOptions = Type.New()

    let MapOptionsClass =
        Class "Microsoft.Maps.MapOptions"
        |=> MapOptions
        |+> [
                Constructor T<unit>
            ]
        |+> Protocol
            [
                "credentials" =% T<string>
                |> WithComment "The Bing Maps Key used to authenticate the application. This property is required and can only be set when using the Map constructor."

                "disableKeyboardInput" =? T<bool>
                |> WithComment "A boolean value indicating whether to disable the map’s response to keyboard input. The default value is false. This property can only be set when using the Map constructor."

                "disableMouseInput" =? T<bool>
                |> WithComment "A boolean value indicating whether to disable the map’s response to mouse input. The default value is false. This property can only be set when using the Map constructor."

                "disableTouchInput" =? T<bool>
                |> WithComment "A boolean value indicating whether to disable the map’s response to touch input. The default value is false. This property can only be set when using the Map constructor."

                "disableUserInput" =? T<bool>
                |> WithComment "A boolean value indicating whether to disable the map’s response to any user input. The default value is false. This property can only be set when using the Map constructor."

                "enableClickableLogo" =? T<bool>
                |> WithComment "A boolean value indicating whether the Bing(TM) logo on the map is clickable. The default value is true. This property can only be set when using the Map constructor."

                "enableSearchLogo" =? T<bool>
                |> WithComment "A boolean value indicating whether to enable the Bing(TM) hovering search logo on the map. The default value is true. This property can only be set when using the Map constructor."

                "height" =? T<bool>
                |> WithComment "The height of the map. The default value is null. If no height is specified, the height of the div is used. If height is specified, then width must be specified as well."
            ]

    let Size = Type.New()

    let SizeClass =
        Class "Microsoft.Maps.Size"
        |=> Size
        |+> Protocol
            [
                "height" =? T<float>
                "width" =? T<float>
            ]

    let ViewOptions = Type.New()

    let ViewOptionsClass =
        Class "Microsoft.Maps.ViewOptions"
        |=> ViewOptions

    let EntityCollection = Type.New()

    let EntityCollectionClass =
        Class "Microsoft.Maps.EntityCollection"
        |=> EntityCollection

    let Range = Type.New()

    let RangeClass =
        Class "Range"
        |=> Range
        |+> Protocol
            [
                "min" =? T<float>
                |> WithComment "The minimum value in the range."

                "max" =? T<float>
                |> WithComment "The maximum value in the range."
            ]

    let MapTypeId = Type.New()

    let MapTypeIdClass =
        Class "Microsoft.Maps.MapTypeId"
        |=> MapTypeId

    let PixelReference = Type.New()

    let PixelReferenceClass =
        Class "Microsoft.Maps.PixelReference"
        |=> PixelReference

    let Map = Type.New()

    let MapClass =
        Class "Microsoft.Maps.Map"
        |=> Map
        |+> [
                Constructor (T<Node> * MapOptions)
                Constructor (T<Node> * ViewOptions)
                Constructor (T<Node>)
            ]
        |+> Protocol
            [
                "entities" =? EntityCollection
                |> WithComment "The map’s entities. Use this property to add or remove entities from the map."

                "blur" => T<unit -> unit>
                |> WithComment "Removes focus from the map control so that it does not respond to keyboard events."

                "dispose" => T<unit -> unit>
                |> WithComment "Deletes the Map object and releases any associated resources."

                "focus" => T<unit -> unit>
                |> WithComment "Applies focus to the map control so that it responds to keyboard events."

                "getBounds" => T<unit> ^-> LocationRect
                |> WithComment "Returns the location rectangle that defines the boundaries of the current map view."

                "getCenter" => T<unit> ^-> Location
                |> WithComment "Returns the location of the center of the current map view."

                "getCopyrights" => T<unit> ^-> Type.ArrayOf T<string>
                |> WithComment "Gets the array of strings representing the attributions of the imagery currently displayed on the map."

                "getCredentials" => T<(string option -> unit) -> unit>
                |> WithComment "Gets the session ID. This method calls the callback function with the session ID as the first parameter."

                "getHeading" => T<unit -> float>
                |> WithComment "Returns the heading of the current map view."

                "getHeight" => T<unit -> int>
                |> WithComment "Returns the height of the map control."

                "getImageryId" => T<unit -> string>
                |> WithComment "Returns the string that represents the imagery currently displayed on the map."

                "getMapTypeId" => T<unit -> string>
                |> WithComment "Returns a string that represents the current map type displayed on the map."

                "getMetersPerPixel" => T<unit -> float>
                |> WithComment "Returns the current scale in meters per pixel of the center of the map."

                "getModeLayer" => T<unit -> Node>
                |> WithComment "Returns the map’s mode node."

                "getOptions" => T<unit> ^-> MapOptions
                |> WithComment "Returns the map options that have been set. Note that if an option is not set, then the default value for that option is assumed and getOptions returns undefined for that option."

                "getPageX" => T<unit -> int>
                |> WithComment "Returns the x coordinate of the top left corner of the map control, relative to the page."

                "getPageY" => T<unit -> int>
                |> WithComment "Returns the y coordinate of the top left corner of the map control, relative to the page."

                "getRootElement" => T<unit -> Node>
                |> WithComment "Returns the map’s root node."

                "getTargetBounds" => T<unit> ^-> LocationRect
                |> WithComment "Returns the location rectangle that defines the boundaries of the view to which the map is navigating."

                "getTargetCenter" => T<unit> ^-> Location
                |> WithComment "Returns the center location of the view to which the map is navigating."

                "getTargetHeading" => T<unit -> float>
                |> WithComment "Returns the heading of the view to which the map is navigating."

                "getTargetMetersPerPixel" => T<unit -> float>
                |> WithComment "Returns the scale in meters per pixel of the center of the view to which the map is navigating."

                "getTargetZoom" => T<unit -> float>
                |> WithComment "Returns the zoom level of the view to which the map is navigating."

                "getUserLayer" => T<unit -> Node>
                |> WithComment "Returns the map’s user node."

                "getViewportX" => T<unit -> int>
                |> WithComment "Returns the x coordinate of the viewport origin (the center of the map), relative to the page."

                "getViewportY" => T<unit -> int>
                |> WithComment "Returns the y coordinate of the viewport origin (the center of the map), relative to the page."

                "getWidth" => T<unit -> int>
                |> WithComment "Returns the width of the map control."

                "getZoom" => T<unit -> float>
                |> WithComment "Returns the zoom level of the current map view."

                "getZoomRange" => T<unit> ^-> Range
                |> WithComment "Returns the range of valid zoom levels for the current map view."

                "isMercator" => T<unit -> bool>
                |> WithComment "Returns whether the map is in a regular Mercator nadir mode."

                "isRotationEnabled" => T<unit -> bool>
                |> WithComment "Returns true if the current map type allows the heading to change; false if the display heading is fixed."

                "setMapType" => MapTypeId ^-> T<unit>
                |> WithComment "Sets the current map type. The specified mapTypeId must be a valid map type ID or a registered map type ID."

                "setOptions" => Size ^-> T<unit>
                |> WithComment "Sets the height and width of the map."

                "setView" => ViewOptions ^-> T<unit>
                |> WithComment "Sets the map view based on the specified options."

                "tryLocationToPixel" => Location * PixelReference ^-> Point
                |> WithComment "Converts a specified Location to a Point on the map relative to the specified PixelReference. If the map is not able to convert the Location, null is returned."

                "tryLocationToPixel" => Location ^-> Point
                |> WithComment "Converts a specified Location to a Point on the map relative to PixelReference.Viewport. If the map is not able to convert the Location, null is returned."

                "tryLocationToPixel" => Type.ArrayOf Location * PixelReference ^-> Point
                |> WithComment "Converts an array of Locations relative to the specified PixelReference and returns an array of Points if all locations were converted. If any of the conversions fail, null is returned."

                "tryLocationToPixel" => Type.ArrayOf Location ^-> Point
                |> WithComment "Converts an array of Locations relative to PixelReference.Viewport and returns an array of Points if all locations were converted. If any of the conversions fail, null is returned."

                "tryPixelToLocation" => Point * PixelReference ^-> Location
                |> WithComment "Converts a specified Point to a Location on the map relative to the specified PixelReference. If the map is not able to convert the Point, null is returned."

                "tryPixelToLocation" => Point ^-> Location
                |> WithComment "Converts a specified Point to a Location on the map relative to PixelReference.Viewport. If the map is not able to convert the Point, null is returned."

                "tryPixelToLocation" => Type.ArrayOf Point * PixelReference ^-> Location
                |> WithComment "Converts an array of Points relative to the specified PixelReference and returns an array of Locations if all points were converted. If any of the conversions fail, null is returned."

                "tryPixelToLocation" => Type.ArrayOf Point ^-> Location
                |> WithComment "Converts an array of Points relative to PixelReference.Viewport and returns an array of Locations if all points were converted. If any of the conversions fail, null is returned."
            ]

    let Assembly =
        Assembly [
            Namespace "IntelliFactory.WebSharper.Bing" [
                AltitudeReferenceClass
                LocationClass
                LocationRectClass
                PointClass
                EventHandlerClass
                EventsClass
                KeyEventArgsClass
                LabelOverlayClass
                MapOptionsClass
                ViewOptionsClass
                EntityCollectionClass
                RangeClass
                MapTypeIdClass
                SizeClass
                MapClass
            ]
        ]

