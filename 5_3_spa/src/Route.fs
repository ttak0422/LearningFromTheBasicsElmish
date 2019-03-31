module Route


open Elmish.Browser.UrlParser


type Route =
    | Top
    | User of string
    | Repo of string * string
    with
        static member toHash =
            function
            | Top -> "#"
            | User userName -> "#/user/" + userName
            | Repo (userName, projectName) -> "#/repo/" + userName + "/" + projectName


let parser : Parser<Route -> Route, Route> =
    let curry f x y = f (x, y)
    oneOf [
        map Top top
        map User (s "user" </> str)
        map (curry Repo) (s "repo" </> str </> str)
    ]