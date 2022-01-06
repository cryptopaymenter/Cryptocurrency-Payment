using System;
using System.Threading;
using System.Collections.Generic;
using SimpleHttpServer;
using SimpleHttpServer.Models;

namespace Cryptopayment_server
{
    //https://github.com/jeske/SimpleHttpServer

    public enum CurrencyIndex
    {
        Tron = 0
    };


    class Program
    {
        static int ServerPort = 2002;
        public static int CurrencyCount = System.Enum.GetValues(typeof(CurrencyIndex)).Length;

        public static string[] MyWallets = null;
        public static string[] MyPrivateKeys = null;

        //static List<Route> Routes = new List<Route>();

        static void Main(string[] args)
        {
            Console.WriteLine("Simple crypto payment http server\n");

           
            MyWallets = new string[CurrencyCount];
            MyPrivateKeys = new string[CurrencyCount];

            MyWallets[(int)CurrencyIndex.Tron] = "Your Tron Address";
            MyPrivateKeys[(int)CurrencyIndex.Tron] = "Your Tron Key";
            HttpServer server = new HttpServer(ServerPort, Routes.GET);
            Thread t = new Thread(new ThreadStart(server.Listen));
            t.Start();
        }
    }


    static class Routes
    {
        static APIRequest API = new APIRequest();
        
        static int min_wallet_addr_length = 8;
        static int max_wallet_addr_length = 512;

        public static List<Route> GET
        {
            get
            {
                try
                {
                    return new List<Route>()
                {
                    new Route()
                    {
                        Callable = HomeIndex,
                        UrlRegex = "^\\/$",
                        Method = "GET"
                    },new Route()
                    {
                        Callable = testIndex,
                        UrlRegex = "^\\/get_data",
                        Method = "GET"
                    },new Route()
                    {
                        Callable = RequestPayment,
                        UrlRegex = "^\\/requestpayment",
                        Method = "GET"
                    },new Route()
                    {
                        Callable = StartPayment,
                        UrlRegex = "^\\/startpayment",
                        Method = "GET"
                    },new Route()
                    {
                        Callable = GetMinimumFee,
                        UrlRegex = "^\\/getminimumfee",
                        Method = "GET"
                    }
                    ,new Route()
                    {
                        Callable = GetServerWallet,
                        UrlRegex = "^\\/getserverwalletaddress",
                        Method = "GET"
                    }
                };
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Routes::GET ~> " + ex.Message + " > " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    return new List<Route>();
                }
            }
        }


        private static HttpResponse testIndex(HttpRequest request)
        {

            return new HttpResponse()
            {
                ContentAsUTF8 = "TEST",
                ReasonPhrase = "OK",
                StatusCode = "200"
            };
        }

        private static HttpResponse HomeIndex(HttpRequest request)
        {
            return new HttpResponse()
            {
                ContentAsUTF8 = "Hello",
                ReasonPhrase = "OK",
                StatusCode = "200"
            };
        }

        private static HttpResponse RequestPayment(HttpRequest request)
        {
            try
            {
                if (request.GetParam == null || request.GetParam.Count < 3)
                    return returnFail();
                else
                {
                    string ccID = request.GetParam["ccID"];
                    string amount = request.GetParam["amount"];
                    string payment_password = request.GetParam["paymentpassword"];
                    string billsender = request.GetParam["billSender"].Replace(" ", "");
                    double damount = 0;
                    if (payment_password == null) payment_password = "";
                    if (billsender == null) billsender = "";

                    if (int.Parse(ccID) >= Program.CurrencyCount)
                        return returnFail("undefined currency id error");
                    if (ccID == null || ccID == "")
                        return returnFail("null currency id error");
                    if (amount == null || amount == "" || amount == "0" || !double.TryParse(amount, out damount) || damount == 0)
                        return returnFail("amount error");
                    if (payment_password != null && payment_password.Length > 0 && isExistSpecialChar(payment_password))
                        return returnFail("Special characters cannot be used in the payment password. (error)");
                    if (payment_password.Length > 16)
                        return returnFail("Too long passworde error");
                    if (request.RemoteIP == null || request.RemoteIP.Length < 7 || request.RemoteIP.Length > 45)
                        return returnFail("fail to identify ip error");

                    if (billsender.Length < min_wallet_addr_length || billsender.Length > max_wallet_addr_length || isExistSpecialChar(billsender))
                        return returnFail("wallet address error");

                    string result = API.RequetPayment(request.RemoteIP, ccID, billsender, payment_password, amount);
                    return new HttpResponse()
                    {
                        ContentAsUTF8 = result,
                        ReasonPhrase = "OK",
                        StatusCode = "200"
                    };
                }
            }
            catch(Exception ex)
            {
                return returnFail("Unknown Error. Check parameters.");
            }
        }

        private static HttpResponse StartPayment(HttpRequest request)
        {
            try
            {
                if (request.GetParam == null || request.GetParam.Count < 3)
                    return returnFail();
                else
                {
                    string ccID = request.GetParam["ccID"];
                    if (int.Parse(ccID) >= Program.CurrencyCount)
                        return returnFail("undefined currency id error");

                   
                    string amount = request.GetParam["amount"];
                    string payment_password = request.GetParam["paymentpassword"];
                    string cwaddr = Program.MyWallets[int.Parse(ccID)].Replace(" ", "");
                    string txid = request.GetParam["txID"];
                    double damount = 0;
                    if (payment_password == null) payment_password = "";
                    if (cwaddr == null) cwaddr = "";

                  
                    if (ccID == null || ccID == "")
                        return returnFail("null currency id error");
                    if (amount == null || amount == "" || amount == "0" || !double.TryParse(amount, out damount) || damount == 0)
                        return returnFail("amount error");
                    if (payment_password != null && payment_password.Length > 0 && isExistSpecialChar(payment_password))
                        return returnFail("Special characters cannot be used in the payment password. (error)");
                    if (payment_password.Length > 16)
                        return returnFail("Too long passworde error");
                    if(request.RemoteIP == null || request.RemoteIP.Length < 7 || request.RemoteIP.Length > 45)
                        return returnFail("fail to identify ip error");
                    if (cwaddr.Length < min_wallet_addr_length || cwaddr.Length > max_wallet_addr_length || isExistSpecialChar(cwaddr))
                        return returnFail("error");
                    if (txid.Length < min_wallet_addr_length || txid.Length > max_wallet_addr_length || isExistSpecialChar(txid))
                        return returnFail("transaction check error");

                    string result = API.StartPayment(request.RemoteIP, ccID, cwaddr, payment_password, amount, txid);
                    return new HttpResponse()
                    {
                        ContentAsUTF8 = result,
                        ReasonPhrase = "OK",
                        StatusCode = "200"
                    };
                }
            }
            catch (Exception ex)
            {
                return returnFail("Unknown Error. Check parameters.");
            }
        }

        private static HttpResponse Refund(HttpRequest request)
        {
            try
            {
                if (request.GetParam == null || request.GetParam.Count < 3)
                    return returnFail();
                else
                {
                    string ccID = request.GetParam["ccID"];
                    if (int.Parse(ccID) >= Program.CurrencyCount)
                        return returnFail("undefined currency id error");

                    string refundamount = "10"; //You need server side logic. This is just example.
                    string refundaddress = request.GetParam["refundaddress"];                    
                    string cwaddr = Program.MyWallets[int.Parse(ccID)].Replace(" ", "");
                    string privatekey = Program.MyPrivateKeys[int.Parse(ccID)].Replace(" ", "");
                    double damount = 0;
                    if (cwaddr == null) cwaddr = "";
                    if (ccID == null || ccID == "")
                        return returnFail("null currency id error");
                    if (refundamount == null || refundamount == "" || refundamount == "0" || !double.TryParse(refundamount, out damount) || damount == 0)
                        return returnFail("amount error");
                    if (request.RemoteIP == null || request.RemoteIP.Length < 7 || request.RemoteIP.Length > 45)
                        return returnFail("fail to identify ip error");
                    if (refundaddress.Length < min_wallet_addr_length || refundaddress.Length > max_wallet_addr_length || isExistSpecialChar(refundaddress))
                        return returnFail("error");
                    if (cwaddr.Length < min_wallet_addr_length || cwaddr.Length > max_wallet_addr_length || isExistSpecialChar(cwaddr))
                        return returnFail("error");
                    if (privatekey.Length < min_wallet_addr_length || privatekey.Length > max_wallet_addr_length || isExistSpecialChar(privatekey))
                        return returnFail("transaction check error");

                    string result = API.Refund(request.RemoteIP, ccID, cwaddr, privatekey, refundamount, refundaddress); //cwaddr to refundaddress
                    return new HttpResponse()
                    {
                        ContentAsUTF8 = result,
                        ReasonPhrase = "OK",
                        StatusCode = "200"
                    };
                }
            }
            catch (Exception ex)
            {
                return returnFail("Unknown Error. Check parameters.");
            }
        }

        private static HttpResponse GetMinimumFee(HttpRequest request)
        {
            string result = API.GetMinimumFee(request.RemoteIP, "0");
            return new HttpResponse()
            {
                ContentAsUTF8 = result,
                ReasonPhrase = "OK",
                StatusCode = "200"
            };
        }

        private static HttpResponse GetServerWallet(HttpRequest request)
        {
            string result = API.GetServerWalletAddress(request.RemoteIP, "0");
            return new HttpResponse()
            {
                ContentAsUTF8 = result,
                ReasonPhrase = "OK",
                StatusCode = "200"
            };
        }




        private static HttpResponse returnFail(string msg = "fail")
        {
            return new HttpResponse()
            {
                ContentAsUTF8 = msg,
                ReasonPhrase = "OK",
                StatusCode = "200"
            };
        }

        private static bool isExistSpecialChar(string msg)
        {
            string str = @"[~!@\#$%^&*\()\=+|\\/:;?""<>']";
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(str);
            return rex.IsMatch(msg);
        }
    }
}
