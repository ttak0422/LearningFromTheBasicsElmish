module Nav.State

open Elmish
open GitHub
open Route
open Types
open Elmish.Navigation

let urlUpdate result model =
    match result with
    | Some Route.Top ->
        { model with Page = TopPage }, Cmd.none
    | Some (Route.User userName) ->
        model, Cmd.OfAsync.either getRepos userName (UserPage >> Loaded) Loadfailed
    | Some (Route.Repo (userName, projectName)) ->
        model, Cmd.OfAsync.either getIssues (userName, projectName) (RepoPage >> Loaded) Loadfailed
    | None ->
        Fable.Core.JS.console.error "error parsing url"
        model, Navigation.modifyUrl "#"

let init result =
    urlUpdate result { Page = TopPage }

let update msg model =
    match msg with
    | Loaded page ->
        { model with Page = page }, Cmd.none
    | Loadfailed e ->
        { model with Page = ErrPage e }, Cmd.none