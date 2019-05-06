module Http.State

// いろいろなアプローチ
// Check [fable-fetch](https://github.com/fable-compiler/fable-fetch)

open Elmish
open Fable.SimpleHttp
open Types

let getRepo url =
    async {
        let! (status, res) = Http.get url
        return 
            match status with
            | 200 -> Ok res
            | x -> Error <| failwithf "Status : %d" x
    }

let init () =
    { Result = "" }, Cmd.none

let update msg model =
    match msg with
    | Click ->
        model, (Cmd.OfAsync.either getRepo "https://api.github.com/repos/elmish/elmish" GotRepo GotRepoErr)
    | GotRepo (Ok repo) ->
        { model with Result = repo }, Cmd.none
    | GotRepo (Error e) ->
        { model with Result = string e }, Cmd.none
    | GotRepoErr e ->
        { model with Result = string e }, Cmd.none