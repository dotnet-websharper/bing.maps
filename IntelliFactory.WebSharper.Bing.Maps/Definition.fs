namespace IntelliFactory.WebSharper.BingExtension

open IntelliFactory.WebSharper.JavaScript.Dom
open IntelliFactory.WebSharper.InterfaceGenerator

module Bing =

    let private ConstantStrings ty l =
        List.map (fun s -> (s =? ty |> WithGetterInline ("'" + s + "'")) :> CodeModel.IClassMember) l

    let private Constants ty l =
        List.map (fun s -> s =? ty :> CodeModel.IClassMember) l

    ///////////////////////////////////////////////////////////////////
    // Ajax API

    let AltitudeReference = Type.New()

    let AltitudeReferenceClass =
        Class "Microsoft.Maps.AltitudeReference"
        |=> AltitudeReference
        |+> ConstantStrings AltitudeReference ["ground"; "ellipsoid"]
        |+> [
                "isValid" => AltitudeReference ^-> T<bool>
                |> WithComment "Determines if the specified reference is a supported AltitudeReference."
            ]

    let AnimationVisibility = Type.New()

    let AnimationVisibilityClass =
        Class "Microsoft.Maps.AnimationVisibility"
        |=> AnimationVisibility
        |+> ConstantStrings AnimationVisibility ["auto"; "hide"; "show"]

    let Location = Type.New()

    let LocationClass =
        Class "Microsoft.Maps.Location"
        |=> Location
        |+> [
                Constructor (T<float> * T<float> * T<float> * AltitudeReference)
                Constructor (T<float> * T<float> * T<float>)
                Constructor (T<float> * T<float>)

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

                "fromLocations" => Type.ArrayOf Location ^-> LocationRect
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

                "getSoutheast" => T<unit> ^-> Location
                |> WithComment "Returns the Location that defines the southeast corner of the LocationRect."
                "getNorthwest" => T<unit> ^-> Location
                |> WithComment "Returns the Location that defines the northwest corner of the LocationRect."

                "intersects" => LocationRect ^-> T<bool>
                |> WithComment "Returns whether the specified LocationRect intersects with this LocationRect."

                "toString" => T<unit -> string>
                |> WithComment "Converts the LocationRect object to a string."
            ]

    let Waypoint = Type.New()
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
                "x" =? T<float>
                |> WithComment "The x value of the coordinate."

                "y" =? T<float>
                |> WithComment "The y value of the coordinate."

                "clone" => T<unit> ^-> Point
                |> WithComment "Returns a copy of the Point object."

                "toString" => T<unit -> string>
                |> WithComment "Converts the Point object into a string."

                "toUrlString" => T<unit -> string>
                |> WithInline "($this.x+','+$this.y)"
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

    let ViewOptions = Type.New()
    let MapOptions = Type.New()
    let MapViewOptions = Type.New()


    let Color = Type.New()

    let ColorClass =
        Class "Microsoft.Maps.Color"
        |=> Color
        |+> [
                Constructor (T<int> * T<int> * T<int> * T<int>)
                |> WithComment "Initializes a new instance of the Color class. The a parameter represents opacity. The range of valid values for all parameters is 0 to 255."

                "cloneColor" => Color ^-> Color
                |> WithInline "clone"
                |> WithComment "Creates a copy of the Color object."

                "fromHex" => T<string> ^-> Color
                |> WithComment "Converts the specified hex string to a Color."
            ]
        |+> Protocol
            [
                "a" =? T<int>
                |> WithComment "The opacity of the color. The range of valid values is 0 to 255."

                "r" =? T<int>
                |> WithComment "The red value of the color. The range of valid values is 0 to 255."

                "g" =? T<int>
                |> WithComment "The green value of the color. The range of valid values is 0 to 255."

                "b" =? T<int>
                |> WithComment "The blue value of the color. The range of valid values is 0 to 255."

                "clone" => T<unit> ^-> Color
                |> WithComment "Returns a copy of the Color object."

                "getOpacity" => T<unit -> float>
                |> WithComment "Returns the opacity of the Color as a value between 0 (a=0) and 1 (a=255)."

                "toHex" => T<unit -> string>
                |> WithComment "Converts the Color into a 6-digit hex string. Opacity is ignored. For example, a Color with values (255,0,0,0) is returned as hex string #000000."

                "toString" => T<unit -> string>
                |> WithComment "Converts the Color object to a string."
            ]

    let MapTypeId = Type.New()

    let MapTypeIdClass =
        Class "Microsoft.Maps.MapTypeId"
        |=> MapTypeId
        |+> [
                "aerial" =? MapTypeId
                |> WithComment "The aerial map style is being used."

                "auto" =? MapTypeId
                |> WithComment "The map is set to choose the best imagery for the current view."

                "birdseye" =? MapTypeId
                |> WithComment "The bird’s eye map type is being used."

                "collinsBart" =? MapTypeId
                |> WithComment "Collin’s Bart (mkt=en-gb) map type is being used."

                "mercator" =? MapTypeId
                |> WithComment "The Mercator style is being used."

                "ordnanceSurvey" =? MapTypeId
                |> WithComment "Ordinance Survey (mkt=en-gb) map type is being used."

                "road" =? MapTypeId
                |> WithComment "The road map style is being used."
            ]

    let private ViewOptionsFields =
        [
            "animate", T<bool>
            "bounds", LocationRect
            "center", Location
            "centerOffset", Point
            "heading", T<float>
            "labelOverlay", LabelOverlay
            "mapTypeId", MapTypeId
            "padding", T<int>
            "zoom", T<int>
        ]

    let private MapOptionsFields =
        [
            "backgroundColor", Color
            "credentials", T<string>
            "disableBirdseye", T<bool>
            "disableKeyboardInput", T<bool>
            "disableMouseInput", T<bool>
            "disablePanning", T<bool>
            "disableTouchInput", T<bool>
            "disableUserInput", T<bool>
            "disableZooming", T<bool>
            "enableClickableLogo", T<bool>
            "enableSearchLogo", T<bool>
            "fixedMapPosition", T<bool>
            "height", T<int>
            "inertialIntensity", T<float>
            "showBreadcrumb", T<bool>
            "showCopyright", T<bool>
            "showDashboard", T<bool>
            "showMapTypeSelector", T<bool>
            "showScalebar", T<bool>
            "tileBuffer", T<int>
            "useInertia", T<bool>
            "width", T<int>
        ]

    let ViewOptionsClass =
        Pattern.Config "Microsoft.Maps.ViewOptions" {
            Required = []
            Optional = ViewOptionsFields
        }
        |=> ViewOptions

    let MapOptionsClass =
        Pattern.Config "Microsoft.Maps.MapOptions" {
            Required = []
            Optional = MapOptionsFields
        }
        |=> MapOptions

    let MapViewOptionsClass =
        Pattern.Config "Microsoft.Maps.MapViewOptions" {
            Required = []
            Optional = MapOptionsFields @ ViewOptionsFields
        }
        |=> MapViewOptions

    let Size = Type.New()

    let SizeClass =
        Pattern.Config "Microsoft.Maps.Size" {
            Required =
                [
                    "height", T<float>
                    "width", T<float>
                ]
            Optional = []
        }
        |=> Size





    let Entity = Type.New()

    let EntityInterface =
        Interface "Microsoft.Maps.Entity"
        |=> Entity

    let EntityCollectionOptions = Type.New()

    let EntityCollectionOptionsClass =
        Pattern.Config "Microsoft.Maps.EntityCollectionOptions" {
            Required = []
            Optional =
                [
                    "visible", T<bool>
                    "zIndex", T<int>
                ]
        }

    let EntityCollection = Type.New()

    let EntityCollectionClass =
        Class "Microsoft.Maps.EntityCollection"
        |=> EntityCollection
        |=> Implements [EntityInterface]
        |+> [
                Constructor T<unit>
                Constructor EntityCollectionOptions
            ]
        |+> Protocol
            [
                "clear" => T<unit -> unit>
                |> WithComment "Removes all entities from the collection."

                "get" => T<int> ^-> Entity
                |> WithComment "Returns the entity at the specified index in the collection."

                "getLength" => T<unit -> int>
                |> WithComment "Returns the number of entities in the collection."

                "getVisible" => T<unit -> bool>
                |> WithComment "Returns whether the entity collection is visible on the map."

                "getZIndex" => T<unit -> int>
                |> WithComment "Gets the z-index of the entity collection with respect to other items on the map."

                "indexOf" => Entity ^-> T<int>
                |> WithComment "Returns the index of the specified entity in the collection. If the entity is not found in the collection, -1 is returned."

                "insert" => Entity * T<int> ^-> T<unit>
                |> WithComment "Inserts the specified entity into the collection at the given index."

                "pop" => T<unit> ^-> Entity
                |> WithComment "Removes the last entity from the collection and returns it."

                "push" => Entity ^-> T<unit>
                |> WithComment "Adds the specified entity to the last position in the collection."

                "remove" => Entity ^-> Entity
                |> WithComment "Removes the specified entity from the collection and returns it."

                "removeAt" => T<int> ^-> Entity
                |> WithComment "Removes the entity at the specified index from the collection and returns it."

                "setOptions" => EntityCollectionOptions ^-> T<unit>
                |> WithComment "Sets the options for the entity collection."

                "toString" => T<unit -> string>
                |> WithComment "Converts the EntityCollection object to a string."
            ]

    let Event = Type.New()

    let EventClass =
        Interface "Microsoft.Maps.Event"

    let KeyEvent = Type.New()
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

                "eventName" =? KeyEvent
                |> WithComment "The event that occurred."

                "handled" =% T<bool>
                |> WithComment "A boolean indicating whether the event is handled. If this property is set to true, the default map control behavior for the event is cancelled."

                "keyCode" =? T<string>
                |> WithComment "The code that identifies the keyboard key that was pressed."

                "originalEvent" =? T<obj>
                |> WithComment "The original browser event."

                "shiftKey" =? T<bool>
                |> WithComment "A boolean indicating if the SHIFT key was pressed."
            ]

    let KeyEventClass =
        Class "Microsoft.Maps.KeyEvent"
        |=> KeyEvent
        |=> Implements [Event]
        |+> ConstantStrings KeyEvent ["keydown"; "keyup"; "keypress"]

    let MouseEvent = Type.New()
    let MouseEventArgs = Type.New()

    let MouseEventArgsClass =
        Class "Microsoft.Maps.MouseEventArgs"
        |=> MouseEventArgs
        |+> Protocol
            [
                "eventName" =? MouseEvent
                |> WithComment "The event that occurred."

                "handled" =% T<bool>
                |> WithComment "A boolean indicating whether the event is handled. If this property is set to true, the default map control behavior for the event is cancelled."

                "isPrimary" =? T<bool>
                |> WithComment "A boolean indicating if the primary button (such as the left mouse button or a tap on a touch screen) was used."

                "isSecondary" =? T<bool>
                |> WithComment "A boolean indicating if the secondary mouse button (such as the right mouse button) was used."

                "isTouchEvent" =? T<bool>
                |> WithComment "A boolean indicating whether the event that occurred was a touch event."

                "originalEvent" =? T<obj>
                |> WithComment "The original browser event."

                "pageX" =? T<int>
                |> WithComment "The x-value of the pixel coordinate on the page of the mouse cursor."

                "pageY" =? T<int>
                |> WithComment "The y-value of the pixel coordinate on the page of the mouse cursor."

                "target" =? Entity
                |> WithComment "The object that fired the event."

                "targetType" =? T<string>
                |> WithComment "The type of the object that fired the event. Valid values include the following: 'map', 'polygon', 'polyline', or 'pushpin'"

                "wheelData" =? T<int>
                |> WithComment "The number of units that the mouse wheel has changed."

                "getX" => T<unit> ^-> T<int>
                |> WithComment "Returns the x-value of the pixel coordinate, relative to the map, of the mouse."

                "getY" => T<unit> ^-> T<int>
                |> WithComment "Returns the y-value of the pixel coordinate, relative to the map, of the mouse."

                "x" =? T<int>
                |> WithGetterInline "$this.getX()"
                |> WithComment "Returns the x-value of the pixel coordinate, relative to the map, of the mouse."

                "y" =? T<int>
                |> WithGetterInline "$this.getY()"
                |> WithComment "Returns the y-value of the pixel coordinate, relative to the map, of the mouse."
            ]

    let MouseEventClass =
        Class "Microsoft.Maps.MouseEvent"
        |=> MouseEvent
        |=> Implements [Event]
        |+> ConstantStrings MouseEvent
            [
                "click"; "dblclick"; "rightclick"
                "mousedown"; "mouseup"; "mousemove"; "mouseover"; "mouseleave"; "mouseout"; "mousewheel"
            ]

    let EntityCollectionEvent = Type.New()
    let EntityCollectionEventArgs = Type.New()

    let EntityCollectionEventClass =
        Class "Microsoft.Maps.EntityCollectionEvent"
        |=> EntityCollectionEvent
        |=> Implements [Event]
        |+> ConstantStrings EntityCollectionEvent
            [ "entityadded"; "entitychanged"; "entityremoved" ]

    let EntityCollectionEventArgsClass =
        Class "Microsoft.Maps.EntityCollectionEventArgs"
        |=> EntityCollectionEventArgs
        |+> Protocol
            [
                "collection" =? EntityCollection
                "entity" =? Entity
            ]

    let UnitEvent = Type.New()

    let UnitEventClass =
        Class "Microsoft.Maps.UnitEvent"
        |=> UnitEvent
        |=> Implements [Event]
        |+> ConstantStrings UnitEvent
            [
                "copyrightchanged"; "imagerychanged"; "maptypechanged"; "targetviewchanged"; "tiledownloadcomplete"
                "viewchange"; "viewchangeend"; "viewchangestart"
            ]

    let EventHandler = Type.New()

    let EventHandlerClass =
        Class "Microsoft.Maps.EventHandler"
        |=> EventHandler

    let Events = Type.New()

    let InfoboxAction = Type.New()

    let InfoboxActionClass =
        Pattern.Config "Microsoft.Maps.InfoboxAction" {
            Required =
                [
                    "label", T<string>
                    "eventHandler", MouseEventArgs ^-> T<unit>
                ]
            Optional = []
        }

    let InfoboxOptions = Type.New()

    let InfoboxOptionsClass =
        Pattern.Config "Microsoft.Maps.InfoboxOptions" {
            Required = []
            Optional =
                [
                    "actions", Type.ArrayOf InfoboxAction
                    "description", T<string>
                    "height", T<int>
                    "htmlContent", T<string>
                    "id", T<string>
                    "location", Location
                    "offset", Point
                    "showCloseButton", T<bool>
                    "showPointer", T<bool>
                    "title", T<string>
                    "titleClickHandler", MouseEventArgs ^-> T<unit>
                    "visible", T<bool>
                    "width", T<int>
                    "zIndex", T<int>
                ]
        }

    let Infobox = Type.New()

    let InfoboxClass =
        Class "Microsoft.Maps.Infobox"
        |=> Infobox
        |=> Implements [EntityInterface]
        |+> [
                Constructor Location
                Constructor (Location * InfoboxOptions)
            ]
        |+> Protocol
            [
                "getActions" => T<unit> ^-> Type.ArrayOf InfoboxAction
                |> WithComment "Returns a list of actions, where each item is a name-value pair indicating an action link name and the event name for the action that corresponds to that action link."

                "getAnchor" => T<unit> ^-> Point
                |> WithComment "Returns the point on the infobox which is anchored to the map. An anchor of (0,0) is the top left corner of the infobox."

                "getDescription" => T<unit -> string>
                |> WithComment "Returns the string that is printed inside the infobox."

                "getHeight" => T<unit -> int>
                |> WithComment "Returns the height of the infobox."

                "getHtmlContent" => T<unit -> string>
                |> WithComment "Returns the infobox as HTML."

                "getId" => T<unit -> string>
                |> WithComment "Returns the ID of the infobox."

                "getLocation" => T<unit> ^-> Location
                |> WithComment "Returns the location on the map where the infobox’s anchor is attached."

                "getOffset" => T<unit -> int>
                |> WithComment "Returns the amount the infobox pointer is shifted from the location of the infobox, or if showPointer is false, then it is the amount the infobox bottom left edge is shifted from the location of the infobox. The default value is (0,0), which means there is no offset."

                "getOptions" => T<unit> ^-> InfoboxOptions
                |> WithComment "Returns the infobox options."

                "getShowCloseButton" => T<unit -> bool>
                |> WithComment "Returns a boolean indicating whether the infobox close button is shown."

                "getShowPointer" => T<unit -> bool>
                |> WithComment "Returns a boolean indicating whether the infobox is drawn with a pointer."

                "getTitle" => T<unit -> string>
                |> WithComment "Returns a string that is the title of the infobox."

                "getTitleClickHandler" => T<unit -> string>
                |> WithComment "Returns the name of the function to call when the title of the infobox is clicked."

                "getVisible" => T<unit -> bool>
                |> WithComment "Returns whether the infobox is visible. A value of false indicates that the infobox is hidden, although it is still an entity on the map."

                "getWidth" => T<unit -> int>
                |> WithComment "Returns the width of the infobox."

                "getZIndex" => T<unit -> int>
                |> WithComment "Returns the z-index of the infobox with respect to other items on the map."

                "setHtmlContent" => T<string -> unit>
                |> WithComment "Sets the HTML content of the infobox. You can use this method to change the look of the infobox."

                "setLocation" => Location ^-> T<unit>
                |> WithComment "Sets the location on the map where the anchor of the infobox is attached."

                "setOptions" => InfoboxOptions ^-> T<unit>
                |> WithComment "Sets options for the infobox."

                "toString" => T<unit -> string>
                |> WithComment "Converts the Infobox object to a string."
            ]

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

    let PixelReference = Type.New()

    let PixelReferenceClass =
        Class "Microsoft.Maps.PixelReference"
        |=> PixelReference
        |+> Constants PixelReference ["control"; "page"; "viewport"]
        |+> [
                "isValid" => PixelReference ^-> T<bool>
                |> WithComment "Determines whether the specified reference is a supported PixelReference."
            ]

    let Map = Type.New()

    let MapClass =
        Class "Microsoft.Maps.Map"
        |=> Map
        |=> Implements [EntityInterface]
        |+> [
                Constructor (T<Node> * MapViewOptions)
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

                "getTargetZoom" => T<unit -> int>
                |> WithComment "Returns the zoom level of the view to which the map is navigating."

                "getUserLayer" => T<unit -> Node>
                |> WithComment "Returns the map’s user node."

                "getViewportX" => T<unit -> int>
                |> WithComment "Returns the x coordinate of the viewport origin (the center of the map), relative to the page."

                "getViewportY" => T<unit -> int>
                |> WithComment "Returns the y coordinate of the viewport origin (the center of the map), relative to the page."

                "getWidth" => T<unit -> int>
                |> WithComment "Returns the width of the map control."

                "getZoom" => T<unit -> int>
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

                "tryLocationToPixel" => Type.ArrayOf Location * PixelReference ^-> Type.ArrayOf Point
                |> WithComment "Converts an array of Locations relative to the specified PixelReference and returns an array of Points if all locations were converted. If any of the conversions fail, null is returned."

                "tryLocationToPixel" => Type.ArrayOf Location ^-> Type.ArrayOf Point
                |> WithComment "Converts an array of Locations relative to PixelReference.Viewport and returns an array of Points if all locations were converted. If any of the conversions fail, null is returned."

                "tryPixelToLocation" => Point * PixelReference ^-> Location
                |> WithComment "Converts a specified Point to a Location on the map relative to the specified PixelReference. If the map is not able to convert the Point, null is returned."

                "tryPixelToLocation" => Point ^-> Location
                |> WithComment "Converts a specified Point to a Location on the map relative to PixelReference.Viewport. If the map is not able to convert the Point, null is returned."

                "tryPixelToLocation" => Type.ArrayOf Point * PixelReference ^-> Type.ArrayOf Location
                |> WithComment "Converts an array of Points relative to the specified PixelReference and returns an array of Locations if all points were converted. If any of the conversions fail, null is returned."

                "tryPixelToLocation" => Type.ArrayOf Point ^-> Type.ArrayOf Location
                |> WithComment "Converts an array of Points relative to PixelReference.Viewport and returns an array of Locations if all points were converted. If any of the conversions fail, null is returned."
            ]

    let Coordinates = Type.New()

    let CoordinatesClass =
        Class "Microsoft.Maps.Coordinates"
        |=> Coordinates
        |+> Protocol
            [
                "accuracy" =? T<float>
                "altitude" =? T<float>
                "altitudeAccuracy" =? T<float>
                "heading" =? T<float>
                "latitude" =? T<float>
                "longitude" =? T<float>
                "speed" =? T<float>
            ]

    let Position = Type.New()

    let PositionClass =
        Class "Microsoft.Maps.Position"
        |=> Position
        |+> Protocol
            [
                "coords" =? Coordinates
                "timestamp" =? T<string>
            ]

    let PositionError = Type.New()

    let PositionErrorClass =
        Class "Microsoft.Maps.PositionError"
        |=> PositionError
        |+> Protocol
            [
                "code" =? T<int>
                "message" =? T<string>
            ]

    let PositionErrorCallbackArgs = Type.New()

    let PositionErrorCallbackArgsClass =
        Class "Microsoft.Maps.PositionErrorCallbackArgs"
        |=> PositionErrorCallbackArgs
        |+> Protocol
            [
                "internalError" =? PositionError
                "errorCode" =? T<int>
            ]

    let PositionSuccessCallbackArgs = Type.New()

    let PositionSuccessCallbackArgsClass =
        Class "Microsoft.Maps.PositionSuccessCallbackArgs"
        |=> PositionSuccessCallbackArgs
        |+> Protocol
            [
                "center" =? Location
                "position" =? Position
            ]

    let PositionOptions = Type.New()

    let PositionOptionsClass =
        Pattern.Config "Microsoft.Maps.PositionOptions" {
            Required = []
            Optional =
                [
                    "enableHighAccuracy", T<bool>
                    "errorCallback", PositionErrorCallbackArgs ^-> T<unit>
                    "maximumAge", T<int>
                    "showAccuracyCircle", T<bool>
                    "successCallback", PositionSuccessCallbackArgs ^-> T<unit>
                    "timeout", T<int>
                    "updateMapView", T<bool>
                ]
        }

    let PolygonOptions = Type.New()

    let PolygonOptionsClass =
        Pattern.Config "Microsoft.Maps.PolygonOptions" {
            Required = []
            Optional =
                [
                    "fillColor", Color
                    "strokeColor", Color
                    "strokeThickness", T<float>
                    "visible", T<bool>
                ]
        }
        |+> Protocol
            [
                "strokeDashArray" =@ Type.ArrayOf T<int>
                |> WithSetterInline "$this.strokeDashArray = $1.join(' ')"
                |> WithGetterInline "$this.strokeDashArray.split(' ')"
            ]

    let PositionCircleOptions = Type.New()

    let PositionCircleOptionsClass =
        Pattern.Config "Microsoft.Maps.PositionCircleOptions" {
            Required = []
            Optional =
                [
                    "polygonOptions", PolygonOptions
                    "showOnMap", T<bool>
                ]
        }

    let GeoLocationProvider = Type.New()

    let GeoLocationProviderClass =
        Class "Microsoft.Maps.GeoLocationProvider"
        |=> GeoLocationProvider
        |+> [
                Constructor Map
            ]
        |+> Protocol
            [
                "addAccuracyCircle" => Location * T<float> * T<int> * PositionCircleOptions ^-> T<unit>
                |> WithComment "Renders a geo location accuracy circle on the map. The accuracy circle is created with the center at the specified location, using the given radiusInMeters, and with the specified number of segments for the accuracy circle polygon. Additional options are also available to adjust the style of the polygon."

                "cancelRequest" => T<unit> ^-> T<unit>
                |> WithComment "Cancels the processing of the current getCurrentPosition request. This method prevents the response from being processed."

                "getCurrentPosition" => PositionOptions ^-> T<unit>
                |> WithComment "Obtains the user’s current location and displays it on the map. Important: The accuracy of the user location obtained using this method varies widely depending on the desktop browser or mobile device of the requesting client. Desktop users may experience low user location accuracy (accuracy circles with large radiuses), while mobile user location accuracy may be much greater (a few meters)."

                "removeAccuracyCircle" => T<unit> ^-> T<unit>
                |> WithComment "Removes the current geo location accuracy circle."
            ]

    let PolylineOptions = Type.New()

    let PolylineOptionsClass =
        Pattern.Config "Microsoft.Maps.PolylineOptions" {
            Required = []
            Optional =
                [
                    "strokeColor", Color
                    "strokeThickness", T<float>
                    "visible", T<bool>
                ]
        }
        |+> Protocol
            [
                "strokeDashArray" =@ Type.ArrayOf T<int>
                |> WithSetterInline "$this.strokeDashArray = $1.join(' ')"
                |> WithGetterInline "$this.strokeDashArray.split(' ')"
            ]

    let Polyline = Type.New()

    let PolylineClass =
        Class "Microsoft.Maps.Polyline"
        |=> Polyline
        |=> Implements [EntityInterface]
        |+> [
                Constructor (Type.ArrayOf Location)
                Constructor (Type.ArrayOf Location * PolylineOptions)
            ]
        |+> Protocol
            [
                "getLocations" => T<unit> ^-> Type.ArrayOf Location
                |> WithComment "Returns the locations that define the polyline."

                "getStrokeColor" => T<unit> ^-> Color
                |> WithComment "Returns the color of the polyline."

                "getStrokeDashArray" => T<unit> ^-> Type.ArrayOf T<int>
                |> WithInline "$this.getStrokeDashArray().split(' ')"
                |> WithComment "Returns the string that represents the stroke/gap sequence used to draw the the polyline."

                "getStrokeThickness" => T<unit -> float>
                |> WithComment "Returns the thickness of the polyline."

                "getVisible" => T<unit -> bool>
                |> WithComment "Returns whether the polyline is visible. A value of false indicates that the polyline is hidden, although it is still an entity on the map."

                "setLocations" => Type.ArrayOf Location ^-> T<unit>
                |> WithComment "Sets the locations that define the polyline."

                "setOptions" => PolylineOptions ^-> T<unit>
                |> WithComment "Sets options for the polyline."

                "toString" => T<unit -> string>
                |> WithComment "Converts the Polyline object to a string."
            ]

    let TileLayer = Type.New()
    let TileLayerOptions = Type.New()
    let TileSource = Type.New()
    let TileSourceOptions = Type.New ()

    let TileLayerOptionsClass =
        Pattern.Config "Microsoft.Maps.TileLayerOptions" {
            Required = []
            Optional =
                [
                    "animationDisplay", AnimationVisibility
                    "mercator", TileSource
                    "opacity", T<float>
                    "visible", T<bool>
                    "zIndex", T<float>
                ]
        }
        |=> TileLayerOptions

    let TileSourceOptionsClass =
        Pattern.Config "Microsoft.Maps.TileSourceOptions" {
            Required = []
            Optional =
                [
                    "height", T<float>
                    "uriConstructor", T<string>
                    "width", T<float>
                ]
        }
        |=> TileSourceOptions

    let TileSourceClass =
        Class "Microsoft.Maps.TileSource"
        |=> TileSource
        |+> [
                Constructor TileSourceOptions
                |> WithComment "Initializes a new instance of the TileSource  class."
            ]
        |+> Protocol
            [
                "getHeight" => T<unit -> float>
                |> WithComment "Returns the pixel height of each tile in the tile source."

                "getUriConstructor" => T<unit -> string>
                |> WithComment "Returns a string that constructs tile URLs used to retrieve tiles for the tile layer."

                "getWidth" => T<unit -> float>
                |> WithComment "Returns the pixel width of each tile in the tile source."

                "toString" => T<unit -> string>
                |> WithComment "Converts the Color object to a string."
            ]

    let TileLayerClass =
        Class "Microsoft.Maps.TileLayer"
        |=> TileLayer
        |=> Implements [Entity]
        |+> [
                Constructor TileLayerOptions
                |> WithComment "Initializes a new instance of the TileLayer class."
            ]
        |+> Protocol
            [
                "a" =? T<int>
                |> WithComment "The opacity of the color. The range of valid values is 0 to 255."

                "getOpacity" => T<unit -> float>
                |> WithComment "Returns the opacity of the tile layer, defined as a double between 0 (not visible) and 1."

                "getTileSource" => T<string> ^-> TileSource
                |> WithComment "Returns the tile source of the tile layer. The projection parameter accepts the following values: mercator, enhancedBirdseyeNorthUp, enhancedBirdseyeSouthUp, enhancedBirdseyeEastUp, enhancedBirdseyeWestUp"

                "getZIndex" => T<unit -> float>
                |> WithComment "Returns the z-index of the tile layer with respect to other items on the map."

                "setOptions" => TileLayerOptions ^-> T<unit>
                |> WithComment "Sets options for the tile layer."

                "toString" => T<unit -> string>
                |> WithComment "Converts the TileLayer object to a string."
            ]

    let Polygon = Type.New()

    let PolygonClass =
        Class "Microsoft.Maps.Polygon"
        |=> Polygon
        |=> Implements [EntityInterface]
        |+> [
                Constructor (Type.ArrayOf Location)
                Constructor (Type.ArrayOf Location * PolygonOptions)
            ]
        |+> Protocol
            [
                "getFillColor" => T<unit> ^-> Color
                |> WithComment "Returns the color of the inside of the polygon."

                "getLocations" => T<unit> ^-> Type.ArrayOf Location
                |> WithComment "Returns the locations that define the polygon."

                "getStrokeColor" => T<unit> ^-> Color
                |> WithComment "Returns the color of the polygon."

                "getStrokeDashArray" => T<unit> ^-> Type.ArrayOf T<int>
                |> WithInline "$this.getStrokeDashArray().split(' ')"
                |> WithComment "Returns the string that represents the stroke/gap sequence used to draw the outline of the polygon."

                "getStrokeThickness" => T<unit -> float>
                |> WithComment "Returns the thickness of the polygon."

                "getVisible" => T<unit -> bool>
                |> WithComment "Returns whether the polygon is visible. A value of false indicates that the polygon is hidden, although it is still an entity on the map."

                "setLocations" => Type.ArrayOf Location ^-> T<unit>
                |> WithComment "Sets the locations that define the polygon."

                "setOptions" => PolygonOptions ^-> T<unit>
                |> WithComment "Sets options for the Polygon."

                "toString" => T<unit -> string>
                |> WithComment "Converts the Polygon object to a string."
            ]

    let PushpinOptions = Type.New()

    let PushpinOptionsClass =
        Pattern.Config "Microsoft.Maps.PushpinOptions" {
            Required = []
            Optional =
                [
                    "anchor", Point
                    "draggable", T<bool>
                    "icon", T<string>
                    "height", T<int>
                    "text", T<string>
                    "textOffset", Point
                    "typeName", T<string>
                    "visible", T<bool>
                    "width", T<int>
                    "zIndex", T<int>
                ]
        }

    let Pushpin = Type.New()

    let PushpinClass =
        Class "Microsoft.Maps.Pushpin"
        |=> Pushpin
        |=> Implements [EntityInterface]
        |+> [
                Constructor Location
                Constructor (Location * PushpinOptions)
            ]
        |+> Protocol
            [
                "getAnchor" => T<unit> ^-> Point
                |> WithComment "Returns the point on the pushpin icon which is anchored to the pushpin location. An anchor of (0,0) is the top left corner of the icon."

                "getIcon" => T<unit -> string>
                |> WithComment "Returns the pushpin icon."

                "getHeight" => T<unit -> int>
                |> WithComment "Returns the height of the pushpin, which is the height of the pushpin icon."

                "getLocation" => T<unit> ^-> Location
                |> WithComment "Returns the location of the pushpin."

                "getText" => T<unit -> string>
                |> WithComment "Returns the text associated with the pushpin."

                "getTextOffset" => T<unit> ^-> Point
                |> WithComment "Returns the amount the text is shifted from the pushpin icon."

                "getTypeName" => T<unit -> string>
                |> WithComment "Returns the type of the pushpin."

                "getVisible" => T<unit -> bool>
                |> WithComment "Returns whether the pushpin is visible. A value of false indicates that the pushpin is hidden, although it is still an entity on the map."

                "getWidth" => T<unit -> int>
                |> WithComment "Returns the width of the pushpin, which is the width of the pushpin icon."

                "getZIndex" => T<unit -> int>
                |> WithComment "Returns the z-index of the pushpin with respect to other items on the map."

                "setLocation" => Location ^-> T<unit>
                |> WithComment "Sets the location of the pushpin."

                "setOptions" => PushpinOptions ^-> T<unit>
                |> WithComment "Sets options for the pushpin."

                "toString" => T<unit -> string>
                |> WithComment "Converts the Pushpin object to a string."
            ]

    module Directions =

        let BusinessDetails = Type.New()
        let BusinessDetailsClass =
            Class "Microsoft.Maps.Directions.BusinessDetails"
            |=> BusinessDetails
            |+> Protocol
                [
                    "businessName" =? T<string>
                    "entityId" =? T<string>
                    "phoneNumber" =? T<string>
                    "website" =? T<string>
                ]

        let BusinessDisambiguationSuggestion = Type.New()
        let BusinessDisambiguationSuggestionClass =
            Class "Microsoft.Maps.Directions.BusinessDisambiguationSuggestion"
            |=> BusinessDisambiguationSuggestion
            |+> Protocol
                [
                    "address" =? T<string>
                    "distance" =? T<float>
                    "entityId" =? T<string>
                    "index" =? T<int>
                    "isApproximate" =? T<bool>
                    "location" =? Location
                    "name" =? T<string>
                    "phoneNumber" =? T<string>
                    "photoUrl" =? T<string>
                    "rooftopLocation" =? Location
                    "website" =? T<string>
                ]

        let RouteResponseCode = Type.New()
        let RouteResponseCodeClass =
            Class "Microsoft.Maps.Directions.RouteResponseCode"
            |=> RouteResponseCode
            |+> Constants RouteResponseCode ["success"; "unknownError"; "cannotFindNearbyRoad"; "tooFar"
                                             "noSolution"; "dataSourceNotFound"; "noAvailableTransitTrip"
                                             "transitStopsTooClose"; "walkingBestAlternative"; "outOfTransitBounds"
                                             "timeout"; "waypointDisambiguation"; "hasEmptyWaypoint"
                                             "exceedMaxWaypointLimit"; "atleastTwoWaypointRequired"
                                             "firstOrLastStoppointIsVia"; "searchServiceFailed"]

        let RouteSummary = Type.New()
        let RouteSummaryClass =
            Class "Microsoft.Maps.Directions.RouteSummary"
            |=> RouteSummary
            |+> Protocol
                [
                    "distance" =? T<float>
                    "monetaryCost" =? T<float>
                    "northEast" =? Location
                    "southWest" =? Location
                    "time" =? T<int>
                    "timeWithTraffic" =? T<int>
                ]

        let RouteIconType = Type.New()
        let RouteIconTypeClass =
            Class "Microsoft.Maps.Directions.RouteIconType"
            |=> RouteIconType
            |+> Constants RouteIconType ["none"; "other"; "auto"; "ferry"
                                         "walk"; "bus"; "train"; "airline"]

        let ManeuverType = Type.New()
        let ManeuverTypeClass =
            Class "Microsoft.Maps.Directions.ManeuverType"
            |=> ManeuverType
            |+> Constants ManeuverType ["none"; "unknown"; "transfer"; "wait"
                                        "takeTransit"; "walk"; "transitDepart"
                                        "transitArrive"]

        let TransitLine = Type.New()
        let TransitLineClass =
            Class "Microsoft.Maps.Directions.TransitLine"
            |=> TransitLine
            |+> Protocol
                [
                    "abbreviatedName" =? T<string>
                    "agencyId" =? T<int>
                    "agencyName" =? T<string>
                    "agencyUrl" =? T<string>
                    "lineColor" =? Color
                    "lineTextColor" =? Color
                    "providerInfo" =? T<string>
                    "verboseName" =? T<string>
                ]

        let DirectionsStepWarningType = Type.New()
        let DirectionsStepWarningTypeClass =
            Class "Microsoft.Maps.Directions.DirectionsStepWarningType"
            |=> DirectionsStepWarningType
            |+> Constants DirectionsStepWarningType
                    ["info"; "minor"; "moderate"; "major"; "ccZoneEnter"; "ccZoneExit"]

        let DirectionsStepWarning = Type.New()
        let DirectionsStepWarningClass =
            Class "Microsoft.Maps.Directions.DirectionsStepWarning"
            |=> DirectionsStepWarning
            |+> Protocol
                [
                    "style" =? DirectionsStepWarningType
                    "text" =? T<string>
                ]

        let RoutePath = Type.New()
        let RoutePathClass =
            Class "Microsoft.Maps.Directions.RoutePath"
            |=> RoutePath
            |+> Protocol
                [
                    "decodedLatitudes" =? Type.ArrayOf T<float>
                    "decodedLongitudes" =? Type.ArrayOf T<float>
                    "decodedRegions" =? Type.ArrayOf T<obj>
                ]

        let RouteSubLeg = Type.New()
        let RouteSubLegClass =
            Class "Microsoft.Maps.Directions.RouteSubLeg"
            |=> RouteSubLeg
            |+> Protocol
                [
                    "actualEnd" =? Location
                    "actualStart" =? Location
                    "endDescription" =? T<string>
                    "routePath" =? RoutePath
                    "startDescription" =? T<string>
                    "summary" =? RouteSummary
                ]

        let DirectionsStep = Type.New()
        let DirectionsStepClass =
            Class "Microsoft.Maps.Directions.DirectionsStep"
            |=> DirectionsStep
            |+> Protocol
                [
                    "childItineraryItems" =? Type.ArrayOf DirectionsStep
                    "coordinate" =? Location
                    "distance" =? T<string>
                    "durationInSeconds" =? T<int>
                    "formattedText" =? T<string>
                    "iconType" =? RouteIconType
                    "isImageRoadShield" =? T<bool>
                    "maneuver" =? ManeuverType
                    "maneuverImageName" =? T<string>
                    "monetaryCost" =? T<float>
                    "postIntersectionHints" =? Type.ArrayOf T<string>
                    "preIntersectionHints" =? Type.ArrayOf T<string>
                    "startStopName" =? T<string>
                    "transitLine" =? TransitLine
                    "transitStopId" =? T<string>
                    "transitTerminus" =? T<string>
                    "warnings" =? DirectionsStepWarning
                ]

        let RouteLeg = Type.New()
        let RouteLegClass =
            Class "Microsoft.Maps.Directions.RouteLeg"
            |=> RouteLeg
            |+> Protocol
                [
                    "endTime" =? T<int> //TODO: DateTime
                    "endWaypointLocation" =? Location
                    "itineraryItems" =? Type.ArrayOf DirectionsStep
                    "originalRouteIndex" =? T<int>
                    "startTime" =? T<int>
                    "startWaypointLocation" =? Location
                    "subLegs" =? Type.ArrayOf RouteSubLeg
                    "summary" =? RouteSummary
                ]

        let Route = Type.New()
        let RouteClass =
            Class "Microsoft.Maps.Directions.Route"
            |=> Route
            |+> Protocol
                [
                    "routeLegs" =? Type.ArrayOf RouteLeg
                ]

        let DirectionsEventArgs = Type.New()
        let DirectionsEventArgsClass =
            Class "Microsoft.Maps.Directions.DirectionsEventArgs"
            |=> DirectionsEventArgs
            |+> Protocol
                [
                    "routeSummary" =? Type.ArrayOf RouteSummary
                    "route" =? Type.ArrayOf Route
                ]

        let WaypointOptions = Type.New()
        let WaypointOptionsClass =
            Pattern.Config "Microsoft.Maps.Directions.WaypointOptions" {
                Required = []
                Optional =
                    [
                        "address", T<string>
                        "businessDetails", BusinessDetails
                        "exactLocation", T<bool>
                        "isViapoint", T<bool>
                        "location", Location
                        "shortAddress", T<string>
                    ]
            }

        let LocationDisambiguationSuggestion = Type.New()
        let LocationDisambiguationSuggestionClass =
            Class "Microsoft.Maps.Directions.LocationDisambiguationSuggestion"
            |=> LocationDisambiguationSuggestion
            |+> Protocol
                [
                    "formattedSuggestion" =? T<string>
                    "location" =? Location
                    "rooftopLocation" =? Location
                    "suggestion" =? T<string>
                ]

        let Disambiguation = Type.New()
        let DisambiguationClass =
            Class "Microsoft.Maps.Directions.Disambiguation"
            |=> Disambiguation
            |+> Protocol
                [
                    "businessSuggestions" =? Type.ArrayOf BusinessDisambiguationSuggestion
                    "hasMoreSuggestions" =? T<bool>
                    "headerText" =? T<string>
                    "locationSuggestions" =? Type.ArrayOf LocationDisambiguationSuggestion
                ]

        let Waypoint = Type.New()
        let WaypointClass =
            Class "Microsoft.Maps.Directions.Waypoint"
            |=> Waypoint
            |+> [
                    Constructor WaypointOptions
                ]
            |+> Protocol
                [
                    "clear" => T<unit> ^-> T<unit>
                    "dispose" => T<unit> ^-> T<unit>
                    "getAddress" => T<unit> ^-> T<string>
                    "getBusinessDetails" => T<unit> ^-> BusinessDetails
                    "getDisambiguationResult" => T<unit> ^-> Disambiguation
                    "getLocation" => T<unit> ^-> Location
                    "getShortAddress" => T<unit> ^-> T<string>
                    "isExactLocation" => T<unit> ^-> T<bool>
                    "isViapoint" => T<unit> ^-> T<bool>
                    "setOptions" => WaypointOptions ^-> T<unit>
                ]

        let DirectionsRenderOptions = Type.New()
        let DirectionsRenderOptionsClass =
            Pattern.Config "Microsoft.Maps.Directions.DirectionsRenderOptions" {
                Required = []
                Optional =
                    [
                        "displayDisclaimer", T<bool>
                        "displayManeuverIcons", T<bool>
                        "displayPostItineraryItemHints", T<bool>
                        "displayPreItineraryItemHints", T<bool>
                        "displayRouteSelector", T<bool>
                        "displayStepWarnings", T<bool>
                        "displayTrafficAvoidanceOption", T<bool>
                        "displayWalkingWarning", T<bool>
                        "drivingPolylineOptions", PolylineOptions
                        "itineraryContainer", T<Element>
                        "lastWaypointIcon", T<string>
                        "stepPushpinOptions", PushpinOptions
                        "transitPolylineOptions", PolylineOptions
                        "viapointPushpinsOptions", PushpinOptions
                        "walkingPolylineOptions", PolylineOptions
                        "waypointPushpinOptions", PushpinOptions
                    ]
            }

        let DistanceUnit = Type.New()
        let DistanceUnitClass =
            Class "Microsoft.Maps.Directions.DistanceUnit"
            |=> DistanceUnit
            |+> Constants DistanceUnit ["kilometers"; "miles"]

        let RouteAvoidance = Type.New()
        let RouteAvoidanceClass =
            Class "Microsoft.Maps.Directions.RouteAvoidance"
            |=> RouteAvoidance
            |+> Constants RouteAvoidance ["none"; "minimizeLimitedAccessHighway"; "minimizeToll"
                                          "avoidLimitedAccesHighway"; "avoidToll"; "avoidExpressTrain"
                                          "avoidAirline"; "avoidBulletTrain"]

        let RouteMode = Type.New()
        let RouteModeClass =
            Class "Microsoft.Maps.Directions.RouteMode"
            |=> RouteMode
            |+> Constants RouteMode ["driving"; "transit"; "walking"]

        let RouteOptimization = Type.New()
        let RouteOptimizationClass =
            Class "Microsoft.Maps.Directions.RouteOptimization"
            |=> RouteOptimization
            |+> Constants RouteOptimization ["shortestTime"; "shortestDistance"
                                             "minimizeMoney"; "minimizeTransfers"; "minimizeWalking"]

        let TimeType = Type.New()
        let TimeTypeClass =
            Class "Microsoft.Maps.Directions.TimeType"
            |=> TimeType
            |+> Constants TimeType ["arrival"; "departure"; "lastAvailable"]

        let TransitOptions = Type.New()
        let TransitOptionsClass =
            Pattern.Config "Microsoft.Maps.Directions.TransitOptions" {
                Required = []
                Optional =
                    [
                        "timeType", TimeType
                        "transitTime", T<IntelliFactory.WebSharper.JavaScript.Date>
                    ]
            }

        let DirectionsRequestOptions = Type.New()
        let DirectionsRequestOptionsClass =
            Pattern.Config "Microsoft.Maps.Directions.DirectionsRequestOptions" {
                Required = []
                Optional =
                    [
                        "avoidTraffic", T<bool>
                        "distanceUnit", DistanceUnit
                        "maxRoutes", T<int>
                        "routeAvoidance", Type.ArrayOf RouteAvoidance
                        "routeDraggable", T<bool>
                        "routeIndex", T<int>
                        "routeMode", RouteMode
                        "routeOptimization", RouteOptimization
                        "transitOptions", TransitOptions
                    ]
            }

        let ResetDirectionsOptions = Type.New()
        let ResetDirectionsOptionsClass =
            Pattern.Config "Microsoft.Maps.Directions.ResetDirectionsOptions" {
                Required = []
                Optional =
                    [
                        "removeAllWaypoints", T<bool>
                        "resetRenderOptions", T<bool>
                        "resetRequestOptions", T<bool>
                    ]
            }

        let DirectionsManager = Type.New()
        let DirectionsManagerClass =
            Class "Microsoft.Maps.Directions.DirectionsManager"
            |=> DirectionsManager
            |+> [
                    Constructor Map
                ]
            |+> Protocol
                [
                    "addWaypoint" => Waypoint ^-> T<unit>
                    "calculateDirections" => T<unit> ^-> T<unit>
                    "clearDisplay" => T<unit> ^-> T<unit>
                    "dispose" => T<unit> ^-> T<unit>
                    "getAllWaypoints" => T<unit> ^-> Type.ArrayOf Waypoint
                    "getMap" => T<unit> ^-> Map
                    "getNearbyMajorRoads" => Location * T<obj -> unit> * T<obj -> unit> * T<obj> ^-> T<unit>
                    "getRenderOptions" => T<unit> ^-> DirectionsRenderOptions
                    "getRequestOptions" => T<unit> ^-> DirectionsRequestOptions
                    "getRouteResult" => T<unit> ^-> Type.ArrayOf Route
                    "removeWaypoint" => Waypoint ^-> T<unit>
                    "removeWaypoint" => T<int> ^-> T<unit>
                    "resetDirections" => ResetDirectionsOptions ^-> T<unit>
                    "resetDirections" => T<unit> ^-> T<unit>
                    "reverseGeocode" => Location * T<obj -> unit> * T<obj -> unit> * T<obj> ^-> T<unit>
                    "setMapView" => T<unit> ^-> T<unit>
                    "setRenderOptions" => DirectionsRenderOptions ^-> T<unit>
                    "setRequestOptions" => DirectionsRequestOptions ^-> T<unit>
                ]

        let DirectionsErrorEventArgs = Type.New()
        let DirectionsErrorEventArgsClass =
            Class "Microsoft.Maps.Directionss.DirectionsErrorEventArgs"
            |=> DirectionsErrorEventArgs
            |+> Protocol
                [
                    "responseCode" =? RouteResponseCode
                    "message" =? T<string>
                ]

        let DirectionsStepEventArgs = Type.New()
        let DirectionsStepEventArgsClass =
            Class "Microsoft.Maps.Directions.DirectionsStepEventArgs"
            |=> DirectionsStepEventArgs
            |+> Protocol
                [
                    "handled" =@ T<bool>
                    "location" =? Location
                    "routeIndex" =? T<int>
                    "routeLegIndex" =? T<int>
                    "step" => DirectionsStep
                    "stepIndex" =? T<int>
                    "stepNumber" =? T<int>
                ]

        let DirectionsStepRenderEventArgs = Type.New()
        let DirectionsStepRenderEventArgsClass =
            Class "Microsoft.Maps.Directions.DirectionsStepRenderEventArgs"
            |=> DirectionsStepRenderEventArgs
            |+> Protocol
                [
                    "containerElement" =@ T<Element>
                    "handled" =@ T<bool>
                    "lastStep" =? T<bool>
                    "routeIndex" =? T<int>
                    "routeLegIndex" =? T<int>
                    "step" =? DirectionsStep
                    "stepIndex" =? T<int>
                ]

        let RouteSelectorEventArgs = Type.New()
        let RouteSelectorEventArgsClass =
            Class "Microsoft.Maps.Directions.RouteSelectorEventArgs"
            |=> RouteSelectorEventArgs
            |+> Protocol
                [
                    "handled" =@ T<bool>
                    "routeIndex" =? T<int>
                ]

        let RouteSelectorRenderEventArgs = Type.New()
        let RouteSelectorRenderEventArgsClass =
            Class "Microsoft.Maps.Directions.RouteSelectorRenderEventArgs"
            |=> RouteSelectorRenderEventArgs
            |+> Protocol
                [
                    "containerElement" =@ T<Element>
                    "handled" =? T<bool>
                    "routeIndex" =? T<int>
                    "routeLeg" =? RouteLeg
                ]

        let RouteSummaryRenderEventArgs = Type.New()
        let RouteSummaryRenderEventArgsClass =
            Class "Microsoft.Maps.Directions.RouteSummaryRenderEventArgs"
            |=> RouteSummaryRenderEventArgs
            |+> Protocol
                [
                    "containerElements" =@ T<Element>
                    "handled" =@ T<bool>
                    "routeLegIndex" =? T<int>
                    "summary" =? RouteSummary
                ]

        let WaypointEventArgs = Type.New()
        let WaypointEventArgsClass =
            Class "Microsoft.Maps.Directions.WaypointEventArgs"
            |=> WaypointEventArgs
            |+> Protocol
                [
                    "waypoint" =? Waypoint
                ]

        let WaypointRenderEventArgs = Type.New()
        let WaypointRenderEventArgsClass =
            Class "Microsoft.Maps.Directions.WaypointRenderEventArgs"
            |=> WaypointRenderEventArgs
            |+> Protocol
                [
                    "containerElement" =@ T<Element>
                    "handled" =@ T<bool>
                    "waypoint" =? Waypoint
                ]

        let RouteSelectorRenderEvent = Type.New()
        let RouteSelectorRenderEventClass =
            Class "Microsoft.Maps.Directions.RouteSelectorRenderEvent"
            |=> RouteSelectorRenderEvent
            |+> ConstantStrings RouteSelectorRenderEvent
                    ["afterRouteSelectorRender"; "beforeRouteSelectorRender"]

        let DirectionsStepRenderEvent = Type.New()
        let DirectionsStepRenderEventClass =
            Class "Microsoft.Maps.Directions.DirectionsStepRenderEvent"
            |=> DirectionsStepRenderEvent
            |+> ConstantStrings DirectionsStepRenderEvent
                    ["afterStepRender"; "beforeStepRender"]

        let RouteSummaryRenderEvent = Type.New()
        let RouteSummaryRenderEventClass =
            Class "Microsoft.Maps.Directions.RouteSummaryRenderEvent"
            |=> RouteSummaryRenderEvent
            |+> ConstantStrings RouteSummaryRenderEvent
                    ["afterSummaryRender"; "beforeSummaryRender"]

        let WaypointRenderEvent = Type.New()
        let WaypointRenderEventClass =
            Class "Microsoft.Maps.Directions.WaypointRenderEvent"
            |=> WaypointRenderEvent
            |+> ConstantStrings WaypointRenderEvent
                    ["afterWaypointRender"; "beforeWaypointRender"]

        let DirectionsErrorEvent = Type.New()
        let DirectionsErrorEventClass =
            Class "Microsoft.Maps.Directions.DirectionsErrorEvent"
            |=> DirectionsErrorEvent
            |+> ConstantStrings DirectionsErrorEvent
                    ["directionsError"]

        let DirectionsEvent = Type.New()
        let DirectionsEventClass =
            Class "Microsoft.Maps.Directions.DirectionsEvent"
            |=> DirectionsEvent
            |+> ConstantStrings DirectionsEvent
                    ["directionsUpdated"]

        let RouteSelectorEvent = Type.New()
        let RouteSelectorEventClass =
            Class "Microsoft.Maps.Directions.RouteSelectorEvent"
            |=> RouteSelectorEvent
            |+> ConstantStrings RouteSelectorEvent
                    ["mouseEnterRouteSelector"; "mouseLeaveRouteSelector"; "routeSelectorClicked"]

        let DirectionsStepEvent = Type.New()
        let DirectionsStepEventClass =
            Class "Microsoft.Maps.Directions.DirectionsStepEvent"
            |=> DirectionsStepEvent
            |+> ConstantStrings DirectionsStepEvent
                    ["itineraryStepClicked"; "mouseEnterStep"; "mouseLeaveStep"]

        let WaypointEvent = Type.New()
        let WaypointEventClass =
            Class "Microsoft.Maps.Directions.WaypointEvent"
            |=> WaypointEvent
            |+> ConstantStrings WaypointEvent
                    ["waypointAdded"; "waypointRemoved"; "changed"; "geocoded"; "reverseGeocoded"]

    module Traffic =

        let TrafficLayer = Type.New()
        let TrafficLayerClass =
            Class "Microsoft.Maps.Traffic.TrafficLayer"
            |=> TrafficLayer
            |+> [
                    Constructor Map
                ]
            |+> Protocol
                [
                    "getTileLayer" => T<unit> ^-> TileLayer
                    "hide" => T<unit> ^-> T<unit>
                    "show" => T<unit> ^-> T<unit>
                ]


    module VenueMaps =

        let Primitive = Type.New()

        let Floor = Type.New()
        let FloorClass =
            Class "Microsoft.Maps.VenueMaps.Floor"
            |=> Floor
            |+> Protocol
                [
                    "name" =? T<string>
                    "primitives" =? Type.ArrayOf Primitive
                    "zoomRange" =? Type.ArrayOf T<float>
                ]

        let PrimitiveClass =
            Class "Microsoft.Maps.VenueMaps.Primitive"
            |=> Primitive
            |+> Protocol
                [
                    "bounds" =? LocationRect
                    "businessId" =? T<string>
                    "categoryId" =? T<string>
                    "categoryName" =? T<string>
                    "center" =? Location
                    "floor" =? Floor
                    "id" =? T<string>
                    "locations" =? Type.ArrayOf Location
                    "name" =? T<string>
                    "highlight" => T<unit> ^-> T<unit>
                    "unhighlight" => T<unit> ^-> T<unit>
                ]

        let Polygon = Type.New()
        let PolygonClass =
            Class "Microsoft.Maps.VenueMaps.Polygon"
            |=> Polygon
            |+> Protocol
                [
                    "bounds" =? LocationRect
                    "center" =? Location
                    "locations" =? Type.ArrayOf Location
                ]

        let Footprint = Type.New()
        let FootprintClass =
            Class "Microsoft.Maps.VenueMaps.Footprint"
            |=> Footprint
            |+> Protocol
                [
                    "polygons" =? Type.ArrayOf Polygon
                    "zoomRange" =? Type.ArrayOf T<float>
                ]

        let Metadata = Type.New()
        let MetadataClass =
            Class "Microsoft.Maps.VenueMaps.Metadata"
            |=> Metadata
            |+> Protocol
                [
                    "CenterLat" =? T<float>
                    "CenterLong" =? T<float>
                    "DefaultFloor" =? T<string>
                    "FloorHeader" =? T<string>
                    "Floors" =? Type.ArrayOf Floor
                    "Footprint" =? Footprint
                    "MapId" =? T<string>
                    "MapType" =? T<string>
                    "Name" =? T<string>
                    "YpId" =? T<string>
                ]

        let NearbyVenue = Type.New()
        let NearbyVenueClass =
            Class "Microsoft.Maps.VenueMaps.NearbyVenue"
            |=> NearbyVenue
            |+> Protocol
                [
                    "distance" =? T<float>
                    "metadata" =? Metadata
                ]

        let VenueMap = Type.New()
        let VenueMapClass =
            Class "Microsoft.Maps.VenueMaps.VenueMap"
            |=> VenueMap
            |+> Protocol
                [
                    "address" =? T<string>
                    "bestMapView" =? ViewOptions
                    "businessId" =? T<string>
                    "center" =? Location
                    "defaultFloor" =? T<string>
                    "floorHeader" =? T<string>
                    "floors" =? Type.ArrayOf Floor
                    "footprint" =? Footprint
                    "id" =? T<string>
                    "name" =? T<string>
                    "phoneNumber" =? T<string>
                    "type" =? T<string>
                    "dispose" => T<unit> ^-> T<unit>
                    "getActiveFloor" => T<unit> ^-> T<string>
                    "hide" => T<unit> ^-> T<unit>
                    "setActiveFloor" => T<string> * T<string> ^-> T<unit>
                    "show" => T<unit> ^-> T<unit>
                ]

        let NearbyVenueMapOptions = Type.New()
        let NearbyVenueMapOptionsClass =
            Pattern.Config "Microsoft.Maps.VenueMaps.NearbyVenueMapOptions" {
                Required = []
                Optional =
                    [
                        "callback", Type.ArrayOf NearbyVenue ^-> T<unit>
                        "location", Location
                        "map", Map
                        "radius", T<float>
                    ]
            }

        let VenueMapCreationOptions = Type.New()
        let VenueMapCreationOptionsClass =
            Pattern.Config "Microsoft.Maps.VenueMaps.VenueMapCreationOptions" {
                Required = []
                Optional =
                    [
                        "error", T<int> * VenueMapCreationOptions ^-> T<unit>
                        "success", VenueMap * VenueMapCreationOptions ^-> T<unit>
                        "venueMapId", T<string>
                    ]
            }

        let VenueMapFactory = Type.New()
        let VenueMapFactoryClass =
            Class "Microsoft.Maps.VenueMaps.VenueMapFactory"
            |=> VenueMapFactory
            |+> [
                    Constructor Map
                ]
            |+> Protocol
                [
                    "create" => VenueMapCreationOptions ^-> T<unit>
                    "getNearbyVenues" => NearbyVenueMapOptions ^-> T<unit>
                ]


    let EventsClass =
        Class "Microsoft.Maps.Events"
        |=> Events
        |+> [
                "addHandler" => Entity * KeyEvent * (KeyEventArgs ^-> T<unit>) ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target."

                "addHandler" => Entity * MouseEvent * (MouseEventArgs ^-> T<unit>) ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target."

                "addHandler" => Entity * Directions.RouteSelectorRenderEvent * (Directions.RouteSelectorRenderEventArgs ^-> T<unit>) ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target."

                "addHandler" => Entity * Directions.DirectionsStepRenderEvent * (Directions.DirectionsStepRenderEventArgs ^-> T<unit>) ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target."

                "addHandler" => Entity * Directions.RouteSummaryRenderEvent * (Directions.RouteSummaryRenderEventArgs ^-> T<unit>) ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target."

                "addHandler" => Entity * Directions.WaypointRenderEvent * (Directions.WaypointRenderEventArgs ^-> T<unit>) ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target."

                "addHandler" => Entity * Directions.DirectionsErrorEvent * (Directions.DirectionsErrorEventArgs ^-> T<unit>) ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target."

                "addHandler" => Entity * Directions.DirectionsEvent * (Directions.DirectionsEventArgs ^-> T<unit>) ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target."

                "addHandler" => Entity * Directions.RouteSelectorEvent * (Directions.RouteSelectorEventArgs ^-> T<unit>) ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target."

                "addHandler" => Entity * Directions.DirectionsStepEvent * (Directions.DirectionsStepEventArgs ^-> T<unit>) ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target."

                "addHandler" => Entity * Directions.WaypointEvent * (Directions.WaypointEventArgs ^-> T<unit>) ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target."

                "addHandler" => Entity * UnitEvent * T<unit -> unit> ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target."

                "addThrottledHandler" => Entity * KeyEvent * (KeyEventArgs ^-> T<unit>) * T<float> ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target, where the minimum interval between events (in milliseconds) is specified in the ThrottleInterval parameter. The last occurrence of the event is called after the specified ThrottleInterval."

                "addThrottledHandler" => Entity * MouseEvent * (MouseEventArgs ^-> T<unit>) * T<float> ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target, where the minimum interval between events (in milliseconds) is specified in the ThrottleInterval parameter. The last occurrence of the event is called after the specified ThrottleInterval."

                "addThrottledHandler" => Entity * UnitEvent * T<unit -> unit> * T<float> ^-> EventHandler
                |> WithComment "Attaches the handler for the event that is thrown by the target, where the minimum interval between events (in milliseconds) is specified in the ThrottleInterval parameter. The last occurrence of the event is called after the specified ThrottleInterval."

                "hasHandler" => Entity * Event ^-> T<bool>
                |> WithComment "Checks if the target has any attached event handler."

                "invoke" => Entity * Event ^-> T<unit>
                |> WithComment "Invokes an event on the target. This causes all handlers for the specified eventName to be called."

                "removeHandler" => EventHandler ^-> T<unit>
                |> WithComment "Detaches the specified handler from the event."
            ]

    ///////////////////////////////////////////////////////////////////
    // REST Locations API

    let AuthenticationResultCode = Type.New()
    let RestResponse = Type.New()
    let ResourceSet = Type.New()
    let Address = Type.New()
    let LocationResource = Type.New()
    let Confidence = Type.New()
    let PointResource = Type.New()

    let RestResponseClass =
        Class "Microsoft.Maps.RestResponse"
        |=> RestResponse
        |+> Protocol
            [
                "statusCode" =? T<int>
                |> WithComment "The HTTP Status code for the request."

                "statusDescription" =? T<string>
                |> WithComment "A description of the HTTP status code."

                "authenticationResultCode" =? AuthenticationResultCode
                |> WithComment "A status code that offers additional information about authentication success or failure."

                "traceId" =? T<string>
                |> WithComment "A unique identifier for the request."

                "copyright" =? T<string>
                |> WithComment "A copyright notice."

                "brandLogoUri" =? T<string>
                |> WithComment "A URL that references a brand image to support contractual branding requirements."

                "resourceSets" =? Type.ArrayOf ResourceSet
                |> WithComment "A collection of ResourceSet objects. A ResourceSet is a container of Resources returned by the request. For more information, see the ResourceSet section below."

                "errorDetails" =? Type.ArrayOf T<string>
                |> WithComment "A collection of error descriptions. For example, ErrorDetails can identify parameter values that are not valid or missing."
            ]

    let AuthenticationResultCodeClass =
        Class "Microsoft.Maps.AuthenticationResultCode"
        |=> AuthenticationResultCode
        |+> ConstantStrings AuthenticationResultCode
            ["ValidCredentials"; "InvalidCredentials"; "CredentialsExpired"; "NotAuthorized"; "NoCredentials"]

    let ResourceSetClass =
        Class "Microsoft.Maps.ResourceSet"
        |=> ResourceSet
        |+> Protocol
            [
                "estimatedTotal" =? T<int>
                |> WithComment "An estimate of the total number of resources in the ResourceSet."

                "resources" =? Type.ArrayOf T<obj>
                |> WithComment "A collection of one or more resources. The resources that are returned depend on the request. Information about resources is provided in the API reference for each Bing Maps REST Services API."
            ]

    let LocationResourceClass =
        Class "Microsoft.Maps.LocationResource"
        |=> LocationResource
        |+> Protocol
            [
                "name" =? T<string>
                |> WithComment "The name of the resource."

                "point" =? PointResource
                |> WithComment "The latitude and longitude coordinates of the location."

                "bbox" =? Type.ArrayOf T<float>
                |> WithComment "A geographic area that contains the location. A bounding box contains SouthLatitude, WestLongitude, NorthLatitude, and EastLongitude values in units of degrees."

                "entityType" =? T<string>
                |> WithComment "The classification of the geographic entity returned, such as Address."

                "address" =? Address
                |> WithComment "The postal address for the location. An address can contain AddressLine, AdminDistrict, AdminDistrict2, CountryRegion, FormattedAddress, Locality, and PostalCode fields."

                "confidence" =? Confidence
                |> WithComment "The confidence in the match."
            ]

    let PointResourceClass =
        Class "Microsoft.Maps.PointResource"
        |=> PointResource
        |+> Protocol
            [
                "type" =? T<string>
                "coordinates" =? Type.ArrayOf T<float>
            ]

    let ConfidenceClass =
        Class "Microsoft.Maps.Confidence"
        |=> Confidence
        |+> ConstantStrings Confidence ["High"; "Medium"; "Low"; "Unknown"]

    let AddressClass =
        Pattern.Config "Microsoft.Maps.Address" {
            Required = []
            Optional =
                [
                    "adminDistrict", T<string>
                    "locality", T<string>
                    "postalCode", T<string>
                    "addressLine", T<string>
                    "countryRegion", T<string>
                ]
        }
        |=> Address

    ///////////////////////////////////////////////////////////////////
    // REST Routes API

    let RouteResource = Type.New()
    let RouteLeg = Type.New()
    let ItineraryItem = Type.New()
    let ItineraryDetail = Type.New()
    let ItineraryIcon = Type.New()
    let ItineraryInstruction = Type.New()
    let TransitLine = Type.New()
    let RoutePath = Type.New()
    let LineResource = Type.New()
    let RouteRequest = Type.New()
    let RouteFromMajorRoadsRequest = Type.New()
    let RouteOptimize = Type.New()
    let RouteAvoid = Type.New()
    let RoutePathOutput = Type.New()
    let DistanceUnit = Type.New()
    let TimeType = Type.New()
    let TravelMode = Type.New()

    let WaypointClass =
        Class "Microsoft.Maps.Waypoint"
        |=> Waypoint
        |+> [
                Constructor T<string>?s
                |> WithInline "$s"

                Constructor Point?p
                |> WithInline "($p.x+','+$p.y)"
            ]

    let RouteResourceClass =
        Class "Microsoft.Maps.RouteResource"
        |=> RouteResource
        |+> Protocol
            [
                "id" =? T<string>
                |> WithComment "A unique ID for the resource."

                "bbox" =? Type.ArrayOf T<float>
                |> WithComment "Defines a rectangular area by using latitude and longitude boundaries that contain the corresponding route or location. A bounding box contains SouthLatitude, WestLongitude, NorthLatitude, and EastLongitude elements."

                "distanceUnit" =? DistanceUnit
                |> WithComment "The unit used for distance."

                "durationUnit" =? T<string>
                |> WithComment "The unit used for time of travel."

                "travelDistance" =? T<float>
                |> WithComment "The physical distance covered by the entire route. Note: This value is not supported for the Transit travel mode."

                "travelDuration" =? T<float>
                |> WithComment "The time in seconds that it takes to travel a corresponding TravelDistance."

                "routeLegs" =? Type.ArrayOf RouteLeg
                |> WithComment "Information about a section of a route between two waypoints. For more information about the fields contained ina routeLeg, see the Route Leg Fields section below."

                "routePath" =? RoutePath
                |> WithComment "A representation of the path of a route. A RoutePath is returned only if the routePathOutput parameter is set to Points. A RoutePath is defined by a Line element that contains of a collection of points. The path of the route is the line that connects these Points. For more information about the fields contained in a route Path, see the Route Path Fields section below."
            ]

    let RouteLegClass =
        Class "Microsoft.Maps.RouteLeg"
        |=> RouteLeg
        |+> Protocol
            [
                "travelDistance" =? T<float>
                |> WithComment "The physical distance covered by a route leg."

                "travelDuration" =? T<float>
                |> WithComment "The time, in seconds, that it takes to travel a corresponding TravelDistance."

                "actualStart" =? PointResource
                |> WithComment "The Point (latitude and longitude) that was used as the actual starting location for the route leg. In most cases, the ActualStart is the same as the requested waypoint. However, if a waypoint is not close to a road, the Routes API chooses a location on the nearest road as the starting point of the route. This ActualStart element contains the latitude and longitude of this new location."

                "actualEnd" =? PointResource
                |> WithComment "The Point (latitude and longitude) that was used as the actual ending location for the route leg. In most cases, the ActualEnd is the same as the requested waypoint. However, if a waypoint is not close to a road, the Routes API chooses a location on the nearest road as the ending point of the route. This ActualEnd element contains the latitude and longitude of this new location."

                "startLocation" =? Type.ArrayOf LocationResource
                |> WithComment "Information about the location of the starting waypoint for a route. A StartLocation is provided only when the waypoint is specified as a landmark or an address. For more information about the fields contained in a Location collection, see the Location Fields table below."

                "endLocation" =? Type.ArrayOf LocationResource
                |> WithComment "Information about the location of the ending waypoint for a route. An EndLocation is provided only when the waypoint is specified as a landmark or an address. For more information about the fields contained in a Location collection, see the Locations Fields table below."

                "itineraryItems" =? Type.ArrayOf ItineraryItem
                |> WithComment "Information that defines a step in the route. For information about the fields that make up the ItineraryItem collection, see the Itinerary Item Fields table below."
            ]

    let ItineraryItemClass =
        Class "Microsoft.Maps.ItineraryItem"
        |=> ItineraryItem
        |+> Protocol
            [
                "childItineraryItems" =? Type.ArrayOf ItineraryItem
                |> WithComment "A collection of ItineraryItems that divides an itinerary item into smaller steps. An itinerary item can have only one set of ChildItineraryItems."

                "compassDirection" =? T<string>
                |> WithComment "The direction of travel associated with a maneuver on a route, such as south or southwest. Note: This value is not supported for the Transit travel mode."

                "details" =? Type.ArrayOf ItineraryDetail
                |> WithComment "Information about one of the maneuvers that is part of the itinerary item. An ItineraryItem can contain more than one Detail collection. For information about the fields contained in a Detail collection, see the Detail Fields table below."

                "exit" =? T<string>
                |> WithComment "The name or number of the exit associated with this itinerary step."

                "hints" =? T<string>
                |> WithComment "Additional information that may be helpful in following a route. In addition to the hint text, this element has an attribute hintType that specifies what the hint refers to, such as “NextIntersection.” Hint is an optional element and a route step can contain more than one hint."

                "iconType" =? ItineraryIcon
                |> WithComment "The type of icon to display."

                "instruction" =? ItineraryInstruction
                |> WithComment "A description of a maneuver in a set of directions. In addition to the content of the instruction field, this field has an attribute maneuverType that is set to the type of maneuver, such as 'TurnLeft.'"

                "maneuverPoint" =? PointResource
                |> WithComment "The coordinates of a point on the Earth where a maneuver is required, such as a left turn. A ManeuverPoint contains Latitude and Longitude elements. Note: This value is not supported for ItineraryItems that are part of a ChildItineraryItems collection."

                "sideOfStreet" =? T<string>
                |> WithComment "The side of the street where the destination is found based on the arrival direction. This field applies to the last itinerary item only."

                "signs" =? T<string>
                |> WithComment "Signage text for the route. There may be more than one sign value for an itinerary item."

                "time" =? T<string>
                |> WithComment "The arrival or departure time for the transit step."

                "tollZone" =? T<string>
                |> WithComment "The name or number of the toll zone."

                "towardsRoadName" =? T<string>
                |> WithComment "The name of the street that the route goes towards in the first itinerary item."

                "transitLine" =? Type.ArrayOf TransitLine
                |> WithComment "Information about the transit line associated with the itinerary item. For more information about the fields contained in the TransitLine collection, see the Transit Line Fields table below."

                "transitStopId" =? T<string>
                |> WithComment "The ID assigned to the transit stop by the transit agency."

                "transitTerminus" =? T<string>
                |> WithComment "The end destination for the transit line in the direction you are traveling."

                "travelDistance" =? T<float>
                |> WithComment "The physical distance covered by this route step. Note: This value is not supported for the Transit travel mode."

                "travelDuration" =? T<float>
                |> WithComment "The time in seconds that it takes to travel a corresponding TravelDistance."

                "travelMode" =? T<string>
                |> WithComment "The mode of travel for a specific step in the route. Note: This value is not supported for ItineraryItems that are part of a ChildItineraryItems collection."

                "warnings" =? T<string>
                |> WithComment "Information about a condition that may affect a specific step in the route. Warning is an optional element and a route step can contain more than one warning."
            ]

    let ItineraryInstructionClass =
        Class "Microsoft.Maps.ItineraryInstruction"
        |=> ItineraryInstruction
        |+> Protocol
            [
                "maneuverType" =? T<string>
                "text" =? T<string>
            ]

    let ItineraryIconClass =
        Class "Microsoft.Maps.ItineraryIcon"
        |=> ItineraryIcon
        |+> ConstantStrings ItineraryIcon ["None"; "Airline"; "Auto"; "Bus"
                                           "Ferry"; "Train"; "Walk"; "Other"]

    let ItineraryDetailClass =
        Class "Microsoft.Maps.ItineraryDetail"
        |=> ItineraryDetail
        |+> Protocol
            [
                "compassDegrees" =? T<string>
                |> WithComment "The direction in degrees. Note: This value is not supported for the Transit travel mode."

                "maneuverType" =? T<string>
                |> WithComment "The type of maneuver described by this detail collection. The ManeuverType in A detail collection can provide information for a portion of the maneuver described by the maneuverType attribute of the corresponding Instruction. For example the maneuverType attribute of an Instruction may specify TurnLeftThenTurnRight as the maneuver while the associated detail items may specify specifics about the TurnLeft and TurnRight maneuvers."

                "names" =? T<string>
                |> WithComment "A street, highway or intersection where the maneuver occurs. If the maneuver is complex, there may be more than one name field in the details collection. The name field may also have no value. This can occur if the name is not known or if a street, highway or intersection does not have a name. Note: This value is only supported for the transit travel mode."

                "startPathIndices" =? Type.ArrayOf T<int>
                |> WithComment "StartPathIndices and EndPathIndices specify index values for specific route path points that are returned in the response when the routePathOutput parameter is set to Points. Together, these two index values define a range of route path points that correspond to a maneuver. Route path index values are integers where the first route path point has an index value of 0."

                "endPathIndices" =? Type.ArrayOf T<int>
                |> WithComment "StartPathIndices and EndPathIndices specify index values for specific route path points that are returned in the response when the routePathOutput parameter is set to Points. Together, these two index values define a range of route path points that correspond to a maneuver. Route path index values are integers where the first route path point has an index value of 0."

                "roadType" =? T<string>
                |> WithComment "The type of road."
            ]

    let TransitLineClass =
        Class "Microsoft.Maps.TransitLine"
        |=> TransitLine
        |+> Protocol
            [
                "verboseName" =? T<string>
                |> WithComment "The full name of the transit line."

                "abbreviatedName" =? T<string>
                |> WithComment "The abbreviated name of the transit line, such as the bus number."

                "agencyId" =? T<string>
                |> WithComment "The ID associated with the transit agency."

                "agencyName" =? T<string>
                |> WithComment "The name of the transit agency."

                "lineColor" =? T<string>
                |> WithComment "The color associated with the transit line. The color is provided as an RGB value."

                "lineTextColor" =? T<string>
                |> WithComment "The color to use for text associated with the transit line. The color is provided as an RGB value."

                "uri" =? T<string>
                |> WithComment "The URI for the transit agency."

                "phoneNumber" =? T<string>
                |> WithComment "The phone number of the transit agency."

                "providerInfo" =? T<string>
                |> WithComment "The contact information for the provider of the transit information."
            ]

    let RoutePathClass =
        Class "Microsoft.Maps.RoutePath"
        |=> RoutePath
        |+> Protocol
            [
                "line" =? LineResource
                |> WithComment "When the points in the line are connected, they represent the path of the route."

                "point" =? PointResource
                |> WithComment "The coordinates of a point on the Earth."
            ]

    let LineResourceClass =
        Class "Microsoft.Maps.LineResource"
        |=> LineResource
        |+> Protocol
            [
                "type" =? T<string>
                "coordinates" =? Type.ArrayOf (Type.ArrayOf T<float>)
            ]

    let RouteRequestClass =
        Pattern.Config "Microsoft.Maps.RouteRequest" {
            Required = []
            Optional =
                [
                    "waypoints", Type.ArrayOf Waypoint
                    "avoid", Type.ArrayOf RouteAvoid
                    "heading", T<int>
                    "optimize", RouteOptimize
                    "routePathOutput", RoutePathOutput
                    "distanceUnit", DistanceUnit
                    "dateTime", T<string>
                    "timeType", TimeType
                    "maxSolutions", T<int>
                    "travelMode", TravelMode
                ]
        }

    let RouteOptimizeClass =
        Class "Microsoft.Maps.RouteOptimize"
        |=> RouteOptimize
        |+> ConstantStrings RouteOptimize ["distance"; "time"; "timeWithTraffic"]

    let RouteAvoidClass =
        Class "Microsoft.Maps.RouteAvoid"
        |=> RouteAvoid
        |+> ConstantStrings RouteAvoid ["highways"; "tolls"; "minimizeHighways"; "minimizeTolls"]

    let RoutePathOutputClass =
        Class "Microsoft.Maps.RoutePathOutput"
        |=> RoutePathOutput
        |+> ConstantStrings RoutePathOutput ["Points"; "None"]

    let DistanceUnitClass =
        Class "Microsoft.Maps.DistanceUnit"
        |=> DistanceUnit
        |+> ConstantStrings DistanceUnit ["Mile"; "Kilometer"]

    let TimeTypeClass =
        Class "Microsoft.Maps.TimeType"
        |=> TimeType
        |+> ConstantStrings TimeType ["Arrival"; "Departure"; "LastAvailable"]

    let TravelModeClass =
        Class "Microsoft.Maps.TravelMode"
        |=> TravelMode
        |+> ConstantStrings TravelMode ["Driving"; "Walking"; "Transit"]

    let RouteFromMajorRoadsRequestClass =
        Pattern.Config "Microsoft.Maps.RouteFromMajorRoadsRequest" {
            Required =
                [
                    "destination", Waypoint
                ]
            Optional =
                [
                    "exclude", T<string>
                    "routePathOutput", RoutePathOutput
                    "distanceUnit", DistanceUnit
                ]
        }
        |=> RouteFromMajorRoadsRequest

    ///////////////////////////////////////////////////////////////////
    // REST Imagery API

    let StaticMapRequest = Type.New()
    let ImagerySet = Type.New()
    let MapLayer = Type.New()
    let MapVersion = Type.New()
    let PushpinRequest = Type.New()

    let StaticMapRequestClass =
        Pattern.Config "Microsoft.Maps.StaticMapRequest" {
            Required =
                [
                    "imagerySet", ImagerySet
                ]
            Optional =
                [
                    "avoid", Type.ArrayOf RouteAvoid
                    "centerPoint", Point
                    "dateTime", T<string>
                    "declutterPins", T<bool>
                    "distanceBeforeFirstTurn", T<int>
                    "mapArea", Point * Point
                    "mapLayer", MapLayer
                    "mapSize", T<int> * T<int>
                    "mapVersion", MapVersion
                    "maxSolutions", T<int>
                    "optimize", RouteOptimize
                    "pushpin", Type.ArrayOf PushpinRequest
                    "query", T<string>
                    "timeType", TimeType
                    "travelMode", TravelMode
                    "waypoints", Type.ArrayOf Waypoint
                    "zoomLevel", T<int>
                ]
        }
        |=> StaticMapRequest

    let ImagerySetClass =
        Class "Microsoft.Maps.ImagerySet"
        |=> ImagerySet
        |+> ConstantStrings ImagerySet ["Aerial"; "AerialWithLabels"; "Road"]

    let MapLayerClass =
        Class "Microsoft.Maps.MapLayer"
        |=> MapLayer
        |+> ConstantStrings MapLayer ["TrafficFlow"]

    let MapVersionClass =
        Class "Microsoft.Maps.MapVersion"
        |=> MapVersion
        |+> ConstantStrings MapVersion ["v0"; "v1"]

    let PushpinRequestClass =
        Pattern.Config "Microsoft.Maps.PushpinRequest" {
            Required =
                [
                    "x", T<float>
                    "y", T<float>
                ]
            Optional =
                [
                    "iconStyle", T<int>
                    "label", T<string>
                ]
        }
        |=> PushpinRequest

    let ImageryMetadataRequest = Type.New()
    let ImageryMetadataInclude = Type.New()
    let ImageryMetadataResource = Type.New()

    let ImageryMetadataRequestClass =
        Pattern.Config "Microsoft.Maps.ImageryMetadataRequest" {
            Required =
                [
                    "imagerySet", ImagerySet
                ]
            Optional =
                [
                    "centerPoint", Point
                    "include", ImageryMetadataInclude
                    "mapVersion", MapVersion
                    "orientation", T<float>
                    "zoomLevel", T<int>
                ]
        }
        |=> ImageryMetadataRequest

    let ImageryMetadataIncludeClass =
        Class "Microsoft.Maps.ImageryMetadataInclude"
        |=> ImageryMetadataInclude
        |+> ConstantStrings ImageryMetadataInclude ["ImageryProviders"]

    let ImageryMetadataResourceClass =
        Class "Microsoft.Maps.ImageryMetadataResource"
        |=> ImageryMetadataResource
        |+> Protocol
            [
                "imageHeight" =? T<int>
                |> WithComment "The height of the image tile."

                "imageWidth" =? T<int>
                |> WithComment "The width of the image tile."

                "imageUrl" =? T<string>
                |> WithComment "Either a URL template for an image tile if a specific point is specified, or a general URL template for the specified imagery set."

                "imageUrlSubdomains" =? Type.ArrayOf T<string>
                |> WithComment "One or more URL subdomains that may be used when constructing an image tile URL."

                "imageryProviders" =? T<obj>

                "vintageStart" =? T<string>
                |> WithComment "The earliest date found in an imagery set or for a specific imagery tile."

                "vintageEnd" =? T<string>
                |> WithComment "The latest date found in an imagery set or for a specific imagery tile."

                "zoomMax" =? T<int>
                |> WithComment "The maximum zoom level available for this imagery set."

                "zoomMin" =? T<int>
                |> WithComment "The minimum zoom level available for this imagery set."

                "orientation" =? T<float>
                |> WithComment "The orientation of the viewport for the imagery metadata in degrees where 0 = North [default], 90 = East, 180 = South, 270 = West."

                "tilesX" =? T<int>
                |> WithComment "The horizontal dimension of the imagery in number of tiles."

                "tilesY" =? T<int>
                |> WithComment "The vertical dimension of the imagery in number of tiles."
            ]

    let LoadModuleArgs = Type.New()

    let LoadModuleArgsClass =
        Pattern.Config "Microsoft.Maps.LoadModuleArgs" {
                Required =
                    [
                        "callback", T<unit -> unit>
                    ]
                Optional = []
            }
        |=> LoadModuleArgs

    let MapsStatics =
        Class "Microsoft.Maps"
        |+> [
                "loadModule" => T<string> ^-> T<unit>
                |> WithComment "Loads the specified registered module, making its functionality available."

                "loadModule" => T<string> * LoadModuleArgs ^-> T<unit>
                |> WithComment "Loads the specified registered module, making its functionality available. A function is specified that is called when the module is loaded."

                "moduleLoaded" => T<string> ^-> T<unit>
                |> WithComment "Signals that the specified module has been loaded and if specified, calls the callback function in loadModule. Call this method at the end of your module script."

                "registerModule" => T<string> * T<string> ^-> T<unit>
                |> WithComment "Registers a module with the map control. The name of the module is specified in moduleKey, the module script is defined in scriptUrl."

                "registerModule" => T<string> * T<string> * T<string> ^-> T<unit>
                |> WithComment "Registers a module with the map control. The name of the module is specified in moduleKey, the module script is defined in scriptUrl, and the options provides the location of a *.css file to load with the module."
            ]

    let Assembly =
        Assembly [
            Namespace "IntelliFactory.WebSharper.Bing.Maps.Resources" [
                (Resource "Js" "http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=7.0")
                    .AssemblyWide()
            ]
            Namespace "IntelliFactory.WebSharper.Bing.Maps" [
                AltitudeReferenceClass
                ColorClass
                CoordinatesClass
                EntityCollectionClass
                EntityCollectionEventArgsClass
                EntityCollectionEventClass
                EntityCollectionOptionsClass
                EntityInterface
                EventClass
                EventHandlerClass
                EventsClass
                GeoLocationProviderClass
                InfoboxActionClass
                InfoboxClass
                InfoboxOptionsClass
                KeyEventArgsClass
                KeyEventClass
                LabelOverlayClass
                LocationClass
                LocationRectClass
                MapClass
                MapOptionsClass
                MapTypeIdClass
                MapViewOptionsClass
                MouseEventArgsClass
                MouseEventClass
                PointClass
                PolygonClass
                PolygonOptionsClass
                PolylineClass
                PolylineOptionsClass
                PushpinClass
                PushpinOptionsClass
                RangeClass
                SizeClass
                TileLayerClass
                TileLayerOptionsClass
                TileSourceClass
                TileSourceOptionsClass
                UnitEventClass
                ViewOptionsClass
                LoadModuleArgsClass
                MapsStatics

                // REST locations
                AddressClass
                AuthenticationResultCodeClass
                ConfidenceClass
                LocationResourceClass
                PointResourceClass
                ResourceSetClass
                RestResponseClass
                WaypointClass

                // REST Routes
                DistanceUnitClass
                ItineraryDetailClass
                ItineraryIconClass
                ItineraryInstructionClass
                ItineraryItemClass
                LineResourceClass
                RouteAvoidClass
                RouteLegClass
                RouteOptimizeClass
                RoutePathClass
                RoutePathOutputClass
                RouteRequestClass
                RouteResourceClass
                TimeTypeClass
                TransitLineClass
                TravelModeClass

                // REST Imagery
                ImageryMetadataIncludeClass
                ImageryMetadataRequestClass
                ImageryMetadataResourceClass
                ImagerySetClass
                MapLayerClass
                MapVersionClass
                PushpinRequestClass
                RouteFromMajorRoadsRequestClass
                StaticMapRequestClass
            ]

            Namespace "IntelliFactory.WebSharper.Bing.Maps.Directions" [
                Directions.BusinessDetailsClass
                Directions.BusinessDisambiguationSuggestionClass
                Directions.RouteResponseCodeClass
                Directions.RouteSummaryClass
                Directions.RouteIconTypeClass
                Directions.ManeuverTypeClass
                Directions.TransitLineClass
                Directions.DirectionsStepWarningTypeClass
                Directions.DirectionsStepWarningClass
                Directions.RoutePathClass
                Directions.RouteSubLegClass
                Directions.DirectionsStepClass
                Directions.RouteLegClass
                Directions.RouteClass
                Directions.DirectionsEventArgsClass
                Directions.WaypointOptionsClass
                Directions.LocationDisambiguationSuggestionClass
                Directions.DisambiguationClass
                Directions.WaypointClass
                Directions.DirectionsRenderOptionsClass
                Directions.DistanceUnitClass
                Directions.RouteAvoidanceClass
                Directions.RouteModeClass
                Directions.RouteOptimizationClass
                Directions.TimeTypeClass
                Directions.TransitOptionsClass
                Directions.DirectionsRequestOptionsClass
                Directions.ResetDirectionsOptionsClass
                Directions.DirectionsManagerClass
                Directions.DirectionsErrorEventArgsClass
                Directions.DirectionsStepEventArgsClass
                Directions.DirectionsStepRenderEventArgsClass
                Directions.RouteSelectorEventArgsClass
                Directions.RouteSelectorRenderEventArgsClass
                Directions.RouteSummaryRenderEventArgsClass
                Directions.WaypointEventArgsClass
                Directions.WaypointRenderEventArgsClass
                Directions.DirectionsErrorEventClass
                Directions.DirectionsStepEventClass
                Directions.DirectionsStepRenderEventClass
                Directions.RouteSelectorEventClass
                Directions.RouteSelectorRenderEventClass
                Directions.RouteSummaryRenderEventClass
                Directions.WaypointEventClass
                Directions.WaypointRenderEventClass
            ]

            Namespace "IntelliFactory.WebSharper.Bing.Maps.Traffic" [
                Traffic.TrafficLayerClass
            ]
        ]



[<Sealed>]
type BingMapsExtension() =
    interface IExtension with
        member x.Assembly = Bing.Assembly

[<assembly: Extension(typeof<BingMapsExtension>)>]
do ()
