module SearchBox.State

open Elmish
open GitHub
open Types

let init() : Model * Cmd<Msg> =
    { Input = ""
      UserState = Init }, Cmd.none

let update msg model : Model * Cmd<Msg> =
    match msg with
    | Input input -> { model with Input = input }, Cmd.none
    | Send ->
        { model with Input = ""
                     UserState = Waiting },
        Cmd.OfAsync.either getUser model.Input ReceiveUser ReceiveUserErr
    | ReceiveUser (Ok user) -> { model with UserState = Loaded user }, Cmd.none
    | ReceiveUser (Error e) -> { model with UserState = Failed <| failwith e }, Cmd.none
    | ReceiveUserErr e -> { model with UserState = Failed e }, Cmd.none