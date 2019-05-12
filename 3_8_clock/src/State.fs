module Clock.State

open System
open Elmish
open Browser
open Types


let init () =
    { Time = DateTime.Now }, Cmd.none

let update msg model =
    match msg with
    | Tick time -> { model with Time = time }, Cmd.none

let tickTime dispatch =
    window.setInterval(fun _ ->
        dispatch <| Tick DateTime.Now
    , 1000) |> ignore