module Work
open System
open FSharp.Data

[<Literal>]
let file = """C:\Users\Krzysztof.Kroczak\Documents\Data.json"""

type Json = JsonProvider<file>


[<Measure>]
type EUR

[<Measure>]
type USD

type Notifications = 
| NoNotifications
| ReceiveNotifications of receiveDeals : bool * receiveAlerts : bool

type PersonalDetails = {FirstName : string; LastName : string; DateOfBirth:DateTime}

type Customer = { 
    Id : int
    IsVip : bool
    Credit : decimal<USD>
    PersonalDetails : PersonalDetails option
    Notifications : Notifications
    }


let getPurchases c = 
    let purchases = 
        Json.Load file
        |> Seq.filter (fun x-> x.CustomerId = c.Id)
        |> Seq.collect (fun x->x.PurchasesByMonth)
        |> Seq.average
    (c, purchases)

let tryPromoteToVip purchases = 
    let customer, amount = purchases
    if amount > 100M then { customer with IsVip = true}
    else customer


let increaseCredit condition customer = 
    let c = (if condition customer then 100M<USD> else 50M<USD>) 
    {customer with Credit = customer.Credit + c}

let increaseCreditOnCustomerIfVip = increaseCredit (fun c->c.IsVip)


let upgradeCustomer = 
     getPurchases 
    >> tryPromoteToVip 
    >> increaseCreditOnCustomerIfVip


let isAdult c = 
    match c.PersonalDetails with
    | None -> false
    | Some value -> value.DateOfBirth.Date >= (DateTime.Now.AddYears -18).Date

let getAlert customer = 
    match customer.Notifications with
    | ReceiveNotifications (receiveAlerts=true)->  sprintf "%d" customer.Id
    | _ -> ""
    


let getCustomer id = {Id=id; IsVip = false; Credit=10M<USD>; PersonalDetails=None; Notifications=NoNotifications }

type CustomerService() =
    member this.UpgradeCustomer id = getCustomer id |> upgradeCustomer