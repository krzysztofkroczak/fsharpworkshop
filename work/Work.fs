module Work
open System

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
    if c.Id % 2 = 0 then (c, 120M)
    else (c, 80M)

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
    