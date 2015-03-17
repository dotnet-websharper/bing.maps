namespace WebSharper.BingExtension

open WebSharper
open WebSharper.JavaScript.Dom
open WebSharper.InterfaceGenerator

module Bing =

    let private ConstantStrings ty l =
        List.map (fun s -> (s =? ty |> WithGetterInline ("'" + s + "'")) :> CodeModel.IClassMember) l
        |> Static

    let private Constants ty l =
        List.map (fun s -> s =? ty :> CodeModel.IClassMember) l
        |> Static

    ///////////////////////////////////////////////////////////////////
    // Ajax API

    let AltitudeReference =
        Class "Microsoft.Maps.AltitudeReference"
        |+> ConstantStrings TSelf ["ground"; "ellipsoid"]
        |+> Static [
                "isValid" => TSelf ^-> T<bool>
                |> WithComment "Determines if the specified reference is a supported AltitudeReference."
            ]

    let AnimationVisibility =
        Class "Microsoft.Maps.AnimationVisibility"
        |+> ConstantStrings TSelf ["auto"; "hide"; "show"]

    let Location =
        Class "Microsoft.Maps.Location"
        |+> Static [
                Constructor (T<float> * T<float> * T<float> * AltitudeReference)
                Constructor (T<float> * T<float> * T<float>)
                Constructor (T<float> * T<float>)

                "areEqual" => TSelf * TSelf ^-> T<bool>
                |> WithComment "Determines if the specified Location objects are equal."

                "normalizeLongitude" => T<float -> float>
                |> WithComment "Normalizes the specified longitude so that it is between -180 and 180."
            ]
        |+> Instance
            [
                "altitude" =? T<float>
                |> WithComment "The altitude of the location."

                "altitudeMode" =? AltitudeReference
                |> WithComment "The reference from which the altitude is measured."

                "latitude" =? T<float>
                |> WithComment "The latitude of the location."

                "longitude" =? T<float>
                |> WithComment "The longitude of the location."

                "clone" => T<unit> ^-> TSelf
                |> WithComment "Returns a copy of the Location object."

                "toString" => T<unit -> string>
                |> WithComment "Converts the Location object to a string."
            ]

    let LocationRect =
        Class "Microsoft.Maps.LocationRect"
        |+> Static [
                Constructor (Location * T<float> * T<float>)

                "fromCorners" => Location * Location ^-> TSelf
                |> WithComment "Returns a LocationRect using the specified locations for the northwest and southeast corners."

                "fromEdges" => T<float> * T<float> * T<float> * T<float> * T<float> * AltitudeReference ^-> TSelf
                |> WithComment "Returns a LocationRect using the specified northern and southern latitudes and western and eastern longitudes for the rectangle boundaries."

                "fromLocations" => Type.ArrayOf Location ^-> TSelf
                |> WithComment "Returns a LocationRect using an array of locations."

                "fromString" => T<string> ^-> TSelf
                |> WithComment "Creates a LocationRect from a string with the following format: \"north,west,south,east\". North, west, south and east specify the coordinate number values."
            ]
        |+> Instance [
                "center" =? Location
                |> WithComment "The location that defines the center of the rectangle."

                "height" =? T<float>
                |> WithComment "The height, in degrees, of the rectangle."

                "width" =? T<float>
                |> WithComment "The width, in degrees, of the rectangle."

                "clone" => T<unit> ^-> TSelf
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

                "intersects" => TSelf ^-> T<bool>
                |> WithComment "Returns whether the specified LocationRect intersects with this LocationRect."

                "toString" => T<unit -> string>
                |> WithComment "Converts the LocationRect object to a string."
            ]

    let Point =
        Class "Microsoft.Maps.Point"
        |+> Static [
                Constructor (T<float> * T<float>)

                "areEqual" => TSelf * TSelf ^-> T<bool>
                |> WithComment "Determines if the specified points are equal."

                "clonePoint" => TSelf ^-> TSelf
                |> WithSourceName "clone"
                |> WithComment "Returns a copy of the Point object."
            ]
        |+> Instance
            [
                "x" =? T<float>
                |> WithComment "The x value of the coordinate."

                "y" =? T<float>
                |> WithComment "The y value of the coordinate."

                "clone" => T<unit> ^-> TSelf
                |> WithComment "Returns a copy of the Point object."

                "toString" => T<unit -> string>
                |> WithComment "Converts the Point object into a string."

                "toUrlString" => T<unit -> string>
                |> WithInline "($this.x+','+$this.y)"
            ]

    let LabelOverlay =
        Class "Microsoft.Maps.LabelOverlay"
        |+> Static [
                "hidden" =? TSelf
                |> WithComment "Map labels are not shown on top of imagery."

                "visible" =? TSelf
                |> WithComment "Map labels are shown on top of imagery."

                "isValid" => TSelf ^-> T<bool>
                |> WithComment "Determines whether the specified labelOverlay is a supported LabelOverlay."
            ]

    let Color =
        Class "Microsoft.Maps.Color"
        |+> Static [
                Constructor (T<int> * T<int> * T<int> * T<int>)
                |> WithComment "Initializes a new instance of the Color class. The a parameter represents opacity. The range of valid values for all parameters is 0 to 255."

                "cloneColor" => TSelf ^-> TSelf
                |> WithInline "clone"
                |> WithComment "Creates a copy of the Color object."

                "fromHex" => T<string> ^-> TSelf
                |> WithComment "Converts the specified hex string to a Color."
            ]
        |+> Instance
            [
                "a" =? T<int>
                |> WithComment "The opacity of the color. The range of valid values is 0 to 255."

                "r" =? T<int>
                |> WithComment "The red value of the color. The range of valid values is 0 to 255."

                "g" =? T<int>
                |> WithComment "The green value of the color. The range of valid values is 0 to 255."

                "b" =? T<int>
                |> WithComment "The blue value of the color. The range of valid values is 0 to 255."

                "clone" => T<unit> ^-> TSelf
                |> WithComment "Returns a copy of the Color object."

                "getOpacity" => T<unit -> float>
                |> WithComment "Returns the opacity of the Color as a value between 0 (a=0) and 1 (a=255)."

                "toHex" => T<unit -> string>
                |> WithComment "Converts the Color into a 6-digit hex string. Opacity is ignored. For example, a Color with values (255,0,0,0) is returned as hex string #000000."

                "toString" => T<unit -> string>
                |> WithComment "Converts the Color object to a string."
            ]

    let MapTypeId =
        Class "Microsoft.Maps.MapTypeId"
        |+> Static [
                "aerial" =? TSelf
                |> WithComment "The aerial map style is being used."

                "auto" =? TSelf
                |> WithComment "The map is set to choose the best imagery for the current view."

                "birdseye" =? TSelf
                |> WithComment "The bird’s eye map type is being used."

                "collinsBart" =? TSelf
                |> WithComment "Collin’s Bart (mkt=en-gb) map type is being used."

                "mercator" =? TSelf
                |> WithComment "The Mercator style is being used."

                "ordnanceSurvey" =? TSelf
                |> WithComment "Ordinance Survey (mkt=en-gb) map type is being used."

                "road" =? TSelf
                |> WithComment "The road map style is being used."
            ]

    let private ViewOptionsFields =
        [
            "animate", T<bool>
            "bounds", LocationRect.Type
            "center", Location.Type
            "centerOffset", Point.Type
            "heading", T<float>
            "labelOverlay", LabelOverlay.Type
            "mapTypeId", MapTypeId.Type
            "padding", T<int>
            "zoom", T<int>
        ]

    let private MapOptionsFields =
        [
            "backgroundColor", Color.Type
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

    let ViewOptions =
        Pattern.Config "Microsoft.Maps.ViewOptions" {
            Required = []
            Optional = ViewOptionsFields
        }

    let MapOptions =
        Pattern.Config "Microsoft.Maps.MapOptions" {
            Required = []
            Optional = MapOptionsFields
        }

    let MapViewOptions =
        Pattern.Config "Microsoft.Maps.MapViewOptions" {
            Required = []
            Optional = MapOptionsFields @ ViewOptionsFields
        }

    let Size =
        Pattern.Config "Microsoft.Maps.Size" {
            Required =
                [
                    "height", T<float>
                    "width", T<float>
                ]
            Optional = []
        }

    let Entity =
        Interface "Microsoft.Maps.Entity"

    let EntityCollectionOptions =
        Pattern.Config "Microsoft.Maps.EntityCollectionOptions" {
            Required = []
            Optional =
                [
                    "visible", T<bool>
                    "zIndex", T<int>
                ]
        }

    let EntityCollection =
        Class "Microsoft.Maps.EntityCollection"
        |=> Implements [Entity]
        |+> Static [
                Constructor T<unit>
                Constructor EntityCollectionOptions
            ]
        |+> Instance
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

    let Event =
        Interface "Microsoft.Maps.Event"

    let KeyEvent =
        Class "Microsoft.Maps.KeyEvent"
        |=> Implements [Event]
        |+> ConstantStrings TSelf ["keydown"; "keyup"; "keypress"]

    let KeyEventArgs =
        Class "Microsoft.Maps.KeyEventArgs"
        |+> Instance
            [
                "altKey" =? T<bool>
                |> WithComment "A boolean indicating if the ALT key was pressed."

                "ctrlKey" =? T<bool>
                |> WithComment "A boolean indicating if the CTRL key was pressed."

                "eventName" =? KeyEvent
                |> WithComment "The event that occurred."

                "handled" =@ T<bool>
                |> WithComment "A boolean indicating whether the event is handled. If this property is set to true, the default map control behavior for the event is cancelled."

                "keyCode" =? T<string>
                |> WithComment "The code that identifies the keyboard key that was pressed."

                "originalEvent" =? T<obj>
                |> WithComment "The original browser event."

                "shiftKey" =? T<bool>
                |> WithComment "A boolean indicating if the SHIFT key was pressed."
            ]

    let MouseEvent =
        Class "Microsoft.Maps.MouseEvent"
        |=> Implements [Event]
        |+> ConstantStrings TSelf
            [
                "click"; "dblclick"; "rightclick"
                "mousedown"; "mouseup"; "mousemove"; "mouseover"; "mouseleave"; "mouseout"; "mousewheel"
            ]

    let MouseEventArgs =
        Class "Microsoft.Maps.MouseEventArgs"
        |+> Instance
            [
                "eventName" =? MouseEvent
                |> WithComment "The event that occurred."

                "handled" =@ T<bool>
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

    let EntityCollectionEvent =
        Class "Microsoft.Maps.EntityCollectionEvent"
        |=> Implements [Event]
        |+> ConstantStrings TSelf
            [ "entityadded"; "entitychanged"; "entityremoved" ]

    let EntityCollectionEventArgs =
        Class "Microsoft.Maps.EntityCollectionEventArgs"
        |+> Instance
            [
                "collection" =? EntityCollection
                "entity" =? Entity
            ]

    let UnitEvent =
        Class "Microsoft.Maps.UnitEvent"
        |=> Implements [Event]
        |+> ConstantStrings TSelf
            [
                "copyrightchanged"; "imagerychanged"; "maptypechanged"; "targetviewchanged"; "tiledownloadcomplete"
                "viewchange"; "viewchangeend"; "viewchangestart"
            ]

    let EventHandler =
        Class "Microsoft.Maps.EventHandler"

    let InfoboxAction =
        Pattern.Config "Microsoft.Maps.InfoboxAction" {
            Required =
                [
                    "label", T<string>
                    "eventHandler", MouseEventArgs ^-> T<unit>
                ]
            Optional = []
        }

    let InfoboxOptions =
        Pattern.Config "Microsoft.Maps.InfoboxOptions" {
            Required = []
            Optional =
                [
                    "actions", Type.ArrayOf InfoboxAction
                    "description", T<string>
                    "height", T<int>
                    "htmlContent", T<string>
                    "id", T<string>
                    "location", Location.Type
                    "offset", Point.Type
                    "showCloseButton", T<bool>
                    "showPointer", T<bool>
                    "title", T<string>
                    "titleClickHandler", MouseEventArgs ^-> T<unit>
                    "visible", T<bool>
                    "width", T<int>
                    "zIndex", T<int>
                ]
        }

    let Infobox =
        Class "Microsoft.Maps.Infobox"
        |=> Implements [Entity]
        |+> Static [
                Constructor Location
                Constructor (Location * InfoboxOptions)
            ]
        |+> Instance
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

    let Range =
        Class "Range"
        |+> Instance
            [
                "min" =? T<float>
                |> WithComment "The minimum value in the range."

                "max" =? T<float>
                |> WithComment "The maximum value in the range."
            ]

    let PixelReference =
        Class "Microsoft.Maps.PixelReference"
        |+> Constants TSelf ["control"; "page"; "viewport"]
        |+> Static [
                "isValid" => TSelf ^-> T<bool>
                |> WithComment "Determines whether the specified reference is a supported PixelReference."
            ]

    let Map =
        Class "Microsoft.Maps.Map"
        |=> Implements [Entity]
        |+> Static [
                Constructor (T<Node> * MapViewOptions)
                Constructor (T<Node> * MapOptions)
                Constructor (T<Node> * ViewOptions)
                Constructor (T<Node>)
            ]
        |+> Instance
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

    let Coordinates =
        Class "Microsoft.Maps.Coordinates"
        |+> Instance
            [
                "accuracy" =? T<float>
                "altitude" =? T<float>
                "altitudeAccuracy" =? T<float>
                "heading" =? T<float>
                "latitude" =? T<float>
                "longitude" =? T<float>
                "speed" =? T<float>
            ]

    let Position =
        Class "Microsoft.Maps.Position"
        |+> Instance
            [
                "coords" =? Coordinates
                "timestamp" =? T<string>
            ]

    let PositionError =
        Class "Microsoft.Maps.PositionError"
        |+> Instance
            [
                "code" =? T<int>
                "message" =? T<string>
            ]

    let PositionErrorCallbackArgs =
        Class "Microsoft.Maps.PositionErrorCallbackArgs"
        |+> Instance
            [
                "internalError" =? PositionError
                "errorCode" =? T<int>
            ]

    let PositionSuccessCallbackArgs =
        Class "Microsoft.Maps.PositionSuccessCallbackArgs"
        |+> Instance
            [
                "center" =? Location
                "position" =? Position
            ]

    let PositionOptions =
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

    let PolygonOptions =
        Pattern.Config "Microsoft.Maps.PolygonOptions" {
            Required = []
            Optional =
                [
                    "fillColor", Color.Type
                    "strokeColor", Color.Type
                    "strokeThickness", T<float>
                    "visible", T<bool>
                ]
        }
        |+> Instance
            [
                "strokeDashArray" =@ Type.ArrayOf T<int>
                |> WithSetterInline "$this.strokeDashArray = $1.join(' ')"
                |> WithGetterInline "$this.strokeDashArray.split(' ')"
            ]

    let PositionCircleOptions =
        Pattern.Config "Microsoft.Maps.PositionCircleOptions" {
            Required = []
            Optional =
                [
                    "polygonOptions", PolygonOptions.Type
                    "showOnMap", T<bool>
                ]
        }

    let GeoLocationProvider =
        Class "Microsoft.Maps.GeoLocationProvider"
        |+> Static [
                Constructor Map
            ]
        |+> Instance
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

    let PolylineOptions =
        Pattern.Config "Microsoft.Maps.PolylineOptions" {
            Required = []
            Optional =
                [
                    "strokeColor", Color.Type
                    "strokeThickness", T<float>
                    "visible", T<bool>
                ]
        }
        |+> Instance
            [
                "strokeDashArray" =@ Type.ArrayOf T<int>
                |> WithSetterInline "$this.strokeDashArray = $1.join(' ')"
                |> WithGetterInline "$this.strokeDashArray.split(' ')"
            ]

    let Polyline =
        Class "Microsoft.Maps.Polyline"
        |=> Implements [Entity]
        |+> Static [
                Constructor (Type.ArrayOf Location)
                Constructor (Type.ArrayOf Location * PolylineOptions)
            ]
        |+> Instance
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

    let TileSourceOptions =
        Pattern.Config "Microsoft.Maps.TileSourceOptions" {
            Required = []
            Optional =
                [
                    "height", T<float>
                    "uriConstructor", T<string>
                    "width", T<float>
                ]
        }

    let TileSource =
        Class "Microsoft.Maps.TileSource"
        |+> Static [
                Constructor TileSourceOptions
                |> WithComment "Initializes a new instance of the TileSource  class."
            ]
        |+> Instance
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

    let TileLayerOptions =
        Pattern.Config "Microsoft.Maps.TileLayerOptions" {
            Required = []
            Optional =
                [
                    "animationDisplay", AnimationVisibility.Type
                    "mercator", TileSource.Type
                    "opacity", T<float>
                    "visible", T<bool>
                    "zIndex", T<float>
                ]
        }

    let TileLayer =
        Class "Microsoft.Maps.TileLayer"
        |=> Implements [Entity]
        |+> Static [
                Constructor TileLayerOptions
                |> WithComment "Initializes a new instance of the TileLayer class."
            ]
        |+> Instance
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

    let Polygon =
        Class "Microsoft.Maps.Polygon"
        |=> Implements [Entity]
        |+> Static [
                Constructor (Type.ArrayOf Location)
                Constructor (Type.ArrayOf Location * PolygonOptions)
            ]
        |+> Instance
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

    let PushpinOptions =
        Pattern.Config "Microsoft.Maps.PushpinOptions" {
            Required = []
            Optional =
                [
                    "anchor", Point.Type
                    "draggable", T<bool>
                    "icon", T<string>
                    "height", T<int>
                    "text", T<string>
                    "textOffset", Point.Type
                    "typeName", T<string>
                    "visible", T<bool>
                    "width", T<int>
                    "zIndex", T<int>
                ]
        }

    let Pushpin =
        Class "Microsoft.Maps.Pushpin"
        |=> Implements [Entity]
        |+> Static [
                Constructor Location
                Constructor (Location * PushpinOptions)
            ]
        |+> Instance
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

        let BusinessDetails =
            Class "Microsoft.Maps.Directions.BusinessDetails"
            |+> Instance
                [
                    "businessName" =? T<string>
                    "entityId" =? T<string>
                    "phoneNumber" =? T<string>
                    "website" =? T<string>
                ]

        let BusinessDisambiguationSuggestion =
            Class "Microsoft.Maps.Directions.BusinessDisambiguationSuggestion"
            |+> Instance
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

        let RouteResponseCode =
            Class "Microsoft.Maps.Directions.RouteResponseCode"
            |+> Constants TSelf ["success"; "unknownError"; "cannotFindNearbyRoad"; "tooFar"
                                 "noSolution"; "dataSourceNotFound"; "noAvailableTransitTrip"
                                 "transitStopsTooClose"; "walkingBestAlternative"; "outOfTransitBounds"
                                 "timeout"; "waypointDisambiguation"; "hasEmptyWaypoint"
                                 "exceedMaxWaypointLimit"; "atleastTwoWaypointRequired"
                                 "firstOrLastStoppointIsVia"; "searchServiceFailed"]

        let RouteSummary =
            Class "Microsoft.Maps.Directions.RouteSummary"
            |+> Instance
                [
                    "distance" =? T<float>
                    "monetaryCost" =? T<float>
                    "northEast" =? Location
                    "southWest" =? Location
                    "time" =? T<int>
                    "timeWithTraffic" =? T<int>
                ]

        let RouteIconType =
            Class "Microsoft.Maps.Directions.RouteIconType"
            |+> Constants TSelf ["none"; "other"; "auto"; "ferry"
                                 "walk"; "bus"; "train"; "airline"]

        let ManeuverType =
            Class "Microsoft.Maps.Directions.ManeuverType"
            |+> Constants TSelf ["none"; "unknown"; "transfer"; "wait"
                                 "takeTransit"; "walk"; "transitDepart"
                                 "transitArrive"]

        let TransitLine =
            Class "Microsoft.Maps.Directions.TransitLine"
            |+> Instance
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

        let DirectionsStepWarningType =
            Class "Microsoft.Maps.Directions.DirectionsStepWarningType"
            |+> Constants TSelf
                    ["info"; "minor"; "moderate"; "major"; "ccZoneEnter"; "ccZoneExit"]

        let DirectionsStepWarning =
            Class "Microsoft.Maps.Directions.DirectionsStepWarning"
            |+> Instance
                [
                    "style" =? DirectionsStepWarningType
                    "text" =? T<string>
                ]

        let RoutePath =
            Class "Microsoft.Maps.Directions.RoutePath"
            |+> Instance
                [
                    "decodedLatitudes" =? Type.ArrayOf T<float>
                    "decodedLongitudes" =? Type.ArrayOf T<float>
                    "decodedRegions" =? Type.ArrayOf T<obj>
                ]

        let RouteSubLeg =
            Class "Microsoft.Maps.Directions.RouteSubLeg"
            |+> Instance
                [
                    "actualEnd" =? Location
                    "actualStart" =? Location
                    "endDescription" =? T<string>
                    "routePath" =? RoutePath
                    "startDescription" =? T<string>
                    "summary" =? RouteSummary
                ]

        let DirectionsStep =
            Class "Microsoft.Maps.Directions.DirectionsStep"
            |+> Instance
                [
                    "childItineraryItems" =? Type.ArrayOf TSelf
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

        let RouteLeg =
            Class "Microsoft.Maps.Directions.RouteLeg"
            |+> Instance
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

        let Route =
            Class "Microsoft.Maps.Directions.Route"
            |+> Instance
                [
                    "routeLegs" =? Type.ArrayOf RouteLeg
                ]

        let DirectionsEventArgs =
            Class "Microsoft.Maps.Directions.DirectionsEventArgs"
            |+> Instance
                [
                    "routeSummary" =? Type.ArrayOf RouteSummary
                    "route" =? Type.ArrayOf Route
                ]

        let WaypointOptions =
            Pattern.Config "Microsoft.Maps.Directions.WaypointOptions" {
                Required = []
                Optional =
                    [
                        "address", T<string>
                        "businessDetails", BusinessDetails.Type
                        "exactLocation", T<bool>
                        "isViapoint", T<bool>
                        "location", Location.Type
                        "shortAddress", T<string>
                    ]
            }

        let LocationDisambiguationSuggestion =
            Class "Microsoft.Maps.Directions.LocationDisambiguationSuggestion"
            |+> Instance
                [
                    "formattedSuggestion" =? T<string>
                    "location" =? Location.Type
                    "rooftopLocation" =? Location.Type
                    "suggestion" =? T<string>
                ]

        let Disambiguation =
            Class "Microsoft.Maps.Directions.Disambiguation"
            |+> Instance
                [
                    "businessSuggestions" =? Type.ArrayOf BusinessDisambiguationSuggestion
                    "hasMoreSuggestions" =? T<bool>
                    "headerText" =? T<string>
                    "locationSuggestions" =? Type.ArrayOf LocationDisambiguationSuggestion
                ]

        let Waypoint =
            Class "Microsoft.Maps.Directions.Waypoint"
            |+> Static [
                    Constructor WaypointOptions
                ]
            |+> Instance
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

        let DirectionsRenderOptions =
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
                        "drivingPolylineOptions", PolylineOptions.Type
                        "itineraryContainer", T<Element>
                        "lastWaypointIcon", T<string>
                        "stepPushpinOptions", PushpinOptions.Type
                        "transitPolylineOptions", PolylineOptions.Type
                        "viapointPushpinsOptions", PushpinOptions.Type
                        "walkingPolylineOptions", PolylineOptions.Type
                        "waypointPushpinOptions", PushpinOptions.Type
                    ]
            }

        let DistanceUnit =
            Class "Microsoft.Maps.Directions.DistanceUnit"
            |+> Constants TSelf ["kilometers"; "miles"]

        let RouteAvoidance =
            Class "Microsoft.Maps.Directions.RouteAvoidance"
            |+> Constants TSelf ["none"; "minimizeLimitedAccessHighway"; "minimizeToll"
                                 "avoidLimitedAccesHighway"; "avoidToll"; "avoidExpressTrain"
                                 "avoidAirline"; "avoidBulletTrain"]

        let RouteMode =
            Class "Microsoft.Maps.Directions.RouteMode"
            |+> Constants TSelf ["driving"; "transit"; "walking"]

        let RouteOptimization =
            Class "Microsoft.Maps.Directions.RouteOptimization"
            |+> Constants TSelf ["shortestTime"; "shortestDistance"
                                 "minimizeMoney"; "minimizeTransfers"; "minimizeWalking"]

        let TimeType =
            Class "Microsoft.Maps.Directions.TimeType"
            |+> Constants TSelf ["arrival"; "departure"; "lastAvailable"]

        let TransitOptions =
            Pattern.Config "Microsoft.Maps.Directions.TransitOptions" {
                Required = []
                Optional =
                    [
                        "timeType", TimeType.Type
                        "transitTime", T<JavaScript.Date>
                    ]
            }

        let DirectionsRequestOptions =
            Pattern.Config "Microsoft.Maps.Directions.DirectionsRequestOptions" {
                Required = []
                Optional =
                    [
                        "avoidTraffic", T<bool>
                        "distanceUnit", DistanceUnit.Type
                        "maxRoutes", T<int>
                        "routeAvoidance", Type.ArrayOf RouteAvoidance
                        "routeDraggable", T<bool>
                        "routeIndex", T<int>
                        "routeMode", RouteMode.Type
                        "routeOptimization", RouteOptimization.Type
                        "transitOptions", TransitOptions.Type
                    ]
            }

        let ResetDirectionsOptions =
            Pattern.Config "Microsoft.Maps.Directions.ResetDirectionsOptions" {
                Required = []
                Optional =
                    [
                        "removeAllWaypoints", T<bool>
                        "resetRenderOptions", T<bool>
                        "resetRequestOptions", T<bool>
                    ]
            }

        let DirectionsManager =
            Class "Microsoft.Maps.Directions.DirectionsManager"
            |+> Static [
                    Constructor Map
                ]
            |+> Instance
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

        let DirectionsErrorEventArgs =
            Class "Microsoft.Maps.Directionss.DirectionsErrorEventArgs"
            |+> Instance
                [
                    "responseCode" =? RouteResponseCode
                    "message" =? T<string>
                ]

        let DirectionsStepEventArgs =
            Class "Microsoft.Maps.Directions.DirectionsStepEventArgs"
            |+> Instance
                [
                    "handled" =@ T<bool>
                    "location" =? Location
                    "routeIndex" =? T<int>
                    "routeLegIndex" =? T<int>
                    "step" => DirectionsStep
                    "stepIndex" =? T<int>
                    "stepNumber" =? T<int>
                ]

        let DirectionsStepRenderEventArgs =
            Class "Microsoft.Maps.Directions.DirectionsStepRenderEventArgs"
            |+> Instance
                [
                    "containerElement" =@ T<Element>
                    "handled" =@ T<bool>
                    "lastStep" =? T<bool>
                    "routeIndex" =? T<int>
                    "routeLegIndex" =? T<int>
                    "step" =? DirectionsStep
                    "stepIndex" =? T<int>
                ]

        let RouteSelectorEventArgs =
            Class "Microsoft.Maps.Directions.RouteSelectorEventArgs"
            |+> Instance
                [
                    "handled" =@ T<bool>
                    "routeIndex" =? T<int>
                ]

        let RouteSelectorRenderEventArgs =
            Class "Microsoft.Maps.Directions.RouteSelectorRenderEventArgs"
            |+> Instance
                [
                    "containerElement" =@ T<Element>
                    "handled" =? T<bool>
                    "routeIndex" =? T<int>
                    "routeLeg" =? RouteLeg
                ]

        let RouteSummaryRenderEventArgs =
            Class "Microsoft.Maps.Directions.RouteSummaryRenderEventArgs"
            |+> Instance
                [
                    "containerElements" =@ T<Element>
                    "handled" =@ T<bool>
                    "routeLegIndex" =? T<int>
                    "summary" =? RouteSummary
                ]

        let WaypointEventArgs =
            Class "Microsoft.Maps.Directions.WaypointEventArgs"
            |+> Instance
                [
                    "waypoint" =? Waypoint
                ]

        let WaypointRenderEventArgs =
            Class "Microsoft.Maps.Directions.WaypointRenderEventArgs"
            |+> Instance
                [
                    "containerElement" =@ T<Element>
                    "handled" =@ T<bool>
                    "waypoint" =? Waypoint
                ]

        let RouteSelectorRenderEvent =
            Class "Microsoft.Maps.Directions.RouteSelectorRenderEvent"
            |+> ConstantStrings TSelf
                    ["afterRouteSelectorRender"; "beforeRouteSelectorRender"]

        let DirectionsStepRenderEvent =
            Class "Microsoft.Maps.Directions.DirectionsStepRenderEvent"
            |+> ConstantStrings TSelf
                    ["afterStepRender"; "beforeStepRender"]

        let RouteSummaryRenderEvent =
            Class "Microsoft.Maps.Directions.RouteSummaryRenderEvent"
            |+> ConstantStrings TSelf
                    ["afterSummaryRender"; "beforeSummaryRender"]

        let WaypointRenderEvent =
            Class "Microsoft.Maps.Directions.WaypointRenderEvent"
            |+> ConstantStrings TSelf
                    ["afterWaypointRender"; "beforeWaypointRender"]

        let DirectionsErrorEvent =
            Class "Microsoft.Maps.Directions.DirectionsErrorEvent"
            |+> ConstantStrings TSelf
                    ["directionsError"]

        let DirectionsEvent =
            Class "Microsoft.Maps.Directions.DirectionsEvent"
            |+> ConstantStrings TSelf
                    ["directionsUpdated"]

        let RouteSelectorEvent =
            Class "Microsoft.Maps.Directions.RouteSelectorEvent"
            |+> ConstantStrings TSelf
                    ["mouseEnterRouteSelector"; "mouseLeaveRouteSelector"; "routeSelectorClicked"]

        let DirectionsStepEvent =
            Class "Microsoft.Maps.Directions.DirectionsStepEvent"
            |+> ConstantStrings TSelf
                    ["itineraryStepClicked"; "mouseEnterStep"; "mouseLeaveStep"]

        let WaypointEvent =
            Class "Microsoft.Maps.Directions.WaypointEvent"
            |+> ConstantStrings TSelf
                    ["waypointAdded"; "waypointRemoved"; "changed"; "geocoded"; "reverseGeocoded"]

    module Traffic =

        let TrafficLayer =
            Class "Microsoft.Maps.Traffic.TrafficLayer"
            |+> Static [
                    Constructor Map
                ]
            |+> Instance
                [
                    "getTileLayer" => T<unit> ^-> TileLayer
                    "hide" => T<unit> ^-> T<unit>
                    "show" => T<unit> ^-> T<unit>
                ]


    module VenueMaps =

        let Primitive' = Class "Microsoft.Maps.VenueMaps.Primitive"

        let Floor =
            Class "Microsoft.Maps.VenueMaps.Floor"
            |+> Instance
                [
                    "name" =? T<string>
                    "primitives" =? Type.ArrayOf Primitive'
                    "zoomRange" =? Type.ArrayOf T<float>
                ]

        let Primitive =
            Primitive'
            |+> Instance
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

        let Polygon =
            Class "Microsoft.Maps.VenueMaps.Polygon"
            |+> Instance
                [
                    "bounds" =? LocationRect
                    "center" =? Location
                    "locations" =? Type.ArrayOf Location
                ]

        let Footprint =
            Class "Microsoft.Maps.VenueMaps.Footprint"
            |+> Instance
                [
                    "polygons" =? Type.ArrayOf Polygon
                    "zoomRange" =? Type.ArrayOf T<float>
                ]

        let Metadata =
            Class "Microsoft.Maps.VenueMaps.Metadata"
            |+> Instance
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

        let NearbyVenue =
            Class "Microsoft.Maps.VenueMaps.NearbyVenue"
            |+> Instance
                [
                    "distance" =? T<float>
                    "metadata" =? Metadata
                ]

        let VenueMap =
            Class "Microsoft.Maps.VenueMaps.VenueMap"
            |+> Instance
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

        let NearbyVenueMapOptions =
            Pattern.Config "Microsoft.Maps.VenueMaps.NearbyVenueMapOptions" {
                Required = []
                Optional =
                    [
                        "callback", Type.ArrayOf NearbyVenue ^-> T<unit>
                        "location", Location.Type
                        "map", Map.Type
                        "radius", T<float>
                    ]
            }

        let VenueMapCreationOptions =
            Pattern.Config "Microsoft.Maps.VenueMaps.VenueMapCreationOptions" {
                Required = []
                Optional =
                    [
                        "error", T<int> * TSelf ^-> T<unit>
                        "success", VenueMap * TSelf ^-> T<unit>
                        "venueMapId", T<string>
                    ]
            }

        let VenueMapFactory =
            Class "Microsoft.Maps.VenueMaps.VenueMapFactory"
            |+> Static [
                    Constructor Map
                ]
            |+> Instance
                [
                    "create" => VenueMapCreationOptions ^-> T<unit>
                    "getNearbyVenues" => NearbyVenueMapOptions ^-> T<unit>
                ]


    let Events =
        Class "Microsoft.Maps.Events"
        |+> Static [
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

    let AuthenticationResultCode =
        Class "Microsoft.Maps.AuthenticationResultCode"
        |+> ConstantStrings TSelf
            ["ValidCredentials"; "InvalidCredentials"; "CredentialsExpired"; "NotAuthorized"; "NoCredentials"]

    let ResourceSet =
        Class "Microsoft.Maps.ResourceSet"
        |+> Instance
            [
                "estimatedTotal" =? T<int>
                |> WithComment "An estimate of the total number of resources in the ResourceSet."

                "resources" =? Type.ArrayOf T<obj>
                |> WithComment "A collection of one or more resources. The resources that are returned depend on the request. Information about resources is provided in the API reference for each Bing Maps REST Services API."
            ]

    let RestResponse =
        Class "Microsoft.Maps.RestResponse"
        |+> Instance
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

    let PointResource =
        Class "Microsoft.Maps.PointResource"
        |+> Instance
            [
                "type" =? T<string>
                "coordinates" =? Type.ArrayOf T<float>
            ]

    let Address =
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

    let Confidence =
        Class "Microsoft.Maps.Confidence"
        |+> ConstantStrings TSelf ["High"; "Medium"; "Low"; "Unknown"]

    let LocationResource =
        Class "Microsoft.Maps.LocationResource"
        |+> Instance
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

    ///////////////////////////////////////////////////////////////////
    // REST Routes API

    let Waypoint =
        Class "Microsoft.Maps.Waypoint"
        |+> Static [
                Constructor T<string>?s
                |> WithInline "$s"

                Constructor Point?p
                |> WithInline "($p.x+','+$p.y)"
            ]

    let ItineraryInstruction =
        Class "Microsoft.Maps.ItineraryInstruction"
        |+> Instance
            [
                "maneuverType" =? T<string>
                "text" =? T<string>
            ]

    let ItineraryIcon =
        Class "Microsoft.Maps.ItineraryIcon"
        |+> ConstantStrings TSelf ["None"; "Airline"; "Auto"; "Bus"
                                   "Ferry"; "Train"; "Walk"; "Other"]

    let ItineraryDetail =
        Class "Microsoft.Maps.ItineraryDetail"
        |+> Instance
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

    let TransitLine =
        Class "Microsoft.Maps.TransitLine"
        |+> Instance
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

    let ItineraryItem =
        Class "Microsoft.Maps.ItineraryItem"
        |+> Instance
            [
                "childItineraryItems" =? Type.ArrayOf TSelf
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

    let RouteLeg =
        Class "Microsoft.Maps.RouteLeg"
        |+> Instance
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

    let DistanceUnit =
        Class "Microsoft.Maps.DistanceUnit"
        |+> ConstantStrings TSelf ["Mile"; "Kilometer"]

    let LineResource =
        Class "Microsoft.Maps.LineResource"
        |+> Instance
            [
                "type" =? T<string>
                "coordinates" =? Type.ArrayOf (Type.ArrayOf T<float>)
            ]

    let RoutePath =
        Class "Microsoft.Maps.RoutePath"
        |+> Instance
            [
                "line" =? LineResource
                |> WithComment "When the points in the line are connected, they represent the path of the route."

                "point" =? PointResource
                |> WithComment "The coordinates of a point on the Earth."
            ]

    let RouteResource =
        Class "Microsoft.Maps.RouteResource"
        |+> Instance
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

    let RouteAvoid =
        Class "Microsoft.Maps.RouteAvoid"
        |+> ConstantStrings TSelf ["highways"; "tolls"; "minimizeHighways"; "minimizeTolls"]

    let RouteOptimize =
        Class "Microsoft.Maps.RouteOptimize"
        |+> ConstantStrings TSelf ["distance"; "time"; "timeWithTraffic"]

    let RoutePathOutput =
        Class "Microsoft.Maps.RoutePathOutput"
        |+> ConstantStrings TSelf ["Points"; "None"]

    let TimeType =
        Class "Microsoft.Maps.TimeType"
        |+> ConstantStrings TSelf ["Arrival"; "Departure"; "LastAvailable"]

    let TravelMode =
        Class "Microsoft.Maps.TravelMode"
        |+> ConstantStrings TSelf ["Driving"; "Walking"; "Transit"]

    let RouteRequest =
        Pattern.Config "Microsoft.Maps.RouteRequest" {
            Required = []
            Optional =
                [
                    "waypoints", Type.ArrayOf Waypoint
                    "avoid", Type.ArrayOf RouteAvoid
                    "heading", T<int>
                    "optimize", RouteOptimize.Type
                    "routePathOutput", RoutePathOutput.Type
                    "distanceUnit", DistanceUnit.Type
                    "dateTime", T<string>
                    "timeType", TimeType.Type
                    "maxSolutions", T<int>
                    "travelMode", TravelMode.Type
                ]
        }

    let RouteFromMajorRoadsRequest =
        Pattern.Config "Microsoft.Maps.RouteFromMajorRoadsRequest" {
            Required =
                [
                    "destination", Waypoint.Type
                ]
            Optional =
                [
                    "exclude", T<string>
                    "routePathOutput", RoutePathOutput.Type
                    "distanceUnit", DistanceUnit.Type
                ]
        }

    ///////////////////////////////////////////////////////////////////
    // REST Imagery API

    let ImagerySet =
        Class "Microsoft.Maps.ImagerySet"
        |+> ConstantStrings TSelf ["Aerial"; "AerialWithLabels"; "Road"]

    let MapLayer =
        Class "Microsoft.Maps.MapLayer"
        |+> ConstantStrings TSelf ["TrafficFlow"]

    let MapVersion =
        Class "Microsoft.Maps.MapVersion"
        |+> ConstantStrings TSelf ["v0"; "v1"]

    let PushpinRequest =
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

    let StaticMapRequest =
        Pattern.Config "Microsoft.Maps.StaticMapRequest" {
            Required =
                [
                    "imagerySet", ImagerySet.Type
                ]
            Optional =
                [
                    "avoid", Type.ArrayOf RouteAvoid
                    "centerPoint", Point.Type
                    "dateTime", T<string>
                    "declutterPins", T<bool>
                    "distanceBeforeFirstTurn", T<int>
                    "mapArea", Point * Point
                    "mapLayer", MapLayer.Type
                    "mapSize", T<int> * T<int>
                    "mapVersion", MapVersion.Type
                    "maxSolutions", T<int>
                    "optimize", RouteOptimize.Type
                    "pushpin", Type.ArrayOf PushpinRequest
                    "query", T<string>
                    "timeType", TimeType.Type
                    "travelMode", TravelMode.Type
                    "waypoints", Type.ArrayOf Waypoint
                    "zoomLevel", T<int>
                ]
        }

    let ImageryMetadataInclude =
        Class "Microsoft.Maps.ImageryMetadataInclude"
        |+> ConstantStrings TSelf ["ImageryProviders"]

    let ImageryMetadataRequest =
        Pattern.Config "Microsoft.Maps.ImageryMetadataRequest" {
            Required =
                [
                    "imagerySet", ImagerySet.Type
                ]
            Optional =
                [
                    "centerPoint", Point.Type
                    "include", ImageryMetadataInclude.Type
                    "mapVersion", MapVersion.Type
                    "orientation", T<float>
                    "zoomLevel", T<int>
                ]
        }

    let ImageryMetadataResource =
        Class "Microsoft.Maps.ImageryMetadataResource"
        |+> Instance
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

    let LoadModuleArgs =
        Pattern.Config "Microsoft.Maps.LoadModuleArgs" {
                Required =
                    [
                        "callback", T<unit -> unit>
                    ]
                Optional = []
            }

    let MapsStatics =
        Class "Microsoft.Maps"
        |+> Static [
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
            Namespace "WebSharper.Bing.Maps.Resources" [
                (Resource "Js" "http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=7.0")
                    .AssemblyWide()
            ]
            Namespace "WebSharper.Bing.Maps" [
                AltitudeReference
                AnimationVisibility
                Color
                Coordinates
                EntityCollection
                EntityCollectionEventArgs
                EntityCollectionEvent
                EntityCollectionOptions
                Entity
                Event
                EventHandler
                Events
                GeoLocationProvider
                InfoboxAction
                Infobox
                InfoboxOptions
                KeyEventArgs
                KeyEvent
                LabelOverlay
                Location
                LocationRect
                Map
                MapOptions
                MapTypeId
                MapViewOptions
                MouseEventArgs
                MouseEvent
                PixelReference
                Point
                Polygon
                PolygonOptions
                Polyline
                PolylineOptions
                Position
                PositionCircleOptions
                PositionError
                PositionOptions
                PositionErrorCallbackArgs
                PositionSuccessCallbackArgs
                Pushpin
                PushpinOptions
                Range
                Size
                TileLayer
                TileLayerOptions
                TileSource
                TileSourceOptions
                UnitEvent
                ViewOptions
                LoadModuleArgs
                MapsStatics

                // REST locations
                Address
                AuthenticationResultCode
                Confidence
                LocationResource
                PointResource
                ResourceSet
                RestResponse
                Waypoint

                // REST Routes
                DistanceUnit
                ItineraryDetail
                ItineraryIcon
                ItineraryInstruction
                ItineraryItem
                LineResource
                RouteAvoid
                RouteLeg
                RouteOptimize
                RoutePath
                RoutePathOutput
                RouteRequest
                RouteResource
                TimeType
                TransitLine
                TravelMode

                // REST Imagery
                ImageryMetadataInclude
                ImageryMetadataRequest
                ImageryMetadataResource
                ImagerySet
                MapLayer
                MapVersion
                PushpinRequest
                RouteFromMajorRoadsRequest
                StaticMapRequest
            ]

            Namespace "WebSharper.Bing.Maps.Directions" [
                Directions.BusinessDetails
                Directions.BusinessDisambiguationSuggestion
                Directions.RouteResponseCode
                Directions.RouteSummary
                Directions.RouteIconType
                Directions.ManeuverType
                Directions.TransitLine
                Directions.DirectionsStepWarningType
                Directions.DirectionsStepWarning
                Directions.RoutePath
                Directions.RouteSubLeg
                Directions.DirectionsStep
                Directions.RouteLeg
                Directions.Route
                Directions.DirectionsEventArgs
                Directions.DirectionsEvent
                Directions.WaypointOptions
                Directions.LocationDisambiguationSuggestion
                Directions.Disambiguation
                Directions.Waypoint
                Directions.DirectionsRenderOptions
                Directions.DistanceUnit
                Directions.RouteAvoidance
                Directions.RouteMode
                Directions.RouteOptimization
                Directions.TimeType
                Directions.TransitOptions
                Directions.DirectionsRequestOptions
                Directions.ResetDirectionsOptions
                Directions.DirectionsManager
                Directions.DirectionsErrorEventArgs
                Directions.DirectionsStepEventArgs
                Directions.DirectionsStepRenderEventArgs
                Directions.RouteSelectorEventArgs
                Directions.RouteSelectorRenderEventArgs
                Directions.RouteSummaryRenderEventArgs
                Directions.WaypointEventArgs
                Directions.WaypointRenderEventArgs
                Directions.DirectionsErrorEvent
                Directions.DirectionsStepEvent
                Directions.DirectionsStepRenderEvent
                Directions.RouteSelectorEvent
                Directions.RouteSelectorRenderEvent
                Directions.RouteSummaryRenderEvent
                Directions.WaypointEvent
                Directions.WaypointRenderEvent
            ]

            Namespace "WebSharper.Bing.Maps.Traffic" [
                Traffic.TrafficLayer
            ]
        ]



[<Sealed>]
type BingMapsExtension() =
    interface IExtension with
        member x.Assembly = Bing.Assembly

[<assembly: Extension(typeof<BingMapsExtension>)>]
do ()
