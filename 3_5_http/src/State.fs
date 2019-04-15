module Http.State

open Elmish
open Fable.PowerPack
open Fable.PowerPack.Fetch
open Types

let getRepo url =
    promise { let! res = fetch url []
              return! res.text() }
let init() : Model * Cmd<Msg> = { Result = "" }, Cmd.none

let update msg model =
    match msg with
    | Click ->
        model,
        (Cmd.ofPromise getRepo "https://api.github.com/repos/elmish/elmish"
             GotRepo GotRepoErr)
    | GotRepo repo -> { model with Result = repo }, Cmd.none
    | GotRepoErr e -> { model with Result = string e }, Cmd.none
