module Page.Top


open Elmish
open Fable.Core.JsInterop
open Fable.Helpers.React.Props
module R = Fable.Helpers.React
open GitHub
open Route


// MODEL
type LoadingStatus =
    | Init
    | Waiting
    | Loaded of User list
    | Failed of exn

type Model =
    { Input : string
      Status : LoadingStatus }


// UPDATE
type Msg =
    | Input of string
    | Send
    | Receive of User list
    | ReceiveError of exn

let update msg model =
    match msg with
    | Input newInput ->
        { model with Input = newInput }, Cmd.none
    | Send ->
        { model with
            Input = ""
            Status = Waiting }, GitHub.searchUsers Receive ReceiveError model.Input
    | Receive users ->
        { model with Status = Loaded users }, Cmd.none
    | ReceiveError e ->
        { model with Status = Failed e }, Cmd.none


// VIEW
let view model dispatch =
    let viewLink path label =
        R.li [] [ R.a [ Href path ] [ R.str label ] ]
    let viewUser user =
        viewLink (Route.toHash <| User user.Login) user.Login
    let viewUsers users =
        R.ul [] (List.map viewUser users)
    R.div[]
        [ R.input
            [ OnInput (fun x -> x.target?value |> string |> Input |> dispatch)
              Value model.Input
              Placeholder "GitHub name"
              AutoFocus true ]
          R.button
            [ Disabled <| (model.Input.Length < 1 )
              OnClick (fun _ -> dispatch Send) ]
            [ R.str "Send" ]
          ( match model.Status with
            | Init -> R.str ""
            | Waiting -> R.str "waiting..."
            | Loaded users -> viewUsers users
            | Failed e -> R.str <| string e ) ]


// INIT
let init () =
    { Input = ""
      Status = Init }