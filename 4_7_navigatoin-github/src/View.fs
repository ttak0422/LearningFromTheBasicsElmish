module Nav.View

open Fable.React
open Fable.React.Props
open GitHub
open Route
open Types

let viewLink path label =
    li [] [ a [ Href <| Route.ToHash path ] [ str label ] ]

let viewNotFoundPage =
    str "not found"

let viewErrPage e =
    str <| string e

let viewTopPage =
    ul []
        [ viewLink (User "fable-compiler") "fable-compiler"
          viewLink (User "elmish") "elmish" ]

let viewUserPage repos =
    ul []
        (repos |> List.map (fun r -> viewLink (Repo (r.Owner, r.Name)) (r.Owner + "/" + r.Name)))

let viewRepoPage issues =
    let viewIssue issue =
        li []
            [ span [] [ str <| "[" + issue.State + "]" ]
              span [] [ str <| "#" + string issue.Number ]
              span [] [ str <| issue.Title ] ]
    ul []
        (List.map viewIssue issues)

let root model dispatch =
    div []
        [ a [ Href "#" ] [ h1 [] [ str "My GitHub Viewer" ] ]
          (match model.Page with
           | NotFound -> viewNotFoundPage
           | ErrPage e -> viewErrPage e
           | TopPage -> viewTopPage
           | UserPage (Ok r) -> viewUserPage r
           | UserPage (Error r) -> viewErrPage <| failwithf "Error : %s" r
           | RepoPage (Ok i) -> viewRepoPage i
           | RepoPage (Error i) -> viewErrPage <| failwithf "Error : %s" i) ]
