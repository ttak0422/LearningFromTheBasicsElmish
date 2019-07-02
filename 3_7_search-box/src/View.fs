module SearchBox.View

open System
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open GitHub
open Types

let root model dispatch =
    let showUser user =
        a [ Href user.HtmlUrl
            Target "_blank" ]
          [ img [ Src user.AvatarUrl
                  Style [ Width 200 ] ]
            div [] [str user.Name ]
            div [] [ (match user.Bio with
                      | Some x -> str x
                      | None -> str "") ] ]

    let internalView model =
        match model.UserState with
        | Init -> str ""
        | Waiting -> str "Waiting..."
        | Loaded user -> showUser user
        | Failed e -> str <| string e

    div [] [
        div [] [ input [ OnInput(fun x -> x.target?value |> string |> Input |> dispatch)
                         Value model.Input
                         AutoFocus true
                         Placeholder "GitHub user" ]
                 button [ Disabled
                            ((String.IsNullOrWhiteSpace model.Input)
                            || (model.UserState = Waiting))
                          OnClick(fun _ -> dispatch Send) ]
                       [ str "Submit" ] ]
        div [] [ internalView model ] ]
