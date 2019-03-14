module Counter

(*
    elm-bookとは一部異なっています．
*)

open Elmish
open Elmish.React
open Elmish.Browser.Navigation
open Fable.Core.JsInterop
open Fable.Import.Browser
open Fable.Helpers.React.Props
module R = Fable.Helpers.React


// MODEL
type Model =
    { Input : string
      Memos : string list }

let init () =
    { Input = ""
      Memos = [] }


// UPDATE
type Msg =
    | Input of value : string
    | Submit

let update msg model =
    match msg with
    | Input input ->
        console.log "input"
        { model with Input = input }
    | Submit ->
        console.log "submit"
        { model with
            Input = ""
            Memos = model.Input :: model.Memos }


// VIEW
let view model dispatch =
    let viewMemo memo =
        R.li [] [ R.str memo ]
    R.div []
        [ R.input
            [ OnInput (fun x -> x.target?value |> string |> Input |> dispatch)
              Value model.Input ]
          R.button
            [ OnClick (fun _ -> dispatch Submit) ]
            [ R.str "Submit" ]
          R.ul [] (List.map viewMemo model.Memos)
        ]


Program.mkSimple init update view
|> Program.withReact "elmish-app"
|> Program.run