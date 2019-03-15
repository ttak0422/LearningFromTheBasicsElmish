module Counter


open Elmish
open Elmish.React
open Fable.Helpers.React.Props
open Fable.SimpleHttp
open Elmish.Browser.Navigation
module R = Fable.Helpers.React


// MODEL
type Model =
    { Result : string }

let init _ =
    ( { Result = "" }, Cmd.none)


// UPDATE
type Msg =
    | Click
    | GotRepo of Result<string, string>
    | GotError of exn

let getRepo () = async {
    let! (statusCode, responseText) = Http.get "https://api.github.com/repos/elmish/elmish"
    return
        match statusCode with
            | 200 -> Ok responseText
            | _ -> Error responseText
}

let update msg model =
    match msg with
    | Click ->
        ( model
        , Cmd.ofAsync getRepo () GotRepo GotError )
    | GotRepo (Ok repo) ->
        ( { model with Result = repo }
        , Cmd.none )
    | GotRepo (Error e) ->
        ( { model with Result = e }
        , Cmd.none )
    | GotError e ->
        ( { model with Result = (string e) }
        , Cmd.none )


// VIEW
let view model dispatch =
    R.div []
        [ R.button [ OnClick (fun _ -> dispatch Click) ] [ R.str "Get" ]
          R.p [] [ R.str model.Result ] ]


Program.mkProgram init update view
|> Program.withReact "elmish-app"
|> Program.run