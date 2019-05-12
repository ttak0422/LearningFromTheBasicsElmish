module SearchBox.GitHub

open Fable.SimpleHttp
open Thoth.Json

module D = Thoth.Json.Decode

type User =
    { Login : string
      AvatarUrl : string
      Name : string
      HtmlUrl : string
      Bio : Option<string> }


    static member Decoder : Decoder<User> =
        Decode.object (fun get ->
            { Login = get.Required.Field "login" Decode.string
              AvatarUrl = get.Required.Field "avatar_url" D.string
              Name = get.Required.Field "name" D.string
              HtmlUrl = get.Required.Field "html_url" D.string
              Bio = get.Optional.Field "bio" D.string })



let getUser name =
    let fetchUser name =
        Http.get <| "https://api.github.com/users/" + name
    async {
        let! (status, res) = fetchUser name
        return
            match status with
            | 200 -> Decode.fromString User.Decoder res
            | x -> Error <| failwithf "Status : %d" x
    }