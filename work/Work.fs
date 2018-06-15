module Work

type Customer = { Id : int ; IsVip : bool; Credit : decimal}

let c = {Id=3; IsVip=true;Credit=100M}

let getPurchases c = 
    if c.Id % 2 = 0 then (c, 120M)
    else (c, 80M)

let tryPromoteToVip purchases = 
    let customer, amount = purchases
    if amount > 100M then { customer with IsVip = true}
    else customer


let increaseCredit condition customer = 
    let c = (if condition customer then 100M else 50M) 
    {customer with Credit = customer.Credit + c}

let increaseCreditOnCustomerIfVip = increaseCredit (fun c->c.IsVip)


let upgradeCustomer = 
     getPurchases 
    >> tryPromoteToVip 
    >> increaseCreditOnCustomerIfVip