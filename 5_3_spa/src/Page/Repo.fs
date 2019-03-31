module Page.Repo


open Elmish
open Fable.Helpers.React.Props
module R = Fable.Helpers.React
open GitHub


// MODEL
type State =
    | Init
    | Loaded of Issue list
    | Error of exn

type Model =
    { UserName : string
      ProjectName : string
      State : State }


// UPDATE
type Msg =
    | GotIssues of Issue list
    | GotIssueError of exn

let update msg model : Model * Cmd<'a> =
    match msg with
    | GotIssues issues -> { model with State = Loaded issues }, Cmd.none
    | GotIssueError e -> { model with State = Error e }, Cmd.none


// VIEW
let viewIssues userName projectName (issue: Issue) =
    R.li []
        [ R.span [] [ R.str <| "[" + issue.State + "]" ]
          R.a
            [ Href <| GitHub.issueUrl userName projectName issue.Number
              Target "_blank" ]
            [ R.str ("#" + (string issue.Number))
              R.str issue.Title ] ]

let view model dispatch =
    match model.State with
    | Init -> R.str "loading..."
    | Loaded issues -> R.ul [] (List.map (viewIssues model.UserName model.ProjectName) issues)
    | Error e -> R.str <| string e


// INIT
let init userName projectName =
    let model =
        { UserName = userName
          ProjectName = projectName
          State = Init }
    let cmd = GitHub.getIssues GotIssues GotIssueError userName projectName
    model, cmd