module Clock.State

open Elmish
open Fable.Import
open System
open Types

let init() : Model * Cmd<Msg> = { Time = DateTime.Now }, Cmd.none

let update msg model : Model * Cmd<Msg> =
    match msg with
    | Tick time -> { model with Time = time }, Cmd.none

let subscription model =
    let tickTime dispatch =
        Browser.window.setInterval
            ((fun _ -> dispatch <| Tick DateTime.Now), 1000) |> ignore
    Cmd.ofSub tickTime
