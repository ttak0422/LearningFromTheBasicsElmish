module Form.View

open System
open Fable.React
open Fable.React.Props
open Fable.Core.JsInterop
open Types

let root model dispatch =
    let viewMemo memo = li [] [ str memo ]
    div [] [ input [ OnInput(fun x -> x.Value |> Input |> dispatch); Value model.Input ]
             button [ Disabled <| String.IsNullOrWhiteSpace(model.Input)
                      OnClick(fun _ -> dispatch Submit) ] [ str "Submit" ]
             ul [] (List.map viewMemo model.Memos) ]