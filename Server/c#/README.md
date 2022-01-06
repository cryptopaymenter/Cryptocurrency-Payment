# Cryptocurrency-Payment C#
 C# Example
 Open Program.cs  
 Change MyWallets, MyPrivateKeys in Main()  
   
Build and open Cryptopayment-server.exe.  
You must allow the program in the firewall and enable port forwarding for it to work.  
  
  
  
# Usage from end user point of view

GetMinimumFee API  
http://127.0.0.1:2002/getminimumfee  
  
GetServerWalletAddress API  
http://127.0.0.1:2002/getserverwalletaddress  
  
RequestPayment API example  
http://127.0.0.1:2002/requestpayment?ccID=0&amount=10&payment_password=qwerty&billSender=TZEnnFyXxWfJ1nDzKAr8Dfkt4H7bgppFk2  
  
StartPayment API example  
http://127.0.0.1:2002/startpayment?ccID=0&amount=10&payment_password=qwerty&txID=a190dbf8aa3f8777203e14824723736ef69a47e4692ae215119745a080ed5e7e  
  
