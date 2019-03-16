module Counter

(*
    ベータ版のFableコンパイラを利用しています
    Requires fable-compiler 2.2.0-beta-011 or higher
    エラー整形無し
*)

open Elmish
open Elmish.React
open Fable.Helpers.React.Props
open Fable.Core.JsInterop
open Fable.SimpleHttp
open System
module R = Fable.Helpers.React


// TYPE
type GitHubUser = Fable.JsonProvider.Generator<"""
{
  "login": "octocat",
  "id": 1,
  "node_id": "MDQ6VXNlcjE=",
  "avatar_url": "https://github.com/images/error/octocat_happy.gif",
  "gravatar_id": "",
  "url": "https://api.github.com/users/octocat",
  "html_url": "https://github.com/octocat",
  "followers_url": "https://api.github.com/users/octocat/followers",
  "following_url": "https://api.github.com/users/octocat/following{/other_user}",
  "gists_url": "https://api.github.com/users/octocat/gists{/gist_id}",
  "starred_url": "https://api.github.com/users/octocat/starred{/owner}{/repo}",
  "subscriptions_url": "https://api.github.com/users/octocat/subscriptions",
  "organizations_url": "https://api.github.com/users/octocat/orgs",
  "repos_url": "https://api.github.com/users/octocat/repos",
  "events_url": "https://api.github.com/users/octocat/events{/privacy}",
  "received_events_url": "https://api.github.com/users/octocat/received_events",
  "type": "User",
  "site_admin": false,
  "name": "monalisa octocat",
  "company": "GitHub",
  "blog": "https://github.com/blog",
  "location": "San Francisco",
  "email": "octocat@github.com",
  "hireable": false,
  "bio": "There once was...",
  "public_repos": 2,
  "public_gists": 1,
  "followers": 20,
  "following": 0,
  "created_at": "2008-01-14T04:33:35Z",
  "updated_at": "2008-01-14T04:33:35Z"
}
""">

// MODEL
type UserState =
    | Init
    | Waiting
    | Loaded of GitHubUser
    | Failed of exn

type Model =
    { Input : string
      UserState : UserState }

let init _ =
    ( { Input = ""
        UserState = Init }
    , Cmd.none )


// UPDATE
type Msg =
    | Input of string
    | Send
    | Receive of Result<GitHubUser, exn>
    | ReceiveError of exn

let get input = async {
    let! (statusCode, response) = Http.get <| "https://api.github.com/users/" + input
    return
        match statusCode with
        | 200 -> Ok <| GitHubUser response
        | _ -> Error <| failwith response
}

let update msg model =
    match msg with
    | Input input -> ( { model with Input = input }, Cmd.none )
    | Send ->
        ( { model with
                Input = ""
                UserState = Waiting }
        , Cmd.ofAsync get model.Input Receive ReceiveError )
    | Receive (Ok user) -> ( { model with UserState = Loaded user }, Cmd.none )
    | Receive (Error e) -> ( { model with UserState = Failed e }, Cmd.none )
    | ReceiveError e -> ( { model with UserState = Failed e }, Cmd.none )


// VIEW
let view model dispatch =
    R.div []
        [ R.div []
            [ R.input
                [ OnChange (fun e -> e.target?value |> string |> Input |> dispatch )
                  AutoFocus true
                  Placeholder "GitHub name"
                  Value model.Input ]
              R.button
                [ Disabled ((model.UserState = Waiting) || (String.IsNullOrWhiteSpace model.Input))
                  OnClick (fun _ -> dispatch Send) ]
                [ R.str "Submit" ] ]
          R.div []
            [ ( match model.UserState with
                | Init -> R.str ""
                | Waiting -> R.str "Waiting..."
                | Loaded user ->
                    R.a
                        [ Href user.html_url
                          Target "_blank" ]
                        [ R.img
                            [ Src user.avatar_url
                              Style [ Width 200 ] ]
                          R.div [] [ R.str user.name ]
                          R.div []
                            [ ( match user.bio with
                                | x when String.IsNullOrWhiteSpace x -> R.str ""
                                | x -> R.str x ) ] ]
                | Failed e ->
                    R.div [] [ R.str (string e) ] ) ] ]


Program.mkProgram init update view
|> Program.withReact "elmish-app"
|> Program.run