module Counter


open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Elmish.React
open Elmish.ReactNative
open Fable.Helpers.React.Props
module R = Fable.Helpers.React
open Route



// MODEL
type Page =
    | NotFound
    | TopPage of Page.Top.Model
    | UserPage of Page.User.Model
    | RepoPage of Page.Repo.Model

type Model =
    { Page : Page }


// UPDATE
type Msg =
    | TopMsg of Page.Top.Msg
    | UserMsg of Page.User.Msg
    | RepoMsg of Page.Repo.Msg

let update msg model =
    match msg with
    | TopMsg topMsg ->
        match model.Page with
        | TopPage topModel ->
            let (newTopModel, topCmd) = Page.Top.update topMsg topModel
            { model with Page = TopPage newTopModel }, Cmd.map TopMsg topCmd
        | _ -> model, Cmd.none
    | UserMsg userMsg ->
        match model.Page with
        | UserPage userModel ->
            let (newUserModel, userCmd) = Page.User.update userMsg userModel
            { model with Page = UserPage newUserModel }, Cmd.map UserMsg userCmd
        | _ -> model, Cmd.none
    | RepoMsg repoMsg ->
        match model.Page with
        | RepoPage repoModel ->
            let (newRepoModel, repoCmd) = Page.Repo.update repoMsg repoModel
            { model with Page = RepoPage newRepoModel }, Cmd.map RepoMsg repoCmd
        | _ -> model, Cmd.none


// NAVIGATE
let urlUpdate result model =
    match result with
    | None ->
        { model with Page = NotFound }, Cmd.none
    | Some Route.Top ->
        { model with Page = TopPage (Page.Top.init()) }, Cmd.none
    | Some (Route.User userName) ->
        let (userModel, userCmd) = Page.User.init userName
        { model with Page = UserPage userModel }, Cmd.map UserMsg userCmd
    | Some (Route.Repo (userName, projectName)) ->
        let (repoModel, repoCmd) = Page.Repo.init userName projectName
        { model with Page = RepoPage repoModel }, Cmd.map RepoMsg repoCmd


// SUBSCRIPTIONS
let subscriptions _ =
    Cmd.none


// VIEW
let viewNotFound =
    R.str "not found"

let view model dispatch =
    R.div []
        [ R.a [ Href "#" ][ R.h1[] [R.str "My GitHub Viewer" ] ]
          ( match model.Page with
            | NotFound -> viewNotFound
            | TopPage topPageModel -> Page.Top.view topPageModel (TopMsg >> dispatch)
            | UserPage userPageModel -> Page.User.view userPageModel (UserMsg >> dispatch)
            | RepoPage repoPageModel -> Page.Repo.view repoPageModel (RepoMsg >> dispatch) ) ]


// INIT
let init result =
    urlUpdate result { Page = TopPage (Page.Top.init()) }


Program.mkProgram init update view
|> Program.toNavigable (parseHash parser) urlUpdate
|> Program.withSubscription subscriptions
|> Program.withReact "elmish-app"
|> Program.run

