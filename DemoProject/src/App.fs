module App.View


open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Core.JsInterop
open Types
open App.State
open Global
open Fable.Helpers.React.Props
module R = Fable.Helpers.React
importAll "../sass/main.sass"


let menuItem label page currentPage =
    R.li
        [ ]
        [ R.a
            [ R.classList [ "is-active", page = currentPage ]
              Href (toHash page) ]
            [ R.str label ] ]

let menu currentPage =
    R.aside
        [ ClassName "menu" ]
        [ R.p [ ClassName "menu-label" ] [ R.str "General" ]
          R.ul
              [ ClassName "menu-list" ]
              [ menuItem "About" Page.About currentPage
                menuItem "Counter" Page.Counter currentPage
                menuItem "Form" Page.Form currentPage
                menuItem "Home" Page.Home currentPage ] ]

let root model dispatch =

    let pageHtml page =
        match page with
        | Page.About -> Info.View.root
        | Page.Counter -> Counter.View.root model.Counter (CounterMsg >> dispatch)
        | Page.Form -> Form.View.root model.Form (FormMsg >> dispatch)
        | Home -> Home.View.root model.Home (HomeMsg >> dispatch)

    R.div
        []
        [ Navbar.View.root
          R.div
              [ ClassName "section" ]
              [ R.div [ ClassName "container" ]
                    [ R.div
                        [ ClassName "columns" ]
                        [ R.div
                            [ ClassName "column is-3" ]
                            [ menu model.CurrentPage ]
                          R.div
                            [ ClassName "column" ]
                            [ pageHtml model.CurrentPage ] ] ] ] ]


open Elmish.React
open Elmish.Debug
open Elmish.HMR


// App
Program.mkProgram init update root
|> Program.toNavigable (parseHash pageParser) urlUpdate
#if DEBUG
|> Program.withDebugger
#endif
|> Program.withReact "elmish-app"
|> Program.run