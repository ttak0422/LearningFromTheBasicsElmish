module Counter

open Counter.State
open Counter.View
open Elmish
open Elmish.React
open Elmish.ReactNative

Program.mkProgram init update root
|> Program.withReact "elmish-app"
|> Program.run
