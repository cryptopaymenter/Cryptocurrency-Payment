using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace Cryptopayment_server
{
    public class APIRequest
    {
        public APIRequest()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public readonly static string API_SERVER = "192.168.0.2";//"apidigitalcurrency.com";
        private readonly static string API_URL = "https://" + API_SERVER + "/jsonapi/";

        static string fail_str = "fail to payment error<br>";

        public string GetMinimumFee(string RemoteIP, string ccID)
        {
            string URL = API_URL + "getminimumfee";

            //string Num = "{\"num\": 200";
            //        //"{\"num\":200}";
            string postData = string.Format("{{\"reqip\":\"{0}\", \"ccID\":{1}}}", RemoteIP, ccID);


            byte[] data = Encoding.ASCII.GetBytes(postData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);

            request.Method = "POST";
            request.Host = API_SERVER;
            request.KeepAlive = true;
            request.AllowAutoRedirect = true;
            request.Accept = "*/*";
            request.ContentType = "*/*";
            request.Referer = "https://" + API_SERVER;
            request.Timeout = 300000;



            bool result = false;
            HttpWebResponse response = null;
            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return fail_str + ex.Message;
            }

            string resultdata = null;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                resultdata = new StreamReader(response.GetResponseStream()).ReadToEnd();

                //if (responseString.Contains("Success")) result = true;
                result = true;
            }

            if (result == true)
            {
                return resultdata;
            }

            return fail_str;
        }

        public string GetServerWalletAddress(string RemoteIP, string ccID)
        {
            string URL = API_URL + "getserverwalletaddress";

            //string Num = "{\"num\": 200";
            //        //"{\"num\":200}";
            string postData = string.Format("{{\"reqip\":\"{0}\", \"ccID\":{1}}}", RemoteIP, ccID);


            byte[] data = Encoding.ASCII.GetBytes(postData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);

            request.Method = "POST";
            request.Host = API_SERVER;
            request.KeepAlive = true;
            request.AllowAutoRedirect = true;
            request.Accept = "*/*";
            request.ContentType = "*/*";
            request.Referer = "https://" + API_SERVER;
            request.Timeout = 300000;



            bool result = false;
            HttpWebResponse response = null;
            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return fail_str + ex.Message;
            }

            string resultdata = null;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                resultdata = new StreamReader(response.GetResponseStream()).ReadToEnd();

                //if (responseString.Contains("Success")) result = true;
                result = true;
            }

            if (result == true)
            {
                return resultdata;
            }

            return fail_str;
        }


        public string RequetPayment(string RemoteIP, string ccID, string billSender, string paymentPS, string amount)
        {
            string URL = API_URL + "requestpayment";

            //string Num = "{\"num\": 200";
            //        //"{\"num\":200}";
            string postData = string.Format("{{\"reqip\":\"{0}\", \"ccID\":{1}, \"amount\":{2}, \"paymentPS\":\"{3}\", \"billSender\":\"{4}\"}}", RemoteIP, ccID, amount, paymentPS, billSender);


            byte[] data = Encoding.ASCII.GetBytes(postData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);

            request.Method = "POST";
            request.Host = API_SERVER;
            request.KeepAlive = true;
            request.AllowAutoRedirect = true;
            request.Accept = "*/*";
            request.ContentType = "*/*";
            request.Referer = "https://" + API_SERVER;
            request.Timeout = 300000;



            bool result = false;
            HttpWebResponse response = null;
            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return fail_str + ex.Message;
            }

            string resultdata = null;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                resultdata = new StreamReader(response.GetResponseStream()).ReadToEnd();

                //if (responseString.Contains("Success")) result = true;
                result = true;
            }

            if (result == true)
            {
                return resultdata;
            }

            return fail_str;
        }

        public string StartPayment(string RemoteIP, string ccID, string cwaddr, string paymentPS, string amount, string txid)
        {
            string URL = API_URL + "startpayment";

            //string Num = "{\"num\": 200";
            //        //"{\"num\":200}";
            string postData = string.Format("{{\"reqip\":\"{0}\", \"ccID\":{1}, \"amount\":{2}, \"paymentPS\":\"{3}\", \"cwaddr\":\"{4}\", \"txID\":\"{5}\"}}", RemoteIP, ccID, amount, paymentPS, cwaddr, txid);


            byte[] data = Encoding.ASCII.GetBytes(postData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);

            request.Method = "POST";
            request.Host = API_SERVER;
            request.KeepAlive = true;
            request.AllowAutoRedirect = true;
            request.Accept = "*/*";
            request.ContentType = "*/*";
            request.Referer = "https://" + API_SERVER;
            request.Timeout = 300000;



            bool result = false;
            HttpWebResponse response = null;
            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return fail_str + ex.Message;
            }

            string resultdata = null;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                resultdata = new StreamReader(response.GetResponseStream()).ReadToEnd();

                //if (responseString.Contains("Success")) result = true;
                result = true;
            }

            if (result == true)
            {
                return resultdata;
            }

            return fail_str;
        }

        public string Refund(string RemoteIP, string ccID, string cwaddr, string privatekey, string refundamount, string refundaddress)
        {
            string URL = API_URL + "refund";

            //string Num = "{\"num\": 200";
            //        //"{\"num\":200}";
            string postData = string.Format("" +
                "{" +
                "{\"reqip\":\"{0}\", " +
                "\"ccID\":{1}, " +
                "\"refundamount\":{2}, " +
                "\"cwaddr\":\"{3}\", " +
                "\"clientprivatekey\":\"{4}\", " +
                "\"refundaddress\":{5}}" +
                "}", RemoteIP, ccID, refundamount, cwaddr, privatekey, refundaddress);


            byte[] data = Encoding.ASCII.GetBytes(postData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);

            request.Method = "POST";
            request.Host = API_SERVER;
            request.KeepAlive = true;
            request.AllowAutoRedirect = true;
            request.Accept = "*/*";
            request.ContentType = "*/*";
            request.Referer = "https://" + API_SERVER;
            request.Timeout = 300000;



            bool result = false;
            HttpWebResponse response = null;
            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return fail_str + ex.Message;
            }

            string resultdata = null;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                resultdata = new StreamReader(response.GetResponseStream()).ReadToEnd();

                //if (responseString.Contains("Success")) result = true;
                result = true;
            }

            if (result == true)
            {
                return resultdata;
            }

            return fail_str;
        }
    }
}
