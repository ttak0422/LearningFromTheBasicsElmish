module Clock

open Elmish
open Elmish.React
open Clock.State
open Clock.View

module R = Fable.Helpers.React

Program.mkProgram init update root
|> Program.withSubscription subscription
|> Program.withReact "elmish-app"
|> Program.run
