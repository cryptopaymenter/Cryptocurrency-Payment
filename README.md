# Cryptocurrency-Payment
 Cryptocurrency payment system using API web  
   
This project can only be used if the apidigitalcurrency.com server is alive.  
# Currency index used for api parameters (ccID)
0: Tron  
  
  
  
  
  
In the future, other cryptocurrencies will be supported by the payment system.  
  
  
  
  
# POST parameters
# apidigitalcurrency.com refund param
Refund exmple  
https://apidigitalcurrency.com/jsonapi/refund  
{  
"reqip":"End User IP",  
"ccID":0,  
"cwaddr":"wallet address",  
"clientprivatekey":"wallet private key",  
"refundamount":2000000,  
"refundaddress":"End user wallet address"  
}  
Coin will be transferred from "cwaddr" to "refundaddress".  
  
  
# apidigitalcurrency.com request payment param
Request param exmple  
https://apidigitalcurrency.com/jsonapi/requestpayment  
{  
"reqip":"End User IP",  
"ccID":0,  
"amount":2000000,  
"paymentPS":"123456",  
"billSender":"End user wallet address"  
}  
You must call this in advance to make a payment.
  
  
  
  
# apidigitalcurrency.com start payment param
https://apidigitalcurrency.com/jsonapi/startpayment  
{  
"reqip":"End User IP",  
"ccID":0,  
"amount":2000000,  
"cwaddr":"Your wallet address",  
"paymentPS":"The payment password end user wrote down in requestpayment",  
"txID":"Transaction ID"  
}  
This must be called after successful requestpayment process.  
Coin will be transferred from End user wallet to "cwaddr".  