module SearchBox.View

open Fable.Core.JsInterop
open Fable.Helpers.React.Props
open System
open Types

module R = Fable.Helpers.React

let root model dispatch =
    let internalView model =
        match model.UserState with
        | Init -> R.str ""
        | Waiting -> R.str "Waiting..."
        | Loaded user ->
            R.a [ Href user.HtmlUrl
                  Target "_blank" ] [ R.img [ Src user.AvatarUrl
                                              Style [ Width 200 ] ]
                                      R.div [] [ R.str user.Name ]
                                      R.div [] [ (match user.Bio with
                                                  | Some x -> R.str x
                                                  | None -> R.str "") ] ]
        | Failed e -> R.str (string e)
    R.div [] [ R.div []
                   [ R.input [ OnChange(fun e ->
                                   e.target?value
                                   |> string
                                   |> Input
                                   |> dispatch)
                               AutoFocus true
                               Placeholder "GitHub name"
                               Value model.Input ]

                     R.button [ Disabled
                                    ((model.UserState = Waiting)
                                     || (String.IsNullOrWhiteSpace model.Input))
                                OnClick(fun _ -> dispatch Send) ]
                         [ R.str "Submit" ] ]
               R.div [] [ internalView model ] ]
