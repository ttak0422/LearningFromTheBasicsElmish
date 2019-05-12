module Clock.Types

open System

type Model =
    { Time : DateTime }

type Msg =
    | Tick of DateTime