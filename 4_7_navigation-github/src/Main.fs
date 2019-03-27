module Main

(*
    TODO: ハッシュを用いない場合のスマートなルーティングver
*)

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Elmish.React
open Fable.Import.Browser
open Fable.Helpers.React.Props
module R = Fable.Helpers.React
open Route
open GitHub


// MODEL
type Page =
    | NotFound
    | ErrorPage of exn
    | TopPage
    | UserPage of Repo list
    | RepoPage of Issue list

type Model =
    { Page : Page }


// UPDATE
type Msg =
    | Loaded of Page
    | LoadFailed of exn

let update msg model =
    match msg with
    | Loaded page ->
        { model with Page = page }, Cmd.none
    | LoadFailed e ->
        { model with Page = ErrorPage e }, Cmd.none


// VIEW
let viewLink path label =
    R.li [] [ R.a [ Href <| Route.toHash path ] [ R.str label ] ]

let viewNotFoundPage =
    R.str "not found"

let viewErrorPage e =
    R.str <| string e

let viewTopPage =
    R.ul []
        [ viewLink (User "fable-compiler") "fable-compiler"
          viewLink (User "elmish") "elmish" ]

let viewUserPage repos =
    R.ul []
        ( repos |> List.map (fun repo -> viewLink (Repo (repo.Owner,repo.Name)) (repo.Owner + "/" + repo.Name) ) )

let viewRepoPage issues =
    let viewIssue issue =
        R.li []
            [ R.span [] [ R.str <| "[" + issue.State + "]" ]
              R.span [] [ R.str <| "#" + string issue.Number ]
              R.span [] [ R.str <| issue.Title ] ]
    R.ul []
        ( List.map viewIssue issues )

let view model dispatch =
    R.div []
        [ R.a [ Href "#"] [ R.h1 [] [ R.str "My GitHub Viewer" ] ]
          ( match model.Page with
            | NotFound -> viewNotFoundPage
            | ErrorPage e -> viewErrorPage e
            | TopPage -> viewTopPage
            | UserPage repos -> viewUserPage repos
            | RepoPage issues -> viewRepoPage issues
          )
        ]


// NAVIGATE
let urlUpdate result model =
    match result with
    // 即時更新できるとき -> 更新
    | Some Route.Top ->
        { model with Page = TopPage }, Cmd.none
    // 即時更新できないとき -> msgを利用してupdateで更新
    | Some (Route.User userName) ->
        model, getRepos (UserPage >> Loaded) LoadFailed userName
    | Some (Route.Repo (userName, projectName)) ->
        model, getIssues (RepoPage >> Loaded) LoadFailed userName projectName
    // パースに失敗したとき -> ここではTOPページにReplaceState
    | None ->
        console.error "error parsing url"
        model, Navigation.modifyUrl "#"


// SUBSCRIPTION
let subscription model =
    Cmd.none


let init result =
    urlUpdate result { Page = TopPage }


Program.mkProgram init update view
|> Program.toNavigable (parseHash parser) urlUpdate
|> Program.withSubscription subscription
|> Program.withReact "elmish-app"
|> Program.run