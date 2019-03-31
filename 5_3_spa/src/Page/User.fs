module Page.User


open Elmish
open Fable.Helpers.React.Props
module R = Fable.Helpers.React
open GitHub
open Route


// MODEL
type State =
    | Init
    | Loaded of Repo list
    | LoadError of exn

type Model =
    { State : State }


// UPDATE
type Msg =
    | GotRepos of Repo list
    | GotRepoError of exn

let update msg model =
    match msg with
    | GotRepos repos ->
        { State = Loaded repos }, Cmd.none
    | GotRepoError e ->
        { model with State = LoadError e}, Cmd.none


// VIEW
let viewLink path label =
    R.li [] [ R.a [ Href path ] [ R.str label ] ]

let view model dispatch =
    match model.State with
    | Init -> R.str "Loading..."
    | LoadError e -> R.str <| string e
    | Loaded repos ->
        R.ul []
            ( repos
            |> List.map (fun repo ->
                viewLink (Route.toHash <| Route.Repo (repo.Owner,repo.Name) ) (repo.Owner + "/" + repo.Name)) )


// INIT
let init userName =
    let model = { State = Init }
    let cmd = GitHub.getRepos GotRepos GotRepoError userName
    model, cmd