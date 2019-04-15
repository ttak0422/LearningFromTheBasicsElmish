module SearchBox.GitHub

open Elmish
open Fable.PowerPack
open Fable.PowerPack.Fetch

module D = Thoth.Json.Decode

type User =
    { Login : string
      AvatarUrl : string
      Name : string
      HtmlUrl : string
      Bio : Option<string> }

let userDecoder : D.Decoder<User> =
    D.object (fun get ->
        { Login = get.Required.Field "login" D.string
          AvatarUrl = get.Required.Field "avatar_url" D.string
          Name = get.Required.Field "name" D.string
          HtmlUrl = get.Required.Field "html_url" D.string
          Bio = get.Optional.Field "bio" D.string })

let getUser toOkMsg toErrMsg name =
    let fetch (url, (decoder : D.Decoder<'a>)) =
        promise { return! fetchAs<'a> url decoder [] }
    let url = "https://api.github.com/users/" + name
    Cmd.ofPromise fetch (url, userDecoder) toOkMsg toErrMsg
