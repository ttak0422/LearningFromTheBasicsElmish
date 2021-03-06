module SPA.Route

open Elmish.UrlParser

type UserName = string

type ProjectName = string

type Route =
    | Top
    | User of UserName
    | Repo of UserName * ProjectName
    static member ToHash =
        function
        | Top -> "#"
        | User userName -> "#user/" + userName
        | Repo (userName, projectName) -> "#repo/" + userName + "/" + projectName

let parser : Parser<Route->Route, Route> =
    let curry f x y = f (x, y)
    oneOf[
        map Top top
        map User (s "user" </> str)
        map (curry Repo) (s "repo" </> str </> str)
    ]