module GitHub

(*
    型注釈書きましょう！！
    TODO: URLのパーセントエンコーディング
*)

open Elmish
open Fable.PowerPack
open Fable.PowerPack.Fetch
module D = Thoth.Json.Decode


type Repo =
    { Name : string
      Description : Option<string>
      Language : Option<string>
      Owner : string
      Fork : int
      Star : int
      Watch : int }

type Issue =
    { Number : int
      Title : string
      State : string }

type User =
    { Login : string
      AvatarUrl : string }


let private repoDecoder : D.Decoder<Repo> =
    D.map7 (fun a b c d e f g -> {
            Name = a
            Description = b
            Language = c
            Owner = d
            Fork = e
            Star = f
            Watch = g } )
        (D.field "name" D.string)
        (D.option (D.field "description" D.string))
        (D.option (D.field "language" D.string))
        (D.at [ "owner"; "login" ] D.string)
        (D.field "forks_count" D.int)
        (D.field "stargazers_count" D.int)
        (D.field "watchers_count" D.int)

let private reposDecoder : D.Decoder<Repo list> =
    D.list repoDecoder

let private issueDecoder : D.Decoder<Issue> =
    D.map3 (fun a b c ->
        { Number = a
          Title = b
          State = c } )
        (D.field "number" D.int)
        (D.field "title" D.string)
        (D.field "state" D.string)

let private issuesDecoder : D.Decoder<Issue list> =
    D.list issueDecoder

let userDecoder : D.Decoder<User> =
    D.map2 (fun a b ->
        { Login = a
          AvatarUrl = b })
          (D.field "login" D.string)
          (D.field "avatar_url" D.string)

let usersDecoder : D.Decoder<User list> =
    D.list userDecoder

let searchUsersDecoder : D.Decoder<User list> =
    D.field "items" usersDecoder

let private fetch (url, (decoder:D.Decoder<'a>)) =
    promise {
        return! fetchAs<'a> url decoder []
    }

(*
    3_5_httpの例のようにFable.SimpleHttp使ってエラー処理すると楽
    手抜き＆fable Powerpack(http://fable.io/fable-powerpack/)の紹介用
    APIの返答がError: 404 Not Found for URLでも動作自体にエラーが無ければOKになる．
*)
let getRepos toOkMsg toErrMsg userName =
    let rawUrl = "https://api.github.com/users/" + userName + "/repos"
    Cmd.ofPromise fetch (rawUrl, reposDecoder) toOkMsg toErrMsg

(*
    上に同じ
*)
let getIssues toOkMsg toErrMsg userName projectName =
    let rawUrl = "https://api.github.com/repos/" + userName  + "/" + projectName + "/issues"
    Cmd.ofPromise fetch (rawUrl, issuesDecoder) toOkMsg toErrMsg

let searchUsers toOkMsg toErrMsg userName =
    let rawUrl = "https://api.github.com/search/users?q=" + userName
    Cmd.ofPromise fetch (rawUrl, searchUsersDecoder) toOkMsg toErrMsg

let issueUrl userName projectName (issueNumber : int) =
    "https://github.com/" + userName + "/" + projectName + "/issues/" + (string issueNumber)