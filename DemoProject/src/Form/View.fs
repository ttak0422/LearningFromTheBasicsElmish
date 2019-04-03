module Form.View


open Fable.Core.JsInterop
open Fable.Helpers.React.Props
open System
module R = Fable.Helpers.React
open Types


let root model dispatch =
    let viewMemo memo =
        R.li [] [ R.str memo]
    R.div []
        [ R.input
            [ OnInput (fun x -> x.target?value |> string |> Input |> dispatch)
              Value model.Input ]
          R.button
            [ Disabled <| String.IsNullOrEmpty (model.Input)
              OnClick (fun _ -> dispatch Submit) ]
            [ R.str "Submit" ]
          R.ul [] (List.map viewMemo model.Memos) ]